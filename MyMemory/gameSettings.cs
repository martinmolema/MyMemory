using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.IO;
using System.Timers;

namespace MyMemory
{
    /// <summary>
    ///   The gameModeType indicates the type of mode: single player/multiplayer and whether flash-mode is enabled
    /// </summary>
    public enum gameModeType {
        /// <summary>
        /// This is the normal game mode: 2 players start with all backs shown
        /// </summary>
        Normal,
        /// <summary>
        /// the single player 'against the clock' mode
        /// </summary>
        SinglePlayer,
        /// <summary>
        /// the normal game mode with 2 players, but now first all the tiles are shown, and points are substracted 
        /// when player cannot find two the same tiles
        /// </summary>
        NormalFlash,
        /// <summary>
        /// the single player mode , but now first all the tiles are shown, and points are substracted 
        /// when player cannot find two the same tiles
        /// </summary>
        SinglePlayerFlash
    };

    /// <summary>an enumerated type for the status of each tile.</summary>
    public enum tileStatusType {
        /// <summary>The status of a tile is not determined</summary>
        Unknown,
        /// <summary>the tile is shown with it's back shown</summary>
        Closed,
        /// <summary>the image is shown temporarily until either a match is found or a two different tiles are clicked</summary>
        ShowTemporary,
        /// <summary>the image is shown indefinitely because it is part of a match </summary>
        Found,
        /// <summary>the image is shown temporarily so the users can learn its positions</summary>
        Flash
    };
    
    /// <summary>an enumerated type for the global game status</summary>
    public enum gameStatusType {
        /// <summary>The game is initial state; users must start new game or load from file</summary>
        Initial,
        /// <summary>Game is paused; for instance to save to file</summary>
        Paused,
        /// <summary>Game is temporarily showing all times; a timer is started to close the tiles</summary>
        Flashing,
        /// <summary>Game has started and player(s) can click tiles.</summary>
        Started,
        /// <summary>Game has finished. All tiles have been found</summary>
        Finished
    };
    
    /// <summary>an enumerated type used when comparing the images of the opened tiles</summary>
    public enum openedImagesCompareResult {
        /// <summary>the two opened tiles have the same image assigned</summary>
        theSame,
        /// <summary>the two opened tiles have different images assigned</summary>
        Different,
        /// <summary>there are currently no two images shown</summary>
        notEnoughImages
    };

    /// <summary>a class to register a player</summary>
    public class playerInfo
    {
        /// <summary>the name of the player</summary>
        public String name;
        /// <summary>the score for the player</summary>
        public int score;
        /// <summery>avatar filename</summery>
        public String avatarFilename;
    }


    /// <summary>A global class with only static information serving as the application's administration
    /// through save and load procedures the state can be saved/retrieved from disk
    /// </summary>
    public class gameSettings
    {
        /// <summary></summary>
        public static playerInfo[] players;
        /// <summary></summary>
        private static gameModeType _gameMode;
        /// <summary></summary>
        public static List<KeyValuePair<string, int>> highscoreNames;
        /// <summary></summary>
        public static String[] tileImages;
        /// <summary></summary>
        public static tileStatusType[] tileStatus;
        /// <summary></summary>
        public static int tilesX, tilesY;
        /// <summary></summary>
        public static int numberOfTiles;
        /// <summary></summary>
        public static int numberOfImages;
        /// <summary></summary>
        private static String _theme;
        /// <summary></summary>
        private static String imagePrefix;
        /// <summary></summary>
        public static String imageBackTile;
        /// <summary></summary>
        private static int _numberOfPlayers;
        /// <summary></summary>
        public static DateTime _singlePlayerTimer;
        /// <summary></summary>
        public static gameStatusType gameStatus;
        /// <summary></summary>
        public static int[] openedImages;
        /// <summary></summary>
        private static int _openedImageCount;
        /// <summary></summary>
        private static int _currentPlayerNumber;
        /// <summary>the names of the files found on disk for themes</summary>
        public static List<String> themesFound;
        /// <summary></summary>
        public static List<KeyValuePair<String, String>> themeImageFiles;

        /// <summary></summary>
        public static bool themeAllowed;

        /// <summary>
        /// the number of temporarily opened images when user is clicking on a closed tile.
        /// </summary>
        /// <value>the number of temporarily opened images when user is clicking on a closed tile.</value>
        public static int openedImageCount { get { return _openedImageCount; } }

        /// <summary>
        /// Getter for the parameter singlePlayerTimer; will return a time in seconds.
        /// </summary>
        /// <returns>a time in seconds</returns>
        public static double singlePlayerTimer
        {
            get { return (DateTime.Now - _singlePlayerTimer).TotalSeconds; }
        }

        /// <summary>returns the current player number (1 or 2)</summary>
        /// <value>returns the current player number (1 or 2)</value>
        public static int currentPlayerNumber { get { return _currentPlayerNumber; } }

        /// <summary>
        ///   The gameMode property indicates the type of mode: single player/multiplayer and whether flash-mode is enabled
        /// </summary>
        /// <value>The gameMode property indicates the type of mode: single player/multiplayer and whether flash-mode is enabled</value>
        public static gameModeType gameMode
        {
            get { return _gameMode; }
            set
            {
                _gameMode = value;
                switch (_gameMode)
                {
                    case gameModeType.Normal:
                    case gameModeType.NormalFlash:
                        numberOfPlayers = 2;
                        break;
                    case gameModeType.SinglePlayer:
                    case gameModeType.SinglePlayerFlash:
                        numberOfPlayers = 1;
                        break;
                }
            }
        }// property gameMode

        /// <summary>
        ///   The number of players
        /// </summary>
        /// <value>The integer representation of the number of players</value>
        public static int numberOfPlayers
        {
            set
            {
                _numberOfPlayers = value;
                players = new playerInfo[numberOfPlayers];
                for (int i = 0; i < numberOfPlayers; i++) players[i] = new playerInfo();
            }
            get { return _numberOfPlayers; }
        }//property numberOfPlayers

        /// <summary>The theme is a string value representing a certain theme ("iconset")
        /// Also checks if the files on disk are sufficient to use this theme. The variable themeAllowed is set to true or false
        /// </summary>
        /// <value>The theme is a string value representing a certain theme ("iconset")</value>
        public static String theme
        {
            get { return _theme; }
            set
            {
                _theme = value;
                imageBackTile = "themes\\theme" + theme + "_back.png";
                enoughImageFilesForTheme();
            }
        }

        /// <summary>Constructor for this class</summary>
        static gameSettings()
        {
            highscoreNames = new List<KeyValuePair<string, int>>();
            themesFound = new List<String>();
            themeImageFiles = new List<KeyValuePair<string, string>>();

            gameStatus = gameStatusType.Initial;

            gameMode = gameModeType.Normal;

            openedImages = new int[2];
            cleanupOpenedImages();

            determineThemeFilesOnDisk();
            theme = themesFound.First();
            setSize(4, 4);

            enoughImageFilesForTheme();

            _currentPlayerNumber = 1;
        }// constructor gameSettings

        // 
        /// <summary>
        ///   initialises the internal administration for tiles according to requested sizes 
        /// </summary>
        /// <param name="tilesXRequested">number of horizontal tiles</param>
        /// <param name="tilesYRequested">number of vertical tiles</param>
        public static void setSize(int tilesXRequested, int tilesYRequested)
        {
            tilesX = tilesXRequested;
            tilesY = tilesYRequested;

            numberOfTiles = tilesX * tilesY;
            numberOfImages = numberOfTiles / 2;

            tileStatus = new tileStatusType[numberOfTiles];
            tileImages = new string[numberOfTiles];

        }//setSize

        // 
        /// <summary>
        ///   Set player name for a certain playernumber; leave score alone 
        /// </summary>
        /// <param name="playerName">the name of the player</param>
        /// <param name="playerNr">the playernumber; values 1 or 2</param>
        public static void setPlayername(String playerName, int playerNr)
        {
            if (playerNr > numberOfPlayers)
            {
                throw new Exception("playernr" + playerNr + " does not exist");
            }
            else { players[playerNr-1].name = playerName; }
        }//setPlayername

        /// <summary>
        /// Assign an avatar to a player
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="playerNr">values 1 or 2 are allowed</param>
        public static void assignPlayerAvatar(string filename, int playerNr) {
            if (playerNr > numberOfPlayers)
            {
                throw new Exception("playernr" + playerNr + " does not exist");
            }
            else { players[playerNr-1].avatarFilename= filename; }
        }//assignPlayerAvatar()

        /// <summary>
        ///   Set the absolute score for a certain player
        /// </summary>
        /// <param name="score">score (integer)</param>
        /// <param name="playerNr">playernumber requested; values 1 or 2</param>
        public static void setScorePlayer(int score, int playerNr)
        {
            if (playerNr > numberOfPlayers)
            {
                throw new Exception("Impossible playernumber");
            }
            else
            {
                players[playerNr-1].score = score;
            }
        }//setScorePlayer

        /// <summary>
        ///   reset the timer for the single player mode
        /// </summary>
        public static void resetSinglePlayerTimer()
        {
            _singlePlayerTimer = DateTime.Now;
        }

        /// <summary>
        /// add a new player with a certain name and absolute score of zero
        /// </summary>
        /// <param name="playername">The new player's name</param>
        /// <param name="playerNr">the new playernumber; values 1 or 2</param>
        public static void newPlayer(String playername, int playerNr)
        {
            players[playerNr-1].name = playername;
            players[playerNr-1].score = 0;
        }

        /// <summary>
        /// Setup a new game; this initialises the board using the gamesettings previous setup by 
        /// the settings dialogbox
        /// </summary>
        /// <returns></returns>
        public static bool setupNewGame()
        {
            tileStatusType tileStartStatus = tileStatusType.Unknown;

            switch (gameSettings.gameMode)
            {
                case gameModeType.Normal:
                    tileStartStatus = tileStatusType.Closed;
                    gameStatus = gameStatusType.Started;
                    break;
                case gameModeType.NormalFlash:
                    tileStartStatus = tileStatusType.Flash;
                    gameStatus = gameStatusType.Flashing;
                    break;
                case gameModeType.SinglePlayer:
                    tileStartStatus = tileStatusType.Closed;
                    gameStatus = gameStatusType.Started;
                    resetSinglePlayerTimer();
                    break;
                case gameModeType.SinglePlayerFlash:
                    tileStartStatus = tileStatusType.Flash;
                    gameStatus = gameStatusType.Flashing;
                    resetSinglePlayerTimer();
                    break;
            }

            bulkChangeTileStatus(tileStartStatus);

            imagePrefix = "themes\\theme-" +theme + "-";

            // setup one list of images for the number of tiles.
            List<String> tempTiles = new List<String>();

            for (int i = 0; i < numberOfImages; i++)
            {
                String img = imagePrefix + String.Format("{0:00}", i + 1) + ".png";
                tempTiles.Add(img);
                tempTiles.Add(img);
            }
            Random rnd = new Random();

            int q = 0;
            while (tempTiles.Count != 0)
            {
                int p = rnd.Next(tempTiles.Count);
                String img = tempTiles[p];
                tempTiles.RemoveAt(p);

                tileImages[q++] = img;
            }
            return true;
        }//setupNewGame()

        /// <summary>
        ///   Restores the game status from file. File open/closed is done by caller (by utilizing a "Using()")
        /// </summary>
        /// <param name="xmlFile">a Stream previously opened via a common Dialog box "OpenFile"</param>
        /// <returns>True if the restore was successful. Otherwise returns false</returns>
        public static bool restoreFromFile(Stream xmlFile)
        {
            try
            {
                XmlDocument xmlSaveInfo = new XmlDocument();
                xmlSaveInfo.Load(xmlFile);

                gameSettings.setupFromXML(xmlSaveInfo);
            }
            catch (Exception ex)
            {
                throw (new Exception("Kan bestand niet openen" + ex.Message));
            }
            return true;
        }//restoreFromFile()

        /// <summary>
        ///   Change the status of ALL present tiles to a certain status
        /// </summary>
        /// <param name="t"></param>
        public static void bulkChangeTileStatus(tileStatusType t)
        {
            for (int i = 0; i < tileStatus.Length; i++) { tileStatus[i] = t; }
        }//bulkChangeTileStatus()

        /// <summary>
        ///   Change the status of ALL present tiles having a certain current status indicated by filter parameter
        /// </summary>
        /// <param name="t">the new status</param>
        /// <param name="filter">the status used as a filter.</param>
        public static void bulkChangeTileStatus(tileStatusType t, tileStatusType filter)
        {
            for (int i = 0; i < tileStatus.Length; i++) { if (tileStatus[i] == filter) tileStatus[i] = t; }
        }//bulkChangeTileStatus()

        /// <summary>
        ///   Setup the game using an XML document (read from file) indicated by <paramref name="xml"/>
        /// </summary>
        /// <param name="xml">An XMLDocument </param>
        /// <returns></returns>
        public static bool setupFromXML(XmlDocument xml)
        {
            XmlNodeList nodes = xml.SelectNodes("/savegame/highscores/place");
            foreach (XmlNode item in nodes)
            {
                XmlNode nodeName = item.SelectSingleNode("./name/text()");
                XmlNode nodeScore = item.SelectSingleNode("./score/text()");

                String strName;
                int intScore;

                strName = nodeName.Value;
                intScore = Convert.ToUInt16(nodeScore.Value);

                KeyValuePair<string, int> combi = new KeyValuePair<string, int>(strName, intScore);
                highscoreNames.Add(combi);

            }
            //<dimensions>< x > 4 </ x >< y > 4 </ y ></ dimensions >
            XmlNode dimensions = xml.SelectSingleNode("//game/dimensions");
            int x, y;

            x = Convert.ToInt16(dimensions.SelectSingleNode("./x/text()").Value);
            y = Convert.ToInt16(dimensions.SelectSingleNode("./y/text()").Value);
            setSize(x, y);

            XmlNodeList tileNodes = xml.SelectNodes("//game/tile");
            foreach (XmlNode n in tileNodes)
            {
                int tileNr;
                XmlNode img, state;
                tileNr = Convert.ToInt16(n.Attributes.GetNamedItem("nr").Value);

                state = n.SelectSingleNode("./state/text()");
                img = n.SelectSingleNode("./image/text()");

                tileImages[tileNr] = img.InnerText;

                tileStatusType tempT = tileStatusType.Unknown;

                switch (state.InnerText)
                {
                    case "Closed":
                        tempT = tileStatusType.Closed;
                        break;
                    case "Flash":
                        tempT = tileStatusType.Flash;
                        break;
                    case "Found":
                        tempT = tileStatusType.Found;
                        break;
                    case "ShowTemporary":
                        tempT = tileStatusType.ShowTemporary;
                        break;
                }

                tileStatus[tileNr] = tempT;

            }


            XmlNode themeNode = xml.SelectSingleNode("//game/theme/text()");
            gameSettings.theme = themeNode.Value;

            XmlNodeList playerNodes = xml.SelectNodes("//game/players/player");
            int j = 0;
            foreach (XmlNode xn in playerNodes)
            {
                XmlNode pAvatar;
                players[j].name = xn.SelectSingleNode("./name/text()").Value;
                players[j].score = Convert.ToInt16(xn.SelectSingleNode("./score/text()").Value);

                pAvatar = xn.SelectSingleNode("./avatar/");
                if (pAvatar != null)  players[j].avatarFilename = xn.SelectSingleNode("./avatar/text()").Value;
                j++;
            }
            highscoreNames.OrderByDescending(xscore => xscore.Value);
            return true;
        }//setupFromXML()

        /// <summary>
        /// Saves the gamesettings to an XML file.
        /// </summary>
        /// <param name="xmlFile"></param>
        /// <returns></returns>
        public static bool saveToXML(Stream xmlFile)
        {
            XmlDocument xml = new XmlDocument();
            XmlElement root, highscores, place, name, score, game, cards, ntheme;

            root = xml.CreateElement("savegame");
            xml.AppendChild(root);

            highscores = xml.CreateElement("highscores");
            game = xml.CreateElement("game");

            root.AppendChild(highscores);
            root.AppendChild(game);

            ntheme = xml.CreateElement("theme");
            ntheme.InnerText = theme;
            game.AppendChild(ntheme);

            XmlElement player, playersNode, pName, pScore, pAvatar;

            // player container
            playersNode = xml.CreateElement("players");

            // players
            foreach (playerInfo p in players)
            {
                player = xml.CreateElement("player");
                pName = xml.CreateElement("name");
                pScore = xml.CreateElement("score");
                pAvatar = xml.CreateElement("avatar");

                pName.InnerText = p.name;
                pScore.InnerText = p.score.ToString();
                pAvatar.InnerText = p.avatarFilename;

                player.AppendChild(pName);
                player.AppendChild(pScore);
                player.AppendChild(pAvatar);
                playersNode.AppendChild(player);
            }
            game.AppendChild(playersNode);

            XmlNode dimensions = xml.CreateElement("dimensions");
            XmlNode dimX = xml.CreateElement("x");
            XmlNode dimY = xml.CreateElement("y");

            dimX.InnerText = tilesX.ToString();
            dimY.InnerText = tilesY.ToString();

            dimensions.AppendChild(dimX);
            dimensions.AppendChild(dimY);
            game.AppendChild(dimensions);

            //
            foreach (KeyValuePair<string, int> item in gameSettings.highscoreNames)
            {
                place = xml.CreateElement("place");
                highscores.AppendChild(place);

                name = xml.CreateElement("name");
                score = xml.CreateElement("score");

                name.InnerText = item.Key;
                score.InnerText = item.Value.ToString();

                place.AppendChild(name).AppendChild(score);
            }

            // 	  <card nr="2"><state>closed</state><image>themes\themeClassic02.png</image></card>	

            cards = xml.CreateElement("cards");
            for (int i = 0; i < numberOfTiles; i++)
            {
                XmlNode card, state, image;
                XmlAttribute attr1;

                card = xml.CreateElement("tile");
                attr1 = xml.CreateAttribute("nr");
                attr1.InnerText = i.ToString();
                card.Attributes.Append(attr1);

                state = xml.CreateElement("state");
                image = xml.CreateElement("image");

                state.InnerText = tileStatus[i].ToString();
                image.InnerText = tileImages[i].ToString();

                card.AppendChild(state);
                card.AppendChild(image);

                game.AppendChild(card);
            }

            xml.Save(xmlFile);
            return true;
        }//saveToXML()

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pos"></param>
        public static void AddOpenedImage(int pos)
        {
            _openedImageCount++;
            openedImages[_openedImageCount - 1] = pos;
        }
        /// <summary>
        ///   The list of opened images is emptied
        /// </summary>
        public static void cleanupOpenedImages()
        {
            _openedImageCount = 0;
        }

        /// <summary>
        /// Compares the opened images whether they have the same images assigned.
        /// </summary>
        /// <returns>one of the results of openedImagesCompareResult</returns>
        public static openedImagesCompareResult OpenedImagesAreTheSame()
        {
            openedImagesCompareResult res = openedImagesCompareResult.notEnoughImages;
            switch (openedImageCount)
            {
                case 0:
                case 1:
                    res = openedImagesCompareResult.notEnoughImages;
                    break;
                case 2:
                    if (tileImages[openedImages[0]] == tileImages[openedImages[1]])
                    {
                        res = openedImagesCompareResult.theSame;
                    }
                    else
                    {
                        res = openedImagesCompareResult.Different;
                    }
                    break;

            }
            return res;
        }

        /// <summary>
        ///  switch players if more than one player
        /// </summary>
        public static void switchCurrentPlayer()
        {
            // only switch players if more than one player
            if (numberOfPlayers > 1)
            {
                switch (_currentPlayerNumber)
                {
                    case 1:
                        _currentPlayerNumber = 2;
                        break;
                    case 2:
                        _currentPlayerNumber = 1;
                        break;
                    default:
                        throw new Exception("Wrong player number!");
                }
            }
        }//switchCurrentPlayer()

        /// <summary>
        /// Check if all tiles are opened. Return true if all tiles are opened (tileStatusType.Found)
        /// </summary>
        /// <returns></returns>
        public static bool allTilesFound()
        {
            bool res = true;
            foreach (tileStatusType t in tileStatus) res = res & (t == tileStatusType.Found);
            return res;
        }//allTilesFound

        /// <summary>
        /// Return TRUE if only one pair remains to be opened
        /// </summary>
        /// <returns></returns>
        public static bool onePairRemaining()
        {
            bool res = false;
            int count = 0;
            foreach (tileStatusType t in tileStatus)
            {
                if (t == tileStatusType.Found) count++;
            }

            res = ((numberOfTiles - count) == 2);

            return res;
        }//onePairRemaining

        /// <summary>
        /// Assign a number of points to the current player
        /// </summary>
        /// <param name="score"></param>
        /// <returns></returns>
        public static int AssignScoreCurrentPlayer(int score)
        {
            players[_currentPlayerNumber - 1].score += score;
            return players[_currentPlayerNumber - 1].score;
        }

        /// <summary>
        /// The opened tiles get assigned a new status according to parameter <paramref name="t"/>
        /// </summary>
        /// <param name="t">the new status</param>
        public static void setStatusOfOpenedImages(tileStatusType t)
        {
            for (int i = 0; i < openedImages.Length; i++) tileStatus[openedImages[i]] = t;
        }

        /// <summary>Checks if the game is over and changes the gameStatus and tilestatus accordingly</summary>
        public static void checkGameOver()
        {
            if (onePairRemaining())
            {
                gameStatus = gameStatusType.Finished;
                bulkChangeTileStatus(tileStatusType.Found);
            }// if one pair is remaining
        }//checkGameOver()

        /// <summary>
        ///   Searches all theme files on disk.
        /// </summary>
        public static void determineThemeFilesOnDisk()
        {
            String[] files;
            files = Directory.GetFiles("themes", "theme*.png");

            themesFound.Clear();

            foreach (String f in files)
            {

                ///TODO: extract theme names from filenames
                if (System.Text.RegularExpressions.Regex.IsMatch(f, "theme-([A-Za-z]*?)-([0-9]{2}).png"))
                {
                    String themeName;
                    KeyValuePair<string, string> combi;

                    System.Text.RegularExpressions.MatchCollection matches;
                    matches = System.Text.RegularExpressions.Regex.Matches(f, "theme-([A-Za-z]*?)-([0-9]{2}).png");

                    themeName = matches[0].Groups[1].Value;

                    if (!themesFound.Contains(themeName)) themesFound.Add(themeName);

                    combi = new KeyValuePair<string, String>(themeName, f);
                    themeImageFiles.Add(combi);
                    Console.WriteLine(themeName, combi.ToString());
                }
            }//for each file

        }//determineThemeFilesOnDisk()

        /// <summary>
        ///   check if there are enough files found for the selected theme.
        /// </summary>
        /// <returns></returns>
        public static bool enoughImageFilesForTheme()
        {
            bool res;

            res = (themeImageFiles.FindAll(t => t.Key == theme).Count >= numberOfTiles/2);
            themeAllowed = res;

            return res;
        }



    }//class
}//namespace
