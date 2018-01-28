//作成日：　2017.03.14
//作成者：　柏
//クラス内容：　Timerクラス
//修正内容リスト：
//名前：　　　日付：　　　内容：
//名前：　　　日付：　　　内容：

namespace MyLib.Utility
{
    public class Timer
    {
        private float currentTime;
        private float lmitTime;
        private bool isTime;

        public Timer(float second) {
            lmitTime = second * 60;
            currentTime = second * 60;
            isTime = false;
        }

        /// <summary>
        /// 初期化
        /// </summary>
        public void Initialize() {
            currentTime = lmitTime;
            isTime = false;
        }

        //時間がなくなる前にカウントダウンする
        public void Update() {
            if (isTime) { return; }
            currentTime--;
            if (currentTime <= 0) {
                isTime = true;
                currentTime = 0;
            }
        }

        /// <summary>
        /// 時間になったかどうかをとる
        /// </summary>
        public bool IsTime{
            get { return isTime; }
        }

        /// <summary>
        /// 残った時間をとる
        /// </summary>
        public float NowTime {
            get { return currentTime; }
            set { currentTime = value * 60; }
        }

        /// <summary>
        /// 経過した時間の割合
        /// </summary>
        /// <returns>比</returns>
        public float Rate() {
            return currentTime / lmitTime;
        }

    }
}
