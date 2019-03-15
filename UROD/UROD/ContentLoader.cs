using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UROD
{
    public static class ContentLoader
    {
        public static Main Main => Main.instance;
        public static void LoadGenericTexture(string path, string name)
        {
            Texture2D texture = Main.Content.Load<Texture2D>(path);
            Main.GenericTextures.Add(name, texture);
        }
        public static void LoadUITexture(string path, string name)
        {
            Texture2D texture = Main.Content.Load<Texture2D>(path);
            Main.UITextures.Add(name, texture);
        }
    }
}
