//作成日：　2017.03.14
//作成者：　柏
//クラス内容：　GameObjectsの親クラス
//修正内容リスト：
//名前：　　　日付：　　　内容：
//名前：　　　日付：　　　内容：

using Microsoft.Xna.Framework;
using MyLib.Device;
using PinchSpring.Def;
using System.Collections.Generic;

namespace PinchSpring.Objects
{
    enum E_Tag {
        Player,
        Enemy,
        Block,
        JustView,
    }
    abstract class GameObject
    {
        protected Sound sound;
        protected GameDevice gameDevice;
        protected string name;
        protected int width;
        protected int height;
        protected int hp;
        protected E_Tag tag;
        protected Vector2 position; //画像の中心座標
        protected Vector2 velocity;
        protected Vector2 rocate;

        protected bool isLand;            //着陸状態かどうか
        protected bool isDead;
        protected ObjectsManager objectsManager;

        public GameObject(string name, GameDevice gameDevice, Vector2 position, Vector2 velocity, E_Tag tag, int width = Parameter.TileSize, int height = Parameter.TileSize) {
            this.name = name;
            this.gameDevice = gameDevice;
            this.position = position;
            this.velocity = velocity;
            this.tag = tag;
            this.width = width;
            this.height = height;
            isDead = false;
            isLand = false;

            sound = gameDevice.GetSound;
            rocate = Vector2.Zero;
        }

        /// <summary>
        /// 初期化
        /// </summary>
        public virtual void Initialize() { }

        /// <summary>
        /// シェーダー色初期化
        /// </summary>
        protected void InitializeColor() { }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="gameTime">時間</param>
        public virtual void Update(GameTime gameTime) {
            position += velocity;
        }

        /// <summary>
        /// GameObjectをコピー
        /// </summary>
        /// <returns>コピーされたObject</returns>
        public abstract object Clone();

        /// <summary>
        /// 当たり処理
        /// </summary>
        public virtual void Hit() {
            hp--;
            isDead = (hp <= 0);
            if (isDead) { InitializeColor(); }
        }

        /// <summary>
        /// 描画
        /// </summary>
        /// <param name="renderer2D">2D描画管理</param>
        /// <param name="cameraOffset">カメラ位置合わせ</param>
        public virtual void Draw(Renderer_2D renderer2D, Vector2 cameraOffset) {
            renderer2D.DrawTexture(name, position - Parameter.CenterOffset + cameraOffset);
        }

        /// <summary>
        /// チャラ管理設定
        /// </summary>
        /// <param name="charaManager">キャラ管理</param>
        public void SetObjectsManager(ObjectsManager objectsManager) {
            this.objectsManager = objectsManager;
        }

        /// <summary>
        /// 位置取得と設定
        /// </summary>
        public Vector2 Position {
            get { return position; }
            set { position = value; }
        }

        /// <summary>
        /// 速度取得と設定
        /// </summary>
        public Vector2 Velocity {
            get { return velocity; }
            set { velocity = value; }
        }

        /// <summary>
        /// Ｘ軸速度取得と設定
        /// </summary>
        public float VelocityX {
            get { return velocity.X; }
            set { velocity.X = value; }
        }

        /// <summary>
        /// Ｙ軸速度取得と設定
        /// </summary>
        public float VelocityY {
            get { return velocity.Y; }
            set { velocity.Y = value; }
        }

        /// <summary>
        /// 死ぬ状態をゲット
        /// </summary>
        public bool IsDead {
            get { return isDead; }
            set { isDead = value; }
        }

        /// <summary>
        /// 名前ゲット
        /// </summary>
        public string Name { get { return name; } }

        /// <summary>
        /// ＨＰの取得、設定
        /// </summary>
        public int Hp {
            get { return hp; }
            set { hp = value; }
        }

        /// <summary>
        /// タグのゲット
        /// </summary>
        public E_Tag Tag { get { return tag; } }

        public virtual float GetTop() { return position.Y; }
        public virtual float GetBottom() { return position.Y + height; }
        public virtual float GetLeft() { return position.X; }
        public virtual float GetRight() { return position.X + width; }

        /// <summary>
        /// objectのX座標を操作する
        /// </summary>
        public float PositionX {
            get { return position.X; }
            set { position.X = value; }
        }

        /// <summary>
        /// objectのY座標を操作する
        /// </summary>
        public float PositionY {
            get { return position.Y; }
            set { position.Y = value; }
        }

        /// <summary>
        /// objectの高さをゲット
        /// </summary>
        public int Height { get { return height; } }

        /// <summary>
        /// objectの幅をゲット
        /// </summary>
        public int Width { get { return width; } }

        /// <summary>
        /// 自分の当たる範囲を出す
        /// </summary>
        /// <param name="offsetX"></param>
        /// <param name="offsetY"></param>
        /// <returns></returns>
        public virtual Rectangle GetRect(float offsetX, float offsetY) {
            int x = (int)(position.X + offsetX);
            int y = (int)(position.Y + offsetY);
            Rectangle thisRect = new Rectangle(x, y, width, height);
            return thisRect;
        }

        protected void CollisionSide() {
            List<GameObject> list = objectsManager.GetCollitionListSquare(this);
            if (list.Count == 0) { return; }
            for (int i = 0; i < list.Count; i++) {
                CollisionSideOne(list[i]);
            }
        }

        private void CollisionSideOne(GameObject other) {
            if (VelocityX < 0) {
                CollisitionLeft(other);
            } else {
                CollisitionRight(other);
            }
        }
        private void CollisitionLeft(GameObject other) {
            bool isCollide = Collider.IsCollisionLeft(this, other);
            if (isCollide) {
                PositionX = other.PositionX + width;
                VelocityX = 0;
            }
        }

        private void CollisitionRight(GameObject other) {
            bool isCollide = Collider.IsCollisionRight(this, other);
            if (isCollide) {
                PositionX = other.PositionX - width;
                VelocityX = 0;
            }
        }

        public void CollisionDown() {
            List<GameObject> list = objectsManager.GetCollitionListSquare(this);
            if (list.Count == 0) { return; }
            for (int i = 0; i < list.Count; i++) {
                CollisionDownOne(list[i]);
                if (VelocityY == 0) { return; }
            }
        }

        private void CollisionDownOne(GameObject other) {
            bool isCollide = Collider.IsCollisionBottom(this, other);
            if (isCollide) {
                VelocityY = 0;
                PositionY = other.PositionY - height;
                isLand = true;
            }
        }

        public void CollisionUp() {
            List<GameObject> list = objectsManager.GetCollitionListSquare(this);
            if (list.Count == 0) { return; }
            for (int i = 0; i < list.Count; i++) {
                CollisionUpOne(list[i]);
                if (VelocityY == 0) { return; }
            }
        }

        private void CollisionUpOne(GameObject other) {
            bool isCollide = Collider.IsCollisionTop(this, other);
            if (isCollide) {
                VelocityY = 0;
                PositionY = other.PositionY + other.Height;
                isLand = true;
            }
        }

    }
}
