﻿//作成日：　2017.03.14
//作成者：　柏
//クラス内容：　Endingシーン
//修正内容リスト：
//名前：　　　日付：　　　内容：
//名前：　　　日付：　　　内容：

using Microsoft.Xna.Framework;
using PinchSpring.Def;
using MyLib.Device;
using PinchSpring.Scene;

namespace PinchSpring.Scene.Scenes
{
    class Ending : IScene
    {
        private Renderer_2D renderer;
        private Sound sound;
        private InputState inputState;

        private bool isEnd;

        public Ending(GameDevice gameDevice) {
            renderer = gameDevice.GetRenderer;
            sound = gameDevice.GetSound;
            inputState = gameDevice.GetInputState;
            isEnd = false;
        }

        /// <summary>
        /// 初期化
        /// </summary>
        public void Initialize() { isEnd = false; }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="gameTime">時間</param>
        public void Update(GameTime gameTime) {
            sound.PlayBGM("gameover");
            if (inputState.WasDown(Parameter.ConfirmKey, Parameter.ConfirmButton)) {
                sound.PlaySE("bullet");
                isEnd = true;
            }
        }

        /// <summary>
        /// 描画
        /// </summary>
        /// <param name="renderer">2D描画管理</param>
        public void Draw(Renderer_2D renderer2D) {
            renderer2D.DrawTexture("Ending", Vector2.Zero);
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
