//作成日：　2017.03.14
//作成者：　柏
//クラス内容：　Titleシーン
//修正内容リスト：
//名前：　　　日付：　　　内容：
//名前：　　　日付：　　　内容：

using Microsoft.Xna.Framework;
using MyLib.Device;

namespace PinchSpring.Scene.Scenes
{
    class Title : IScene
    {
        private Renderer_2D renderer2D;
        private Sound sound;
        private InputState inputState;
        private TitleSelect titleSelect;

        private bool isEnd;

        public Title(GameDevice gameDevice) {
            renderer2D = gameDevice.GetRenderer;
            sound = gameDevice.GetSound;
            inputState = gameDevice.GetInputState;
            titleSelect = new TitleSelect(gameDevice);
            isEnd = false;
            Initialize();
        }

        /// <summary>
        /// 初期化
        /// </summary>
        public void Initialize() {
            isEnd = false;
            titleSelect.Initialize();
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="gameTime">時間</param>
        public void Update(GameTime gameTime) {
            sound.PlayBGM("title");
            titleSelect.Update();
            isEnd = titleSelect.IsSelectEnd;
        }

        /// <summary>
        /// 描画
        /// </summary>
        /// <param name="renderer">2D描画管理</param>
        public void Draw(Renderer_2D renderer2D) {
            renderer2D.DrawTexture("Title", Vector2.Zero);
            titleSelect.Draw(renderer2D);
        }

        /// <summary>
        /// シーンを閉める
        /// </summary>
        public void Shutdown() { }

        /// <summary>
        /// 終わりフラッグを取得
        /// </summary>
        /// <returns>エンドフラッグ</returns>
        public bool IsEnd() { return isEnd; }

        /// <summary>
        /// 次のシーン
        /// </summary>
        /// <returns>シーンのEnum</returns>
        public E_Scene Next() {
            int select = titleSelect.GetSelect;
            switch (select) {
                case 0: return E_Scene.GAMEPLAY;     //return E_Scene.BOSSBATTLE;
                case 1: return E_Scene.OPERATE;
                case 2: return E_Scene.STAFFROLL;
                default: return E_Scene.GAMEPLAY;
            }
        }

    }
}
