//作成日：　2017.03.14
//作成者：　柏
//クラス内容：　Loadingシーン
//修正内容リスト：
//名前：　　　日付：　　　内容：
//名前：　　　日付：　　　内容：

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyLib.Device;
using MyLib.Scene.Loaders;

namespace PinchSpring.Scene.Scenes
{
    class Loading : IScene
    {
        private ResouceManager resouceManager;
        private ResouceLoader resouceLoader;
        private Sound sound;
        private bool isEnd;

        private int totalResouceNum;    //全種類リソース合計数

        /// <summary>
        /// リソースそろう
        /// </summary>
        /// <returns>リソースリスト</returns>
        private string[,] ResouceList()
        {
            string imagePath  = "./IMAGE/";
            string effectPath = "./EFFECT/";
            string bgmPath = "./MP3/";
            string sePath = "./WAV/";

            string[,] list = new string[,] {
                {"Title",imagePath},        {"startText",imagePath},
                {"operateText",imagePath},  {"staffText",imagePath},
                {"Clear",imagePath},        {"Ending",imagePath},
                {"Operate1",imagePath},     {"Operate2",imagePath},
                {"staffRoll",imagePath},    { "fadeImage",imagePath},
                {"fireball",imagePath},     {"barYellow",imagePath},
                {"beam",imagePath},         { "Pause",imagePath},
                {"distanceBar",imagePath},  {"PlayerICO",imagePath},
                {"barRed",imagePath},       {"wakeFlameParticleColor",imagePath},
                {"bossHpBar",imagePath},    {"redMark",imagePath},
                {"particle",imagePath},     {"particleColor",imagePath},
                {"greenMark",imagePath},    {"stars",imagePath},
                {"smoke",imagePath},        {"particleColor_2",imagePath},
                {"cussor",imagePath},       {"player",imagePath},
                {"player1",imagePath},　　　{"broadBase",imagePath},
                {"block",imagePath},        {"spring",imagePath},

                {"Particle",effectPath},    

                {"title",bgmPath},          {"clear",bgmPath},
                {"operate",bgmPath},        {"gameover",bgmPath},
                {"staffroll",bgmPath},      {"BossBGM",bgmPath},
                {"StageBGM",bgmPath},       

                {"beam",sePath},            {"bullet",sePath},
                {"laserburn",sePath},       {"crumble",sePath},
                {"gameclear",sePath},       {"checkpoint",sePath},
                {"fire",sePath},            {"itemGet",sePath},
                {"enemyDead",sePath}, 
            };
            return list;
        }

        public Loading(GameDevice gameDevice) {
            resouceManager = gameDevice.GetResouceManager;
            sound = gameDevice.GetSound;
            resouceLoader = new ResouceLoader(resouceManager, ResouceList());
        }

        /// <summary>
        /// 初期化
        /// </summary>
        public void Initialize() {
            isEnd = false;
            resouceLoader.Initialize();
            totalResouceNum = resouceLoader.Count;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="gameTime">時間</param>
        public void Update(GameTime gameTime) {
            sound.PlayBGM("title");
            if (!resouceLoader.IsEnd) { resouceLoader.Update(); }
            else { isEnd = true; }
        }

        /// <summary>
        /// 描画
        /// </summary>
        /// <param name="renderer">2D描画管理</param>
        public void Draw(Renderer_2D renderer2D) {
            renderer2D.DrawTexture("Loading", Vector2.Zero);

            int currentCount = resouceLoader.CurrentCount;
            int pasent = (int)(currentCount / (float)totalResouceNum * 100f);
            int currentPasent = 0;
            Vector2 pasentPosition = new Vector2(27, 671);

            //完成率をバーで表示
            for (int j = 0; j < 970; j++) {
                renderer2D.DrawTexture("loadBar", pasentPosition);
                if (currentPasent >= 970 * pasent / 100) { break; }
                pasentPosition.X++;
                currentPasent++;
            }

            //完成率を数字で表示
            if (totalResouceNum != 0) {
                renderer2D.DrawNumber("number", new Vector2(850, 600), pasent, 1.5f);
            }
        }

        /// <summary>
        /// シーンを閉める
        /// </summary>
        public void Shutdown() { }

        /// <summary>
        /// 終わりフラッグを取得
        /// </summary>
        /// <returns>エンドフラッグ</returns>
        public bool IsEnd() { return isEnd; }

        /// <summary>
        /// 次のシーン
        /// </summary>
        /// <returns>シーンのEnum</returns>
        public E_Scene Next() {
            return E_Scene.TITLE; 
        }
    }
}
