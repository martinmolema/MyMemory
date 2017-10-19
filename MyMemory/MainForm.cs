using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace MyMemory
{
    /// <summary>
    /// the main form of the application
    /// </summary>
    public partial class MainForm : Form
    {
        TableLayoutPanel tblTiles;

        /// <summary>Initialises the mainform</summary>
        public MainForm()
        {
            InitializeComponent();
            clearGrid();
        }//MainForm()

        /// <summary>
        /// Menu item: Bestand->Opslaan
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        }//opslaanToolStripMenuItem_Click()

        /// <summary>
        /// Menu item Bestand->Afsluiten
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void afsluitenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>Menu Item Bestand->Laden</summary>
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
                        // Dialog yielded filename. Open the stream
                        if (gameSettings.restoreFromFile(xmlFile))
                        {
                            setPlayerInfo();
                            redrawGrid();
                        }
                    }
                }//using XML file to read
            }// if user selected file to open
        }//ladenToolStripMenuItem_Click()

        /**
         * Menu Item Game-> Nieuw
         */
        private void nieuwSpelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dlgPlayerNameEntry frm = new dlgPlayerNameEntry();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                //setup new game administration for new players using the gamesettings (theme/size)

                gameSettings.setupNewGame();

                if (gameSettings.numberOfPlayers == 1)
                {
                    System.Windows.Forms.Timer scoreTimer = new System.Windows.Forms.Timer();
                    scoreTimer.Interval = 1000;
                    scoreTimer.Tick += HandleScoreTimer;
                    scoreTimer.Enabled = true;
                }

                redrawGrid();

                if (gameSettings.gameStatus == gameStatusType.Flashing)
                {
                    System.Windows.Forms.Timer scoreTimer = new System.Windows.Forms.Timer();
                    scoreTimer.Interval = 8000;
                    scoreTimer.Tick += HandleFlashingTimer;
                    scoreTimer.Enabled = true;
                }
                else
                {
                    gameSettings.gameStatus = gameStatusType.Started;
                }

            }
        }//nieuwSpelToolStripMenuItem_Click()

        private void HandleFlashingTimer(Object source, EventArgs e)
        {
            gameSettings.bulkChangeTileStatus(tileStatusType.Closed);
            gameSettings.gameStatus = gameStatusType.Started;
            redrawGrid();
            gameSettings.resetSinglePlayerTimer();

            System.Windows.Forms.Timer timer = source as System.Windows.Forms.Timer;

            timer.Enabled = false;
        }//HandleFlashingTimer()

        private void HandleScoreTimer(Object source, EventArgs e)
        {
            String newTime;
            newTime = Convert.ToInt32(gameSettings.singlePlayerTimer).ToString();
            this.lblPlayer2Score.Text  = newTime;
            this.lblPlayer2Score.Invalidate();

            System.Windows.Forms.Timer timer = source as System.Windows.Forms.Timer;

            timer.Start();
        }//HandleScoreTimer()

        /// <summary>
        /// Redraw the grid using the gamesettings as source of information
        /// </summary>
        private void redrawGrid()
        {
 
            setPlayerInfo();
            clearGrid();

            tblTiles.ColumnCount = gameSettings.tilesX;
            tblTiles.RowCount = gameSettings.tilesY;

            // add all the images to tiles
            for (int i = 0; i < gameSettings.numberOfTiles;i++)
            {
                PictureBox pb = new PictureBox();
                int x, y;

                // determine X+Y from loop variable
                x = i % gameSettings.tilesX;
                y = i / gameSettings.tilesX;

                // check tile status and determine image file to load
                switch(gameSettings.tileStatus[i] )
                {
                    case tileStatusType.Closed:
                        pb.Image = Image.FromFile(gameSettings.imageBackTile);
                        break;
                    case tileStatusType.ShowTemporary:
                        pb.Image = Image.FromFile(gameSettings.tileImages[i]);
                        break;
                    case tileStatusType.Found:
                        pb.Image = Image.FromFile(gameSettings.tileImages[i]);
                        break;
                    case tileStatusType.Flash:
                        pb.Image = Image.FromFile(gameSettings.tileImages[i]);
                        break;
                }

                // determine size; this is arbitrary because later the images are resized based on the Window size
                pb.Size = new System.Drawing.Size(64, 64);
                pb.SizeMode = PictureBoxSizeMode.Zoom;
                pb.Tag = i;

                // add the image
                tblTiles.Controls.Add(pb, x, y);
            }
            
            // set row and column styles of the container (tableLayout)
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

            // autosize the table
            autosizeTable();

            // attach a click handler to each picturebox
            foreach(Control ctrl in tblTiles.Controls)
            {
                if (ctrl is PictureBox)
                {
                    ctrl.Click += new System.EventHandler (this.Tile_clicked);
                }
            }

        }//redrawGrid()

        ///<summary>click handler for each tile</summary>
        private void Tile_clicked(object sender, EventArgs e) {
            PictureBox pbox = sender as PictureBox;
            String imgPath="";
            bool needRedraw = false;

            int pos = Convert.ToInt16(pbox.Tag);
            if (gameSettings.openedImageCount > 1) return;

            switch (gameSettings.tileStatus[pos])
            {
                case tileStatusType.Closed:
                    bool theSame;

                    imgPath = gameSettings.tileImages[pos];
                    gameSettings.tileStatus[pos] = tileStatusType.ShowTemporary;
                    needRedraw = true;

                    gameSettings.AddOpenedImage(pos);

                    theSame = (gameSettings.OpenedImagesAreTheSame() == openedImagesCompareResult.theSame);

                    if (gameSettings.openedImageCount == 2 && ! theSame)
                    {
                        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
                        timer.Interval = 2000;
                        timer.Tick += HandleTimer;
                        timer.Enabled = true;
                        timer.Start();
                    }

                    if (theSame)
                    {
                        changePlayerScore(1); // add one point to score
                        gameSettings.setStatusOfOpenedImages(tileStatusType.Found);
                        gameSettings.cleanupOpenedImages();

                        gameSettings.checkGameOver();

                        if (gameSettings.gameStatus == gameStatusType.Finished)
                        {
                            OpenAllTiles();

                            MessageBox.Show("Game Over");
                        }
                    }
                    break;
                case tileStatusType.ShowTemporary:
                    imgPath = gameSettings.imageBackTile;
                    gameSettings.tileStatus[pos] = tileStatusType.Closed;
                    break;
                case tileStatusType.Flash:
                    // ignore; no redraw 
                    break;
                default:
                    //ignore: already locked
                    break;
            }
            if (needRedraw) pbox.Image = Image.FromFile(imgPath);

        }//Tile_clicked()

        /// <summary>switch to another player</summary>
        private void switchPlayer()
        {
            gameSettings.switchCurrentPlayer();
            gameSettings.cleanupOpenedImages();

            switch (gameSettings.currentPlayerNumber)
            {
                case 1:
                    this.lblPlayer1Name.ForeColor = Color.Red;
                    this.lblPlayer2Name.ForeColor = Color.Black;
                    break;
                case 2:
                    this.lblPlayer1Name.ForeColor = Color.Black ;
                    this.lblPlayer2Name.ForeColor = Color.Red;
                    break;
            }
        }//switchPlayer()

        /// <summary>
        ///   Handle the timer to flip the wrong tiles back to it's closed status
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void HandleTimer(Object source, EventArgs e)
        {
            PictureBox pb;
            System.Windows.Forms.Timer timer = source as System.Windows.Forms.Timer;
            timer.Enabled = false;
            timer.Dispose();

            int i=0;

            foreach (Control ctrl in tblTiles.Controls)
            {
                if (ctrl is PictureBox) {
                    pb = ctrl as PictureBox;

                    if (gameSettings.tileStatus[i] == tileStatusType.ShowTemporary)
                    {
                        pb.Image = Image.FromFile(gameSettings.imageBackTile);
                    }                   

                    i++;
                }
            }

            gameSettings.setStatusOfOpenedImages(tileStatusType.Closed);

            switchPlayer();
            gameSettings.cleanupOpenedImages();
        }//HandleTimer()

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

        /// <summary>
        ///   Sets the player info on the screen based on the gamesettings.gameMode
        /// </summary>
        private void setPlayerInfo()
        {
            switch (gameSettings.gameMode)
            {
                case gameModeType.Normal:
                case gameModeType.NormalFlash:
                    this.lblPlayer1Name.Text = gameSettings.players[0].name;
                    this.lblPlayer2Name.Text = gameSettings.players[1].name;
                    this.lblPlayer1Score.Text = gameSettings.players[0].score.ToString();
                    this.lblPlayer2Score.Text = gameSettings.players[1].score.ToString();
                    break;
                case gameModeType.SinglePlayer:
                case gameModeType.SinglePlayerFlash:
                    this.lblPlayer1Name.Text = gameSettings.players[0].name;
                    this.lblPlayer1Score.Text = gameSettings.players[0].score.ToString();
                    this.lblPlayer2Name.Text = "Tijd";
                    this.lblPlayer2Score.Text = "-";
                    break;
            }
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

        }// clearGrid()

        /*
         * Autosizes the table so that the images are scaled
         */
        private void autosizeTable()
        {
            tblTiles.Width = this.ClientRectangle.Width - 20;
            tblTiles.Height = this.ClientRectangle.Height - tblTiles.Top;
            
            int tileH, tileW;

            tileW = tblTiles.Width / gameSettings.tilesX;
            tileH = tblTiles.Height / gameSettings.tilesY;

            //lblPlayer1Name.Text = tblTiles.Width.ToString() + "/ " + this.Width.ToString() + "/ " + tileW;
            //lblPlayer2Name.Text = tblTiles.Height.ToString() + "/" + this.Height.ToString() + "/ " + tileH;

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
        }//autosizeTable

        /// <summary>
        ///   Changes the players score (current player)
        /// </summary>
        /// <param name="addScore">the number of points to be added to the current player's score</param>
        private void changePlayerScore(int addScore)
        {
            int playerNr = gameSettings.currentPlayerNumber;
            int newscore = gameSettings.AssignScoreCurrentPlayer(1);

            switch (playerNr)
            {
                case 1:
                    this.lblPlayer1Score.Text = newscore.ToString();
                    break;
                case 2:
                    this.lblPlayer2Score.Text = newscore.ToString();
                    break;
            }


        }//changePlayerScore()

        /// <summary>Shows all images</summary>
        private void OpenAllTiles()
        {
            foreach (Control ctrl in tblTiles.Controls)
            {
                if (ctrl is PictureBox)
                {
                    PictureBox pb = ctrl as PictureBox;
                    int pos = Convert.ToInt16 (pb.Tag);
                    pb.Image = Image.FromFile(gameSettings.tileImages[pos]);
                }
            }
        }

    }// end class
}// Namespace
