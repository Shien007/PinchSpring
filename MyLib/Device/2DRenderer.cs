//作成日：　2017.03.14
//作成者：　柏
//クラス内容：　2D描画管理クラス
//修正内容リスト：
//名前：　　　日付：　　　内容：
//名前：　　　日付：　　　内容：

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyLib.Device
{
    public class Renderer_2D
    {
        private ContentManager contentManager;
        private GraphicsDevice graphicsDevice;
        private SpriteBatch spriteBatch;

        public Renderer_2D(ContentManager content, GraphicsDevice graphics) {
            contentManager = content;
            graphicsDevice = graphics;
            spriteBatch = new SpriteBatch(graphicsDevice);
        }

        /// <summary>
        /// 描画始め
        /// </summary>
        public void Begin() {
            spriteBatch.Begin();
        }

        /// <summary>
        /// 描画終わり
        /// </summary>
        public void End() {
            spriteBatch.End();
        }

        /// <summary>
        /// 2D画像描画（通常）
        /// </summary>
        /// <param name="name">アセット名</param>
        /// <param name="position">位置</param>
        public void DrawTexture(string name, Vector2 position) {
            Texture2D texture = ResouceManager.GetTexture(name);
            spriteBatch.Draw(texture, position, Color.White);
        }

        /// <summary>
        /// 2D画像描画（透明度あり）
        /// </summary>
        /// <param name="name">アセット名</param>
        /// <param name="position">位置</param>
        /// <param name="alpha">透明度</param>
        public void DrawTexture(string name, Vector2 position, float alpha = 1.0f) {
            Texture2D texture = ResouceManager.GetTexture(name);
            spriteBatch.Draw(texture, position, Color.White * alpha);
        }

        /// <summary>
        /// 2D画像描画（色と大きさの設定あり）
        /// </summary>
        /// <param name="name">アセット名</param>
        /// <param name="position">位置</param>
        /// <param name="color">色</param>
        /// <param name="scale">大きさ</param>
        public void DrawTexture(string name, Vector2 position, Color color, Vector2 scale) {
            Texture2D texture = ResouceManager.GetTexture(name);
            spriteBatch.Draw(texture, position, new Rectangle(0, 0, ResouceManager.GetTextureWidth(name), ResouceManager.GetTextureHeight(name)), color, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
        }

        /// <summary>
        /// 2D画像描画（リソースの描画範囲設定あり）
        /// </summary>
        /// <param name="name">アセット名</param>
        /// <param name="position">位置</param>
        /// <param name="rect">描画範囲</param>
        public void DrawTexture(string name, Vector2 position, Rectangle rect) {
            Texture2D texture = ResouceManager.GetTexture(name);
            spriteBatch.Draw(texture, position, rect, Color.White);
        }

        /// <summary>
        /// 2D画像描画（リソースの描画範囲と透明度の設定あり）
        /// </summary>
        /// <param name="name">アセット名</param>
        /// <param name="position">位置</param>
        /// <param name="rect">描画範囲</param>
        /// <param name="alpha">透明度</param>
        public void DrawTexture(string name, Vector2 position, Rectangle rect, float alpha = 1.0f) {
            Texture2D texture = ResouceManager.GetTexture(name);
            spriteBatch.Draw(texture, position, rect, Color.White * alpha);
        }

        /// <summary>
        /// 2D画像描画(よく使う全設定あり)
        /// </summary>
        /// <param name="name">アセット名</param>
        /// <param name="position">位置</param>
        /// <param name="alpha">透明度</param>
        /// <param name="rect">描画範囲</param>
        /// <param name="scale">大きさ</param>
        /// <param name="rocate">回転孤度</param>
        /// <param name="origin">回転中心</param>
        public void DrawTexture(string name, Vector2 position, float alpha, Rectangle rect, Vector2 scale, float rocate, Vector2 origin) {
            Texture2D texture = ResouceManager.GetTexture(name);
            spriteBatch.Draw(texture, position, rect, Color.White * alpha, rocate, origin, scale, SpriteEffects.None,1.0f);
        }

        /// <summary>
        /// 数字描画
        /// </summary>
        /// <param name="name">アセット名</param>
        /// <param name="position">位置</param>
        /// <param name="num">数字</param>
        /// <param name="scale">大きさ</param>
        public void DrawNumber(string name, Vector2 position, int num, float scale = 1f) {
            Texture2D texture = ResouceManager.GetTexture(name);
            foreach (var n in num.ToString()) {
                spriteBatch.Draw(texture, position,
                    new Rectangle((n - '0') * 16, 0, 16, 32), Color.White
                    , 0f,Vector2.Zero, scale, SpriteEffects.None, 0);
                position.X += 16 * scale;
            }
        }

        /// <summary>
        /// 文字表示用 by柏　2017.02.08
        /// </summary>
        /// <param name="fontName">フォント</param>
        /// <param name="data">表示したい文字</param>
        /// <param name="position">表示位置</param>
        /// <param name="color">色</param>
        /// <param name="scale">大きさ</param>
        public void DrawString(string data, Vector2 position, Color color, float scale = 1.0f, string fontName = "font1" ) {
            SpriteFont font = ResouceManager.GetFont(fontName);
            spriteBatch.DrawString(font, data, position, color, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
        }
    }
}
