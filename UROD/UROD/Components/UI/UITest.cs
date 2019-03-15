using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UROD.Components.UI
{
    public class UITest : UIElement
    {
        public string MyText;
        public UITest(Vector2 pos)
        {
            Main main = Main.instance;
            Position = new Vector2(main.ScreenWidth / 2 - 240, main.ScreenHeight - 62);
            Width = 480;
            Height = 60;
            MyText = "This is UIElement derived class. Neat";
        }
        public override void DrawSelf(SpriteBatch spriteBatch)
        {
            Utils.DrawBorderedRectangle(Position, Width, Height, Color.Blue, Color.DarkBlue, spriteBatch);
            spriteBatch.DrawString(Main.instance.DefaultFont, MyText, Position + new Vector2(240, 20) - new Vector2(MyText.Length * 3.25f, 0), Color.White);
        }

        public override void Update(GameTime gameTime)
        {
            if(BeenClicked)
            {
                MyText = "My position has been adjusted by +15.";
                Position.Y += 15;
            }
            if(BeenRightClicked)
            {
                MyText = "My position has been adjusted by -15";
                Position.Y -= 15;
            }
        }
    }
}
