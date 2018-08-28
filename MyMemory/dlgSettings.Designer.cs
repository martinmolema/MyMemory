namespace MyMemory
{
    /// <summary></summary>
    partial class dlgSettings
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
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.cbxTheme = new System.Windows.Forms.ComboBox();
            this.lblTheme = new System.Windows.Forms.Label();
            this.numSizeX = new System.Windows.Forms.NumericUpDown();
            this.numSizeY = new System.Windows.Forms.NumericUpDown();
            this.lblAfmetingen = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNrOfTiles = new System.Windows.Forms.TextBox();
            this.lblError = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numSizeX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSizeY)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Enabled = false;
            this.btnOK.Location = new System.Drawing.Point(12, 92);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(237, 92);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Annuleren";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // cbxTheme
            // 
            this.cbxTheme.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxTheme.FormattingEnabled = true;
            this.cbxTheme.Location = new System.Drawing.Point(78, 12);
            this.cbxTheme.MaxDropDownItems = 2;
            this.cbxTheme.Name = "cbxTheme";
            this.cbxTheme.Size = new System.Drawing.Size(121, 21);
            this.cbxTheme.Sorted = true;
            this.cbxTheme.TabIndex = 2;
            this.cbxTheme.SelectedValueChanged += new System.EventHandler(this.cbxTheme_SelectedValueChanged);
            // 
            // lblTheme
            // 
            this.lblTheme.AutoSize = true;
            this.lblTheme.Location = new System.Drawing.Point(15, 15);
            this.lblTheme.Name = "lblTheme";
            this.lblTheme.Size = new System.Drawing.Size(40, 13);
            this.lblTheme.TabIndex = 3;
            this.lblTheme.Text = "Thema";
            // 
            // numSizeX
            // 
            this.numSizeX.Location = new System.Drawing.Point(78, 39);
            this.numSizeX.Maximum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.numSizeX.Minimum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numSizeX.Name = "numSizeX";
            this.numSizeX.Size = new System.Drawing.Size(47, 20);
            this.numSizeX.TabIndex = 4;
            this.numSizeX.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numSizeX.ValueChanged += new System.EventHandler(this.checkBoardSize);
            // 
            // numSizeY
            // 
            this.numSizeY.Location = new System.Drawing.Point(152, 39);
            this.numSizeY.Maximum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.numSizeY.Minimum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numSizeY.Name = "numSizeY";
            this.numSizeY.Size = new System.Drawing.Size(47, 20);
            this.numSizeY.TabIndex = 5;
            this.numSizeY.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.numSizeY.ValueChanged += new System.EventHandler(this.checkBoardSize);
            // 
            // lblAfmetingen
            // 
            this.lblAfmetingen.AutoSize = true;
            this.lblAfmetingen.Location = new System.Drawing.Point(15, 41);
            this.lblAfmetingen.Name = "lblAfmetingen";
            this.lblAfmetingen.Size = new System.Drawing.Size(60, 13);
            this.lblAfmetingen.TabIndex = 6;
            this.lblAfmetingen.Text = "Afmetingen";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(132, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "X";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(205, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(13, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "=";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(267, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Kaartjes";
            // 
            // txtNrOfTiles
            // 
            this.txtNrOfTiles.Enabled = false;
            this.txtNrOfTiles.Location = new System.Drawing.Point(225, 39);
            this.txtNrOfTiles.Name = "txtNrOfTiles";
            this.txtNrOfTiles.ReadOnly = true;
            this.txtNrOfTiles.Size = new System.Drawing.Size(36, 20);
            this.txtNrOfTiles.TabIndex = 10;
            // 
            // lblError
            // 
            this.lblError.ForeColor = System.Drawing.Color.Red;
            this.lblError.Location = new System.Drawing.Point(15, 69);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(297, 13);
            this.lblError.TabIndex = 11;
            this.lblError.Text = "-";
            // 
            // dlgSettings
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(325, 132);
            this.Controls.Add(this.lblError);
            this.Controls.Add(this.txtNrOfTiles);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblAfmetingen);
            this.Controls.Add(this.numSizeY);
            this.Controls.Add(this.numSizeX);
            this.Controls.Add(this.lblTheme);
            this.Controls.Add(this.cbxTheme);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Name = "dlgSettings";
            this.Text = "-";
            this.Load += new System.EventHandler(this.dlgSettings_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numSizeX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSizeY)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        /// <summary></summary>
        private System.Windows.Forms.Button btnOK;
        /// <summary></summary>
        private System.Windows.Forms.Button btnCancel;
        /// <summary></summary>
        private System.Windows.Forms.Label lblTheme;
        /// <summary></summary>
        private System.Windows.Forms.Label lblAfmetingen;
        /// <summary></summary>
        private System.Windows.Forms.Label label1;
        /// <summary></summary>
        public System.Windows.Forms.ComboBox cbxTheme;
        /// <summary></summary>
        public System.Windows.Forms.NumericUpDown numSizeX;
        /// <summary></summary>
        public System.Windows.Forms.NumericUpDown numSizeY;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtNrOfTiles;
        private System.Windows.Forms.Label lblError;
    }
}