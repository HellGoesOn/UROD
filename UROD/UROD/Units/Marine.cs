using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using UROD.Components.Models;

namespace UROD.Units
{
    public class Marine : Unit
    {
        public Marine(Vector2 pos, string name)
        {
            Name = name;
            Position = pos;
            Width = 20;
            Height = 20;
            Health = MaxHealth = 45;
            MoveSpeed = 2.2f;

        }
        public override void Update(GameTime gameTime)
        {
        }
        public override void DrawSelf(SpriteBatch sprite)
        {
            Utils.DrawRectangle(this.Position, this.Width, this.Height, Color.Black, sprite);
            if (BeingSelected)
            {
                Utils.DrawBorderedRectangle(this.Position - new Vector2(2, 2), this.Width + 4, this.Height + 4, Color.White * 0f, Color.Lime, sprite, 0.000000000000000000000000000000001f);
            }
        }
        public override void ResetEffects()
        {
        }
    }
}
