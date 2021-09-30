# ATTとは

iOS14からAppleの広告配信用の識別子（IDFA）にアクセスするにはユーザーに許可を求めることが必須になりました。そのためアプリ内でユーザーに回答を求めるダイアログを表示する必要があります。（しないとリジェクトされるらしい）

![ATT_1](https://user-images.githubusercontent.com/26345138/135487106-2026ae04-067b-4d66-902c-1d19ea44570c.png)

# 使い方（ローカライズして実機で確認まで）
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

```
 - 設定済み、設定不可、設定完了時にそれぞれ関数を実行するようになっています。

3. iOSでビルドしてUnity-iPhone.xcworkspaceを開く

![ATT_3](https://user-images.githubusercontent.com/26345138/135497165-852ff0f0-4acf-405a-b117-a7127b8d7d71.png)

4.Targets > UnityFramework > Frameworks and LibraryでAdSupport.frameworkとAppTrackingTransparency.frameworkを追加する

![ATT_4](https://user-images.githubusercontent.com/26345138/135499375-d4b53e37-5681-4559-91e8-b762bb69641c.png)

5.Project > Unity-iPhone > Info > Localizationで”Japanese”と"English"がなければ追加する（他にもローカライズしたい言語があれば追加する）

![ATT_5](https://user-images.githubusercontent.com/26345138/135500607-53ec980d-8ec3-4e28-a23c-dddc7e3e265a.png)

6.Unity-Iphone Test > InfoPlistを開いて右のLocalizationの”English”と”Japanese”にチェック、Target MembershipのUnity-Iphoneにチェックを入れる。

![ATT_6](https://user-images.githubusercontent.com/26345138/135501411-0cce6ee9-fb82-490f-9ceb-59f97cac9d95.png)

7.InfoPlistにそれぞれローカライズしたコードを記述する

```
"NSUserTrackingUsageDescription" = "好みに合わせた広告を表示するために利用されます。";
```

![ATT_7](https://user-images.githubusercontent.com/26345138/135502538-bc6ce277-19b6-40d9-b429-896fdd37ca38.png)

8.ビルドしてダイアログが表示されていることを確認

![ATT_1](https://user-images.githubusercontent.com/26345138/135487106-2026ae04-067b-4d66-902c-1d19ea44570c.png)

## 参考ページ
- [iOS14のATT(App Tracking Transparency)に対応してAdMob広告を表示した](http://blog.be-style.jpn.com/article/188329627.html)

## その他
- ローカライズの自動化もしたかったけど、なぜかうまく行かなくてあきらめました…

