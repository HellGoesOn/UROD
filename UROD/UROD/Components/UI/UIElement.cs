using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UROD.Components.UI
{
    public abstract class UIElement
    {
        public int Width;
        public int Height;
        public Vector2 Position;
        public List<UIElement> SubElements;
        public virtual void OnClick() { }
        public Rectangle Hitbox => new Rectangle((int)Position.X, (int)Position.Y, Width, Height);
        public bool ContainsMouse => Hitbox.Contains(CursorManager.MousePosition.ToPoint());
        public Texture2D Texture;
        public bool BeenClicked => ContainsMouse && CursorManager.HasClicked;
        public bool BeenRightClicked => ContainsMouse && CursorManager.HasRightClicked;
        public abstract void Update(GameTime gameTime);
        public abstract void DrawSelf(SpriteBatch spriteBatch);
        public void UpdateSubElements(GameTime gameTime)
        {
            foreach(UIElement element in SubElements)
            {
                element.Update(gameTime);
            }
        }
    }
}
