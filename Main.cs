using UnityEngine;

public class Main : MonoBehaviour
{
    void Awake ()
    {
        // iOSのときのみATT操作を実行
#if UNITY_IOS
        RequestATT ();
#endif
    }

#if UNITY_IOS
    // ATT操作を実行
    void RequestATT ()
    {
        int status = ATT.GetATTStatus ();

        // ATT未設定のとき
        if (status == 0)
            // リクエストダイアログを表示
            ATT.RequestATT (CompleteATT);
        else
        {
            // ATT設定不可
            if (status == 1 || status == 2) {
                CantRequestATT ();
                return;
            }

            // 設定済み
            AlreadyAnswerdATT ();
        }
    }

    // 回答済みのとき実行
    void AlreadyAnswerdATT ()
    {
        Debug.Log ("設定済み");
    }

    // ユーザー回答後に実行
    void CompleteATT (int status)
    {
        Debug.Log ("設定完了");
    }

    // 設定不可のとき実行
    void CantRequestATT ()
    {
        Debug.Log ("設定不可");
    }
#endif
}
