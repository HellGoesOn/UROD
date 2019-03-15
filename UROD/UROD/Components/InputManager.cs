using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UROD.Components
{
    public static class InputManager
    {
        public static KeyboardState OldStateKeyboardState;
        public static KeyboardState KeyboardState => Keyboard.GetState();
        
        public static bool IsKeyPressed(Keys key)
        {
            return KeyboardState.IsKeyDown(key);
        }
        public static bool IsKeyTriggered(Keys key)
        {
            return KeyboardState.IsKeyDown(key) && !OldStateKeyboardState.IsKeyDown(key);
        }

        public static void Update()
        {
            OldStateKeyboardState = KeyboardState;
        }
    }
}
