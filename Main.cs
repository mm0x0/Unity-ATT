using UnityEngine;

public class Main : MonoBehaviour
{
    void Awake ()
    {
        // iOSのときのみATT操作を実行
#if UNITY_IOS
        ManageATT ();
#endif
    }

#if UNITY_IOS
    // ATT操作を実行
    void ManageATT ()
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
                CantManageATT ();
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
    void CantManageATT ()
    {
        Debug.Log ("設定不可");
    }
#endif
}
