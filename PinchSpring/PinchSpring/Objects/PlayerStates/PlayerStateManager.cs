//作成日：　2017.03.14
//作成者：　柏
//クラス内容：　Playerのモーション管理クラス
//修正内容リスト：
//名前：　　　日付：　　　内容：
//名前：　　　日付：　　　内容：

using MyLib.Device;
using PinchSpring.Objects.PlayerStates.States;
using System.Collections.Generic;

namespace PinchSpring.Objects.PlayerStates
{
    enum E_StateType
    {
        Move,
        Jump,
        Rebound,
        Fall,
        PushDown,
        StandUp,
        None,
    }
    class PlayerStateManager
    {
        private Player player;
        private List<I_PlayerState> stateList;
        private GameDevice gameDevice;

        public PlayerStateManager(Player player, GameDevice gameDevice) {
            this.player = player;
            this.gameDevice = gameDevice;
            Initialize();
        }

        /// <summary>
        /// ステート登録
        /// </summary>
        public void Initialize() {
            stateList = new List<I_PlayerState>() {
                { new Player_Move(player, gameDevice)},
                { new Player_Jump(player, gameDevice)},
                { new Player_Rebound(player, gameDevice)},
                { new Player_Fall(player, gameDevice)},
                { new Player_PushDown(player, gameDevice)},
                { new Player_StandUp(player, gameDevice)},
            };
        }

        /// <summary>
        /// ステート更新
        /// </summary>
        public void Update() {
            stateList[(int)player.StateType].Update();
        }

        /// <summary>
        /// 描画
        /// </summary>
        /// <param name="renderer2D">2D描画管理</param>
        public void Draw(Renderer_2D renderer2D) {
            
        }
    }
}
