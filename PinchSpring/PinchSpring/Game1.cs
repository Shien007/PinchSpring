//作成日：　2017.03.14
//作成者：　柏
//クラス内容：　Gameクラス
//修正内容リスト：
//名前：　　　日付：　　　内容：
//名前：　　　日付：　　　内容：

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using PinchSpring.Def;
using MyLib.Device;
using PinchSpring.Scene;
using PinchSpring.Scene.Scenes;

namespace PinchSpring
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager graphics;
        private SceneManager sceneManager;
        private GameDevice gameDevice;
        private Renderer_2D renderer2D;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = Parameter.ScreenHeight;
            graphics.PreferredBackBufferWidth = Parameter.ScreenWidth;
            Content.RootDirectory = "Content";
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
            gameDevice = new GameDevice(Content, GraphicsDevice);
            renderer2D = gameDevice.GetRenderer;

            sceneManager = new SceneManager();
            sceneManager.Add(E_Scene.TITLE, new Title(gameDevice));
            sceneManager.Add(E_Scene.GAMEPLAY, new GamePlay(gameDevice, GraphicsDevice));
            sceneManager.Add(E_Scene.ENDING, new Ending(gameDevice));
            sceneManager.Add(E_Scene.CLEAR, new Clear(gameDevice));
            sceneManager.Add(E_Scene.LOADING, new Loading(gameDevice));
            sceneManager.Add(E_Scene.OPERATE, new Operate(gameDevice));
            sceneManager.Add(E_Scene.STAFFROLL, new StaffRoll(gameDevice));
            sceneManager.Change(E_Scene.LOADING);

            Window.Title = "PinchSpring";
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // TODO: use this.Content to load your game content here
            //gameDevice.Initialize();
            NewPixel();
        }


        /// <summary>
        /// ピクセルずつ設定し、テクスチャ素材を作る
        /// </summary>
        public void NewPixel()
        {
            //エフェクト用絵を作り出す

            //フェードイン・フェードアウト用
            Texture2D fade = new Texture2D(GraphicsDevice, Parameter.ScreenWidth, Parameter.ScreenHeight);
            Color[] fadeColor = new Color[Parameter.ScreenWidth * Parameter.ScreenHeight];
            int index = 0;
            for (int i = 0; i < Parameter.ScreenHeight; i++)
            {
                for (int j = 0; j < Parameter.ScreenWidth; j++)
                {
                    fadeColor[index] = new Color(0, 0, 0);
                    index++;
                }
            }
            fade.SetData(fadeColor);
            gameDevice.GetResouceManager.LoadTexture("fade", fade);

            //ロード用
            Texture2D loadBar = new Texture2D(GraphicsDevice, 1, 32);
            Color[] loadBarColor = new Color[1 * 32];
            for (int i = 0; i < loadBar.Height; i++)
            {
                loadBarColor[i] = (i >= 9 && i <= 11) ?
                    new Color(255, 255, 255) : new Color(255, 255, 0);
            }
            loadBar.SetData(loadBarColor);
            gameDevice.GetResouceManager.LoadTexture("loadBar", loadBar);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            gameDevice.UnLoad();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            // TODO: Add your update logic here
            gameDevice.Update();
            sceneManager.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            renderer2D.Begin();
            GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            sceneManager.Draw(renderer2D);
            renderer2D.End();

            base.Draw(gameTime);
        }
    }
}
