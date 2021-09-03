using Game1.Content;
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
    public class BuyMenu
    {
        // Textures 
        private SpriteFont font;
        private Texture2D buybuttonT;
        private Texture2D turretTexture;
        private Vector2 Turretposition;
        private Vector2 origin;

        //objects
        public List<Button> buttons;
        public HordeSurvival bought;


        public bool upgrade = false;
        public bool Sniperupgrade;
        public bool Minigunupgrade;
        public double buytimewait = 2;
        Rectangle mouseRectangle;
        // base prices
        public int Speedupgradecost = 300;
        public double Damageupgradecost = 500;
        public int Turretcost = 1500;
        public int Healthcost = 2000;
        public int TurretUpgradeCost = 3000;
        private MouseState _currentMouse;

        // imports textures in constructor
        public BuyMenu(Texture2D buybuttontexture, SpriteFont font2)
        {
            
            font = font2;
            buybuttonT = buybuttontexture;
            

        }





        public void Update(GameTime gameTime, HordeSurvival game, Texture2D Ttexture)
        {
            buytimewait += (float)gameTime.ElapsedGameTime.TotalSeconds;

            bought = game;
            turretTexture = Ttexture;
            origin = new Vector2(turretTexture.Width / 2, turretTexture.Height / 2);
            Turretposition = new Vector2(497, 70);


            _currentMouse = Mouse.GetState();
            mouseRectangle = new Rectangle(_currentMouse.X, _currentMouse.Y, 1, 1);

            // assigning buttons to textures and setting values
            var SpeedUpgradeButton = new Button(buybuttonT, font)
            {
                Position = new Vector2(130, 120),
                Cost = Speedupgradecost,
                Text = "" + Speedupgradecost,
                colour = Color.Gray,



            };
            var DamageUpgradeButton = new Button(buybuttonT, font)
            {
                Position = new Vector2(130, 240),
                Cost = (int)Damageupgradecost,
                Text = "" + Damageupgradecost,
                colour = Color.Gray,



            };
            var HealthButton = new Button(buybuttonT, font)
            {
                Position = new Vector2(130, 360),
                Cost = Healthcost,
                Text = "" + Healthcost,
                colour = Color.Gray,



            };
            var TurretBuyButton = new Button(buybuttonT, font)
            {
                Position = new Vector2(460, 120),
                Cost = Turretcost,
                Text = "" + Turretcost,
                colour = Color.Gray,



            };
            var TurretMinigun = new Button(buybuttonT, font)
            {
                Position = new Vector2(570, 360),
                Cost = TurretUpgradeCost,
                Text = "" + TurretUpgradeCost,
                colour = Color.Gray,



            };
            var TurretSniper = new Button(buybuttonT, font)
            {
                Position = new Vector2(350, 360),
                Cost = TurretUpgradeCost,
                Text = "" + TurretUpgradeCost,
                colour = Color.Gray,



            };



            //adds the buttons to a list
            buttons = new List<Button>()
            {
                SpeedUpgradeButton,
                DamageUpgradeButton,
                TurretBuyButton,
                HealthButton,
                TurretMinigun,
                TurretSniper,

            };

            for (int i = 0; i < buttons.Count; i++)
            {
                if (buttons[i].Cost > bought.Cash)
                {
                    buttons[i].colourH = Color.Red;
                }
                else
                    buttons[i].colourH = Color.Green;
            }

            if (bought.player.timetoshoot <= 0.1f)
            {
                buttons[0].Text = "N/A";
            }
            if (Damageupgradecost >= 50000)
            {
                buttons[1].Text = "N/A";
            }


            //assigning buttons to what happens when button is clicked
            SpeedUpgradeButton.Click += SpeedUpgradeButton_Click;
            DamageUpgradeButton.Click += DamageUpgradeButton_Click;
            TurretBuyButton.Click += TurretBuyButton_Click;
            HealthButton.Click += HealthButton_Click;
            TurretMinigun.Click += TurretMinigun_Click;
            TurretSniper.Click += TurretSniper_Click;
            
            //updating buttons



            if (!upgrade && bought.AddTurret.Count >= 1)
            {
                for (int i = 0; i < bought.AddTurret.Count; i++)
                {
                    if (bought.AddTurret[i].TurretUpgraded == false)
                    {


                        for (int j = 4; j < 6; j++)
                        {

                            buttons[j].Update(gameTime);

                        }





                    }
                }
            }

            for (int i = 0; i < 4; i++)
            {
       
                buttons[i].Update(gameTime);

            }
        }

        public void upgradeUpdate()
        {
            _currentMouse = Mouse.GetState();
            mouseRectangle = new Rectangle(_currentMouse.X, _currentMouse.Y, 1, 1);
            
            if (upgrade)
            {
                if (Sniperupgrade == true && Minigunupgrade == false)
                {
                    SniperTurretUpgrade();
                    
                }

                if (Minigunupgrade == true && Sniperupgrade == false)
                {
                    MinigunTurretUpgrade();
                    
                }
            }
        }



        // Clicking events

        private void TurretSniper_Click(object sender, EventArgs e)
        {

            // checks for enough cash then updates the values, for this case for turret
            if (bought.Cash >= TurretUpgradeCost && !upgrade && bought.AddTurret.Count > 0)
            {
                for (int i = 0; i < bought.AddTurret.Count; i++)
                {
                    if (bought.AddTurret[i].TurretUpgraded == false)
                    {
                        bought.Cash = bought.Cash - Turretcost;
                        upgrade = true;
                        Sniperupgrade = true;
                    }
                }
            }
        }

        private void TurretMinigun_Click(object sender, EventArgs e)
        {

            // checks for enough cash then updates the values, for this case for turret

            if (bought.Cash >= TurretUpgradeCost && !upgrade && bought.AddTurret.Count > 0)
            {
                for (int i = 0; i < bought.AddTurret.Count; i++)
                {
                    if (bought.AddTurret[i].TurretUpgraded == false)
                    {
                        bought.Cash = bought.Cash - Turretcost;
                        upgrade = true;
                        Minigunupgrade = true;
                    }
                }
            }
        }

        private void HealthButton_Click(object sender, EventArgs e)
        {
            Fullhealth();
        }

        private void TurretBuyButton_Click(object sender, EventArgs e)
        {
            Buyturret();
        }

        private void DamageUpgradeButton_Click(object sender, EventArgs e)
        {
            Damageupgrade();
        }

        private void SpeedUpgradeButton_Click(object sender, EventArgs e)
        {
            Shootingspeedupgrade();
        }

        // clicking events ended
        public void SniperTurretUpgrade()
        {

            _currentMouse = Mouse.GetState();
            mouseRectangle = new Rectangle(_currentMouse.X, _currentMouse.Y, 1, 1);

            for (int i = 0; i < bought.AddTurret.Count; i++)
            {
                bought.AddTurret[i].color = Color.White;
                if (mouseRectangle.Intersects(bought.AddTurret[i].Rectangle) && !bought.AddTurret[i].TurretUpgraded)
                {
                    bought.AddTurret[i].color = Color.Gray;
                    if (mouseState.LeftButton == ButtonState.Pressed)
                    {
                        bought.AddTurret[i].currenttexture = bought.snipertexture;
                        Sniperupgrade = false;
                        bought.AddTurret[i].color = Color.White;
                        bought.AddTurret[i].TurretUpgraded = true;
                        upgrade = false;
                        bought.AddTurret[i].shoottimer = 1.25f;
                        bought.AddTurret[i].damage = 80;
                    }
                }
            }
        }
        public void MinigunTurretUpgrade()
        {

            _currentMouse = Mouse.GetState();
            mouseRectangle = new Rectangle(_currentMouse.X, _currentMouse.Y, 1, 1);

            for (int i = 0; i < bought.AddTurret.Count; i++)
            {
                bought.AddTurret[i].color = Color.White;
                if (mouseRectangle.Intersects(bought.AddTurret[i].Rectangle) && !bought.AddTurret[i].TurretUpgraded)
                {
                    bought.AddTurret[i].color = Color.Gray;
                    if (mouseState.LeftButton == ButtonState.Pressed)
                    {
                        bought.AddTurret[i].currenttexture = bought.miniguntexture;
                        Minigunupgrade = false;
                        bought.AddTurret[i].color = Color.White;
                        bought.AddTurret[i].TurretUpgraded = true;
                        upgrade = false;
                        bought.AddTurret[i].shoottimer = 0.15f;
                        bought.AddTurret[i].damage = 10;
                    }
                }
            }
        }



        public void Fullhealth()

        {
            // checks for enough cash and makes sure health is below 100 then updates the values, for this case health
            if (bought.Cash >= Healthcost && buytimewait >= 0.5 && bought.player.health != 100)
            {
                bought.player.health = 100;
                bought.Cash = bought.Cash - Healthcost;
                buytimewait = 0;
            }
        }
        public void Shootingspeedupgrade()
        {
            // checks for enough cash then updates the values, for this case speed
            if (bought.Cash >= Speedupgradecost && buytimewait >= 0.5 && bought.player.timetoshoot >= 0.1f)
            {
                bought.player.timetoshoot = bought.player.timetoshoot - 0.015f;
                bought.Cash = bought.Cash - Speedupgradecost;
                Speedupgradecost = Speedupgradecost * 2;
                buytimewait = 0;
            }
            
        }

        public void Damageupgrade()
        {
            // checks for enough cash then updates the values, for this case damage
            if (bought.Cash >= Damageupgradecost && buytimewait >= 0.5 && Damageupgradecost <= 50000)
            {
                bought.player.damage = bought.player.damage + 5;
                bought.Cash = bought.Cash - (int)Damageupgradecost;
                Damageupgradecost = (Damageupgradecost * 2) - 200;
                buytimewait = 0;
            }
        }

        public void Buyturret()
        {
           // checks for enough cash then allows you to place a turret
             if (bought.Cash >= Turretcost && buytimewait >= 0.5 && bought.turrettobeplaced == false)
            {
                bought.turrettobeplaced = true;
                bought.Cash = bought.Cash - Turretcost;
                Turretcost = Turretcost + 1500;
                buytimewait = 0;
            }
        }

        
        public void Draw(SpriteBatch spriteBatch)
        {
            // Draws all text
            spriteBatch.DrawString(font, "Press Escape To Exit Buy Menu", new Vector2(250, 450), Color.Black);
            spriteBatch.DrawString(font, "Player Upgrades:", new Vector2(20, 40), Color.Yellow);
            spriteBatch.DrawString(font, "Shooting Speed++", new Vector2(100,90), Color.Black);          
            spriteBatch.DrawString(font, "Damage++", new Vector2(120, 210), Color.Black);
            spriteBatch.DrawString(font, "Replenish Health", new Vector2(95, 330), Color.Black);
            spriteBatch.DrawString(font, "Turret", new Vector2(470, 90), Color.Black);           
            spriteBatch.Draw(turretTexture, Turretposition, null, Color.White, 0, origin, 0.25f, SpriteEffects.None, 0);
   


            if (upgrade == true)
            {
                spriteBatch.DrawString(font, "Upgrade turret before trying to buy another", new Vector2(215, 410), Color.LightBlue);
            }
            if (bought.turrettobeplaced == true)
            {
                spriteBatch.DrawString(font, "Place turret before buying another one", new Vector2(230, 430), Color.Yellow);
            }

            

            if (!upgrade && bought.AddTurret.Count >= 1)
            {
                 for (int i = 0; i < bought.AddTurret.Count; i++)
                 {
                    if (bought.AddTurret[i].TurretUpgraded == false)
                    {
                        spriteBatch.Draw(bought.miniguntexture, new Vector2(610,305), null, Color.White, 0, origin, 0.25f, SpriteEffects.None, 0);
                        spriteBatch.Draw(bought.snipertexture, new Vector2(387,305), null, Color.White, 0, origin, 0.25f, SpriteEffects.None, 0);
                        spriteBatch.DrawString(font, "Turret Upgrades", new Vector2(425, 220), Color.Yellow);
                        spriteBatch.DrawString(font, "Sniper Turret", new Vector2(331, 330), Color.DarkRed);
                        spriteBatch.DrawString(font, "Minigun Turret", new Vector2(545, 330), Color.LightBlue);

                        for (int j = 4; j < buttons.Count; j++)
                        {

                            buttons[j].Draw(spriteBatch);

                        }


                        

                    
                     }
                 }
            }



            for (int j = 0; j < buttons.Count-2; j++)
            {

               buttons[j].Draw(spriteBatch);

            }
        

        }
    }
}
