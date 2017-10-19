namespace MyMemory
{
    partial class dlgPlayerNameEntry
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.grpNames = new System.Windows.Forms.GroupBox();
            this.lblPlayername2 = new System.Windows.Forms.Label();
            this.lblPlayername1 = new System.Windows.Forms.Label();
            this.txtSpeler2 = new System.Windows.Forms.TextBox();
            this.txtSpeler1 = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdoNormalFlash = new System.Windows.Forms.RadioButton();
            this.rdoSinglePlayerFlash = new System.Windows.Forms.RadioButton();
            this.rdoSinglePlayer = new System.Windows.Forms.RadioButton();
            this.rdoNormalGameMode = new System.Windows.Forms.RadioButton();
            this.grpNames.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpNames
            // 
            this.grpNames.Controls.Add(this.lblPlayername2);
            this.grpNames.Controls.Add(this.lblPlayername1);
            this.grpNames.Controls.Add(this.txtSpeler2);
            this.grpNames.Controls.Add(this.txtSpeler1);
            this.grpNames.Location = new System.Drawing.Point(12, 79);
            this.grpNames.Name = "grpNames";
            this.grpNames.Size = new System.Drawing.Size(463, 80);
            this.grpNames.TabIndex = 0;
            this.grpNames.TabStop = false;
            this.grpNames.Text = "Namen van de spelers";
            // 
            // lblPlayername2
            // 
            this.lblPlayername2.AutoSize = true;
            this.lblPlayername2.Location = new System.Drawing.Point(7, 50);
            this.lblPlayername2.Name = "lblPlayername2";
            this.lblPlayername2.Size = new System.Drawing.Size(46, 13);
            this.lblPlayername2.TabIndex = 3;
            this.lblPlayername2.Text = "Speler 2";
            // 
            // lblPlayername1
            // 
            this.lblPlayername1.AutoSize = true;
            this.lblPlayername1.Location = new System.Drawing.Point(7, 26);
            this.lblPlayername1.Name = "lblPlayername1";
            this.lblPlayername1.Size = new System.Drawing.Size(46, 13);
            this.lblPlayername1.TabIndex = 2;
            this.lblPlayername1.Text = "Speler 1";
            // 
            // txtSpeler2
            // 
            this.txtSpeler2.Location = new System.Drawing.Point(111, 47);
            this.txtSpeler2.Name = "txtSpeler2";
            this.txtSpeler2.Size = new System.Drawing.Size(187, 20);
            this.txtSpeler2.TabIndex = 1;
            this.txtSpeler2.TextChanged += new System.EventHandler(this.txt_TextChanged);
            // 
            // txtSpeler1
            // 
            this.txtSpeler1.Location = new System.Drawing.Point(111, 20);
            this.txtSpeler1.Name = "txtSpeler1";
            this.txtSpeler1.Size = new System.Drawing.Size(187, 20);
            this.txtSpeler1.TabIndex = 0;
            this.txtSpeler1.TextChanged += new System.EventHandler(this.txt_TextChanged);
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Enabled = false;
            this.btnOK.Location = new System.Drawing.Point(22, 165);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.CausesValidation = false;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(400, 165);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Annuleren";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdoNormalFlash);
            this.groupBox1.Controls.Add(this.rdoSinglePlayerFlash);
            this.groupBox1.Controls.Add(this.rdoSinglePlayer);
            this.groupBox1.Controls.Add(this.rdoNormalGameMode);
            this.groupBox1.Location = new System.Drawing.Point(12, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(463, 49);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Game mode";
            // 
            // rdoNormalFlash
            // 
            this.rdoNormalFlash.AutoSize = true;
            this.rdoNormalFlash.Location = new System.Drawing.Point(99, 20);
            this.rdoNormalFlash.Name = "rdoNormalFlash";
            this.rdoNormalFlash.Size = new System.Drawing.Size(111, 17);
            this.rdoNormalFlash.TabIndex = 1;
            this.rdoNormalFlash.Text = "Two player - Flash";
            this.rdoNormalFlash.UseVisualStyleBackColor = true;
            this.rdoNormalFlash.CheckedChanged += new System.EventHandler(this.rdoNormalFlash_CheckedChanged);
            // 
            // rdoSinglePlayerFlash
            // 
            this.rdoSinglePlayerFlash.AutoSize = true;
            this.rdoSinglePlayerFlash.Location = new System.Drawing.Point(326, 20);
            this.rdoSinglePlayerFlash.Name = "rdoSinglePlayerFlash";
            this.rdoSinglePlayerFlash.Size = new System.Drawing.Size(114, 17);
            this.rdoSinglePlayerFlash.TabIndex = 3;
            this.rdoSinglePlayerFlash.Text = "Single Player Flash";
            this.rdoSinglePlayerFlash.UseVisualStyleBackColor = true;
            this.rdoSinglePlayerFlash.CheckedChanged += new System.EventHandler(this.rdoSinglePlayerFlash_CheckedChanged);
            // 
            // rdoSinglePlayer
            // 
            this.rdoSinglePlayer.AutoSize = true;
            this.rdoSinglePlayer.Location = new System.Drawing.Point(223, 20);
            this.rdoSinglePlayer.Name = "rdoSinglePlayer";
            this.rdoSinglePlayer.Size = new System.Drawing.Size(86, 17);
            this.rdoSinglePlayer.TabIndex = 2;
            this.rdoSinglePlayer.Text = "Single Player";
            this.rdoSinglePlayer.UseVisualStyleBackColor = true;
            this.rdoSinglePlayer.CheckedChanged += new System.EventHandler(this.rdoSinglePlayer_CheckedChanged);
            // 
            // rdoNormalGameMode
            // 
            this.rdoNormalGameMode.AutoSize = true;
            this.rdoNormalGameMode.Checked = true;
            this.rdoNormalGameMode.Location = new System.Drawing.Point(10, 20);
            this.rdoNormalGameMode.Name = "rdoNormalGameMode";
            this.rdoNormalGameMode.Size = new System.Drawing.Size(83, 17);
            this.rdoNormalGameMode.TabIndex = 0;
            this.rdoNormalGameMode.TabStop = true;
            this.rdoNormalGameMode.Text = "Two Players";
            this.rdoNormalGameMode.UseVisualStyleBackColor = true;
            this.rdoNormalGameMode.CheckedChanged += new System.EventHandler(this.rdoNormalGameMode_CheckedChanged);
            // 
            // dlgPlayerNameEntry
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(487, 200);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.grpNames);
            this.Name = "dlgPlayerNameEntry";
            this.Text = "Spelers...";
            this.Load += new System.EventHandler(this.dlgPlayerNameEntry_Load);
            this.grpNames.ResumeLayout(false);
            this.grpNames.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpNames;
        private System.Windows.Forms.Label lblPlayername1;
        private System.Windows.Forms.TextBox txtSpeler2;
        private System.Windows.Forms.TextBox txtSpeler1;
        private System.Windows.Forms.Label lblPlayername2;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdoNormalFlash;
        private System.Windows.Forms.RadioButton rdoSinglePlayerFlash;
        private System.Windows.Forms.RadioButton rdoSinglePlayer;
        private System.Windows.Forms.RadioButton rdoNormalGameMode;
    }
}