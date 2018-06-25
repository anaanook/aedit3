using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using aedit.Classes.UI;
using aedit.Classes.Core;
using aedit.Classes.Ex;
using static aedit.Classes.Core.PaletteManager;
namespace aedit
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class aedit3 : Game
    {
        public Vector2 center {
            get {
                Vector2 c = new Vector2(graphics.PreferredBackBufferWidth/2, graphics.PreferredBackBufferHeight/2);
                
                return Vector2.Transform(c, Matrix.Invert(gameScale));
            }
        }
        public static Matrix gameScale = Matrix.CreateScale(2, 2, -1) * Matrix.CreateTranslation(new Vector3(0, 0, 1));
        public static aedit3 root;
        public GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        UIManager manager;
        BitmapFont b;
        public bool fullscreen = false;
        Effect effect;
        Effect uiEffect;
        Starfield Starfield;
        public KeyboardState oldKeyboardState;
        public KeyboardState KeyboardState;
        public aedit3()
        {
            root = this;
            graphics = new GraphicsDeviceManager(this)
            {
                GraphicsProfile = GraphicsProfile.HiDef,
                PreferredDepthStencilFormat = DepthFormat.Depth24Stencil8,
                PreferredBackBufferWidth = 1280,
                PreferredBackBufferHeight = 720
                
            };
            Window.IsBorderless = true;
            Palette(9, 0, 0);
            Content.RootDirectory = "Content";
        }
        /// <summary>
        /// 
        /// 
        /// </summary>
        void InputTest() {
            KeyboardState k = Keyboard.GetState();
            if(k.IsKeyDown(Keys.LeftAlt) || k.IsKeyDown(Keys.RightAlt)) {
                if(k.IsKeyDown(Keys.Enter) && oldKeyboardState.IsKeyUp(Keys.Enter)) {
                    if (!fullscreen) {
                        Console.WriteLine("Go fullscreen homie");
                        int oldwidth = graphics.PreferredBackBufferWidth;
                        int oldheight = graphics.PreferredBackBufferHeight;
                        graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
                        graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
                        float scalefactorX = graphics.PreferredBackBufferWidth / (float)oldwidth;
                        float scalefactorY = graphics.PreferredBackBufferHeight / (float)oldheight;
                        float test = (oldwidth * scalefactorY);
                        gameScale = Matrix.CreateScale(2 * scalefactorY, 2 * scalefactorY, -1) * Matrix.CreateTranslation((graphics.PreferredBackBufferWidth - test) / 2, 0, 1);
                        Console.WriteLine();
                        Window.Position = new Point(0, 0);
                        graphics.ApplyChanges();
                        fullscreen = true;
                    } else {
                        graphics.PreferredBackBufferWidth = 1280;
                        graphics.PreferredBackBufferHeight = 720;
                        Window.Position = new Point(GraphicsDevice.DisplayMode.Width/2 -1280/2, GraphicsDevice.DisplayMode.Height/2 - 720/2);
                        gameScale = Matrix.CreateScale(2 , 2 , -1) * Matrix.CreateTranslation(0, 0, 1);
                        graphics.ApplyChanges();
                        fullscreen =false;
                    }

                }
            }

            oldKeyboardState = k;
        }
        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            // TODO: use this.Content to load your game conte
            FontManager.Init();
            b = FontManager.UIFont;
            manager = new UIManager();

            GraphicsDevice.SamplerStates[1] = SamplerState.PointWrap;
            Starfield = new Starfield();
            effect = Content.Load<Effect>("shaders/shader_basic");
            effect.Parameters["Palette"].SetValue( Content.Load<Texture2D>("palette"));
            uiEffect = Content.Load<Effect>("shaders/shader_ui");
            uiEffect.Parameters["Palette"].SetValue(Content.Load<Texture2D>("palette"));
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            KeyboardState = Keyboard.GetState();
            manager.Update();
            Starfield.Update(gameTime);
            InputTest();
            manager.postUpdate();
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            UIRenderTarget.DrawGroup(spriteBatch);

            GraphicsDevice.Clear(ClearOptions.DepthBuffer | ClearOptions.Target | ClearOptions.Stencil,Color.Black,1,0);



            spriteBatch.Begin(
                SpriteSortMode.FrontToBack,
                BlendState.AlphaBlend,
                SamplerState.PointClamp,
                DepthStencilState.Default,
                null,
                uiEffect,
                gameScale
                );

            manager.Draw(spriteBatch);
            spriteBatch.End();

            spriteBatch.Begin(
                SpriteSortMode.FrontToBack,
                BlendState.AlphaBlend,
                SamplerState.PointWrap,
                DepthStencilState.Default,
                null,
                effect,
                gameScale
                );
            Starfield.Draw(spriteBatch);
            
            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
