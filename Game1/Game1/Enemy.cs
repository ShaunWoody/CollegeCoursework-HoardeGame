using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game1.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Game1
{
    public class Enemy : Sprites

    {
       // variables
        public bool colliding = false;
        public bool isdead = false;
        public bool help = false;
        public int amountofmoney = 0;
        public float speed = 1.5f;
        public float enemydistance { get; private set; }


        // Constructor
        public Enemy(Texture2D texture) : base(texture)
        {
            damage = 10;
            size = 0.02f;

        }

        public void FindPlayer(Vector2 playerposition, GameTime gameTime)
        {



            
            var x = playerposition.X - position.X; // X distance

            var y = playerposition.Y - position.Y; // Y distance

            

            var direction = new Vector2(x, y); // from Enemy to Player

            direction.Normalize();



            Vector2 move = new Vector2(0, 0);

           


            move = direction;



            if (Vector2.Distance(playerposition, position) >= 28)
            {

                // Moves in both directions to head straight towards the player
                position.X += move.X * speed;

                position.Y += move.Y * speed;



            }


            





        }
        public void IfCollided(Vector2 playerposition, Player player,GameTime gameTime)
        {



            if (Vector2.Distance(player.position,position) <= 28) // gets whether the player is in distance
            {

                

                player.Health(damage,gameTime); // if in distance then the player is damaged by how much damage the enemy does
                

            }



        }

        public void TurretDistance(Vector2 playerposition)
        {
            
                enemydistance = Vector2.Distance(playerposition, position); // gets the distance from the enemy to the player
            
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, position, null, Color.White, rotation, origin, size, SpriteEffects.None, 0);
              
        }

        public void Update(Vector2 playerposition,Player player,GameTime gameTime,List<Turret> turret)
        {

            FindPlayer(playerposition,gameTime);
            IfCollided(playerposition,player,gameTime);
            TurretDistance(playerposition);
        }




    }
}
