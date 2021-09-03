using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1.Content
{
    public class Turret : Sprites
    {
        // Variables
        static public MouseState mouseState;
        public bool bullethit = false;
        float turretshoottimer;
        public float shoottimer = 0.65f;
        public Texture2D currenttexture;
        public bool TurretUpgraded = false;
        public Color color = Color.White;


        public Rectangle Rectangle // Creates a rectangle around the turret
        {
            get
            {
                return new Rectangle((int)position.X-25, (int)position.Y-25, 50, 50);
            }
        }


        // constructor
        public Turret(Texture2D texture) : base(texture)
        {
            position = new Vector2(0, 0);
            currenttexture = texture;
            damage = 20;
        }

    

          


        

        public void TurretRotation(Enemy small,List<Enemy> enemy, List<Turret> turret)
        {

            foreach (Enemy enemies in enemy)
            {


                
                
                small = enemy[0];
                
                for (int i = 0; i < enemy.Count; i++)
                {
                    //compare if small is greater than of any element of the array
                    //assign that element in it.
                    
                    


                        if (small.enemydistance >= enemy[i].enemydistance)
                        {
                            small = enemy[i];
                            distance.X = small.position.X -position.X;
                            distance.Y = small.position.Y - position.Y;
                            rotation = (float)Math.Atan2(distance.Y,distance.X); // rotation of the turret equals the angle between the two distances
                        }
                    
                }




            }



        }
        public void TurretShoot( Texture2D bullettexture, List<Bullet> bullet, List<Turret> turret, List<Enemy> enemy)
        {




            

            if (turretshoottimer >= shoottimer && enemy.Count != 0) // turrets shoots every certain seconds
            {

                
                Bullet BnewBullet = new Bullet(bullettexture);

                for (int i = 0; i < turret.Count; i++)
                {
                    BnewBullet.Velocity = new Vector2((float)Math.Cos(rotation), (float)Math.Sin(rotation)) * 30f; // bullet direction is the items rotation

                }
                BnewBullet.position = position;
                BnewBullet.isVisible = true;
                BnewBullet.size = 0.0065f;
                BnewBullet.damage = damage;



                bullet.Add(BnewBullet); // adds the bullet to the list


                turretshoottimer = 0; // resets the timer


            }


        }

        public void Update(GameTime gameTime, Enemy small, List<Enemy> enemy, Texture2D bullettexture, List<Bullet> bullet, List<Turret> turret)
        {
            turretshoottimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            TurretRotation(small,enemy,turret);
            TurretShoot( bullettexture, bullet,turret, enemy);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(currenttexture, position, null, color, rotation, origin, 0.25f, SpriteEffects.None, 0);
        }

    }
}
