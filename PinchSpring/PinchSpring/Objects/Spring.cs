//作成日：　2017.03.14
//作成者：　柏
//クラス内容：　ばねのクラス
//修正内容リスト：
//名前：　　　日付：　　　内容：
//名前：　　　日付：　　　内容：

using Microsoft.Xna.Framework;
using MyLib.Device;
using PinchSpring.Def;
using System;

namespace PinchSpring.Objects
{
    class Spring : GameObject
    {
        private float radiu;            //回転孤度
        private float reboundRadio;     //圧縮の割合

        public Spring(GameDevice gameDevice, Vector2 position)
            : base("spring", gameDevice, position, Vector2.Zero, E_Tag.JustView) {
            radiu = 0;
        }

        /// <summary>
        /// コピー用コンストラクタ
        /// </summary>
        /// <param name="other">コピー対象</param>
        public Spring(Spring other)
            : this(other.gameDevice, other.position)
        { }

        /// <summary>
        /// Springをコピー
        /// </summary>
        /// <returns>コピーされたSpring</returns>
        public override object Clone() {
            return new Spring(this);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="gameTime">時間</param>
        public override void Update(GameTime gameTime) {

        }

        /// <summary>
        /// 描画
        /// </summary>
        /// <param name="renderer2D">2D描画管理</param>
        /// <param name="cameraOffset">カメラ位置合わせ</param>
        public override void Draw(Renderer_2D renderer2D, Vector2 cameraOffset) {
            //プレーヤーを射出する初期孤度はπ/2のため(角度の合わせて単位円上の座標計算)
            Vector2 radiuOffsetBase = new Vector2(
               (float)Math.Cos(radiu + MathHelper.PiOver2),
               (float)Math.Sin(radiu + MathHelper.PiOver2)
               );
            int size = Parameter.TileSize;
            Vector2 drawPosition = position + cameraOffset + new Vector2(0, size / 2);

            //描画座標計算(回す + 弾む)
            Vector2 radiuOffset = drawPosition + new Vector2(0, size * 1.5f) - radiuOffsetBase *( size + size / 2 * reboundRadio);

            renderer2D.DrawTexture(name, radiuOffset, 1,
                new Rectangle(0, 0, size, size), new Vector2(1, reboundRadio),
                radiu, Parameter.CenterOffset);
        }

        /// <summary>
        /// 当たり処理
        /// </summary>
        public override void Hit() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="radiu"></param>
        public void SetRadiu(float radiu) {
            this.radiu = radiu;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reboundRadio"></param>
        public void SetReboundRadio(float reboundRadio) {
            this.reboundRadio = reboundRadio * reboundRadio;
        }
    }
}
