//作成日：　2017.03.14
//作成者：　柏
//クラス内容：　キャラの管理クラス
//修正内容リスト：
//名前：　　　日付：　　　内容：
//名前：　　　日付：　　　内容：

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyLib.Device;
using PinchSpring.Def;

namespace PinchSpring.Objects
{
    enum E_Creator {
        Stage01 = 1,
        Stage02,
        Stage03,
        BossStage = 101,
    }

    class ObjectsManager
    {
        private GameDevice gameDevice;
        private GraphicsDevice graphicsDevice;
        private static Random rnd = new Random();

        private List<GameObject> gameObjects;
        private Player player;

        public ObjectsManager(GameDevice gameDevice, GraphicsDevice graphicsDevice) {
            this.gameDevice = gameDevice;
            this.graphicsDevice = graphicsDevice;
            gameObjects = new List<GameObject>();

            //AddPlayer();
        }

        /// <summary>
        /// Player生成
        /// </summary>
        private void AddPlayer() {
            player = new Player(gameDevice, new Vector2(150, 150), graphicsDevice);
            player.SetObjectsManager(this);
            AddObject(player);
        }

        /// <summary>
        /// Object追加
        /// </summary>
        /// <param name="chara">キャラ</param>
        public void AddObject(GameObject newObject) {
            gameObjects.Add(newObject);
        }

        /// <summary>
        /// 複数のObjectを一気に追加
        /// </summary>
        /// <param name="list">キャラのリスト</param>
        public void AddList(List<GameObject> list) {
            list.ForEach(c => c.SetObjectsManager(this));
            gameObjects.AddRange(list);
        }


        /// <summary>
        /// 複数のObjectを一気に追加
        /// </summary>
        /// <param name="matrix">objectの配列</param>
        public void AddIntMatrix(GameObject[,] matrix) {
            for (int num = 0; num < matrix.GetLength(0) * matrix.GetLength(1); num++) {
                int lineCount = num / matrix.GetLength(1);
                int colCount = num % matrix.GetLength(1);
                if (matrix[lineCount, colCount] == null) { continue; }
                AddObject(matrix[lineCount, colCount]);
            } 
        }

        /// <summary>
        /// 初期化
        /// </summary>
        public void Initialize() {
            //gameObjects.Clear();
            player.Initialize();
            //AddObject(player);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="gameTime">時間</param>
        public void Update(GameTime gameTime) {
            UpdateCharacters(gameTime);
        }

        /// <summary>
        /// 描画
        /// </summary>
        /// <param name="renderer">2D描画管理</param>
        public void Draw(Renderer_2D renderer2D, Vector2 cameraOffset) {
            gameObjects.ForEach(c => {
                if (c != null) { c.Draw(renderer2D, cameraOffset); }
            });
        }

        /// <summary>
        /// キャラ全員の更新
        /// </summary>
        /// <param name="gameTime">時間</param>
        private void UpdateCharacters(GameTime gameTime) {
            Hit();
            gameObjects.ForEach(c => c.Update(gameTime));
            gameObjects.RemoveAll(c => c.IsDead);
        }

        /// <summary>
        /// 当たり処理
        /// </summary>
        private void Hit() {
            List<GameObject> objects = gameObjects.FindAll(o => !o.IsDead);
            objects = objects.FindAll(o => 
                Math.Abs(GetPlayer().PositionX - o.PositionX) < 100 &&
                Math.Abs(GetPlayer().PositionY - o.PositionY) < 100
            );
            for (int i = 0; i < objects.Count; i++) {
                for (int j = i + 1; j < objects.Count; j++) {
                    if (j >= objects.Count) { break; }
                    //if (objects[i].Tag == objects[j].Tag) { continue; }
                    if (objects[i].GetType() == objects[j].GetType()) { continue; }
                    //if (Collider.CollidesWith(objects[i], objects[j])) 
                    {
                        //objects[i].Hit();
                        //objects[j].Hit();
                    }
                }
            }
        }

        /// <summary>
        /// playerを設置する
        /// </summary>
        /// <param name="player">player</param>
        public void SetPlayer(Player player) {
            this.player = player;
            player.SetObjectsManager(this);
        }

        /// <summary>
        /// 当たりチェックするObjectをとる
        /// </summary>
        /// <param name="target">基準になってるObject</param>
        /// <returns>チェック必要あるObjects</returns>
        public List<GameObject> GetCollitionListSquare(GameObject target) {
            List<GameObject> list = gameObjects.FindAll(o => 
                HorizontalIn(target, o) && VerticalIn(target, o));
            list.Remove(target);
            return list;
        }

        /// <summary>
        /// 水平方向の当たりチェック範囲に入ってるかをチェック(距離は３ピクセル以内チェックはじめ)
        /// </summary>
        /// <param name="target">基準になってるObject</param>
        /// <param name="check">チェックするObject</param>
        /// <returns></returns>
        private bool VerticalIn(GameObject target, GameObject check) {
            return Math.Abs(target.Position.X - check.Position.X) <= Parameter.PlayerWidth + 3;
        }

        /// <summary>
        /// 縦方向の当たりチェック範囲に入ってるかをチェック(距離は３ピクセル以内チェックはじめ)
        /// </summary>
        /// <param name="target">基準になってるObject</param>
        /// <param name="check">チェックするObject</param>
        /// <returns></returns>
        private bool HorizontalIn(GameObject target, GameObject check) {
            return Math.Abs(target.Position.Y - check.Position.Y) <= Parameter.PlayerHeight + 3;
        }

        /// <summary>
        /// 自機を取得
        /// </summary>
        /// <returns>自機</returns>
        public Player GetPlayer() { return player; }

        /// <summary>
        /// ゲームデバイスの取得
        /// </summary>
        /// <returns>ゲームデバイス</returns>
        public GameDevice GetGameDevice() { return gameDevice; }

        /// <summary>
        /// ステージクリア状態取得
        /// </summary>
        /// <returns></returns>
        public bool IsStageClear() { return player.IsClear(); }

        /// <summary>
        /// ゲームクリア状態取得
        /// </summary>
        /// <returns>クリア状態</returns>
        public bool IsGameClear() {
            return false;
        }

        /// <summary>
        /// シーン終わり状態の取得
        /// </summary>
        /// <returns>終わり状態</returns>
        public bool IsEnding() {
            return gameObjects.Find(c => c.Name == "player") == null;
        }
    }
}
