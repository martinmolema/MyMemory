using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.XPath;
using System.IO;

namespace MyMemory
{
    public class playerInfo
    {
        public String name;
        public int score;
    }


    public class gameSettings
    {
        public static playerInfo[] players;
        public static List<KeyValuePair<string, int>> highscoreNames;
        public static String[] tileImages;
        public static int[] tileStatus;
        public static int tilesX, tilesY;
        public static int numberOfTiles;
        public static int numberOfImages;
        public static String theme;
        private static String imagePrefix;
        public static String imageBackTile;
         

        static gameSettings()
        {
            highscoreNames = new List<KeyValuePair<string, int>>();
            setTheme("Classic");
            setSize(4, 4);


            players = new playerInfo[2];

            players[0] = new playerInfo();
            players[1] = new playerInfo();

        }

        private static void setTheme(String aTheme)
        {
            theme = aTheme;
            imageBackTile = "themes\\theme" + theme + "_back.png";
        }

        // initialises the internal administration for tiles according to requested sizes
        public static void setSize(int tilesXRequested, int tilesYRequested)
        {
            tilesX = tilesXRequested;
            tilesY = tilesYRequested;

            numberOfTiles = tilesX * tilesY;
            numberOfImages = numberOfTiles / 2;

            tileStatus = new int[numberOfTiles];
            tileImages = new string[numberOfTiles];

        }//setSize

        // Set player names; leave score alone
        public static void setPlayernames(String player1Name, String player2Name)
        {
            players[0].name = player1Name;
            players[1].name = player2Name;
        }

        public static void setScorePlayerA(int score)
        {
            players[0].score = score;
        }//setScorePlayerA

        public static void setScorePlayerB(int score)
        {
            players[1].score = score;
        }//setScorePlayerB

        public static bool setupNewGame(String player1Name, String player2Name)
        {
            setPlayernames(player1Name, player2Name);
            setScorePlayerA(0);
            setScorePlayerB(0);

            switch (gameSettings.theme)
            {
                case "Classic":
                    imagePrefix = "themes\\themeClassic";
                    break; 
                case "Children":
                    imagePrefix = "themes\\themeAnimals";
                    break;
                case "Animals":
                    imagePrefix = "themes\\themeAnimals";
                    break;
                default:
                    throw (new Exception("Onjuiste thema selectie!"));
            }


            // setup one list of images for the number of tiles.
            List<String> tempTiles = new List<String>();

            for(int i = 0; i < numberOfImages; i++) {
                String img = imagePrefix + String.Format("{0:00}", i+1) + ".png";
                tempTiles.Add(img);
                tempTiles.Add(img);
            }
            Random rnd = new Random();

            int q=0;
            while (tempTiles.Count !=0 )
            {
                int p = rnd.Next(tempTiles.Count);
                String img = tempTiles[p];
                tempTiles.RemoveAt(p);

                tileImages[q++] = img;

            }
            return true;
        }//setupNewGame()

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

        public static bool setupFromXML(XmlDocument xml)
        {
            XmlNodeList nodes = xml.SelectNodes("/savegame/highscores/place");
            foreach (XmlNode item in nodes)
            {
                XmlNode nodeName    = item.SelectSingleNode("./name/text()");
                XmlNode nodeScore   = item.SelectSingleNode("./score/text()");

                String strName;
                int intScore;

                strName = nodeName.Value;
                intScore = Convert.ToUInt16(nodeScore.Value);

                KeyValuePair<string, int> combi = new KeyValuePair<string, int>(strName, intScore);
                highscoreNames.Add(combi);

            }

            XmlNodeList tileNodes = xml.SelectNodes("//game/tile");
            int maxx=-1, maxy=-1;
            foreach(XmlNode n in tileNodes)
            {
                int tx, ty;
                tx = Convert.ToInt16( n.Attributes.GetNamedItem("x").Value);
                ty = Convert.ToInt16(n.Attributes.GetNamedItem("y").Value);

                maxx = Math.Max(tx, maxx);
                maxy = Math.Max(ty, maxy);
            }
            setSize(maxx+1, maxy+1);
            foreach (XmlNode n in tileNodes)
            {
                int tileNr;
                XmlNode img, state;
                tileNr = Convert.ToInt16(n.Attributes.GetNamedItem("nr").Value);

                state = n.SelectSingleNode("./state/text()");
                img = n.SelectSingleNode("./image/text()");

                tileImages[tileNr] = img.InnerText;
                tileStatus[tileNr] = Convert.ToInt16(state.InnerText);

            }


            XmlNode themeNode = xml.SelectSingleNode("//game/theme/text()");
            gameSettings.theme = themeNode.Value;

            XmlNode dimensions = xml.SelectSingleNode("//game/dimensions");
            tilesX = Convert.ToInt16(dimensions.SelectSingleNode("./x/text()"));
            tilesY = Convert.ToInt16(dimensions.SelectSingleNode("./y/text()"));

            XmlNodeList playerNodes = xml.SelectNodes("//game/players/player");
            int j = 0;
            foreach(XmlNode xn in playerNodes)
            {
                players[j].name = xn.SelectSingleNode("./name/text()/").Value;
                players[j].score = Convert.ToInt16(xn.SelectSingleNode("./score/text()/").Value);
                j++;
            }
            highscoreNames.OrderByDescending(xscore => xscore.Value);
            return true;
        }//setupFromXML()

        public static bool saveToXML(Stream xmlFile)
        {
            XmlDocument xml = new XmlDocument() ;
            XmlElement root, highscores, place,name,score, game,cards, ntheme;

            root = xml.CreateElement("savegame");
            xml.AppendChild(root);

            highscores = xml.CreateElement("highscores");
            game = xml.CreateElement("game");
            
            root.AppendChild(highscores);
            root.AppendChild(game);

            ntheme = xml.CreateElement("theme");
            ntheme.InnerText = theme;
            game.AppendChild(ntheme);

            XmlElement player, players, pName, pScore ;

            // player container
            players = xml.CreateElement("players");

            // players
            foreach(playerInfo p in players)
            {
                player = xml.CreateElement("player");
                pName = xml.CreateElement("name");
                pScore = xml.CreateElement("score");
                pName.InnerText = p.name;
                pScore.InnerText = p.score.ToString();

                player.AppendChild(pName);
                player.AppendChild(pScore);
                players.AppendChild(player);
            }
            game.AppendChild(players);

            XmlNode dimensions = xml.CreateElement("dimensions");
            XmlNode dimX = xml.CreateElement("x");
            XmlNode dimY = xml.CreateElement("y");

            dimX.InnerText = tilesX.ToString();
            dimY.InnerText = tilesY.ToString();

            dimensions.AppendChild(dimX);
            dimensions.AppendChild(dimY);
            game.AppendChild(dimensions);

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
    }
}
