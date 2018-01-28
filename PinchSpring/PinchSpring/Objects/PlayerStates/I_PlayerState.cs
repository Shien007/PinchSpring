//作成日：　2017.03.14
//作成者：　柏
//クラス内容：　Playerのモーションインタフェース
//修正内容リスト：
//名前：　　　日付：　　　内容：
//名前：　　　日付：　　　内容：

using MyLib.Device;

namespace PinchSpring.Objects.PlayerStates
{
    interface I_PlayerState
    {
        void Update();      //更新
        void ChangeNext();  //次のステートに移行
        void Draw(Renderer_2D renderer2D);  //描画
        
    }
}
