using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;
using UROD.Components.UI;
using UROD.Components;
using UROD.Components.Models;
using UROD.Units;

namespace UROD
{
    public class Main : Game
    {
#pragma warning disable IDE0044 // Добавить модификатор "только для чтения"
        GraphicsDeviceManager graphics;
#pragma warning restore IDE0044 // Добавить модификатор "только для чтения"
        SpriteBatch spriteBatch;
        public static Main instance;

        public int ScreenWidth => graphics.PreferredBackBufferWidth;
        public int ScreenHeight => graphics.PreferredBackBufferHeight;

        public Texture2D CursorTexture;
        public Dictionary<string, Texture2D> UITextures;
        public Dictionary<string, Texture2D> GenericTextures;

        public Dictionary<string, UIElement> UIElements;

        public Dictionary<string, Unit> Units;
        public List<string> UnitsToRemove;

        public SpriteFont DefaultFont;

        public Main()
        {
            instance = this;
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        
        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width -10;
            graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height - 200;
            graphics.ApplyChanges();
            // TODO: Add your initialization logic here
            DefaultFont = Content.Load<SpriteFont>("DefaultFont");
            UITextures = new Dictionary<string, Texture2D>();
            GenericTextures = new Dictionary<string, Texture2D>();

            Units = new Dictionary<string, Unit>();
            UIElements = new Dictionary<string, UIElement>();
            UnitsToRemove = new List<string>();


            UIElements.Add("Test", new UITest(new Vector2(200, 200)));
            base.Initialize();
        }
        
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            ContentLoader.LoadUITexture("Cursors", "CursorTexture");
            ContentLoader.LoadGenericTexture("MagicPixel", "Pixel");
            for(int i = 0; i < 5; i++)
            {
                var Marine = new Marine(new Vector2(100 + i * 50, 100 + i * 50), "Marine" + i);
                Units.Add("Marine" + i, Marine);
            }
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            Content.Unload();
        }
        
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            foreach(KeyValuePair<string, UIElement> element in UIElements)
            {
                element.Value.Update(gameTime);
            }
            foreach(KeyValuePair<string, Unit> pair in Units)
            {
                pair.Value.HardUpdate(gameTime);
                if(pair.Value.Health <= 0)
                {
                    UnitsToRemove.Add(pair.Key);
                }
            }
            for (int i = UnitsToRemove.Count; i > 0; i--)
            {
                Units.Remove(UnitsToRemove[i - 1]);
                UnitsToRemove.RemoveAt(i - 1);
            }
            CursorManager.Update(gameTime, Keyboard.GetState());
            InputManager.Update();
            base.Update(gameTime);
        }
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            ///Draw units 
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.DepthRead, (RasterizerState)null);
            foreach (KeyValuePair<string, Unit> pair in Units)
            {
                pair.Value.DrawSelf(spriteBatch);
                if (pair.Value.BeingSelected || InputManager.IsKeyPressed(Keys.LeftAlt))
                {
                    Utils.DrawBorderedRectangle(pair.Value.Center + new Vector2(-20, -28), 40, 10, Color.Gray, Color.DarkGray, spriteBatch, .9f);
                    Utils.DrawRectangle(pair.Value.Center + new Vector2(-20, -28), Utils.GetPercent(pair.Value.Health, pair.Value.MaxHealth, 40), 10, pair.Value.HealthColor, spriteBatch, 1f);
                }
            }
            spriteBatch.End();

            ///Draw Interface
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, (DepthStencilState)null, (RasterizerState)null);
            if(CursorManager.MouseBox != null) CursorManager.MouseBox.DrawSelf(spriteBatch);
            foreach (KeyValuePair<string, UIElement> element in UIElements)
            {
                element.Value.DrawSelf(spriteBatch);
            }
            spriteBatch.DrawString(DefaultFont, Units.Count + "\n" + CursorManager.MouseOverInterface + "\n" + CursorManager.MousePosition, new Vector2(10, 10), Color.White);
            CursorManager.DrawSelf(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
