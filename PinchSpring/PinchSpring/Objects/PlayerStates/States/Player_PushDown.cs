//作成日：　2017.03.14
//作成者：　柏
//クラス内容：　倒れるモーション
//修正内容リスト：
//名前：　　　日付：　　　内容：
//名前：　　　日付：　　　内容：

using MyLib.Device;
using MyLib.Utility;

namespace PinchSpring.Objects.PlayerStates.States
{
    class Player_PushDown : I_PlayerState
    {
        private Player player;
        private InputState inputState;
        public Player_PushDown(Player player, GameDevice gameDevice) {
            this.player = player;
            inputState = gameDevice.GetInputState;
        }

        /// <summary>
        /// 更新
        /// </summary>
        public void Update() {
            //ChangeNext();
        }

        /// <summary>
        /// 次のパターンに移行
        /// </summary>
        public void ChangeNext() {
            player.StateType = E_StateType.Jump;
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
