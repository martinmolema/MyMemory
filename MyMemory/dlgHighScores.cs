using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyMemory
{
    /// <summary></summary>
    public partial class dlgHighscores : Form
    {
        /// <summary></summary>
        public dlgHighscores()
        {
            InitializeComponent();
        }

        /// <summary></summary>
        private void dlgHighscores_Load(object sender, EventArgs e)
        {
            foreach (KeyValuePair<string, int> item in gameSettings.highscoreNames)
            {
                this.txtScores.AppendText(item.Key + ":" + item.Value);
                this.txtScores.AppendText(Environment.NewLine);
            }
        }
    }
}
