using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    public class Bullet : Sprites

    {
        // Variables
        static public MouseState mouseState;
        public bool bullethit = false;
        public Color color = Color.Black;

        // Constructor
        public Bullet(Texture2D texture): base (texture)
        {
            isVisible = false;
            damage = 20;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, position, null, color, rotation, origin, size, SpriteEffects.None, 0);
        }
    }
}
