//作成日：　2017.03.14
//作成者：　柏
//クラス内容：　ブロッククラス
//修正内容リスト：
//名前：　　　日付：　　　内容：
//名前：　　　日付：　　　内容：

using Microsoft.Xna.Framework;
using MyLib.Device;
using PinchSpring.Def;

namespace PinchSpring.Objects
{
    class Block : GameObject
    {
        public Block(string name, GameDevice gameDevice, Vector2 position)
            : base(name, gameDevice, position, Vector2.Zero, E_Tag.Block)
        {
            Initialize();
        }

        /// <summary>
        /// 初期化
        /// </summary>
        public override void Initialize()
        { }

        /// <summary>
        /// コピー用コンストラクタ
        /// </summary>
        /// <param name="other">コピー対象</param>
        public Block(Block other)
            : this(other.name, other.gameDevice, other.position)
        { }

        /// <summary>
        /// Blockをコピー
        /// </summary>
        /// <returns>コピーされたBlock</returns>
        public override object Clone() {
            return new Block(this);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="gameTime">時間</param>
        public override void Update(GameTime gameTime){ }

        /// <summary>
        /// 当たり処理
        /// </summary>
        public override void Hit(){ }

    }
}
