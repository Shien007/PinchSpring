//作成日：　2017.03.22
//作成者：　柏
//クラス内容：　板ポリゴンクラス
//修正内容リスト：
//名前：　　　日付：　　　内容：
//名前：　　　日付：　　　内容：

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PinchSpring.Def;
using MyLib.Device;
using PinchSpring.Objects;

namespace PinchSpring
{
    class BroadPolygon
    {
        private GraphicsDevice graphicsDevice;
        private VertexPositionTexture[] verts;
        private VertexBuffer markVertexBuffer;
        private Effect effect;
        private Texture2D texture;
        private Vector3 position;
        private Matrix world;
        private Matrix projection;
        private float broadSize;

        public BroadPolygon(GraphicsDevice graphicsDevice, Effect effect, Texture2D texture)
        {
            this.graphicsDevice = graphicsDevice;
            this.effect = effect.Clone();
            this.texture = texture;

            verts = new VertexPositionTexture[4];
            position = Vector3.Zero;
            world = Matrix.CreateTranslation(position);
            
            projection = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.PiOver2, //真正面
                Parameter.ScreenWidth / Parameter.ScreenHeight,      //スクリーンの幅と高さの比
                1,      //near
                15      //far  2Dのため
            );
            broadSize = 1.0f;
            InitializeEffect();
            InitializeVertices();
        }

        /// <summary>
        /// シェーダー設定
        /// </summary>
        private void InitializeEffect() {
            effect.Parameters["tex"].SetValue(texture);
            effect.Parameters["size"].SetValue(1);
            effect.CurrentTechnique = effect.Techniques["Technique1"];
        }

        /// <summary>
        /// 頂点設定
        /// </summary>
        private void InitializeVertices() {
            verts[0] = new VertexPositionTexture(position, new Vector2(0, 0));
            verts[1] = new VertexPositionTexture(new Vector3(position.X, position.Y + broadSize, position.Z), new Vector2(0, 1));
            verts[2] = new VertexPositionTexture(new Vector3(position.X + broadSize, position.Y, position.Z), new Vector2(1, 0));
            verts[3] = new VertexPositionTexture(new Vector3(position.X + broadSize, position.Y + broadSize, position.Z), new Vector2(1, 1));
            markVertexBuffer = new VertexBuffer(graphicsDevice, typeof(VertexPositionTexture), verts.Length, BufferUsage.None);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="gameTime">時間</param>
        public void Update(GameTime gameTime) {
            VertexUpdate();
            world = Matrix.CreateTranslation(position);
        }

        /// <summary>
        /// 板の頂点座標更新
        /// </summary>
        private void VertexUpdate() {
            verts[0] = new VertexPositionTexture(position, new Vector2(0, 0));
            verts[1] = new VertexPositionTexture(new Vector3(position.X, position.Y + broadSize, position.Z), new Vector2(0, 1));
            verts[2] = new VertexPositionTexture(new Vector3(position.X + broadSize, position.Y, position.Z), new Vector2(1, 0));
            verts[3] = new VertexPositionTexture(new Vector3(position.X + broadSize, position.Y + broadSize, position.Z), new Vector2(1, 1));
        }

        /// <summary>
        /// 描画
        /// </summary>
        public void Draw(Vector2 cameraOffset2D) {
            Vector3 cameraOffset3D = new Vector3(position.X + cameraOffset2D.X, position.Y + cameraOffset2D.Y, 0);
            Matrix view = Matrix.CreateLookAt(position + new Vector3(0, 0, 5), position, Vector3.Up);
            graphicsDevice.SetVertexBuffer(markVertexBuffer);
            effect.Parameters["WorldViewProjection"].SetValue(view * projection);
            foreach (EffectPass pass in effect.CurrentTechnique.Passes) {
                pass.Apply();
                graphicsDevice.DrawUserPrimitives<VertexPositionTexture>(
                    PrimitiveType.TriangleStrip, verts, 0, 2);
            }
        }

        /// <summary>
        /// 描画方法を設定
        /// </summary>
        /// <param name="techniqueNum">描画方法の番号</param>
        public void SetTechnique(int techniqueNum) {
            effect.CurrentTechnique = effect.Techniques["Technique" + techniqueNum];
        }

        /// <summary>
        /// テクスチャー大きさ設定
        /// </summary>
        /// <param name="textureSize">テクスチャー大きさ</param>
        public void SetTextureSize(float textureSize) {
            effect.Parameters["size"].SetValue(textureSize);
        }

        /// <summary>
        /// 位置設定
        /// </summary>
        /// <param name="position">位置座標</param>
        public void SetPosition(Vector3 position) {
            this.position = position;
            world = Matrix.CreateTranslation(position);
        }
    }
}
