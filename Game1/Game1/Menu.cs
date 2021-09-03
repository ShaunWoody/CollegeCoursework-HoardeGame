using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Game1.HordeSurvival;

namespace Game1
{
    public class Menu
    {
        // Objects
        public Button Playbutton;
        public Button Quitbutton;
        public Button Leaderboardbutton;
        public HordeSurvival game;
        
        // Variables
        static public MouseState mouseState;
        public SpriteFont font;

        // Constructor
        public Menu()
        {
            mouseState = Mouse.GetState();
        }

        public virtual void Update(GameTime gameTime,Texture2D buttontexture, SpriteFont Font,HordeSurvival games,GameState currentState)
        {
           
            // Creation of buttons
            var Startbutton = new Button(buttontexture, Font)
            {
                Position = new Vector2(300, 130),
                Text = "Start",


            };
            font = Font;
            var Leader = new Button(buttontexture, Font)
            {
                Position = new Vector2(300, 250),
                Text = "LeaderBoard"

            };

            var Quit = new Button(buttontexture, Font)
            {
                Position = new Vector2(300, 370),
                Text = "Quit"

            };

            Playbutton = Startbutton;
            Quitbutton = Quit;
            Leaderboardbutton = Leader;
           
            // Assigns buttons to events
            Playbutton.Click += Playbutton_Click;
            Quitbutton.Click += Quitbutton_Click;
            Leaderboardbutton.Click += Leaderboardbutton_Click;
           
            game = games;

         


            // Updates the button
            Playbutton.Update(gameTime);
            Quitbutton.Update(gameTime);
            Leaderboardbutton.Update(gameTime);



        }

        // Button Events
        private void Leaderboardbutton_Click(object sender, EventArgs e)
        {
            game.currentState = GameState.Leaderboard;
        }

        private void Quitbutton_Click(object sender, EventArgs e)
        {
            game.Exit();
        }

        public void Playbutton_Click(object sender, EventArgs e)
        {

            game.currentState = GameState.Gameplay;
            
        }
        



        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, "Hoarde \n      Survival", new Vector2(315, 40), Color.Red);
            Playbutton.Draw(spriteBatch);
            Quitbutton.Draw(spriteBatch);
            Leaderboardbutton.Draw(spriteBatch);
        }


    }
}
