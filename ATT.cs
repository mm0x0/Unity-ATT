using System;
using System.Runtime.InteropServices;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

public class ATT
{
#if UNITY_IOS
    private const string DLL_NAME = "__Internal";

    // ATT承認状態を取得（同期）
    [DllImport (DLL_NAME)]
    private static extern int _GetATTStatus ();
    public static int GetATTStatus ()
    {
        if (Application.isEditor) {
            return -1;
        }
        return _GetATTStatus ();
    }

    // ATT承認を要求（非同期）
    [DllImport (DLL_NAME)]
    private static extern void _RequestATT (OnCompleteCallback callback);

    private delegate void OnCompleteCallback (int status);
    private static SynchronizationContext _context;
    private static UnityAction <int> _OnComplete;

    public static void RequestATT (UnityAction <int> OnComplete)
    {
        if (Application.isEditor) {
            // 呼出元のActionの引数を0にして実行する
            OnComplete?.Invoke (0);
            return;
        }
        _context = SynchronizationContext.Current;
        _OnComplete = OnComplete;
        _RequestATT (OnRequestComplete);
    }

    // ATT要求完了時のコールバック
    [AOT.MonoPInvokeCallback (typeof (OnCompleteCallback))]
    private static void OnRequestComplete (int status)
    {
        if (_OnComplete != null) {
            _context.Post(_ => {
                _OnComplete?.Invoke (status);
                _OnComplete = null;
            }, null);
        }
    }
#endif
}