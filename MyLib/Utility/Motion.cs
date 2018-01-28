//作成日：　2017.03.14
//作成者：　柏
//クラス内容：　2Dアニメーション描画クラス
//修正内容リスト：
//名前：　　　日付：　　　内容：
//名前：　　　日付：　　　内容：

using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace MyLib.Utility
{
    public class Motion
    {
        private Range range;
        private Timer timer;
        private int motionNumber;

        private Dictionary<int, Rectangle> rectangles = new Dictionary<int, Rectangle>();

        public Motion() {
            Initialize(new Range(0, 0), new Timer(0.5f));
        }

        public Motion(Range range, Timer timer) {
            Initialize(range, timer);
        }

        public void Initialize(Range range, Timer timer) {
            this.range = range;
            this.timer = timer;
            motionNumber = range.First();
        }

        public void Add(int index, Rectangle rect) {
            if (rectangles.ContainsKey(index)) {
                return;
            }
            rectangles.Add(index, rect);
        }

        private void MotionUpdate() {
            motionNumber += 1;
            if (range.IsOutOfRange(motionNumber)) {
                motionNumber = range.First();
            }
        }

        public void Update(GameTime gameTime) {
            if (range.IsOutOfRange()) { return; }
            timer.Update();
            if (timer.IsTime) {
                MotionUpdate();
                timer.Initialize();
            }
        }

        public Rectangle DrawingRange() {
            return rectangles[motionNumber];
        }

    }
}
