namespace ChampSelectNames
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tmr1 = new System.Windows.Forms.Timer(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnOpGGAll = new System.Windows.Forms.Button();
            this.chkAutoOPGG = new System.Windows.Forms.CheckBox();
            this.cmbRegion = new System.Windows.Forms.ComboBox();
            this.txtParticipants = new System.Windows.Forms.TextBox();
            this.btnUGGALL = new System.Windows.Forms.Button();
            this.chkAutoUGG = new System.Windows.Forms.CheckBox();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tmr1
            // 
            this.tmr1.Enabled = true;
            this.tmr1.Interval = 500;
            this.tmr1.Tick += new System.EventHandler(this.tmr1_Tick);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 158);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(326, 22);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // btnOpGGAll
            // 
            this.btnOpGGAll.Location = new System.Drawing.Point(12, 98);
            this.btnOpGGAll.Name = "btnOpGGAll";
            this.btnOpGGAll.Size = new System.Drawing.Size(75, 23);
            this.btnOpGGAll.TabIndex = 10;
            this.btnOpGGAll.Text = "OP.GG All";
            this.btnOpGGAll.UseVisualStyleBackColor = true;
            this.btnOpGGAll.Click += new System.EventHandler(this.btnOpGGAll_Click);
            // 
            // chkAutoOPGG
            // 
            this.chkAutoOPGG.AutoSize = true;
            this.chkAutoOPGG.Location = new System.Drawing.Point(93, 102);
            this.chkAutoOPGG.Name = "chkAutoOPGG";
            this.chkAutoOPGG.Size = new System.Drawing.Size(112, 17);
            this.chkAutoOPGG.TabIndex = 11;
            this.chkAutoOPGG.Text = "Auto open OP.GG";
            this.chkAutoOPGG.UseVisualStyleBackColor = true;
            // 
            // cmbRegion
            // 
            this.cmbRegion.FormattingEnabled = true;
            this.cmbRegion.Items.AddRange(new object[] {
            "EUW",
            "NA",
            "EUNE",
            "OCE",
            "KR",
            "JP",
            "BR",
            "LAS",
            "LAN",
            "RU"});
            this.cmbRegion.Location = new System.Drawing.Point(247, 100);
            this.cmbRegion.Name = "cmbRegion";
            this.cmbRegion.Size = new System.Drawing.Size(67, 21);
            this.cmbRegion.TabIndex = 12;
            // 
            // txtParticipants
            // 
            this.txtParticipants.Location = new System.Drawing.Point(12, 3);
            this.txtParticipants.Multiline = true;
            this.txtParticipants.Name = "txtParticipants";
            this.txtParticipants.ReadOnly = true;
            this.txtParticipants.Size = new System.Drawing.Size(302, 89);
            this.txtParticipants.TabIndex = 13;
            // 
            // btnUGGALL
            // 
            this.btnUGGALL.Location = new System.Drawing.Point(12, 127);
            this.btnUGGALL.Name = "btnUGGALL";
            this.btnUGGALL.Size = new System.Drawing.Size(75, 23);
            this.btnUGGALL.TabIndex = 14;
            this.btnUGGALL.Text = "U.GG All";
            this.btnUGGALL.UseVisualStyleBackColor = true;
            this.btnUGGALL.Click += new System.EventHandler(this.btnUGGALL_Click);
            // 
            // chkAutoUGG
            // 
            this.chkAutoUGG.AutoSize = true;
            this.chkAutoUGG.Location = new System.Drawing.Point(93, 131);
            this.chkAutoUGG.Name = "chkAutoUGG";
            this.chkAutoUGG.Size = new System.Drawing.Size(105, 17);
            this.chkAutoUGG.TabIndex = 15;
            this.chkAutoUGG.Text = "Auto open U.GG";
            this.chkAutoUGG.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(326, 180);
            this.Controls.Add(this.chkAutoUGG);
            this.Controls.Add(this.btnUGGALL);
            this.Controls.Add(this.txtParticipants);
            this.Controls.Add(this.cmbRegion);
            this.Controls.Add(this.chkAutoOPGG);
            this.Controls.Add(this.btnOpGGAll);
            this.Controls.Add(this.statusStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form1";
            this.Text = "ChampSelectSpy - 1.1";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer tmr1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Button btnOpGGAll;
        private System.Windows.Forms.CheckBox chkAutoOPGG;
        private System.Windows.Forms.ComboBox cmbRegion;
        private System.Windows.Forms.TextBox txtParticipants;
        private System.Windows.Forms.Button btnUGGALL;
        private System.Windows.Forms.CheckBox chkAutoUGG;
    }
}

