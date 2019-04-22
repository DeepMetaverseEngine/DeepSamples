
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using UnityEngine.Networking;

public class HttpRequest : MonoBehaviour {
    
    //public enum MsgID
    //{
    //    Register = 1,
    //    Login = 2,
    //    Update = 29,
    //    ServerList = 101,
    //}

    public enum UserCenterMsgID
    {
        Login = 2,          //登录
        Register = 3,       //注册
    }

    public enum GameCenterMsgID
    {
        Init = 1,           //初始化，版本更新
        ServerList = 2,     //刷新服务器列表
        CloudImage = 3,     //万象优图
        Activation = 4,     //账号激活
    }


    public enum ResultType
    {
        Success,
        TimeOut,
        Error
    }

    public delegate IEnumerator Request(RequestData reqData);
    public delegate void Response(ResponseData data);
    //public event OnResponse ResponseEvent;

    private Queue<RequestData> RequestQueue = new Queue<RequestData>();
    private RequestData mCurRequest;

    //soncket多线程方式下的返回包队列，会在主线程分发.
    private Queue<ResponseData> ResponseQueue = new Queue<ResponseData>();

    private string mRequestUrl;
    public string RequestUrl { get { return mRequestUrl; } set { mRequestUrl = value; } }
    //超时时间（秒）
    private float mTimeOutCur;
    private float mTimeOutMax = 10;
    public float TimeOutSEC { get { return mTimeOutMax; } set { mTimeOutMax = value; } }
    //是否转菊花
    //private bool mShowWaiting = true;
    //public bool ShowWaiting { get { return mShowWaiting; } set { mShowWaiting = value; } }

    /// <summary>
    /// GET方式请求
    /// </summary>
    /// <param name="url">请求地址</param>
    /// <param name="msgId">协议ID(区分用，可自定义)</param>
    /// <param name="response">回调函数</param>
    public void RequestGet(string url, int msgId, Response response, bool isWaiting = true)
    {
        ReadyToRequest(new RequestData(msgId, url, null, isWaiting, GET, response));
    }

    /// <summary>
    /// POST方式请求
    /// </summary>
    /// <param name="url">请求地址</param>
    /// <param name="arg">请求参数</param>
    /// <param name="msgId">协议ID(区分用，可自定义)</param>
    /// <param name="response">回调函数</param>
    public void RequestPost(string url, Dictionary<string, string> arg, int msgId, Response response, bool isWaiting = true)
    {
        ReadyToRequest(new RequestData(msgId, url, arg, isWaiting, POST, response));
    }

    public void PostWithSocket(string url, DeepCore.Http.HttpPostHandler handler)
    {
        System.Uri uri = new System.Uri(url);
        DeepCore.Http.WebClient.PostAsync(uri, System.Text.Encoding.UTF8, handler);
    }

    public void PostWithSocket(string url, Dictionary<string, string> arg, int msgId, Response response, bool isWaiting = true)
    {
        //mCurRequest = new RequestData(msgId, url, arg, isWaiting, null, response);
        //mTimeOutCur = mTimeOutMax;
        //if (isWaiting)
        //    GameAlertManager.Instance.ShowLoading(true, true, mTimeOutMax);

        //ZeusNetManage.Instance.NetClient.RequestHttpPostAsync(url, arg, (result) =>
        ////ZeusBattleClientBot.ZeusBot.ZeusNetClient.RequestHttpPostAsync(url, arg, (result) =>
        //{
        //    if (response != null && mCurRequest != null)
        //    {
        //        ResultType tType = result != null ? ResultType.Success : ResultType.Error;
        //        ResponseData repData = new ResponseData(msgId, tType, result, isWaiting, response);
        //        ResponseQueue.Enqueue(repData);
        //        mCurRequest = null;
        //    }
        //});
    }

    private void ReadyToRequest(RequestData reqData)
    {
        if (reqData == null)
            return;

        if (mCurRequest == null)
        {
            if (reqData.ReqFunc != null)
            {
                StartCoroutine(reqData.ReqFunc.Invoke(reqData));
            }
        }
        else
        {
            RequestQueue.Enqueue(reqData);
        }
    }

    // 信頼する証明書のハッシュリスト
    readonly static List<string> TrustedThumbprints = new List<string>()
    {
        "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX",
    };

    const X509ChainStatusFlags IgnoreChainStatus =
            X509ChainStatusFlags.RevocationStatusUnknown |  // 証明書が失効しているかどうか判断できない
            X509ChainStatusFlags.OfflineRevocation |  // 証明書失効リストが使えなかった
            X509ChainStatusFlags.PartialChain |  // X509チェーンをルート証明書に構築できなかった
            X509ChainStatusFlags.UntrustedRoot;  // ルート証明書が信頼されていない

    private static bool OnRemoteCertificateValidationCallback(
      object sender,
      X509Certificate certificate,
      X509Chain chain,
      SslPolicyErrors sslPolicyErrors)
    {
        //无脑OK
        return true;



        // エラーがなければ OK
        if (sslPolicyErrors == SslPolicyErrors.None)
        {
            return true;
        }

        // 信頼するハッシュリストと比較し、一致するなら OK
        if (TrustedThumbprints.Contains(((X509Certificate2)certificate).Thumbprint))
        {
            return true;
        }

        // SslPolicyError.RemoteCertificateChainErrors 以外のエラーがあるなら NG
        if ((sslPolicyErrors & ~SslPolicyErrors.RemoteCertificateChainErrors) != 0)
        {
            return false;
        }

        // IgnoreChainStatus 以外のチェーンエラーがあるなら NG
        for (int i = 0; i < chain.ChainStatus.Length; ++i)
        {
            if ((chain.ChainStatus[i].Status & ~IgnoreChainStatus) != 0)
            {
                return false;
            }
        }

        // 証明書チェーン内に信頼する証明書と一致するものがあれば OK とする
        for (int i = 0; i < chain.ChainElements.Count; ++i)
        {
            var element = chain.ChainElements[i];
            if (TrustedThumbprints.Contains(element.Certificate.Thumbprint))
            {
                TrustedThumbprints.Add(element.Certificate.Thumbprint);
                return true;
            }
        }
        return false;
    }

/// <summary>
/// POST请求
/// </summary>
/// <param name="url"></param>
/// <param name="post"></param>
/// <returns></returns>
private IEnumerator POST(RequestData reqData)
    {
        mCurRequest = reqData;
        mTimeOutCur = mTimeOutMax;
        if (reqData.IsWaiting)
        {
            GameAlertManager.Instance.ShowLoading(true, true, mTimeOutMax);
        }

        WWWForm form = new WWWForm();
        foreach (KeyValuePair<string, string> post_arg in reqData.PostData)
        {
            form.AddField(post_arg.Key, post_arg.Value);
            //GameDebug.Log("key = " + post_arg.Key + ", Value = " + post_arg.Value);
        }
        UnityWebRequest www = UnityWebRequest.Post(reqData.ReqUrl, form);
        yield return www.SendWebRequest();

        //response
        ResultType result = ResultType.TimeOut;
        if (www.error != null)
        {
            //POST请求失败
            //GameDebug.Log("error is :" + www.error);
            result = ResultType.Error;
        }
        else
        {
            //POST请求成功
            //GameDebug.Log("request ok : " + www.text);
            result = ResultType.Success;
        }

        //call back
        if (mCurRequest != null && reqData.RspFunc != null)
        {
            ResponseData repData = new ResponseData(reqData.MsgId, result, www.downloadHandler.text, reqData.IsWaiting);
            reqData.RspFunc(repData);
        }

        if (reqData.IsWaiting)
            GameAlertManager.Instance.ShowLoading(false);
        mCurRequest = null;
        www.Dispose();
    }

    /// <summary>
    /// GET请求
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    private IEnumerator GET(RequestData reqData)
    {
        mCurRequest = reqData;
        mTimeOutCur = mTimeOutMax;
        if (reqData.IsWaiting)
        {
            GameAlertManager.Instance.ShowLoading(true, true, mTimeOutMax);
        }

        WWW www = new WWW(reqData.ReqUrl);
        yield return www;

        //response
        ResultType result = ResultType.TimeOut;
        if (www.error != null)
        {
            //GET请求失败
            //GameDebug.Log("error is :" + www.error);
            result = ResultType.Error;
        }
        else
        {
            //GET请求成功
            //GameDebug.Log("request ok : " + www.text);
            result = ResultType.Success;
        }

        //call back
        if (mCurRequest != null && reqData.RspFunc != null)
        {
            ResponseData repData = new ResponseData(reqData.MsgId, result, www.text, reqData.IsWaiting);
            reqData.RspFunc(repData);
        }

        if (reqData.IsWaiting)
            GameAlertManager.Instance.ShowLoading(false);
        mCurRequest = null;
    }

    public void Awake()
    {
        System.Net.ServicePointManager.ServerCertificateValidationCallback = OnRemoteCertificateValidationCallback;
        //System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Ssl3;
    }

    public void Update()
    {
        if (mCurRequest != null)
        {
            float delta = UnityEngine.Time.deltaTime;
            mTimeOutCur -= delta;
            if (mTimeOutCur <= 0)   //time out
            {
                if (mCurRequest.RspFunc != null)
                {
                    ResponseData repData = new ResponseData(mCurRequest.MsgId, ResultType.TimeOut, "", mCurRequest.IsWaiting, mCurRequest.RspFunc);
                    ResponseQueue.Enqueue(repData);
                    //mCurRequest.RspFunc(repData);
                }
                mCurRequest = null;
            }
        }
        else
        {
            if (RequestQueue.Count > 0)
            {
                RequestData reqData = RequestQueue.Peek();
                if (reqData != null && reqData.ReqFunc != null)
                {
                    StartCoroutine(reqData.ReqFunc.Invoke(reqData));
                }
                RequestQueue.Dequeue();
            }
        }

        //分发socket异步模式下的返回包.
        if (ResponseQueue.Count > 0)
        {
            while (ResponseQueue.Count > 0)
            {
                ResponseData rspData = ResponseQueue.Dequeue();
                if (rspData != null && rspData.RspFunc != null)
                {
                    rspData.RspFunc(rspData);
                    rspData.RspFunc = null;
                }
                if (rspData.IsWaiting)
                    GameAlertManager.Instance.ShowLoading(false);
            }
        }
    }

    public void Destroy()
    {
        StopAllCoroutines();
        RequestQueue.Clear();
        mCurRequest = null;
        DeepCore.Unity3D.UnityHelper.Destroy(this);
    }

    public class RequestData
    {
        public int MsgId { get; set; }
        public Request ReqFunc { get; set; }
        public Response RspFunc { get; set; }
        public string ReqUrl { get; set; }
        public Dictionary<string, string> PostData { get; set; }
        public bool IsWaiting { get; set; }
        public RequestData(int id, string reqUrl, Dictionary<string, string> postData, bool waiting, Request request, Response response)
        {
            MsgId = id;
            ReqUrl = reqUrl;
            PostData = postData;
            IsWaiting = waiting;
            ReqFunc = request;
            RspFunc = response;
        }
    }

    public class ResponseData
    {
        public int MsgId { get; set; }
        public ResultType Result { get; set; }
        public string Content { get; set; }
        public Response RspFunc { get; set; }
        public bool IsWaiting { get; set; }
        public ResponseData(int id, ResultType result, string content, bool waiting, Response response = null)
        {
            MsgId = id;
            Result = result;
            Content = content;
            IsWaiting = waiting;
            RspFunc = response;
        }
    }
	
}

/// <summary>
/// HTTP加载
/// </summary>
public class AlltollHttpRequest : MonoBehaviour
{
    private STATE m_eState;
    private enum STATE
    {
        START = 0,
        STOP,
        CLOSE
    }

    /// <summary>
    /// Res the send.
    /// </summary>
    internal void ReSend()
    {
        this.m_eState = STATE.START;
    }

    /// <summary>
    /// Close this instance.
    /// </summary>
    internal void Close()
    {
        this.m_eState = STATE.CLOSE;
    }

    public static void Request(string url, WWWForm form, byte[] byteData, Dictionary<string, string> headers,
        System.Action<WWW> callback, System.Action<string, System.Action, System.Action> error_callback)
    {
        GameObject obj = new GameObject("AlltollHttpRequest");
        AlltollHttpRequest loader = obj.AddComponent<AlltollHttpRequest>();
        loader.m_eState = STATE.START;
        loader.StartCoroutine(loader.request(url, form, byteData, headers, callback, error_callback));
    }

    //request
    internal IEnumerator request(string url, WWWForm form, byte[] byteData, Dictionary<string, string> headers, System.Action<WWW> callback,
                                    System.Action<string, System.Action, System.Action> error_callback)
    {
        WWW www = null;
        bool ok = true;
        for (; ok;)
        {
            switch (this.m_eState)
            {
                case STATE.START:
                    url = System.Uri.EscapeUriString(url);

                    if (byteData != null)
                    {
                        if (headers == null)
                        {
                            www = new WWW(url, byteData);
                        }
                        else
                        {
                            www = new WWW(url, byteData, headers);
                        }
                    }
                    else if (form != null)
                    {
                        if (headers == null)
                        {
                            www = new WWW(url, form);
                        }
                        else
                        {
                            www = new WWW(url, form.data, headers);
                        }
                    }
                    else if (form == null)
                    {
                        if (headers == null)
                        {
                            www = new WWW(url);
                        }
                        else
                        {
                            www = new WWW(url, new byte[] { (byte)0 }, headers);
                        }
                    }

                    yield return www;
                    try
                    {
                        if (www.error != null)
                        {
                            Debugger.LogError("ERROR HTTP : " + www.error + url);
                            this.m_eState = STATE.STOP;
                            if (error_callback != null)
                                error_callback(www.error, ReSend, Close);
                        }
                        else
                        {
                            this.m_eState = STATE.CLOSE;
                            if (callback != null)
                                callback(www);
                        }
                    }
                    catch (Exception ex)
                    {
                        this.m_eState = STATE.STOP;
                        if (error_callback != null)
                            error_callback(ex.StackTrace, ReSend, Close);
                        else
                            this.m_eState = STATE.CLOSE;
                    }
                    www.Dispose();
                    www = null;
                    break;
                case STATE.STOP:
                    yield return new WaitForFixedUpdate();
                    break;
                case STATE.CLOSE:
                    DeepCore.Unity3D.UnityHelper.Destroy(this.gameObject);
                    ok = false;
                    break;
            }
        }
    }

}
