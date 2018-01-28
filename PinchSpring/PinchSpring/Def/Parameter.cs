//作成日：　2017.03.14
//作成者：　柏
//クラス内容：　常数管理クラス(CSV管理予定)
//修正内容リスト：
//名前：　　　日付：　　　内容：
//名前：　　　日付：　　　内容：

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace PinchSpring.Def
{
    static class Parameter
    {
        public const int ScreenWidth = 1024;
        public const int ScreenHeight = 768;

        public const int TileSize = 64;

        public const int PlayerMaxHp = 3;
        public const int PlayerSpeed = 5;
        public const int PlayerWidth = TileSize;
        public const int PlayerHeight = TileSize * 2;

        public const int ReboundMaxPower = 15;
        public const float RocateMaxRadiu = (float)Math.PI / 6;

        //中心座標変換差分
        public static Vector2 CenterOffset = new Vector2(TileSize / 2, TileSize / 2);

        //キーボード
        public static Keys LeftKey = Keys.Left;
        public static Keys RightKey = Keys.Right;
        public static Keys UpKey = Keys.Up;
        public static Keys DownKey = Keys.Down;

        public static Keys ChangeKey = Keys.Space;
        public static Keys ConfirmKey = Keys.Enter;

        //コントローラー
        public static Buttons LeftButton = Buttons.LeftThumbstickLeft;
        public static Buttons RightButton = Buttons.LeftThumbstickRight;
        public static Buttons UpButton = Buttons.LeftThumbstickUp;
        public static Buttons DownButton = Buttons.LeftThumbstickDown;

        public static Buttons ChangeButton = Buttons.RightShoulder;
        public static Buttons ConfirmButton = Buttons.A;

        public static readonly Vector2 StartText = new Vector2(380, 420);
        public static readonly Vector2 OperateText = new Vector2(380, 500);
        public static readonly Vector2 StaffText = new Vector2(380, 580);
        public static readonly Vector2 SelectionOffset = new Vector2(-60, 15);
    }
}
