using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Management;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace ChampSelectSpy
{
    public partial class Form1 : Form
    {
        private struct ClientData
        {
            public bool isRunning;
            public int ProcessId;
            public string cmdline;
            public int RiotPort;
            public string RiotToken;
            public int ClientPort;
            public string ClientToken;
            public string Region;
            public string SummonerDisplayName;
            public String GameState;
        }

        readonly List<string> participants = new();
        class Summoner
        {
            public String DisplayName;
        }

        private ClientData ClientInfo;
        ManagementClass mngmtClass;

        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            mngmtClass = new ManagementClass("Win32_Process");
            toolStripStatusLabel1.Text = "Waiting for League...";
            chkAutoOPGG.Checked = Properties.Settings.Default.AutoOPGG;
            chkAutoUGG.Checked = Properties.Settings.Default.AutoUGG;
            chkAutoPORO.Checked = Properties.Settings.Default.AutoPORO;
            cmbRegion.Text = Properties.Settings.Default.Region;
            chkTopMost.Checked = Properties.Settings.Default.TopMost;
            this.TopMost = Properties.Settings.Default.TopMost;
            chkAutoMinimize.Checked = Properties.Settings.Default.AutoMinimize;

        }


        public static string GetBetween(string strSource, string strStart, string strEnd)
        {
            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                int Start, End;
                Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                End = strSource.IndexOf(strEnd, Start);
                return strSource.Substring(Start, End - Start);
            }

            return "";
        }

        private void Tmr1_Tick(object sender, EventArgs e)
        {
            Process[] processes = Process.GetProcessesByName("LeagueClientUx");
            if (processes.Length > 0)
            {
                ClientInfo.isRunning = true;
                ClientInfo.ProcessId = processes[0].Id;
                toolStripStatusLabel1.Text = "Summoner: " + ClientInfo.SummonerDisplayName + " - Gamestate: " + ClientInfo.GameState;
                if (ClientInfo.cmdline == null)
                {
                    foreach (ManagementObject obj in mngmtClass.GetInstances())
                    {
                        if (obj["Name"].Equals("LeagueClientUx.exe"))
                        {
                            ClientInfo.cmdline = obj["Name"] + " [" + obj["CommandLine"] + "]";
                            ClientInfo.RiotPort = int.Parse(GetBetween(ClientInfo.cmdline, "--riotclient-app-port=", "\" \"--no-rads"));
                            ClientInfo.RiotToken = Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes("riot:" + GetBetween(ClientInfo.cmdline, "--riotclient-auth-token=", "\" \"--riotclient")));
                            ClientInfo.ClientPort = int.Parse(GetBetween(ClientInfo.cmdline, "--app-port=", "\" \"--install"));
                            ClientInfo.ClientToken = Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes("riot:" + GetBetween(ClientInfo.cmdline, "--remoting-auth-token=", "\" \"--respawn-command=LeagueClient.exe")));
                        }
                    }
                }
            }
            else
            {
                ClientInfo = default;
                toolStripStatusLabel1.Text = "Waiting for League...";
            }

            if (ClientInfo.isRunning)
            {
                if (ClientInfo.Region == null || ClientInfo.Region == "")
                {
                    ClientInfo.Region = GetBetween(MakeRequest(ClientInfo, "GET", "/lol-rso-auth/v1/authorization", client: true), "currentPlatformId\":\"", "\",\"subject").ToLower();
                }

                if (ClientInfo.SummonerDisplayName == null)
                {
                    Summoner summ = JsonConvert.DeserializeObject<Summoner>(MakeRequest(ClientInfo, "GET", "/lol-summoner/v1/current-summoner", client: true));
                    if (summ != null)
                    {
                        ClientInfo.SummonerDisplayName = summ.DisplayName;

                    }
                }

                ClientInfo.GameState = MakeRequest(ClientInfo, "GET", "/lol-gameflow/v1/gameflow-phase", true).Trim('"');
                if (ClientInfo.GameState == "ChampSelect")
                {
                    if (participants.Count < 5)
                    {
                        DataSet dataSet = JsonConvert.DeserializeObject<DataSet>(MakeRequest(ClientInfo, "GET", "/chat/v5/participants/champ-select", client: false));
                        DataTable dataTable = dataSet.Tables["participants"];
                        if (dataTable.Rows.Count == 5)
                        {
                            foreach (DataRow row in dataTable.Rows)
                            {
                                participants.Add(row["name"].ToString());
                            }
                            if (chkAutoOPGG.Checked)
                            {
                                BtnOpGGAll_Click(null, null);
                            }
                            if (chkAutoUGG.Checked)
                            {
                                BtnUGGALL_Click(null, null);
                            }
                            if (chkAutoPORO.Checked)
                            {
                                BtnPOROALL_Click(null, null);
                            }
                        }
                    }


                }
                else if (ClientInfo.GameState == "InProgress")
                {
                    if (Properties.Settings.Default.AutoMinimize)
                        this.WindowState = FormWindowState.Minimized;
                }
                else
                {
                    participants.Clear();
                }
                if (participants.Count > 0)
                {
                    txtParticipants.Text = string.Join("\r\n", participants);
                }
                else
                {
                    txtParticipants.Text = string.Empty;
                }
            }

        }
        private static string MakeRequest(ClientData info, string method, string url, bool client)
        {
            int port = client ? info.ClientPort : info.RiotPort;
            string authToken = client ? info.ClientToken : info.RiotToken;
            try
            {
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
                HttpWebRequest obj = (HttpWebRequest)WebRequest.Create("https://127.0.0.1:" + port + url);
                obj.PreAuthenticate = true;
                obj.ContentType = "application/json";
                obj.Method = method;
                obj.Headers.Add("Authorization", "Basic " + authToken);
                using StreamReader streamReader = new StreamReader(((HttpWebResponse)obj.GetResponse()).GetResponseStream());
                return streamReader.ReadToEnd();
            }
            catch
            {
                return "";
            }
        }

        private void BtnOpGGAll_Click(object sender, EventArgs e)
        {
            if (participants.Count > 0)
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = "https://www.op.gg/multisearch/" + cmbRegion.Text + "?summoners=" + string.Join(",", participants),
                    UseShellExecute = true
                });
            }
        }

        private void BtnUGGALL_Click(object sender, EventArgs e)
        {
            if (participants.Count > 0)
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = "https://u.gg/multisearch?summoners=" + string.Join(",", participants) + "&region=" + ClientInfo.Region,
                    UseShellExecute = true
                });
            }
        }

        private void BtnPOROALL_Click(object sneder, EventArgs e)
        {
            if (participants.Count > 0)
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = "https://porofessor.gg/pregame/" + cmbRegion.Text.ToLower() + "/" + string.Join(",", participants) + "/soloqueue",
                    UseShellExecute = true
                });
            }
        }

        private void ChkAutoOPGG_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.AutoOPGG = chkAutoOPGG.Checked;
            Properties.Settings.Default.Save();
        }

        private void ChkAutoUGG_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.AutoUGG = chkAutoUGG.Checked;
            Properties.Settings.Default.Save();
        }

        private void ChkAutoPORO_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.AutoPORO = chkAutoPORO.Checked;
            Properties.Settings.Default.Save();
        }

        private void CmbRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.Region = cmbRegion.Text;
            Properties.Settings.Default.Save();
        }

        private void chkTopMost_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.TopMost = chkTopMost.Checked;
            this.TopMost = chkTopMost.Checked;
            Properties.Settings.Default.Save();

        }

        private void chkAutoMinimize_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.AutoMinimize = chkAutoMinimize.Checked;
            Properties.Settings.Default.Save();

        }
    }
}
