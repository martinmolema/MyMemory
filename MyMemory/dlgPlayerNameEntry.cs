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
    public partial class dlgPlayerNameEntry : Form
    {

        /// <summary></summary>
        public String username1,username2;

        /// <summary></summary>
        public dlgPlayerNameEntry()
        {
            InitializeComponent();
        }

        /// <summary></summary>
        private void enableButtons()
        {
            if (gameSettings.numberOfPlayers == 2)
            {
                this.btnOK.Enabled = (this.txtSpeler1.Text != "" &&
                                      this.txtSpeler1.Text.Length >= 3 &&
                                      this.txtSpeler2.Text != "" &&
                                      this.txtSpeler2.Text.Length >= 3);
            }
            else
            {
                this.btnOK.Enabled = (this.txtSpeler1.Text != "" &&
                                      this.txtSpeler1.Text.Length >= 3);
            }

            this.txtSpeler2.Visible = (gameSettings.numberOfPlayers == 2);
            this.lblPlayername2.Visible = (gameSettings.numberOfPlayers == 2);
            this.imgAvatar2.Visible = (gameSettings.numberOfPlayers == 2);
            this.btnAvatar2.Visible = (gameSettings.numberOfPlayers == 2);

        }//enableButtons()

        /// <summary></summary>
        private void dlgPlayerNameEntry_Load(object sender, EventArgs e)
        {
            this.txtSpeler1.Text = gameSettings.players[0].name;
            this.txtSpeler2.Text = gameSettings.players[1].name;
            this.rdoNormalFlash.Checked = false;
            this.rdoNormalGameMode.Checked = false;
            this.rdoSinglePlayer.Checked = false;
            this.rdoSinglePlayerFlash.Checked = false;
            switch (gameSettings.gameMode)
            {
                case gameModeType.Normal:
                    this.rdoNormalGameMode.Checked = true;
                    break;
                case gameModeType.NormalFlash:
                    this.rdoNormalFlash.Checked = true;
                    break;
                case gameModeType.SinglePlayer:
                    this.rdoSinglePlayer.Checked = true;
                    break;
                case gameModeType.SinglePlayerFlash:
                    this.rdoSinglePlayerFlash.Checked = true;
                    break;

            }
            showAvatars();
        }//dlgPlayerNameEntry_Load()
        /// <summary></summary>

        private void btnOK_Click(object sender, EventArgs e)
        {
            switch (gameSettings.gameMode)
            {
                case gameModeType.Normal:
                case gameModeType.NormalFlash:
                    gameSettings.newPlayer(this.txtSpeler1.Text ,1);
                    gameSettings.newPlayer(this.txtSpeler2.Text, 2);
                    break;
                case gameModeType.SinglePlayer:
                case gameModeType.SinglePlayerFlash:
                    gameSettings.newPlayer(this.txtSpeler1.Text, 1);
                    break;
            }
        }//btnOK_Click

        private void rdoNormalGameMode_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rdoNormalGameMode.Checked) gameSettings.gameMode = gameModeType.Normal;
            enableButtons();
        }

        private void rdoNormalFlash_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rdoNormalFlash.Checked) gameSettings.gameMode = gameModeType.NormalFlash;
            enableButtons();
        }

        private void rdoSinglePlayer_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rdoSinglePlayer.Checked) gameSettings.gameMode= gameModeType.SinglePlayer;
            enableButtons();
        }

        private void rdoSinglePlayerFlash_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoSinglePlayerFlash.Checked) gameSettings.gameMode = gameModeType.SinglePlayerFlash;
            enableButtons();
        }

        private void btnAvatar1_Click(object sender, EventArgs e)
        {
            if ( dlgFileOpen.ShowDialog() == DialogResult.OK)
            {
                gameSettings.assignPlayerAvatar(dlgFileOpen.FileName,1);
                showAvatars();
            }
            
        }

        private void btnAvatar2_Click(object sender, EventArgs e)
        {
            if (dlgFileOpen.ShowDialog() == DialogResult.OK)
            {
                gameSettings.assignPlayerAvatar(dlgFileOpen.FileName, 2);
                showAvatars();
            }

        }
        /// <summary>
        /// Shows the avatars if applicable
        /// </summary>
        private void showAvatars()
        {
            try
            {
                imgAvatar1.Image = Image.FromFile(gameSettings.players[0].avatarFilename);
                imgAvatar2.Image = Image.FromFile(gameSettings.players[1].avatarFilename);
            }
            catch(Exception e)
            {
                // do nothing
                Console.Write(e.Message);
            }

        }//showAvatars()

        private void dlgFileOpen_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void txt_TextChanged(object sender, EventArgs e)
        {
            enableButtons();
        }
    }
}
