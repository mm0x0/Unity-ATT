# ATTとは

iOS14からAppleの広告配信用の識別子（IDFA）にアクセスするにはユーザーに許可を求めることが必須になりました。そのためアプリ内でユーザーに回答を求めるダイアログを表示する必要があります。（しないとリジェクトされるらしい）

![ATT_1](https://user-images.githubusercontent.com/26345138/135487106-2026ae04-067b-4d66-902c-1d19ea44570c.png)

# 使い方
1. Assetsの中にATT.cs、Plugins > iOSの中にATT.mmを配置する

![ATT_2](https://user-images.githubusercontent.com/26345138/135490915-7412cdf8-25eb-4501-9d95-9d9a0ce3519c.png)

2. ATTを呼び出すコードを書く（Main.csの内容）

```
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

```
 - 設定済み、設定不可、設定完了時にそれぞれ関数を実行するようになっています。

3. iOSでビルド
