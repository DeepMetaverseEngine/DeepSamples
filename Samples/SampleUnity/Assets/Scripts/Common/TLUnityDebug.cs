using DeepCore.Unity3D.Game.Battle;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class TLUnityDebug : MonoBehaviour
    {

        //调试模式开关，控制是否显示GUI调试信息，内外网切换开关等，线上正式版本打包要设为false.
        public static bool DEBUG_MODE = true;
        
        public bool ShowDebugInfo { get { return bDebug; } }

        private static TLUnityDebug mInstance = null;
        private StringBuilder mSB = null;
        private bool mShowWindow = false;
        private const string BtnName = "日志";

        private GUIStyle labStyle = new GUIStyle();
        private Rect BtnDebugPos = new Rect(10, 10, 0.15f * Screen.width, 0.08f * Screen.height);
        private Rect mWindowRect = new Rect(0.02f * Screen.width,
                                                 0.02f * Screen.height,
                                                 Screen.width - (2 * 0.02f * Screen.width),
                                                 Screen.height - (2 * 0.02f * Screen.height));

        private GUIContent clearLabel = new GUIContent("Clear", "Clear the contents of the console.");
        private GUIContent collapseLabel = new GUIContent("Collapse", "Hide repeated messages.");

        private const int WINDOW_ID = 121121;
        private Vector2 mScrollPos;
        private Rect DRAG_WINDOW = new Rect(0, 0, 10000, 20);
        private Object mLockSign = new Object();
        private bool mCollapse = false;
        private List<ConsoleMessage> mMessageList = new List<ConsoleMessage>();

        private bool bDebug = false;
        //bool bIsShowRz = false;

        private bool mShowBodySize = false;
        private bool mShowGuard = false;
        private bool mShowAttack = false;
        public GUISkin mTLSkin;
    void Awake()
    {
        mInstance = this;
        DontDestroyOnLoad(this.gameObject);
            Init();
        }

        void OnEnable()
        {
            labStyle.fontSize = Screen.width / 40;
            SetDebug(DEBUG_MODE);
            //Application.RegisterLogCallback(OnHandleLog);
            Application.logMessageReceived += Application_logMessageReceived;
        }

        private void Application_logMessageReceived(string condition, string stackTrace, LogType type)
        {
            OnHandleLog(condition, stackTrace, type);
        }

        void OnDisable()
        {
            //Application.RegisterLogCallback(null);
            Application.logMessageReceived -= Application_logMessageReceived;
        }
        
        void OnGUI()
        {
            if (!DEBUG_MODE) return;

            if(mTLSkin != null)
            {
                UnityEngine.GUI.skin = mTLSkin;
            }
       
            bDebug = UnityEngine.GUI.Toggle(new Rect(0, 300, 100, 20), bDebug, "Debug");

            if (bDebug == false) { return; }

            if (UnityEngine.GUI.Button(BtnDebugPos, BtnName))
            {
                mShowWindow = !mShowWindow;
            }

            if (mShowWindow)
            {
                mWindowRect = GUILayout.Window(WINDOW_ID, mWindowRect, UpdateWindow, BtnName);
            }

            if (mDebugInterfaceIml == null) { return; }

            int height = 360;
            bool body = UnityEngine.GUI.Toggle(new Rect(0, height, 100, 20), mShowBodySize, "BodySize");
            if (mShowBodySize != body)
            {
                mShowBodySize = body;
                mDebugInterfaceIml.ShowBodySize(mShowBodySize);
            }

            bool guard = UnityEngine.GUI.Toggle(new Rect(0, height+20, 100, 20), mShowGuard, "Guard");
            if (guard != mShowGuard)
            {
                mShowGuard = guard;
                mDebugInterfaceIml.ShowGuard(mShowGuard);
            }

            bool attack = UnityEngine.GUI.Toggle(new Rect(0, height+40, 100, 20), mShowAttack, "Attack");
            if (mShowAttack != attack)
            {
                mShowAttack = attack;
                mDebugInterfaceIml.ShowAttack(mShowAttack);
            }

            //if(GUI.Button(new Rect(0, 250, 50, 30), "Near"))
            //{
            //    mDebugInterfaceIml.DoNear();
            //}
            //if(GUI.Button(new Rect(70, 250, 50, 30), "Far"))
            //{
            //    mDebugInterfaceIml.DoFar();
            //}
            //if(GUI.Button(new Rect(0, 210, 50, 30), "-H"))
            //{
            //    mDebugInterfaceIml.DoSub_H();
            //}
            //if(GUI.Button(new Rect(70, 210, 50, 30), "+H"))
            //{
            //    mDebugInterfaceIml.DoAdd_H();
            //}
            //if(GUI.Button(new Rect(0, 170, 50, 30), "-RX"))
            //{
            //    mDebugInterfaceIml.DoSub_RX();
            //}
            //if(GUI.Button(new Rect(70, 170, 50, 30), "+RX"))
            //{
            //    mDebugInterfaceIml.DoAdd_RX();
            //}
            //if(GUI.Button(new Rect(0, 130, 50, 30), "-RY"))
            //{
            //    mDebugInterfaceIml.DoSub_RY();
            //}
            //if(GUI.Button(new Rect(70, 130, 50, 30), "+RY"))
            //{
            //    mDebugInterfaceIml.DoAdd_RY();
            //}

        }

        void UpdateWindow(int windowID)
        {
            lock (mLockSign)
            {
                mScrollPos = GUILayout.BeginScrollView(mScrollPos);

                // Go through each logged entry
                for (int i = 0; i < mMessageList.Count; i++)
                {
                    ConsoleMessage entry = mMessageList[i];

                    // If this message is the same as the last one and the collapse feature is chosen, skip it
                    if (mCollapse && i > 0 && entry.message == mMessageList[i - 1].message)
                    {
                        continue;
                    }

                    // Change the text colour according to the log type
                    switch (entry.type)
                    {
                        case LogType.Error:
                        case LogType.Exception:
                            UnityEngine.GUI.contentColor = Color.green;
                            labStyle.normal.textColor = Color.green;
                            break;
                        case LogType.Warning:
                            UnityEngine.GUI.contentColor = Color.yellow;
                            labStyle.normal.textColor = Color.yellow;
                            break;
                        default:
                            UnityEngine.GUI.contentColor = Color.white;
                            labStyle.normal.textColor = Color.white;
                            break;
                    }

                    GUILayout.Label(entry.message, labStyle);
                }

                UnityEngine.GUI.contentColor = Color.white;

                GUILayout.EndScrollView();

                GUILayout.BeginHorizontal();

                // Clear button
                if (GUILayout.Button(clearLabel))
                {
                    mMessageList.Clear();
                }

                // Collapse toggle
                mCollapse = GUILayout.Toggle(mCollapse, collapseLabel, GUILayout.ExpandWidth(false));

                GUILayout.EndHorizontal();
                UnityEngine.GUI.DragWindow(DRAG_WINDOW);
            }
        }

        private IDebugCameraInterface mDebugInterfaceIml = null;

        public void SetInterface(IDebugCameraInterface iml)
        {
            mDebugInterfaceIml = iml;
        }

        public static TLUnityDebug GetInstance()
        {
            return mInstance;
        }
        private void Init()
        {
            mSB = new StringBuilder();
        }
        public void Log(string txt)
        {

        }
        public void ErrorLog(string txt)
        {

        }
        private void OnHandleLog(string message, string stackTrace, LogType type)
        {
            AutoClear();

            mSB.Length = 0;
            mSB.Append("\n------------------------------------------------------------\n");
            mSB.Append(message);
            mSB.Append("\n");
            mSB.Append(stackTrace);
            lock (mLockSign)
            {
                mMessageList.Add(new ConsoleMessage(mSB.ToString(), type));
            }
        }
        private void AutoClear()
        {
            lock (mLockSign)
            {
                if (mMessageList.Count > 200)
                {
                    mMessageList.RemoveRange(0, 100);
                }
            }
        }

        internal struct ConsoleMessage
        {
            public readonly string message;
            public readonly LogType type;

            public ConsoleMessage(string message, LogType type)
            {
                this.message = message;
                this.type = type;
            }
        }

        public static void SetDebug(bool isShow)
        {
            DEBUG_MODE = isShow;
            Debugger.IsDebugBuild = isShow;
            Debugger.UseDebug = isShow;
        }
        
        
    }
