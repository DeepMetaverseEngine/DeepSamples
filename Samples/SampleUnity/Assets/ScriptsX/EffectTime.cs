using UnityEngine;
using System.Collections;

#if UNITY_EDITOR

using UnityEditor;

[CustomEditor(typeof(EffectTime))]
public class EffectTimeEditor: Editor
{
    EffectTime mTarget;

    private void OnEnable()
    {
        mTarget = target as EffectTime;
    }
    public override void OnInspectorGUI()
    {
        SetField((v) => mTarget.localTestBody = (Animation)v,
            () => EditorGUILayout.ObjectField("Test Unit", mTarget.localTestBody, typeof(Animation), true));
        if (mTarget.localTestBody)
        {
            SetField((v) => mTarget.localTestAnim = v, () =>
            {
                int p = 0;
                AnimationClip[] clips = AnimationUtility.GetAnimationClips(mTarget.localTestBody.gameObject);
                string[] names = new string[clips.Length];
                for (int i = 0; i < names.Length; i++)
                {
                    names[i] = clips[i].name;
                    if (mTarget.localTestAnim == names[i])
                        p = i;
                }
                return names[EditorGUILayout.Popup("Animation", p, names)];
            });
        }

        base.OnInspectorGUI();
    }

    void SetField<T>(System.Action<T> value, System.Func<T> func, string undoTip = null)
    {
        EditorGUI.BeginChangeCheck();
        T result = func();
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(mTarget, undoTip == null ? "Field Change" : undoTip);
            value(result);
            EditorUtility.SetDirty(mTarget);
        }
    }
}

#endif

public class EffectTime : MonoBehaviour {

    [Tooltip("持续时间（-1=无限）")]
    public float duration = -1;

    [Tooltip("自动隐藏")]
    public bool autoHidden;

    [Tooltip("延迟")]
    public float delay;

    [Tooltip("循环检测@JYK")]
    public bool isLoop = false;

    [HideInInspector]
    public Animation localTestBody;
    [HideInInspector]
    public string localTestAnim;

    private void OnEnable()
    {
        if (delay > 0)
        {
            for (int i = 0; i < gameObject.transform.childCount; i++)
                gameObject.transform.GetChild(i).gameObject.SetActive(false);

            Invoke("Active", delay);
        }
#if UNITY_EDITOR
        if (localTestBody && localTestBody.GetClip(localTestAnim)) localTestBody.Play(localTestAnim);
#endif

        if (autoHidden && duration > 0)
            Invoke("Deactive", delay + duration);

    }

    void Active()
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
            gameObject.transform.GetChild(i).gameObject.SetActive(true);
    }

    void Deactive()
    {
        gameObject.SetActive(false);
    }
}
