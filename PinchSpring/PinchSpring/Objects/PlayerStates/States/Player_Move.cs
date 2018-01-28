//作成日：　2017.03.14
//作成者：　柏
//クラス内容：　移動モーション
//修正内容リスト：
//名前：　　　日付：　　　内容：
//名前：　　　日付：　　　内容：

using MyLib.Device;
using PinchSpring.Def;

namespace PinchSpring.Objects.PlayerStates.States
{
    class Player_Move : I_PlayerState
    {
        private Player player;
        private InputState inputState;
        private E_StateType nextStateType;
        public Player_Move(Player player, GameDevice gameDevice) {
            this.player = player;
            inputState = gameDevice.GetInputState;
            nextStateType = E_StateType.None;
        }

        /// <summary>
        /// 更新
        /// </summary>
        public void Update() {
            MoveSideCheck();
            ChangeNext();
        }

        /// <summary>
        /// 左右移動チェック
        /// </summary>
        private void MoveSideCheck() {
            player.IsLandCheck();
            if (!player.IsLand) {
                nextStateType = E_StateType.Fall;
                return;
            }
            float unitSpeed = 1;
            if (inputState.IsDown(Parameter.DownKey, Parameter.DownButton)) {
                nextStateType = E_StateType.Jump;
                return;
            }
            if (inputState.IsDown(Parameter.LeftKey, Parameter.LeftButton)) {
                player.VelocityX = -unitSpeed;
            }
            else if (inputState.IsDown(Parameter.RightKey, Parameter.RightButton)) {
                player.VelocityX = unitSpeed;
            }
        }

        /// <summary>
        /// 次のパターンに移行
        /// </summary>
        public void ChangeNext() {
            if (nextStateType == E_StateType.None) { return; }
            player.StateType = nextStateType;
            nextStateType = E_StateType.None;
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
