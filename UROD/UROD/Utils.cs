using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
    
namespace UROD
{
    public static class Utils
    {
        public static void DrawRectangle(Vector2 position, int width, int height, Color color, SpriteBatch spriteBatch, float smh = 0f)
        {
            float val =
                     GetPercent(position.Y, (float)Main.instance.ScreenHeight, 1f) > 0 ?
                     GetPercent(position.Y, (float)Main.instance.ScreenHeight, 1f) : 0.0000000000000000000000001f;
            spriteBatch.Draw
                 (
                     Main.instance.GenericTextures["Pixel"],
                     position,
                     new Rectangle(0, 0, width, height),
                     color,
                     0f,
                     Vector2.Zero,
                     1f,
                     SpriteEffects.None,
                     smh == 0 ? val : smh
                 );
        }
        public static void DrawBorderedRectangle(Vector2 position, int width, int height, Color color, Color borderColor, SpriteBatch spriteBatch, float smh = 0f)
        {
            float val =
                     GetPercent(position.Y, (float)Main.instance.ScreenHeight, 1f) > 0 ?
                     GetPercent(position.Y, (float)Main.instance.ScreenHeight, 1f) : 0.0000000000000000000000001f;
            spriteBatch.Draw
                 (
                     Main.instance.GenericTextures["Pixel"],
                     position,
                     new Rectangle(0, 0, width, height),
                     color,
                     0f,
                     Vector2.Zero,
                     1f,
                     SpriteEffects.None,
                     smh == 0 ? val : smh
                 );
            #region Draw Borders
            spriteBatch.Draw
                     (
                         Main.instance.GenericTextures["Pixel"],
                         position,
                         new Rectangle(0, 0, 2, height),
                         borderColor,
                         0f,
                         Vector2.Zero,
                         1f,
                         SpriteEffects.None,
                         smh == 0 ? val : smh + .01f
                     );
            spriteBatch.Draw
                     (
                         Main.instance.GenericTextures["Pixel"],
                         position,
                         new Rectangle(0, 0, width, 2),
                         borderColor,
                         0f,
                         Vector2.Zero,
                         1f,
                         SpriteEffects.None,
                         smh == 0 ? val: smh + .01f
                     );
            spriteBatch.Draw
                     (
                         Main.instance.GenericTextures["Pixel"],
                         position + new Vector2(width - 2, 0),
                         new Rectangle(0, 0, 2, height),
                         borderColor,
                         0f,
                         Vector2.Zero,
                         1f,
                         SpriteEffects.None,
                         smh == 0 ? val: smh + .01f
                     );
            spriteBatch.Draw
                     (
                         Main.instance.GenericTextures["Pixel"],
                         position + new Vector2(0, height - 2),
                         new Rectangle(0, 0, width, 2),
                         borderColor,
                         0f,
                         Vector2.Zero,
                         1f,
                         SpriteEffects.None,
                         smh == 0 ? val : smh + .01f
                     );
            #endregion
        }

        #region Get Percentage
        public static int GetPercent(int currentValue, int maxValue, int maxOutput)
        {
            return (currentValue * maxOutput) / maxValue;
        }
        public static float GetPercent(float currentValue, float maxValue, float maxOutput)
        {
            return (currentValue * maxOutput) / maxValue;
        }
        public static double GetPercent(double currentValue, double maxValue, double maxOutput)
        {
            return (currentValue * maxOutput) / maxValue;
        }
        #endregion
        public static string SpliceText(string text, int lineLength)
        {
            return Regex.Replace(text, "(.{" + lineLength + "})" + ' ', "$1" + Environment.NewLine);
        }
    }
}
