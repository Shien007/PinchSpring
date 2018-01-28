//作成日：　2017.03.14
//作成者：　柏
//クラス内容：　Playerクラス
//修正内容リスト：
//名前：柏　　日付：2017.4.8　　　内容：当たり判定改良
//名前：　　　日付：　　　内容：

using MyLib.Device;
using MyLib.Utility;
using PinchSpring.Def;
using PinchSpring.Objects.PlayerStates;
using PinchSpring.Scene;

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PinchSpring.Objects
{
    enum E_Direction {
        UP,
        DOWN,
        LEFT,
        RIGHT,
        NONE_LR,
        NONE_UD,
    }

    class Player : GameObject
    {
        private InputState inputState;
        private GraphicsDevice graphicsDevice;
        private bool isClear;
        private Timer rocateTimer;      //回転は何フレーム一回を設定
        private E_StateType stateType;
        private PlayerStateManager stateManager;
        private float radiu;            //回転孤度
        private bool isRebound;         //弾む状態かどうか
        private bool isRocateBack;      //回転した分を戻すかどうか
        private float reboundRadio;     //圧縮の割合

        private Spring spring;

        public Player(GameDevice gameDevice, Vector2 position, GraphicsDevice graphicsDevice)
            : base("player", gameDevice, position, Vector2.Zero, E_Tag.Player, Parameter.TileSize, Parameter.TileSize * 2)
        {
            inputState = gameDevice.GetInputState;
            this.graphicsDevice = graphicsDevice;
            stateManager = new PlayerStateManager(this, gameDevice);
            spring = new Spring(gameDevice, position);
            radiu = 0;
            Initialize();
        }

        /// <summary>
        /// 初期化
        /// </summary>
        public override void Initialize() {
            isClear = false;
            isLand = false;
            isRebound = false;
            isRocateBack = false;
            rocateTimer = new Timer(0.01f);
            stateType = E_StateType.Fall;
            hp = Parameter.PlayerMaxHp;
            position = new Vector2(300, 200);
            velocity = Vector2.Zero;
            reboundRadio = 1;

            base.Initialize();
        }



        /// <summary>
        /// コピー用コンストラクタ
        /// </summary>
        /// <param name="other">コピー対象</param>
        public Player(Player other)
            : this(other.gameDevice, other.position, other.graphicsDevice)
        { }

        /// <summary>
        /// Playerをコピー
        /// </summary>
        /// <returns>コピーされたBlock</returns>
        public override object Clone() {
            return new Player(this);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="gameTime">時間</param>
        public override void Update(GameTime gameTime) {
            stateManager.Update();
            MoveSide();
            Rocation();
            spring.Position = position;
            spring.Update(gameTime);
            //2はステーぎの一番右から2番目のブロックの位置を指している
            isClear = position.X >= (Stage.Map_XMax - 2) * Parameter.TileSize;
        }

        /// <summary>
        /// 横移動
        /// </summary>
        private void MoveSide() {
            float speedX =  VelocityX * Parameter.PlayerSpeed;
            for (float i = 0; i < Math.Abs(speedX); i++) {
                MoveSideOne();
            }
        }

        /// <summary>
        /// 横の単位移動
        /// </summary>
        private void MoveSideOne() {
            VelocityX *= 0.99f;    //速度補間、CSVで管理する予定
            if (velocity.Length() <= 0.2f) { velocity = Vector2.Zero; } //速度の初期化
            CollisionSide();
            if (Math.Abs(VelocityX) >= 1) {
                PositionX += (VelocityX / Math.Abs(VelocityX));
            }
            else if (Math.Abs(VelocityX) >= 0.2f) {
                PositionX += VelocityX;
            }
            InScreen();
        }

        /// <summary>
        /// 移動できる範囲内に抑える
        /// </summary>
        public void InScreen() {
            Vector2 minPosition = Vector2.Zero;
            //プレーヤーのサイズは64*128(tilesize * tilesize*2)のため
            Vector2 maxPosition = new Vector2(
                (Stage.Map_XMax - 1) * Parameter.TileSize, 
                (Stage.Map_YMax - 2) * Parameter.TileSize);
            position = Vector2.Clamp(position, minPosition, maxPosition);
        }
        
        public void IsLandCheck() {
            if (isRebound) { return; }
            isLand = false;
            List<GameObject> list = objectsManager.GetCollitionListSquare(this);
            for (int i = 0; i < list.Count; i++) {
                isLand = isLand ? isLand : Collider.IsCollisionBottom(this, list[i]);
            }
        }

        /// <summary>
        /// 回転する処理
        /// </summary>
        private void Rocation() {
            if (!isRocateBack) { return; }
            radiu *= 0.93f; //孤度補間、CSVで管理する予定
            radiu = Math.Abs(radiu) <= 0.01f ? 0 : radiu;   //孤度の初期化
        }

        /// <summary>
        /// 当たり処理
        /// </summary>
        public override void Hit() {
            base.Hit();
            sound.PlaySE("fire");
        }

        /// <summary>
        /// 描画
        /// </summary>
        /// <param name="renderer2D">2D描画管理</param>
        /// <param name="cameraOffset">カメラ位置合わせ</param>
        public override void Draw(Renderer_2D renderer2D, Vector2 cameraOffset) {
            Vector2 drawPosition = position - Parameter.CenterOffset + cameraOffset;

            //プレーヤーを射出する初期孤度はπ/2のため(角度の合わせて単位円上の座標計算)
            Vector2 radiuOffsetBase = new Vector2(
                (float)Math.Cos(radiu + MathHelper.PiOver2),
                (float)Math.Sin(radiu + MathHelper.PiOver2)
                );
            int size = Parameter.TileSize;

            //バネの描画
            spring.SetRadiu(radiu);
            spring.SetReboundRadio(reboundRadio);
            spring.Draw(renderer2D, cameraOffset);

            //上部分の描画座標計算(公転の円の中心に戻してから計算)
            Vector2 radiuOffsetUp   = drawPosition + new Vector2(0, height) - radiuOffsetBase * height * reboundRadio;
            //下部分の描画座標計算(公転の円の中心に戻してから計算)
            Vector2 radiuOffsetDown = drawPosition + new Vector2(0, height) - radiuOffsetBase * size;

            //上の部分を描画する(自転は真ん中中心なのでOffset)
            renderer2D.DrawTexture("player1",
                radiuOffsetUp + Parameter.CenterOffset, 1,
                new Rectangle(0, 0, size, size), Vector2.One,
                radiu, Parameter.CenterOffset);
            //下の部分を描画する(自転は真ん中中心なのでOffset)
            renderer2D.DrawTexture("player1",
                radiuOffsetDown + Parameter.CenterOffset, 1,
                new Rectangle(0, 0, size, size), Vector2.One,
                radiu + (float)Math.PI, Parameter.CenterOffset);
        }

        /// <summary>
        /// ステージクリア状態取得
        /// </summary>
        /// <returns></returns>
        public bool IsClear() { return isClear; }

        /// <summary>
        /// 現在モーションゲット
        /// </summary>
        public E_StateType StateType {
            get { return stateType; }
            set { stateType = value; }
        }

        /// <summary>
        /// 現在の弾む状態設定
        /// </summary>
        public bool IsRebound {
            get { return isRebound; }
            set { isRebound = value; }
        }

        /// <summary>
        /// 現在の着陸状態設定
        /// </summary>
        public bool IsLand {
            get { return isLand; }
            set { isLand = value; }
        }

        /// <summary>
        /// 孤度設定
        /// </summary>
        public float Radiu {
            get { return radiu; }
            set { radiu = value; }
        }

        /// <summary>
        /// 弾む角度の割合設定
        /// </summary>
        public float ReboundRadio {
            get { return reboundRadio; }
            set { reboundRadio = value; }
        }

        /// <summary>
        /// 回った分を戻すかどうかを出す
        /// </summary>
        public bool IsRocateBack {
            get { return isRocateBack; }
            set { isRocateBack = value; }
        }
    }
}
