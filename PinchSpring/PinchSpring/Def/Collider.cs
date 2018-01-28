//作成日：　2017.03.14
//作成者：　柏
//クラス内容：　当たり判定クラス
//修正内容リスト：
//名前：柏　　日付：2017.4.8　　　内容：当たり判定改良
//名前：　　　日付：　　　内容：

using Microsoft.Xna.Framework;
using PinchSpring.Objects;

namespace PinchSpring.Def
{
    static class Collider
    {
        /// <summary>
        /// 正方形同士の左方向あたり判定
        /// </summary>
        /// <param name="thisObj">主動Object</param>
        /// <param name="other">対象Object</param>
        /// <returns></returns>
        public static bool IsCollisionLeft(GameObject thisObj, GameObject otherObj){
            if (thisObj.Tag == otherObj.Tag) { return false; }
            Rectangle otherRect = otherObj.GetRect(0, 0);
            Rectangle thisObjCheckRect  = new Rectangle(
                (int)thisObj.GetLeft() - 1,
                (int)thisObj.GetTop(),
                1,
                thisObj.Height);
            return IsCollision(thisObjCheckRect, otherRect);
        }

        /// <summary>
        /// 正方形同士の右方向あたり判定
        /// </summary>
        /// <param name="thisObj">主動Object</param>
        /// <param name="other">対象Object</param>
        /// <returns></returns>
        public static bool IsCollisionRight(GameObject thisObj, GameObject otherObj) {
            if (thisObj.Tag == otherObj.Tag) { return false; }
            Rectangle otherRect = otherObj.GetRect(0, 0);
            Rectangle thisObjCheckRect =  new Rectangle(
                    (int)thisObj.GetRight(),
                    (int)thisObj.GetTop(),
                    1,
                    thisObj.Height);
            return IsCollision(thisObjCheckRect, otherRect);
        }
        
        /// <summary>
        /// 正方形同士の上方向チェック
        /// </summary>
        /// <param name="thisObj">主動Object</param>
        /// <param name="otherObj">対象Object</param>
        /// <returns></returns>
        public static bool IsCollisionTop(GameObject thisObj, GameObject otherObj) {
            if (thisObj.Tag == otherObj.Tag) { return false; }
            Rectangle otherRect = otherObj.GetRect(0, 0);
            Rectangle thisObjCheckRect =  new Rectangle(
                (int)thisObj.GetLeft(),
                (int)thisObj.GetTop() - 1,
                thisObj.Width,
                1);
            return IsCollision(thisObjCheckRect, otherRect);
        }


        /// <summary>
        /// 正方形同士の下方向あたり判定
        /// </summary>
        /// <param name="thisObj">主動Object</param>
        /// <param name="otherObj">対象Object</param>
        /// <returns></returns>
        public static bool IsCollisionBottom(GameObject thisObj, GameObject otherObj) {
            if (thisObj.Tag == otherObj.Tag) { return false; }
            Rectangle otherRect = otherObj.GetRect(0, 0);
            Rectangle thisObjCheckRect = new Rectangle(
                (int)thisObj.GetLeft(),
                (int)thisObj.GetBottom(),
                thisObj.Width,
                1 );
            return IsCollision(thisObjCheckRect, otherRect);
        }

        /// <summary>
        /// 当たり判定(通用)
        /// </summary>
        /// <param name="other">判定対象</param>
        /// <returns></returns>
        public static bool IsCollision(Rectangle obj1, Rectangle obj2) {
            return obj1.Intersects(obj2);
        }
        
    }
}
