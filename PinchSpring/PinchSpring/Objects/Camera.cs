//作成日：　2017.03.14
//作成者：　柏
//クラス内容：　Cameraクラス
//修正内容リスト：
//名前：　　　日付：　　　内容：
//名前：　　　日付：　　　内容：

using Microsoft.Xna.Framework;
using MyLib.Utility;
using PinchSpring.Def;
using PinchSpring.Scene;
using System;
using System.Collections.Generic;

namespace PinchSpring.Objects
{
    enum ShackDirection
    {
        Left,
        Right,
        Up,
        Down,
        LeftUp,
        RightUp,
        LeftDown,
        RightDown,
        None
    }

    class Camera
    {
        private Vector2 isMoved_P;
        private Stage stage;

        public Camera(Stage stage) { this.stage = stage; }

        public void SetStage(Stage stage) { this.stage = stage; }

        public void NextViewPort(Vector2 viewPort) {
            isMoved_P.X = (Parameter.ScreenWidth - Parameter.TileSize) / 2 - viewPort.X;
            isMoved_P.Y = (Parameter.ScreenHeight - Parameter.TileSize) / 2 - viewPort.Y;
            CheckCamera(viewPort);    //範囲内に収める
        }

        /// <summary>
        /// 映っている範囲の画面外チェック
        /// </summary>
        /// <param name="viewPort">映リたい画面中心</param>
        private void CheckCamera(Vector2 viewPort) {
            Vector2 stageScale = Stage.GetStageScale();
            Vector2 centerOffset = Parameter.CenterOffset;
            Vector2 viewLT = viewPort - new Vector2((Parameter.ScreenWidth - Parameter.TileSize), (Parameter.ScreenHeight - Parameter.TileSize)) / 2;
            Vector2 viewRB = viewPort + new Vector2((Parameter.ScreenWidth + Parameter.TileSize), (Parameter.ScreenHeight + Parameter.TileSize)) / 2;

            //Left
            if (viewLT.X < -centerOffset.X) {
                isMoved_P.X = centerOffset.X;
            }
            //Top
            if (viewLT.Y < -centerOffset.Y) {
                isMoved_P.Y = centerOffset.Y;
            }
            //Right
            if (viewRB.X > stageScale.X - centerOffset.X) {
                isMoved_P.X = Parameter.ScreenWidth - stageScale.X + centerOffset.X;
            }
            //Bottom
            if (viewRB.Y > stageScale.Y - centerOffset.Y) {
                isMoved_P.Y = Parameter.ScreenHeight - stageScale.Y + centerOffset.Y;
            }
        }

        public Vector2 GetIsMoved_P {
            get { return isMoved_P; }
        }

    }
}
