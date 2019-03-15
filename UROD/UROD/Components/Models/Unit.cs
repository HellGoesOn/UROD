using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using UROD.Components.Basic;

namespace UROD.Components.Models
{
    public abstract class Unit : Entity
    {
        public int Health;
        public int MaxHealth;
        public int Armor;
        public float MoveSpeed;

        public Unit Target;

        public Vector2 MovementTarget = new Vector2(-1, -1);
        public string Name;
        public Texture2D UnitTexture;
        public bool BeingSelected;
        public override void DrawSelf(SpriteBatch sprite)
        {
        }
        
        public void HardUpdate(GameTime gameTime)
        {
            Update(gameTime);
            if(MovementTarget != new Vector2(-1, -1))
            {
                if (Vector2.Distance(Center, MovementTarget) > MoveSpeed * 0.5f)
                {
                    bool collided = false;
                    Vector2 Velocity = (MovementTarget - Center);
                    Velocity.Normalize();
                    foreach (KeyValuePair<string, Unit> pair in Main.instance.Units)
                    {
                        if (pair.Value == this)
                            continue;
                        Rectangle rect = new Rectangle((int)(this.Hitbox.X + Velocity.X), (int)(this.Hitbox.Y + Velocity.Y), Width, Height);
                        if(rect.Intersects(pair.Value.Hitbox))
                        {
                            collided = true;
                            break;
                        }
                    }
                    Position += !collided ? Velocity * MoveSpeed : Vector2.Zero;
                }
                else
                    MovementTarget = new Vector2(-1, -1);
            }
            HardResetEffects();
        }
        public override void Update(GameTime gameTime)
        {
        }

        public abstract void ResetEffects();

        public void HardResetEffects()
        {
            ResetEffects();
            BeingSelected = false;
        }

        public Color HealthColor
        {
            get
            {
                if (Health >= (int)(MaxHealth * .8f))
                    return Color.Lime;
                if (Health <= (int)(MaxHealth * .4f))
                    return Color.Red;
                return Color.Yellow;
            }
        }
    }
}
