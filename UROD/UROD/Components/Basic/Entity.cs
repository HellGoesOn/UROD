using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UROD.Components.Basic
{
    public abstract class Entity
    {
        public int Width;
        public int Height;
        public Vector2 Position;
        public Vector2 Center => new Vector2(Position.X + (Width * .5f), Position.Y + (Height * .5f));
        public Rectangle Hitbox => new Rectangle((int)Position.X, (int)Position.Y, Width, Height);

        public abstract void Update(GameTime gameTime);

        public abstract void DrawSelf(SpriteBatch sprite);
    }
}
