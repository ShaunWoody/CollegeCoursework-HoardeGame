using Game1.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Game1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class HordeSurvival : Game
    {
        // Update/Draw
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private SpriteFont font;
        private SpriteFont font2;
        public int i = 1;
        public Texture2D buymenutexture;
        public Texture2D bullettexture;
        public Texture2D TurretTexture;
        public Texture2D snipertexture;
        public Texture2D miniguntexture;
        public bool turrettobeplaced = false;
        public Texture2D currenttexture;


        // Objects
        List<Enemy> enemy = new List<Enemy>();
        Random rng = new Random();
        public Player player;
        List<Bullet> bullet = new List<Bullet>();
        public BuyMenu buymenu;
        public Menu menu = new Menu();
        public Turret closeturret;
        public GameOver gameover = new GameOver();
        public Enemy small;
        public List<Turret> AddTurret = new List<Turret>();
        public Turret turret;
        public Leaderboard leaderBoard;
        //buttons
        public Texture2D buttontexture;
        public Texture2D buybuttontext;
        
        // Wave Variables
        public int WaveNumber = 1;
        static public MouseState mouseState;
        protected int amountofenemies = 2;
        public double spawntime = 1;
        public int healthmulti = 0;
        public float speedmulti = 0.1f;
        int count = 0;
        public int maxenemy;
        public int Smaxeney;
        public int enemiestospawn = 1;
        public int Senemiestospawn = 1;
        public int Cash = 0;
        float spawn = 0;
        float spawnrate;

        // GameStates
        public enum GameState
        {
            MainMenu,
            Gameplay,
            Leaderboard,
            EndOfGame,
            EndOfWave,
            Buymenu,
        }
        public GameState currentState = GameState.MainMenu;
        public GameState previousState;
        public bool updated = false;
      
      






        

        public HordeSurvival()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here#
            IsMouseVisible = true;
            int width = graphics.GraphicsDevice.Viewport.Width;
            int height = graphics.GraphicsDevice.Viewport.Height;


            

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

          
            var playertexture = Content.Load<Texture2D>("survivor");
            
            player = new Player(playertexture);

            bullettexture = Content.Load<Texture2D>("smallcircle");
            
            buttontexture = Content.Load<Texture2D>("rectangle2");

            font = Content.Load<SpriteFont>("Health");
            font2 = Content.Load<SpriteFont>("ButtonFont");

            TurretTexture = Content.Load<Texture2D>("turret");
            currenttexture = TurretTexture;
            
            snipertexture = Content.Load<Texture2D>("sniperturret");
            miniguntexture = Content.Load<Texture2D>("minigunturret");
            buymenu = new BuyMenu(Content.Load<Texture2D>("rectanglebuy"), Content.Load<SpriteFont>("buyfont"));

            leaderBoard = Leaderboard.Load();
        }







        // TODO: use this.Content to load your game content here


        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
       

       

        protected override void Update(GameTime gameTime)
        {

            mouseState = Mouse.GetState();


  



            switch (currentState)
            {




                case GameState.MainMenu:

                    menu.Update(gameTime, buttontexture, font2, this, currentState);

                    previousState = GameState.MainMenu;


                    break;


                case GameState.Gameplay:

                    spawn += (float)gameTime.ElapsedGameTime.TotalSeconds;
                    spawnrate -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                    {
                        currentState = GameState.MainMenu;
                    }
                    
               

                        foreach (Turret turrets in AddTurret)
                    {
                        turrets.Update(gameTime, small, enemy, bullettexture, bullet, AddTurret);
                    }
                    player.Update(gameTime, bullettexture, bullet);
                    Wave();

                    foreach (Enemy enemies in enemy)
                    {
                        enemies.Update(player.position, player, gameTime, AddTurret);

                    }



                    UpdateBullets();
                    BulletEnemy();
                    TurretUpdate();
                    if (player.isdead == true)
                    {
                        currentState = GameState.EndOfGame;
                    }





                    break;

                case GameState.EndOfWave:
                    mouseState = Mouse.GetState();

                    if (Keyboard.GetState().IsKeyDown(Keys.R))
                    {
                        currentState = GameState.Gameplay;
                    }
                    player.Update(gameTime, bullettexture, bullet);
                    UpdateBullets();
                    TurretUpdate();
                    buymenu.upgradeUpdate();
                    if (Keyboard.GetState().IsKeyDown(Keys.B))
                        currentState = GameState.Buymenu;

                    break;


                case GameState.Buymenu:

                    buymenu.Update(gameTime, this, TurretTexture);
                    updated = true;
                    



                    if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                    {
                        currentState = GameState.EndOfWave;
                    }
                    break;
                case GameState.Leaderboard:
                    if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                    {
                        currentState = previousState;
                    }
                    break;
                case GameState.EndOfGame:
                    gameover.Update(gameTime,buttontexture,font2,this,currentState);
                    updated = true;
                    previousState = GameState.EndOfGame;
                    
                    break;
            }

        }

        protected override void Draw(GameTime gameTime)
        {
       



            GraphicsDevice.Clear(Color.Gray);

            spriteBatch.Begin();

            var center = graphics.GraphicsDevice.Viewport.Bounds.Center.ToVector2();

            
            if (currentState == GameState.MainMenu)
            {
                menu.Draw(spriteBatch);
            }
            if (currentState == GameState.Leaderboard)
            {

                 GraphicsDevice.Clear(Color.Black);

                leaderBoard.Draw(spriteBatch, font2, font);
                
  
            }

            if (currentState == GameState.Gameplay || currentState == GameState.EndOfWave)
            {
                GraphicsDevice.Clear(Color.Gray);

                
                spriteBatch.DrawString(font2, "Wave: " + WaveNumber, new Vector2(335, 450), Color.Black);
                spriteBatch.DrawString(font, Cash  + "$", new Vector2(370, 0), Color.LightGreen);
                spriteBatch.DrawString(font, "Health: " + player.health, new Vector2(0, 0), Color.Red);
                player.Draw(spriteBatch);
                foreach (Turret turrets in AddTurret)
                {
                    turrets.Draw(spriteBatch);
                }
                foreach (Enemy enemies in enemy)
                {
                    enemies.Draw(spriteBatch);
                }
                foreach (Bullet bullets in bullet)
                    bullets.Draw(spriteBatch);

               


            }
            if (currentState == GameState.EndOfWave)
            {

                spriteBatch.DrawString(font, "Wave over, Press B for Buy Menu or R to resume", new Vector2(300, 250), Color.Black);

                if (buymenu.upgrade == true)
                spriteBatch.DrawString(font, "Press which turret you want to upgrade", new Vector2(300, 235), Color.LightBlue);
                
                if (turrettobeplaced == true)
                {
                    spriteBatch.DrawString(font, "Press the right mouse button to place turret at players position", new Vector2(300, 265), Color.Yellow);
                }
            }

            if (currentState == GameState.Buymenu && updated == true)
            {
                buymenu.Draw(spriteBatch);
                spriteBatch.DrawString(font, Cash + "$", new Vector2(370, 0), Color.LightGreen);
                updated = false;
            }
            if (currentState == GameState.EndOfGame && updated == true)
            {   
                
                GraphicsDevice.Clear(Color.Black);
                gameover.Draw(spriteBatch);

            }

            spriteBatch.End();







            // TODO: Add your drawing code here

            base.Draw(gameTime);
            }

        public void Wave()
        {
            int Randx = rng.Next(-120, 920);
            int Randy = rng.Next(-50, 550);
            int TBLR = rng.Next(1, 5);
            int randomenemy = rng.Next(1, 6);
            bool specialenemy = false;
            bool enemyspawned = false;

            Vector2 Newposition = new Vector2();



            if (spawn >= spawntime)
            {




                // gets a random side of the screen and random position on that side
                if (TBLR == 1)
                {
                    Newposition = new Vector2(Randx, -50);
                }
                if (TBLR == 2)
                {
                    Newposition = new Vector2(Randx, 550);
                }
                if (TBLR == 3)
                {
                    Newposition = new Vector2(-50, Randy);
                }
                if (TBLR == 4)
                {
                    Newposition = new Vector2(920, Randy);
                }

                spawn = 0;

                Enemy HnewEnemy = new Enemy(Content.Load<Texture2D>("smallcircleg"));
                Enemy newEnemy = new Enemy(Content.Load<Texture2D>("smallcircle"));
                Enemy BnewEnemy = new Enemy(Content.Load<Texture2D>("smallcircleb"));



                // DefaultEnemy
               
                newEnemy.speed = newEnemy.speed + speedmulti;
                newEnemy.position = Newposition;
                newEnemy.size = 0.025f;
                newEnemy.amountofmoney = 50;
                newEnemy.health = newEnemy.health + healthmulti;


                // FastEenemy
                HnewEnemy.health = newEnemy.health - HnewEnemy.health / 2;
                HnewEnemy.speed = newEnemy.speed + 2.5f;
                HnewEnemy.position = Newposition;
                HnewEnemy.size = 0.023f;
                HnewEnemy.amountofmoney = 100;
                // TankEnemy
                BnewEnemy.size = 0.03f;
                BnewEnemy.health = (newEnemy.health + 400) + healthmulti;
                BnewEnemy.speed = (newEnemy.speed / 2) + speedmulti;
                BnewEnemy.position = Newposition;
                BnewEnemy.amountofmoney = 500;

                if (randomenemy == 3 && WaveNumber >= 3)
                {
                    specialenemy = true;
                }



                // checks if an enemy has been spawned
                if (count < amountofenemies && specialenemy == true && enemyspawned == false && Smaxeney < Senemiestospawn)
                {
                    enemy.Add(HnewEnemy);
                    count = count + 1;
                    Smaxeney = Smaxeney + 1;
                    enemyspawned = true;
                }

                if (count < amountofenemies && maxenemy < enemiestospawn && WaveNumber % 5 == 0 && enemyspawned == false && spawnrate <= 0)
                {
                    enemy.Add(BnewEnemy);
                    maxenemy = maxenemy + 1;
                    count = count + 1;
                    spawnrate = 2;
                    enemyspawned = true;
                }

                if (count < amountofenemies && specialenemy == false && enemyspawned == false)
                {
                    enemy.Add(newEnemy);
                    count = count + 1;
                    enemyspawned = true;
                }


                

                if (enemy.Count() <= 0) // when there is no enemies left
                {

                    if (WaveNumber % 5 == 0)
                    {
                        enemiestospawn = enemiestospawn + 1;
                    }
                    if (WaveNumber % 3 == 0)
                    {
                        Senemiestospawn = enemiestospawn + 1;
                    }
                    if (WaveNumber >= 10 && spawntime > 0.5)
                    {
                        spawntime = spawntime - 0.025;
                    }
                    if (WaveNumber >= 8)
                    {
                        healthmulti = healthmulti + 5;
                    }
                    currentState = GameState.EndOfWave;
                    WaveNumber = WaveNumber + 1;
                    count = 0;
                    
                    speedmulti = speedmulti + 0.05f;
                    amountofenemies = amountofenemies + 2;
                    maxenemy = 0;
                    Smaxeney = 0;
                    Wave(); // recalls the program


                }
            }


        }







        public void TurretUpdate()
        {



            if (mouseState.RightButton == ButtonState.Pressed && turrettobeplaced == true)
            {

                Turret newturret = new Turret(currenttexture);
                if (AddTurret.Count >= 1)
                closeturret = AddTurret[0];
                for (int i = 0; i < AddTurret.Count; i++)
                {

                    if (Vector2.Distance(closeturret.position, player.position) >= (Vector2.Distance(AddTurret[i].position, player.position))) // gets the closest turrets
                    {
                       
                        closeturret = AddTurret[i];
                    }

                }
                newturret.position = player.position; // adds turret at player position
               



                if (AddTurret.Count > 0)
                {
                    if (Vector2.Distance(closeturret.position, player.position) >= 50) // if the turret is to close to another turret it can not be placed
                    {
                        AddTurret.Add(newturret);
                        turrettobeplaced = false;
                    }
                }
                if (AddTurret.Count == 0)
                {
                    
                    AddTurret.Add(newturret);
                    turrettobeplaced = false;
                }
      


             }
        }


        public void UpdateBullets()
        {
            foreach (Bullet bullets in bullet)
            {
                bullets.position += bullets.Velocity;
                if (Vector2.Distance(bullets.position, player.position) > 1000) // if bullets are far away they are deleted
                    bullets.isVisible = false;
                if (currentState == GameState.EndOfWave) // if its the end of the wave bullets are deleted
                    bullets.isVisible = false;
            }
            for (int i = 0; i < bullet.Count; i++)
            {
                {
                    if (!bullet[i].isVisible)
                        bullet.RemoveAt(i);
                }
            }
        }
 
        public void BulletEnemy()
        {
            foreach (Bullet bullets in bullet)
            {
                foreach (Enemy enemies in enemy)
                {

                    if (Vector2.Distance(bullets.position, enemies.position) <= 20) // checks if bullet is in range with enenmy
                    {
                        bullets.bullethit = true;
                        enemies.health = enemies.health - bullets.damage; // if in range damages the enemy
                        
                    }
                    if (enemies.health <= 0)
                    {
                        enemies.isdead = true; // checks if enemy is dead
                        
                    }

                }
            }
            for (int i = 0; i < enemy.Count; i++)
            {

                
                    

                    if (enemy[i].isdead == true)
                    {
                        Cash = Cash + enemy[i].amountofmoney; // gives the player the amount of cash for the enemy
                        enemy.RemoveAt(i); // if enemy is dead then removes it
                        

                    }
                
                
            }
            for (int i = 0; i < bullet.Count; i++)
            {
                {
                    if (bullet[i].bullethit)
                        bullet.RemoveAt(i); // if bullet hits enemy then it removes the bullet
                }
            }
        }
 
    }   
        
    
}
