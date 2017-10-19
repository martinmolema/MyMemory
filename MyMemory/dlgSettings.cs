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
    public partial class dlgSettings : Form
    {
        /// <summary></summary>
        public dlgSettings()
        {
            InitializeComponent();
        }

        /// <summary></summary>
        private void dlgSettings_Load(object sender, EventArgs e)
        {
            this.cbxTheme.Text = gameSettings.theme;
            this.numSizeX.Value = gameSettings.tilesX;
            this.numSizeY.Value = gameSettings.tilesY;
        }
    }
}
