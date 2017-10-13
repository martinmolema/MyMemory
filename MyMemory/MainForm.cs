using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using System.Timers;

namespace MyMemory
{
    public partial class MainForm : Form
    {
        TableLayoutPanel tblTiles;
        int imagesShown = 0;

        public MainForm()
        {
            InitializeComponent();
            clearGrid();
        }

        private void opslaanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = this.saveFileDlg;
            DialogResult res = dlg.ShowDialog();

            if (res == DialogResult.OK)
            {
                Stream s;
                s = dlg.OpenFile();
                using (s)
                {
                    gameSettings.saveToXML(s);
                }
            }

        }

        private void afsluitenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ladenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = this.fileOpenDlg;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                Stream xmlFile = dlg.OpenFile();
                using (xmlFile)
                {
                    if (xmlFile != null)
                    {
                        if (gameSettings.restoreFromFile(xmlFile))
                        {
                            setPlayerInfo();
                            redrawGrid();
                        }
                    }
                }

            }
        }//ladenToolStripMenuItem_Click()

        private void nieuwSpelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dlgPlayerNameEntry frm = new dlgPlayerNameEntry();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                //setup new game for new players using the gamesettings (theme/size)
                gameSettings.setupNewGame(frm.username1, frm.username2);

                redrawGrid();
            }
        }

        private void redrawGrid()
        {
 
            setPlayerInfo();
            clearGrid();

            tblTiles.ColumnCount = gameSettings.tilesX;
            tblTiles.RowCount = gameSettings.tilesY;

            for (int i = 0; i < gameSettings.numberOfTiles;i++)
            {
                PictureBox pb = new PictureBox();
                int x, y;

                x = i % gameSettings.tilesX;
                y = i / gameSettings.tilesX;

                switch(gameSettings.tileStatus[i] )
                {
                    case 0:
                        pb.Image = Image.FromFile(gameSettings.imageBackTile);
                        break;
                    case 1:
                        pb.Image = Image.FromFile(gameSettings.tileImages[i]);
                        break;
                    case 2:
                        pb.Image = Image.FromFile(gameSettings.tileImages[i]);
                        break;
                }
               

                pb.Size = new System.Drawing.Size(64, 64);
                pb.SizeMode = PictureBoxSizeMode.Zoom;
                pb.Tag = i;

                tblTiles.Controls.Add(pb, x, y);
            }

            foreach (RowStyle r in tblTiles.RowStyles)
            {
                r.SizeType = SizeType.Absolute;
                r.Height = 64;
            }

            foreach(ColumnStyle c in tblTiles.ColumnStyles)
            {
                c.SizeType = SizeType.Absolute;
                c.Width = 64;
            }

            autosizeTable();

            foreach(Control ctrl in tblTiles.Controls)
            {
                if (ctrl is PictureBox)
                {
                    ctrl.Click += new System.EventHandler (this.Tile_clicked);

                }
            }

            autosizeTable();

        }//redrawGrid()

        private void Tile_clicked(object sender, EventArgs e) {
            PictureBox pbox = sender as PictureBox;
            String imgPath="";

            int pos = Convert.ToInt16(pbox.Tag);
            if (imagesShown > 1) return;

            switch (gameSettings.tileStatus[pos])
            {
                case 0:
                    imgPath = gameSettings.tileImages[pos];
                    gameSettings.tileStatus[pos] = 1;
                    imagesShown++;
                    System.Timers.Timer timer = new System.Timers.Timer();
                    timer.Interval = 5000;
                    timer.Elapsed += HandleTimer;
                    timer.Enabled = true;
                    timer.AutoReset = false;

                    break;
                case 1:
                    imgPath = gameSettings.imageBackTile;
                    gameSettings.tileStatus[pos] = 0;
                    break;
                default:
                    //ignore: already locked
                    break;
            }
            pbox.Image = Image.FromFile(imgPath);

        }//Tile_clicked()

        private void HandleTimer(Object source, ElapsedEventArgs e)
        {
            PictureBox pb;
            imagesShown = 0;

            int i=0;
            foreach (Control ctrl in tblTiles.Controls)
            {
                if (ctrl is PictureBox) {
                    pb = ctrl as PictureBox;
                    gameSettings.tileStatus[i++] = 0;
                    pb.Image = Image.FromFile(gameSettings.imageBackTile);
                }
            }
        }

        private void highscoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dlgHighscores frm = new dlgHighscores();
            frm.ShowDialog();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dlgSettings frm = new dlgSettings();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                gameSettings.theme = frm.cbxTheme.Text;
                gameSettings.setSize(Convert.ToInt16( frm.numSizeX.Value), Convert.ToInt16(frm.numSizeY.Value));
            }
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            autosizeTable();
        }

        private void setPlayerInfo()
        {
            this.lblPlayer1Name.Text = gameSettings.players[0].name;
            this.lblPlayer2Name.Text = gameSettings.players[1].name;
            this.lblPlayer1Score.Text = gameSettings.players[0].score.ToString();
            this.lblPlayer2Score.Text = gameSettings.players[1].score.ToString();
        }//setPlayerInfo

        private void clearGrid()
        {
            this.Controls.Remove(tblTiles);

            tblTiles = new TableLayoutPanel();

            tblTiles.Top = tblheader.Bottom + 5;
            this.Controls.Add(tblTiles);
            tblTiles.CellBorderStyle = TableLayoutPanelCellBorderStyle.None;
            tblTiles.Padding = new Padding(0);
            tblTiles.Margin = new Padding(0);

            imagesShown = 0;
        }

        private void autosizeTable()
        {
            tblTiles.Width = this.ClientRectangle.Width - 20;
            tblTiles.Height = this.ClientRectangle.Height - tblTiles.Top;
            
            int tileH, tileW;

            tileW = tblTiles.Width / gameSettings.tilesX;
            tileH = tblTiles.Height / gameSettings.tilesY;

            lblPlayer1Name.Text = tblTiles.Width.ToString() + "/ " + this.Width.ToString() + "/ " + tileW;
            lblPlayer2Name.Text = tblTiles.Height.ToString() + "/" + this.Height.ToString() + "/ " + tileH;

            foreach (Control ctrl in tblTiles.Controls)
            {
                if (ctrl is PictureBox)
                {
                    PictureBox pb = ctrl as PictureBox;
                    pb.Width = tileW;
                    pb.Height = tileH;
                    pb.BorderStyle = BorderStyle.None;
                    pb.Padding = new Padding(0);
                    pb.Margin = new Padding(0);
                }
            }
        }
    }
}
