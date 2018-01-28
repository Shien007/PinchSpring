//作成日：　2017.03.14
//作成者：　柏
//クラス内容：　GamePlayシーン
//修正内容リスト：
//名前：　　　日付：　　　内容：
//名前：　　　日付：　　　内容：

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PinchSpring.Def;
using MyLib.Device;
using PinchSpring.Scene;
using PinchSpring.Objects;
using System;

namespace PinchSpring.Scene.Scenes
{
    class GamePlay : IScene
    {
        private GameDevice gameDevice;
        private Sound sound;
        private InputState inputState;


        private int stageNumber;
        private bool isEnd;

        private E_Scene next;
        private GraphicsDevice graphicsDevice;
        private ObjectsManager objectsManager;
        private Stage stage;
        private Camera camera;

        public GamePlay(GameDevice gamedevice, GraphicsDevice graphicsDevice) {
            gameDevice = gamedevice;
            sound = gameDevice.GetSound;
            inputState = gameDevice.GetInputState;
            this.graphicsDevice = graphicsDevice;
            objectsManager = new ObjectsManager(gamedevice, graphicsDevice);
            stage = new Stage(gamedevice, graphicsDevice);
            stage.Load("TestStage");
            objectsManager.AddIntMatrix(Stage.MapData);
            objectsManager.SetPlayer(GetPlayer());
            camera = new Camera(stage);
            stageNumber = 1;
            
        }

        public Player GetPlayer() {
            GameObject[,] data = Stage.MapData;
            for (int num = 0; num < data.GetLength(0) * data.GetLength(1); num++) {
                int lineCount = num / data.GetLength(1);
                int colCount = num % data.GetLength(1);
                if (data[lineCount, colCount] is Player) {
                    return (Player)data[lineCount, colCount];
                }
            }
            return null;
        }

        /// <summary>
        /// 初期化
        /// </summary>
        public void Initialize() {
            stage.Load("TestStage");
            isEnd = false;
            objectsManager.Initialize();
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="gameTime">時間</param>
        public void Update(GameTime gameTime) {
            if (inputState.WasDown(Parameter.ConfirmKey, Parameter.ConfirmButton)) {
                next = E_Scene.ENDING;
                isEnd = true;
            }
            if (objectsManager.IsStageClear()) {
                next = E_Scene.CLEAR;
                isEnd = true;
            }
            objectsManager.Update(gameTime);
            sound.PlayBGM("StageBGM");
            camera.NextViewPort(objectsManager.GetPlayer().Position);
            //sound.PlayBGM("stage" + stageNumber);
        }

        /// <summary>
        /// 描画
        /// </summary>
        /// <param name="renderer">2D描画管理</param>
        public void Draw(Renderer_2D renderer) {
            objectsManager.Draw(renderer, camera.GetIsMoved_P); //Vector2.Zero
        }
        
        /// <summary>
        /// シーンを閉める
        /// </summary>
        public void Shutdown() {
            stage.Unload();
        }

        /// <summary>
        /// 終わりフラッグを取得
        /// </summary>
        /// <returns>エンドフラッグ</returns>
        public bool IsEnd() { return isEnd; }

        /// <summary>
        /// 次のシーン
        /// </summary>
        /// <returns>シーンのEnum</returns>
        public E_Scene Next() { return next;  }
    }
}
