using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    public class Sprites
    {
        public Vector2 direction;
        protected Texture2D _texture;
        public Vector2 position;
        public float rotation;
        public Vector2 distance;
        public Vector2 origin;
        public Vector2 Velocity;
        public bool isVisible;
        public int health = 100;
        public int damage;
        public float size;


        // Base constructor for inherited members, calls texture
        public Sprites(Texture2D texture)
        {
            _texture = texture;
            origin = new Vector2(_texture.Width / 2, _texture.Height / 2);
            
        }

        // Update and draw used to override
        public void Update()
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, position, null, Color.White, rotation, origin, 1f, SpriteEffects.None, 0);
        }


    }
}
