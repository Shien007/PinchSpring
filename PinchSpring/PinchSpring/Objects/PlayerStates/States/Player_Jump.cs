//作成日：　2017.03.14
//作成者：　柏
//クラス内容：　ジャンプモーション
//修正内容リスト：
//名前：　　　日付：　　　内容：
//名前：　　　日付：　　　内容：

using Microsoft.Xna.Framework;
using MyLib.Device;
using PinchSpring.Def;
using System;

namespace PinchSpring.Objects.PlayerStates.States
{
    class Player_Jump : I_PlayerState
    {
        private Player player;
        private InputState inputState;
        public Player_Jump(Player player, GameDevice gameDevice) {
            this.player = player;
            inputState = gameDevice.GetInputState;
        }

        /// <summary>
        /// 更新
        /// </summary>
        public void Update() {
            Jump();
        }

        private void Jump() {
            int maxVelocityX = 10;
            if (inputState.IsDown(Parameter.DownKey, Parameter.DownButton)) {
                float velocityY = player.VelocityY;
                
                //限界まで弾む速度を上げる
                player.VelocityY = velocityY <= -Parameter.ReboundMaxPower ? velocityY : velocityY - 0.5f;
                //playerの回転割合計算　0.3fは補間用（速度比率の3割）1は回転割合の初期値
                player.ReboundRadio = player.VelocityY / Parameter.ReboundMaxPower * 0.3f + 1;

                if (inputState.IsDown(Parameter.LeftKey, Parameter.LeftButton)) {
                    Rocate(-1); //左はマイナス
                }
                else if (inputState.IsDown(Parameter.RightKey, Parameter.RightButton)) {
                    Rocate(1);  //右はプラス
                }
            }
            if (inputState.IsUp(Parameter.DownKey, Parameter.DownButton)) {
                player.VelocityX = (float)Math.Cos(MathHelper.PiOver2 + player.Radiu) * player.VelocityY;
                player.VelocityX = player.VelocityX > maxVelocityX ? maxVelocityX : player.VelocityX;
                player.IsRocateBack = true;
                player.IsRebound = true;
                player.IsLand = false;
                player.ReboundRadio = 1;
                ChangeNext();
            }
        }

        /// <summary>
        /// プレーヤーを回す
        /// </summary>
        /// <param name="offset">方向offset（１：右、－１：左）</param>
        private void Rocate(int offset) {
            float radiu = player.Radiu;
            float maxRadiu = Parameter.RocateMaxRadiu * offset;
            float rocateSpeed = (float)Math.PI / 180 * offset;

            player.Radiu = radiu * offset < 0 ? radiu + rocateSpeed : radiu;
            radiu = player.Radiu;
            player.Radiu = Math.Abs(radiu) > Math.Abs(maxRadiu) ? maxRadiu : radiu + rocateSpeed;
            player.IsRocateBack = false;
        }

        /// <summary>
        /// 次のパターンに移行
        /// </summary>
        public void ChangeNext() {
            player.StateType = E_StateType.Rebound;
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
