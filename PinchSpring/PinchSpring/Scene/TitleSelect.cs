//作成日：　2017.03.14
//作成者：　柏
//クラス内容：　タイトル選択
//修正内容リスト：
//名前：　　　日付：　　　内容：
//名前：　　　日付：　　　内容：

using System;
using Microsoft.Xna.Framework;
using PinchSpring.Def;
using MyLib.Device;

namespace PinchSpring.Scene
{
    enum E_Selection {
        Start = 0,
        Operate,
        StaffRoll,
    }

    class TitleSelect
    {
        private InputState inputState;
        private Sound sound;
        private E_Selection select;
        private float velocity;     //揺れるスピード

        private Vector2 startText;
        private Vector2 operateText;
        private Vector2 staffText;
        private Vector2 beam1;      //ビームの射出する座標（選択肢１）
        private Vector2 beam2;      //ビームの射出する座標（選択肢２）
        private Vector2 beam3;      //ビームの射出する座標（選択肢３）

        private bool isDown;        //選択肢を揺れる（下方向かどうか）
        private bool isSelect;
        private bool selectEnd;     //選択終わりかどうか

        public TitleSelect(GameDevice gameDevice) {
            inputState = gameDevice.GetInputState;
            sound = gameDevice.GetSound;
            Initialize();
        }

        /// <summary>
        /// 初期化
        /// </summary>
        public void Initialize() {
            select = E_Selection.Start;
            isDown = true;
            isSelect = false;
            selectEnd = false;
            velocity = 0;

            startText = Parameter.StartText;
            operateText = Parameter.OperateText;
            staffText = Parameter.StaffText;
            beam1 = startText + Parameter.SelectionOffset;
            beam2 = operateText + Parameter.SelectionOffset;
            beam3 = staffText + Parameter.SelectionOffset;
        }

        /// <summary>
        /// 更新
        /// </summary>
        public void Update() {
            Select();   //選択する
            Move();     //選択肢によって選択肢を揺れる
            ToNext();   //選択肢によってbeamを射出する処理
        }

        /// <summary>
        /// 選択肢揺れる
        /// </summary>
        private void Move() {
            float unitSpeed = 0.5f;
            velocity += isDown ? unitSpeed : -unitSpeed;
            if (Math.Abs(velocity) > 2) { isDown = !isDown; }

            switch (select) {
                case E_Selection.Start:
                    startText.Y += velocity;

                    operateText = Parameter.OperateText;
                    staffText = Parameter.StaffText;
                    break;
                case E_Selection.Operate:
                    operateText.Y += velocity;

                    startText = Parameter.StartText;
                    staffText = Parameter.StaffText;
                    break;
                case E_Selection.StaffRoll:
                    staffText.Y += velocity;

                    startText = Parameter.StartText;
                    operateText = Parameter.OperateText;
                    break;
            }
        }

        /// <summary>
        /// 選択肢を選ぶ
        /// </summary>
        private void Select() {
            if (isSelect) { return; }
            if (inputState.WasDown(Parameter.DownKey, Parameter.DownButton)) {
                SelectProcess(1);   //下は１
            }
            else if (inputState.WasDown(Parameter.UpKey, Parameter.UpButton)) {
                SelectProcess(-1);   //上は-１
            }
            else if (inputState.WasDown(Parameter.ConfirmKey, Parameter.ConfirmButton)) {
                isSelect = true;
                sound.PlaySE("beam");
            }
        }

        /// <summary>
        /// 選択肢の処理
        /// </summary>
        /// <param name="offset">上下offset（１：下、－１：上）</param>
        private void SelectProcess(int offset) {
            int selection = (int)select;
            int maxSelection = 3;
            selection = (selection + offset + maxSelection) % (maxSelection);
            select = (E_Selection)selection;
            sound.PlaySE("enemyDead");
        }


        /// <summary>
        /// 選択肢を決める
        /// </summary>
        private void ToNext() {
            if (!isSelect) { return; }
            int beamSpeed = 12;
            int endDistance = 640;
            switch (select) {
                case E_Selection.Start:
                    beam1.X += beamSpeed;
                    break;
                case E_Selection.Operate:
                    beam2.X += beamSpeed;
                    break;
                case E_Selection.StaffRoll:
                    beam3.X += beamSpeed;
                    break;
            }
            //演出終わると、次のシーンに行く(ビームの移動距離チェック)
            selectEnd = (beam1.X + beam2.X + beam3.X - 2 * Parameter.StaffText.X) > endDistance;
        }

        /// <summary>
        /// 選択された選択肢を出す
        /// </summary>
        public int GetSelect { get { return (int)select; } }

        /// <summary>
        /// 選択終わりのフラッグを出す
        /// </summary>
        public bool IsSelectEnd { get { return selectEnd; } }

        /// <summary>
        /// 描画
        /// </summary>
        /// <param name="renderer">2D描画管理</param>
        public void Draw(Renderer_2D renderer2D) {
            renderer2D.DrawTexture("startText", startText);
            renderer2D.DrawTexture("operateText", operateText);
            renderer2D.DrawTexture("staffText", staffText);
            //選択肢によって、beamを描画する
            switch (select) {
                case E_Selection.Start:
                    renderer2D.DrawTexture("beam", beam1);
                    break;
                case E_Selection.Operate:
                    renderer2D.DrawTexture("beam", beam2);
                    break;
                case E_Selection.StaffRoll:
                    renderer2D.DrawTexture("beam", beam3);
                    break;
            }
        }
    }
}
