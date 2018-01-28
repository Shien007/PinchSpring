//作成日：　2017.03.14
//作成者：　柏
//クラス内容：　sound管理クラス
//修正内容リスト：
//名前：　　　日付：　　　内容：
//名前：　　　日付：　　　内容：

using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;    //wav
using Microsoft.Xna.Framework.Media;    //mp3

namespace MyLib.Device
{
    public class Sound
    {
        private Dictionary<string, SoundEffectInstance> seInstances;    //WAVインスタンス管理用
        private List<SoundEffectInstance> sePlayList;   //WAVインスタンスの再生リスト
        
        private string currentBGM;  //現在再生中のアセット名

        public Sound() {
            MediaPlayer.IsRepeating = true; //mp3の再生を循環する

            seInstances = new Dictionary<string, SoundEffectInstance>();
            sePlayList = new List<SoundEffectInstance>();
            currentBGM = null;
        }

        #region BGM関連

        /// <summary>
        /// BGM再生中止
        /// </summary>
        public void StopBGM() {
            MediaPlayer.Stop();
            currentBGM = null;
        }

        /// <summary>
        /// BGM再生
        /// </summary>
        /// <param name="name"></param>
        public void PlayBGM(string name) {
            if (currentBGM == name) { return; }
            if (IsPlayingBGM()) {
                StopBGM();
            }
            MediaPlayer.Volume = 0.5f;
            currentBGM = name;
            Song bgm = ResouceManager.GetBGM(currentBGM);
            MediaPlayer.Play(bgm);
        }

        /// <summary>
        /// BGMのループ設定
        /// </summary>
        /// <param name="loopFlag">循環スイッチ</param>
        public void ChangeBGMLoopFlag(bool loopFlag) {
            MediaPlayer.IsRepeating = loopFlag;
        }

        /// <summary>
        /// ゲット再生中止情報
        /// </summary>
        /// <returns></returns>
        public bool IsStoppedBGM() {
            return (MediaPlayer.State == MediaState.Stopped);
        }

        /// <summary>
        /// ゲット再生一時停止情報
        /// </summary>
        /// <returns></returns>
        public bool IsPausedBGM() {
            return (MediaPlayer.State == MediaState.Paused);
        }

        /// <summary>
        /// ゲット再生情報
        /// </summary>
        /// <returns></returns>
        public bool IsPlayingBGM() {
            return (MediaPlayer.State == MediaState.Playing);
        }

        #endregion

        #region WAV関連

        /// <summary>
        /// SE再生リストを生成
        /// </summary>
        /// <param name="name">SEアセット名</param>
        public void CreateSEInstance(string name) {
            if (seInstances.ContainsKey(name)) { return; }
            SoundEffect se = ResouceManager.GetSE(name);
            seInstances.Add(name, se.CreateInstance());
        }

        /// <summary>
        /// SE再生
        /// </summary>
        /// <param name="name">SEアセット名</param>
        public void PlaySE(string name) {
            SoundEffect se = ResouceManager.GetSE(name);
            se.Play();
        }

        /// <summary>
        /// SEリストからSE再生
        /// </summary>
        /// <param name="name">SEアセット名</param>
        /// <param name="loopFlag">ループスイッチ</param>
        public void PlaySEInstance(string name, bool loopFlag = false) {
            var data = seInstances[name];
            data.IsLooped = loopFlag;
            data.Play();
            sePlayList.Add(data);
        }

        /// <summary>
        /// SE再生の一時停止
        /// </summary>
        /// <param name="name">SEアセット名</param>
        public void PausedSE(string name) {
            foreach (var se in sePlayList) {
                if (se.State == SoundState.Playing) { se.Stop(); }
            }
        }

        /// <summary>
        /// 再生リストを空にする
        /// </summary>
        public void RemoveSE() {
            sePlayList.RemoveAll(se => se.State == SoundState.Stopped);
        }

        #endregion

        /// <summary>
        /// 使ったlistとdictionaryを初期化する
        /// </summary>
        public void Unload() {
            sePlayList.Clear();
        }

    }
}
