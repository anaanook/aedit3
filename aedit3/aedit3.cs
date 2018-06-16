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

namespace aedit
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class aedit3 : Game
    {
        public static Matrix gameScale = Matrix.CreateScale(2, 2, -1) * Matrix.CreateTranslation(new Vector3(0, 0, 1));
        public static aedit3 root;
        public GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        UIManager manager;
        BitmapFont b;
        Effect effect;
        Starfield Starfield;
        KeyboardState oldKeyboardState;
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
                    Console.WriteLine("Go fullscreen homie");
                    int oldwidth = graphics.PreferredBackBufferWidth;
                    int oldheight = graphics.PreferredBackBufferHeight;
                    graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
                    graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;

                    float scalefactorX = graphics.PreferredBackBufferWidth / (float)oldwidth;
                    float scalefactorY = graphics.PreferredBackBufferHeight / (float)oldheight;

                    gameScale = Matrix.CreateScale(2 * scalefactorY, 2 * scalefactorY, -1) * Matrix.CreateTranslation((graphics.PreferredBackBufferWidth- oldwidth)/2/2, 0, 1);
                    Console.WriteLine();
                    Window.IsBorderless = true;
                    Window.Position = new Point(0, 0);
                    graphics.ApplyChanges();
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
            manager.Update();
            Starfield.Update(gameTime);
            InputTest();
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(ClearOptions.DepthBuffer | ClearOptions.Target | ClearOptions.Stencil,Color.Black,1,0);



            spriteBatch.Begin(
                SpriteSortMode.FrontToBack,
                BlendState.AlphaBlend,
                SamplerState.PointClamp,
                DepthStencilState.Default,
                null,
                null,
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
