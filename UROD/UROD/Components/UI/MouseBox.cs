using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UROD.Components.UI
{
    public class MouseBox
    {
        public Vector2 Position;
        public Rectangle Box;
        public MouseBox(int x, int y)
        {
            Position.X = x;
            Position.Y = y;
        }
        public void DrawSelf(SpriteBatch spriteBatch)
        {
            Vector2 mousePos = CursorManager.MousePosition;
            Color borderColor = CursorManager.CursorColor;
            Color mainColor = CursorManager.CursorColor == Color.Red ? Color.DarkRed : CursorManager.CursorColor;
            Box =
                new Rectangle((int)Math.Min(Position.X, mousePos.X),
                (int)Math.Min(Position.Y, mousePos.Y),
                (int)Math.Abs(Position.X - mousePos.X),
                (int)Math.Abs(Position.Y - mousePos.Y));
            Utils.DrawBorderedRectangle(Box.Location.ToVector2(), Box.Width, Box.Height, CursorManager.CursorColor * .25f, CursorManager.CursorColor * .85f, spriteBatch);
                
        }
    }
}
