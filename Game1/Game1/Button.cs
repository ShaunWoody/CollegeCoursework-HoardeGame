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
    public class Button
    {

        private MouseState _currentMouse;

        private SpriteFont _font;

        private bool _isHovering;

        private MouseState _previousMouse;

        private Texture2D _texture;


        public event EventHandler Click;

        public bool Clicked { get; private set; }

        public Color Pencolour { get; set; }

        public Color colour { get; set; }

        public Color colourH { get; set; }

        public Vector2 Position { get; set; }
        public int Cost { get; set; }
       
        public Rectangle Rectangle // draws a rectangle around the button
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height);
            }
        }
    
        public string Text { get; set; }

        // Constructor
        public Button(Texture2D texture, SpriteFont font)
        {
            _texture = texture;

            _font = font;

            Pencolour = Color.Black;

            colour = Color.White;

            colourH = Color.Gray;
        }

        public void Draw(SpriteBatch spriteBatch)
        {


            if (_isHovering)
                colour = colourH;

            spriteBatch.Draw(_texture, Rectangle, colour);

            if(!string.IsNullOrEmpty(Text)) // checks if text is inside of the button then puts it in the centre
            {
                var x = (Rectangle.X + (Rectangle.Width / 2)) - (_font.MeasureString(Text).X / 2);
                var y = (Rectangle.Y + (Rectangle.Height / 2)) - (_font.MeasureString(Text).Y / 2);

                spriteBatch.DrawString(_font, Text , new Vector2(x, y), Pencolour);
            }
        }
        
        public void Update(GameTime gameTime)
        {
            _previousMouse = _currentMouse;
            _currentMouse = Mouse.GetState();
            
            
            var mouseRectangle = new Rectangle(_currentMouse.X, _currentMouse.Y, 1, 1);
            _isHovering = false;

            if(mouseRectangle.Intersects(Rectangle)) // checks if mouse collides with the buttons
            {
                _isHovering = true;
               
                if (_previousMouse.LeftButton == ButtonState.Released && _currentMouse.LeftButton == ButtonState.Pressed) // if button is collided with mouse and clicked then it calls the event
                {
         
                       
                    Click?.Invoke(this, new EventArgs());
                    
                    
                   
                }
            }
        }

    }
}
