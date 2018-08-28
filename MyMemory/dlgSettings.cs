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
            updateBoardsizeIndicator();

            this.cbxTheme.Items.AddRange(gameSettings.themesFound.ToArray());
        }
        /// <summary>
        /// event to capture input changes from the board size numscrollers
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBoardSize(object sender, EventArgs e)
        {
            updateBoardsizeIndicator();
        }//event checkBoardSize()

        /// <summary>
        /// 
        /// </summary>
        private void updateBoardsizeIndicator()
        {
            int x, y;
            x = Convert.ToInt16(this.numSizeX.Value);
            y = Convert.ToInt16(this.numSizeY.Value);

            this.txtNrOfTiles.Text = (x * y).ToString();

            bool tilesOk = ((x * y) % 2 == 0);

            btnOK.Enabled = tilesOk && (this.cbxTheme.Text!="");
            if (! tilesOk) this.lblError.Text = "Dit geeft een oneven aantal afbeeldingen";
            else this.lblError.Text = "";
        }//updateBoardsizeIndicator()

        private void cbxTheme_SelectedValueChanged(object sender, EventArgs e)
        {
            gameSettings.theme = this.cbxTheme.Text;
            btnOK.Enabled = gameSettings.themeAllowed;
        }
    }
}
