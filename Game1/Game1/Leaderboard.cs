using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Game1
{
    public class Leaderboard
    {
        private static string _filename = "leaderboard.xml";

        public List<FileInput> DisplayScore;

        public List<FileInput> Scores;

        public List<int> Ssd = new List<int>();


        public Leaderboard()
            : this(new List<FileInput>())
        {

        }
        public Leaderboard(List<FileInput> scores)
        {
            Scores = scores;

            DisplayScores();
        }


        public void Add(FileInput input) // This method adds the data to the file
        {
            Scores.Add(input); 
           
            Scores = Scores.OrderByDescending(c => c.WaveSurvived).ToList();
        }

        public static Leaderboard Load()
        {

            if (!File.Exists(_filename))
            {

                return new Leaderboard(); // if file doesnt exists then it makes that file
            }

            using (var reader = new StreamReader(new FileStream(_filename, FileMode.Open))) // opens the file
            {
                var serillizer = new XmlSerializer(typeof(List<FileInput>));

                var scores = (List<FileInput>)serillizer.Deserialize(reader);

                return new Leaderboard(scores); // returns the leaderboard with all the data from the file
            }
        }

        public void Save()
        {
            //overrides the file if it already exists to save the scores
            using (var writer = new StreamWriter(new FileStream(_filename, FileMode.Create)))
            {
                var serillizer = new XmlSerializer(typeof(List<FileInput>));

                serillizer.Serialize(writer, Scores);
            }
        }

        public void DisplayScores() // makes it so it only displays 100 scores
        {
            DisplayScore = Scores.Take(100).ToList();
        }
        public void Update(int WaveNumber, string Playername) // Calls all the methods in the class
        {

            Add(new FileInput()
            {
                Name = Playername,
                WaveSurvived = WaveNumber

            });


            DisplayScores();
            Save();
        }
        public void Draw(SpriteBatch spriteBatch,SpriteFont font2,SpriteFont font)
        {

            var x = 0;
            var y = 80;
            int position = 1;
            spriteBatch.DrawString(font2, "Leaderboard", new Vector2(300, 30), Color.Yellow);
            for (int i = 0; i < DisplayScore.Count; i++)
            {


                string leader = position + ". " + DisplayScore[i].Name + ":" + DisplayScore[i].WaveSurvived + "\n";

                if (y >= 480) // displays the scores saved until it reaches the bottom of the screen then it displays it on another column
                {
                    y = 80;
                    x = x + 160;
                    spriteBatch.DrawString(font, leader, new Vector2(x, y), Color.Yellow);


                }
                else
                    spriteBatch.DrawString(font, leader, new Vector2(x, y), Color.Yellow);
                position = position + 1;

                y = y + 20;

            }

        }
    }
}
