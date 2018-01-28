//作成日：　2017.03.15
//作成者：　柏
//クラス内容：　落下モーション
//修正内容リスト：
//名前：　　　日付：　　　内容：
//名前：　　　日付：　　　内容：

using Microsoft.Xna.Framework;
using MyLib.Device;
using PinchSpring.Def;
using System;

namespace PinchSpring.Objects.PlayerStates.States
{
    class Player_Fall : I_PlayerState
    {
        private Player player;
        private InputState inputState;
        public Player_Fall(Player player, GameDevice gameDevice)
        {
            this.player = player;
            inputState = gameDevice.GetInputState;
        }

        /// <summary>
        /// 更新
        /// </summary>
        public void Update() {
            Fall();
        }
        
        /// <summary>
        /// 落下処理
        /// </summary>
        private void Fall(){
            float unitSpeed = 0.3f;
            player.VelocityY += unitSpeed;
            for (float i = 0; i <= player.VelocityY; i += unitSpeed) {
                player.PositionY += unitSpeed;
                player.CollisionDown();
                player.InScreen();
            }
            if (player.IsLand) {
                player.Velocity = Vector2.Zero;
                ChangeNext();
            }
        }

        /// <summary>
        /// 次のパターンに移行
        /// </summary>
        public void ChangeNext(){
            player.StateType = E_StateType.Move;
        }

        /// <summary>
        /// エフェクト描画予定
        /// </summary>
        /// <param name="renderer2D">2D描画管理</param>
        public void Draw(Renderer_2D renderer2D)
        {

        }
    }
}
