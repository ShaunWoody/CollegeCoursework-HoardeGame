using System;
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
    public class GameOver : Menu
    {

        // Objects
        new List<Button> buttons;
        new Button SaveScore;
        // Variables
        public bool leaderboardupdated = false;
        public string Playername = "";
        public bool updated = false;
        public string writing = string.Empty;
        bool pressed = true;
        bool getname = false;
        bool gotname = false;
       
        public KeyboardState currentkeystate, prevkey;

        float clicktimer = 0;
        // Constructor
        public GameOver()
        {
            mouseState = Mouse.GetState();
            
        }
        public override void Update(GameTime gameTime, Texture2D buttontexture, SpriteFont Font, HordeSurvival games, GameState currentState)
        {
            clicktimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            prevkey = currentkeystate;
            
            currentkeystate = Keyboard.GetState();

            if (updated == false)
            {
                base.Update(gameTime, buttontexture, Font, games, currentState);
                updated = true;
            }

            SaveScore = new Button(buttontexture, Font)
            {
                Text = "Save Score",
                Position = new Vector2(300, 130),
            };
           
            buttons = new List<Button>()
            {
                SaveScore,
                Quitbutton,
                Leaderboardbutton,
                

            };
           
            
            SaveScore.Click += Clicked;
           
            foreach (Button button in buttons)
            {
                button.colour = Color.Black;
                button.Pencolour = Color.Red;
                button.Update(gameTime);
            }


            if (getname == true)
            {
                GetPlayerName();
            }
            if (gotname == true)
            {
                game.leaderBoard.Update(game.WaveNumber, Playername);
                leaderboardupdated = true;
                gotname = false;
                
            }

            


        }



        private void Clicked(object sender, EventArgs e)
        {

            if (clicktimer  > 1 && leaderboardupdated == false)
            {

                getname = true;
                clicktimer = 0;
            }


      
            }
    
        public void  GetPlayerName()
        {

          
                KeyboardState keyState = Keyboard.GetState();
                Keys[] pressedKeys = keyState.GetPressedKeys();

           


            foreach  (Keys key in pressedKeys) // gets whether the key pressed 
            {
                if (currentkeystate.IsKeyDown(key) && prevkey.IsKeyUp(key))
                {
                    pressed = true;
                }
                else
                    pressed = false;
               


            }

            if (pressedKeys.Length > 0 && pressed == true)
            {


               
               
                

                    if (pressedKeys[0] == Keys.Back) // allows you to use backspace to delete most recent letter
                    {

                        if (Playername.Length > 0)
                        Playername = Playername.Substring(0, Playername.Length - 1);
                    }

                    if (pressedKeys[0] >= Keys.A && pressedKeys[0] <= Keys.Z) // allows you to enter your name
                {
                        var keyvalue = pressedKeys[0].ToString();




                        Playername = Playername + keyvalue;// creates the string
                    pressed = keyState.IsKeyUp(pressedKeys[0]); 
                    }
                    if (pressedKeys[0] == Keys.Enter)
                    {
                    getname = false;
                    gotname = true;
                }


            }







        }
        public override void Draw(SpriteBatch spriteBatch)
        {
           
            spriteBatch.DrawString(font, "Game Over ",new Vector2(310,20) , Color.Red);
            spriteBatch.DrawString(font, "You survived " + game.WaveNumber + " Rounds", new Vector2(230,  60), Color.Red);
            if (getname == true)
            {
                spriteBatch.DrawString(font, "Type name\nThen press enter\nto save", new Vector2(0, 100), Color.Yellow);
                spriteBatch.DrawString(font, Playername, new Vector2(0, 200), Color.Orange);
            }
            foreach (Button button in buttons)
            {
                button.Draw(spriteBatch);
            }
        }




    }
}
