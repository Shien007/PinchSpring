//作成日：　2017.03.15
//作成者：　柏
//クラス内容：　落下モーション
//修正内容リスト：
//名前：　　　日付：　　　内容：
//名前：　　　日付：　　　内容：

using MyLib.Device;
using System;

namespace PinchSpring.Objects.PlayerStates.States
{
    class Player_Rebound : I_PlayerState
    {
        private Player player;
        private InputState inputState;
        public Player_Rebound(Player player, GameDevice gameDevice) {
            this.player = player;
            inputState = gameDevice.GetInputState;
        }

        /// <summary>
        /// 更新
        /// </summary>
        public void Update() {
            Rebound();
        }

        /// <summary>
        /// 弾む処理
        /// </summary>
        private void Rebound(){
            float unitSpeed = 0.3f;
            player.VelocityY += unitSpeed;
            for (float i = player.VelocityY; i <= 0; i += unitSpeed) {
                player.PositionY -= unitSpeed;
                player.CollisionUp();
                player.InScreen();
            }
            player.IsRebound = (player.VelocityY < 0);
            player.IsLand = player.IsRebound;
            if (!player.IsRebound) { ChangeNext(); }
        }

        /// <summary>
        /// 次のパターンに移行
        /// </summary>
        public void ChangeNext() {
            player.StateType = E_StateType.Fall;
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
