using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using Game1.Content;

namespace Game1
{
    public class Player : Sprites
    {
        // Object
        public Turret closestturret;
        
       // Variables
        static public MouseState mouseState;
        private int speed = 2;
        public bool isdead = false;
        public bool tookdamage = false ;
        public float wait = 0;
        public float shoottimer = 0;
        public int borderoffset = 30;
        public float timetoshoot = 0.2f;
        public bool IsMouseVisible { get; private set; }
       
        
        // Constructor
        public Player(Texture2D texture) : base (texture)
        {
           
            Mouse.SetPosition(0, 250);
            position = new Vector2(450, 200);
            origin = new Vector2((_texture.Width / 2) - 35, (_texture.Height / 2) + 10);
            damage = 20;
        }

        
        
            
        public void Health(int damage, GameTime gameTime)
        {

            
            if (tookdamage == false) // checks to see if damage has already been took
            {
                health = health - damage;
                tookdamage = true;
                wait = 0;
            }
            if (tookdamage == true) // if damage has already been took then it waits a second before more can be dealt
            {
                
                
                if (wait >= 1)
                {
                    wait = 0;
                    tookdamage = false;
                }
                
            }
  
            if (health <= 0) // checks if the player is dead
            {
                isdead = true;
            }
        }

 








        public void Update(GameTime gameTime, Texture2D bullettexture, List<Bullet> bullet)
        {
           
            Input();
            MouseRotation();

            if (mouseState.LeftButton == ButtonState.Pressed) // when left button is pressed player shoots
                Shoot(bullettexture,bullet);

            wait += (float)gameTime.ElapsedGameTime.TotalSeconds;
            shoottimer += (float)gameTime.ElapsedGameTime.TotalSeconds;


        }

        public void Shoot(Texture2D bullettexture, List<Bullet> bullet)
        {
            if (shoottimer > timetoshoot)
            {


                Bullet newBullet = new Bullet(bullettexture);
                newBullet.Velocity = new Vector2((float)Math.Cos(rotation), (float)Math.Sin(rotation)) * 20f; // gets the rotation of the player and speed of bullet
                newBullet.position = position; // adds bullet at the current position
                newBullet.isVisible = true;
                newBullet.damage = damage; // sets the bullets damage as the current players damage
                newBullet.size = 0.005f;



                bullet.Add(newBullet); // adds new bullet to the list


                shoottimer = 0;


            }


        }

        

        public void Input()
        {
            mouseState = Mouse.GetState();
            int mouseY = mouseState.Y;
            int mouseX = mouseState.X;





                if (Keyboard.GetState().IsKeyDown(Keys.A))
                {
                     
                        if (position.X > 20)
                            position.X -= speed;
                     
                    
                }

                if (Keyboard.GetState().IsKeyDown(Keys.D))
                {
                    if (position.X < 790)
                        position.X += speed;
                   
                }

                if (Keyboard.GetState().IsKeyDown(Keys.W))
                {
                    if (position.Y > 20)
                        position.Y -= speed;
                   
                }

                if (Keyboard.GetState().IsKeyDown(Keys.S))
                {
                    if (position.Y < 480)
                        position.Y += speed;
                    
                }
            
            


        }
 
        public void MouseRotation() // Mouse Rotation, Makes the player look at the cursor.
        {
            mouseState = Mouse.GetState();
            IsMouseVisible = true;

            distance.X = mouseState.X - position.X;
            distance.Y = mouseState.Y - position.Y;
            rotation = (float)Math.Atan2(distance.Y, distance.X); // Uses Triangles to find the shortest distance to the cursor.
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, position, null, Color.White, rotation, origin, 0.35f, SpriteEffects.None, 0);
        }

    }
}


