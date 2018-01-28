//作成日：　2017.03.14
//作成者：　柏
//クラス内容：　Operateシーン
//修正内容リスト：
//名前：　　　日付：　　　内容：
//名前：　　　日付：　　　内容：

using Microsoft.Xna.Framework;
using PinchSpring.Def;
using MyLib.Device;

namespace PinchSpring.Scene.Scenes
{
    class Operate : IScene
    {
        private Renderer_2D renderer2D;
        private Sound sound;
        private InputState inputState;
        private int page;

        private bool isEnd;

        public Operate(GameDevice gameDevice)
        {
            renderer2D = gameDevice.GetRenderer;
            sound = gameDevice.GetSound;
            inputState = gameDevice.GetInputState;
            page = 1;
            isEnd = false;
        }

        /// <summary>
        /// 初期化
        /// </summary>
        public void Initialize() {
            page = 1;
            isEnd = false;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="gameTime">ゲームタイム</param>
        public void Update(GameTime gameTime) {
            sound.PlayBGM("operate");
            if (inputState.WasDown(Parameter.ConfirmKey, Parameter.ConfirmButton)) {
                page++;
                isEnd = page > 2;
                sound.PlaySE("bullet");
            }
        }

        /// <summary>
        /// 描画
        /// </summary>
        /// <param name="renderer">2D描画管理</param>
        public void Draw(Renderer_2D renderer2D) {
            if (page == 1) { renderer2D.DrawTexture("Operate1", Vector2.Zero); }
            else { renderer2D.DrawTexture("Operate2", Vector2.Zero); }
        }

        /// <summary>
        /// シーンを閉める
        /// </summary>
        public void Shutdown() { Initialize(); }

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
            return E_Scene.TITLE;
        }
    }
}
