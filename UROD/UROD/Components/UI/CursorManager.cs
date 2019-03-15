using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UROD.Components.Basic;
using UROD.Components.Models;

namespace UROD.Components.UI
{
    public static class CursorManager
    {
        public static Color CursorColor;
        private static float _scale;
        private static bool _smallEnough;
        private static int _cursorIndex;

        public static bool MouseOverInterface;

        private static bool _isMouseBoxing;

        private static MouseState _oldState;
        private static MouseState _mouseState => Mouse.GetState();

        public static bool HasClicked => _mouseState.LeftButton == ButtonState.Pressed && _oldState.LeftButton == ButtonState.Released;
        public static bool HasRightClicked => _mouseState.RightButton == ButtonState.Pressed && _oldState.RightButton == ButtonState.Released;
        public static bool LeftButtonCurrent => _mouseState.LeftButton == ButtonState.Pressed;
        public static bool RightButtonCurrent => _mouseState.RightButton == ButtonState.Pressed;

        public static MouseBox MouseBox;
        private static Vector2 Offset;
        public static Vector2 MousePosition => new Vector2(Mouse.GetState().Position.X, Mouse.GetState().Position.Y);

        public static List<Unit> SelectedUnits;

        static CursorManager()
        {
            SelectedUnits = new List<Unit>();
            _scale = 1.2f;
            _smallEnough = false;
            CursorColor = Color.Lime;
        }

        public static void Update(GameTime gameTime, KeyboardState state)
        {
            _cursorIndex = 0;
            MouseOverInterface = false;
            HandleUserInterfaceInteractions();
            if (HasClicked) _isMouseBoxing = true;
            HandleMouseBoxing();

            if (state.IsKeyDown(Keys.A) && !MouseOverInterface)
            {
                _cursorIndex = 2;
                CursorColor = Color.Red;
            }
            else
            {
                CursorColor = Color.Lime;
            }

            CursorBreathVFX();

            if(HasRightClicked && !MouseOverInterface)
            {
                foreach(Unit unit in SelectedUnits)
                {
                    unit.MovementTarget = MousePosition;
                }
            }
            if(InputManager.IsKeyTriggered(Keys.S))
            {
                foreach (Unit unit in SelectedUnits)
                {
                    unit.MovementTarget = new Vector2(-1, -1);
                }
            }
            foreach(Unit unit in SelectedUnits)
            {
                unit.BeingSelected = true;
            }
            if (!LeftButtonCurrent) _isMouseBoxing = false;
            _oldState = _mouseState;
        }
        /// <summary>
        /// Makes Cursor `pulse` by increasing & decreasing its scale
        /// </summary>
        private static void CursorBreathVFX()
        {
            if (_cursorIndex == 0)
            {
                if (!_smallEnough && _scale > 1f)
                {
                    _scale -= 0.01f;
                    if (_scale <= 1f)
                        _smallEnough = true;
                }
                else if (_smallEnough && _scale < 1.2f)
                {
                    _scale += 0.01f;
                    if (_scale >= 1.2f)
                        _smallEnough = false;
                }
            }
            else
            {
                _scale = 1f;
                _smallEnough = true;
            }
        }

        private static void HandleMouseBoxing()
        {
            if (LeftButtonCurrent && !MouseOverInterface)
            {
                _cursorIndex = 1;
                Offset = new Vector2(17, 17);
            }
            else
            {
                Offset = Vector2.Zero;
            }
            if (MouseBox == null && HasClicked && !MouseOverInterface)
            {
                MouseBox = new MouseBox((int)MousePosition.X, (int)MousePosition.Y);
            }
            if (_isMouseBoxing && !LeftButtonCurrent)
            {
                SelectedUnits.Clear();
                foreach (KeyValuePair<string, Unit> unit in Main.instance.Units)
                {
                    if (MouseBox != null)
                    {
                        if (unit.Value.Hitbox.Intersects(MouseBox.Box) && !SelectedUnits.Contains(unit.Value))
                        {
                            if (InputManager.IsKeyPressed(Keys.A))
                                unit.Value.Health -= 5;
                            SelectedUnits.Add(unit.Value);
                        }
                    }
                }
                MouseBox = null;
            }
        }

        private static void HandleUserInterfaceInteractions()
        {
            foreach (KeyValuePair<string, UIElement> element in Main.instance.UIElements)
            {
                if (element.Value.ContainsMouse)
                {
                    MouseOverInterface = true;
                    break;
                }
                if (MouseBox != null)
                {
                    if (MouseBox.Box.Intersects(element.Value.Hitbox))
                    {
                        MouseOverInterface = true;
                        break;
                    }
                }
            }
        }

        public static void DrawSelf(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw
               (
                   Main.instance.UITextures["CursorTexture"],
                   MousePosition - Offset,
                   new Rectangle(0, 34 * _cursorIndex, 34, 34),
                   CursorColor,
                   0f,
                   Vector2.Zero,
                   _scale,
                   SpriteEffects.FlipHorizontally,
                   1f
               );
        }
    }

    public enum IssuedCommand : int
    {
        None,
        Attack,
        Move,
        Cast,
        CannotCommand
    }
}
