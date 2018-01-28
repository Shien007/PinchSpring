//作成日：　2017.03.14
//作成者：　柏
//クラス内容：  gameDeviceまとめ管理
//修正内容リスト：
//名前：　　　日付：　　　内容：
//名前：　　　日付：　　　内容：

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyLib.Device
{
    public class GameDevice
    {
        private ResouceManager resouceManager;  //リソース管理用
        private Renderer_2D renderer2D;          //2D描画用
        private InputState inputState;      //入力用
        private Sound sound;                //音声用
        private GraphicsDevice graphics;    //描画用

        public GameDevice(ContentManager content, GraphicsDevice graphics) {
            resouceManager = new ResouceManager(content);
            renderer2D = new Renderer_2D(content, graphics);
            inputState = new InputState();
            sound = new Sound();
            this.graphics = graphics;
            Initialize();
        }

        /// <summary>
        /// ロードシーンで使う最低限のリソースを読み込む
        /// </summary>
        public void Initialize() {
            resouceManager.LoadEffect("TargetMark");
            resouceManager.LoadTextures("number");
            resouceManager.LoadTextures("Loading");
            resouceManager.LoadFont("font1");
            resouceManager.LoadBGM("title");
        }

        /// <summary>
        /// 更新
        /// </summary>
        public void Update() { inputState.Update(); }

        /// <summary>
        /// リセット
        /// </summary>
        public void UnLoad() {
            sound.Unload();
            resouceManager.UnLoad();
        }

        /// <summary>
        /// 入力デバイス取得
        /// </summary>
        public InputState GetInputState { get { return inputState; } }

        /// <summary>
        /// 2D描画デバイス取得
        /// </summary>
        public Renderer_2D GetRenderer { get { return renderer2D; } }

        /// <summary>
        /// サウンドデバイス取得
        /// </summary>
        public Sound GetSound { get { return sound; } }

        /// <summary>
        /// リソース管理取得
        /// </summary>
        public ResouceManager GetResouceManager { get { return resouceManager; } }
    }
}
