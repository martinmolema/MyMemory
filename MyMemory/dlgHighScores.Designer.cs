﻿namespace MyMemory
{
    partial class dlgHighscores
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
            this.txtScores = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtScores
            // 
            this.txtScores.Location = new System.Drawing.Point(12, 12);
            this.txtScores.Multiline = true;
            this.txtScores.Name = "txtScores";
            this.txtScores.Size = new System.Drawing.Size(452, 238);
            this.txtScores.TabIndex = 0;
            // 
            // dlgHighscores
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(476, 262);
            this.Controls.Add(this.txtScores);
            this.Name = "dlgHighscores";
            this.Text = "Highscores";
            this.Load += new System.EventHandler(this.dlgHighscores_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtScores;
    }
}