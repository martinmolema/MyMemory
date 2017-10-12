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
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSpeler2 = new System.Windows.Forms.TextBox();
            this.txtSpeler1 = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.grpNames.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpNames
            // 
            this.grpNames.Controls.Add(this.label2);
            this.grpNames.Controls.Add(this.label1);
            this.grpNames.Controls.Add(this.txtSpeler2);
            this.grpNames.Controls.Add(this.txtSpeler1);
            this.grpNames.Location = new System.Drawing.Point(12, 12);
            this.grpNames.Name = "grpNames";
            this.grpNames.Size = new System.Drawing.Size(321, 80);
            this.grpNames.TabIndex = 0;
            this.grpNames.TabStop = false;
            this.grpNames.Text = "Namen van de spelers";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Speler 2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Speler 1";
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
            this.btnOK.Location = new System.Drawing.Point(22, 98);
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
            this.btnCancel.Location = new System.Drawing.Point(235, 98);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Annuleren";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // dlgPlayerNameEntry
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(340, 135);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.grpNames);
            this.Name = "dlgPlayerNameEntry";
            this.Text = "Spelers...";
            this.Load += new System.EventHandler(this.dlgPlayerNameEntry_Load);
            this.grpNames.ResumeLayout(false);
            this.grpNames.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpNames;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSpeler2;
        private System.Windows.Forms.TextBox txtSpeler1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
    }
}