using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections.Generic;

#if UNITY_EDITOR

using UnityEditor;

[CustomEditor(typeof(EffectControlHelper))]
public class EffectControlHelperEditor : Editor
{
    static Keyframe k0 = new Keyframe(0, 0, 1, 1);
    static Keyframe k1 = new Keyframe(1, 1, 1, 1);

    EffectControlHelper helper;
    SerializedProperty tweenProperties;
    GUIContent content = new GUIContent();
    int gridID = 0;

    AnimationCurve copyCurve;
    Gradient copyGradient;

    private void OnEnable()
    {
        helper = target as EffectControlHelper;
        tweenProperties = serializedObject.FindProperty("tweens");
        Undo.undoRedoPerformed += OnUndoRedo;
    }

    void OnUndoRedo()
    {
        serializedObject.Update();
        Repaint();
    }

    void OnDisable()
    {
        Undo.undoRedoPerformed -= OnUndoRedo;
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        AddTween();

        SetProperty("总延迟", serializedObject.FindProperty("delay"));

        RepeatSetting();

        gridID = GUILayout.SelectionGrid(gridID, GetTweenNames(), 2);
        if (gridID >= 0 && gridID < helper.tweens.Count)
        {
            EditorGUILayout.BeginVertical("helpbox");
            {
                ShowListControl(ref gridID);
                if (gridID >= 0 && gridID < helper.tweens.Count)
                    ShowAndSetTween(gridID);
            }
            EditorGUILayout.EndVertical();
        }

        if (GUILayout.Button("测试播放"))
        {
            if (!Application.isPlaying)
            {
                EditorApplication.isPlaying = true;
            }
            else
            {
                helper.RePlay();
            }              
        }

        SampleAnimation();
    }

    void AddTween()
    {
        SetField((v) =>
        {
            tweenProperties.GetArrayElementAtIndex(++tweenProperties.arraySize - 1).FindPropertyRelative("body").objectReferenceValue = v;
            InitTween(tweenProperties.arraySize - 1, 0);
            serializedObject.ApplyModifiedPropertiesWithoutUndo();
            gridID = tweenProperties.arraySize - 1;
        }, () => EditorGUILayout.ObjectField("拖入需要控制的变换节点", null, typeof(Transform), true), "Set Controller");

        SetField((v) =>
        {
            tweenProperties.GetArrayElementAtIndex(++tweenProperties.arraySize - 1).FindPropertyRelative("body").objectReferenceValue = v;
            InitTween(tweenProperties.arraySize - 1, 3);
            serializedObject.ApplyModifiedPropertiesWithoutUndo();
            gridID = tweenProperties.arraySize - 1;
        }, () => EditorGUILayout.ObjectField("拖入需要控制的材质节点", null, typeof(Renderer), true), "Set Controller");

        SetField((v) =>
        {
            tweenProperties.GetArrayElementAtIndex(++tweenProperties.arraySize - 1).FindPropertyRelative("body").objectReferenceValue = v;
            InitTween(tweenProperties.arraySize - 1, 3);
            serializedObject.ApplyModifiedPropertiesWithoutUndo();
            gridID = tweenProperties.arraySize - 1;
        }, () => EditorGUILayout.ObjectField("拖入需要控制的UI网格节点", null, typeof(UIMeshRenderer), true), "Set Controller");

        SetField((v) =>
        {
            tweenProperties.GetArrayElementAtIndex(++tweenProperties.arraySize - 1).FindPropertyRelative("body").objectReferenceValue = v;
            InitTween(tweenProperties.arraySize - 1, 3);
            serializedObject.ApplyModifiedPropertiesWithoutUndo();
            gridID = tweenProperties.arraySize - 1;
        }, () => EditorGUILayout.ObjectField("拖入需要控制的图形节点", null, typeof(MaskableGraphic), true), "Set Controller");

        SetField((v) =>
        {
            tweenProperties.GetArrayElementAtIndex(++tweenProperties.arraySize - 1).FindPropertyRelative("body").objectReferenceValue = v;
            InitTween(tweenProperties.arraySize - 1, 5);
            serializedObject.ApplyModifiedPropertiesWithoutUndo();
            gridID = tweenProperties.arraySize - 1;
        }, () => EditorGUILayout.ObjectField("拖入需要控制显示的节点", null, typeof(GameObject), true), "Set Controller");

        SetField((v) =>
        {
            tweenProperties.GetArrayElementAtIndex(++tweenProperties.arraySize - 1).FindPropertyRelative("body").objectReferenceValue = v;
            InitTween(tweenProperties.arraySize - 1, 8);
            serializedObject.ApplyModifiedPropertiesWithoutUndo();
            gridID = tweenProperties.arraySize - 1;
        }, () => EditorGUILayout.ObjectField("拖入需要控制的投影节点", null, typeof(ProjectorPlane), true), "Set Controller");
    }

    void RepeatSetting()
    {
        #region 简易复生
        SerializedProperty repeatTimes = serializedObject.FindProperty("repeatTimes");
        SetField((v) =>
        {
            repeatTimes.arraySize = Mathf.Max(0, v);
            serializedObject.ApplyModifiedPropertiesWithoutUndo();
        }, () => EditorGUILayout.DelayedIntField("简易重复", repeatTimes.arraySize), "Set Count");

        if (repeatTimes.arraySize > 0)
        {
            EditorGUILayout.BeginVertical("helpBox");
            {
                for (int i = 0; i < repeatTimes.arraySize; i++)
                {
                    SetField((v) =>
                    {
                        repeatTimes.GetArrayElementAtIndex(i).floatValue = Mathf.Max(0, v);
                        serializedObject.ApplyModifiedPropertiesWithoutUndo();
                    }, () => EditorGUILayout.FloatField(string.Format("  延迟[{0}]", i), repeatTimes.GetArrayElementAtIndex(i).floatValue), "Set Time");
                }

                if (repeatTimes.arraySize > 0)
                {
                    SetProperty("依次偏移", serializedObject.FindProperty("offset"));
                    SetProperty("依次偏转", serializedObject.FindProperty("angleOff"));
                    SetProperty("依次缩放", serializedObject.FindProperty("scale"));
                    SetProperty("存在时间", serializedObject.FindProperty("instanceExistTime"));
                }
            }
            EditorGUILayout.EndVertical();
        }
        #endregion

        #region 自定义复生
        SerializedProperty avatars = serializedObject.FindProperty("avatars");
        SerializedProperty avatar;
        SetField((v) =>
        {
            int oldSize = avatars.arraySize;
            avatars.arraySize = Mathf.Max(0, v);
            serializedObject.ApplyModifiedPropertiesWithoutUndo();
            for (int i = oldSize; i < avatars.arraySize; i++)
            {
                avatar = avatars.GetArrayElementAtIndex(i);
                avatar.FindPropertyRelative("scale").vector3Value = Vector3.one;
                avatar.FindPropertyRelative("duration").floatValue = 1;
            }
            serializedObject.ApplyModifiedPropertiesWithoutUndo();
        }, () => EditorGUILayout.DelayedIntField("自定义重复", avatars.arraySize), "Set Count");

        for (int i = 0; i < avatars.arraySize; i++)
        {
            avatar = avatars.GetArrayElementAtIndex(i);
            EditorGUILayout.BeginVertical("helpbox");
            {
                SetProperty("延迟", avatar.FindPropertyRelative("delay"));
                SetProperty("位置", avatar.FindPropertyRelative("position"));
                SetProperty("角度", avatar.FindPropertyRelative("rotation"));
                SetProperty("比例", avatar.FindPropertyRelative("scale"));
                SetProperty("存在时间", avatar.FindPropertyRelative("duration"));
            }
            EditorGUILayout.EndVertical();
        }
        #endregion
    }

    void ShowListControl(ref int i)
    {
        EditorGUILayout.BeginHorizontal();
        {
            EffectControlHelper.TweenGroup tween = helper.tweens[i];
            SetField((v) => tween.body = v, () => EditorGUILayout.ObjectField(tween.body, tween.body.GetType(), true), "Change Body");

            if (GUILayout.Button("复制", GUILayout.Width(40)))
            {
                if (i >= 0)
                {
                    Undo.RecordObject(helper, "Copy");
                    EffectControlHelper.TweenGroup clone = new EffectControlHelper.TweenGroup(helper.tweens[i]);
                    helper.tweens.Add(clone);
                    EditorUtility.SetDirty(helper);
                    serializedObject.Update();
                }
            }

            if (GUILayout.Button("上移", GUILayout.Width(40)))
            {
                if (i > 0)
                {
                    Undo.RecordObject(helper, "Move Up");
                    tweenProperties.MoveArrayElement(i, i - 1);
                    serializedObject.ApplyModifiedPropertiesWithoutUndo();
                    EditorUtility.SetDirty(helper);
                }
            }

            if (GUILayout.Button("下移", GUILayout.Width(40)))
            {
                if (i < tweenProperties.arraySize - 1)
                {
                    Undo.RecordObject(helper, "Move Down");
                    tweenProperties.MoveArrayElement(i, i + 1);
                    serializedObject.ApplyModifiedPropertiesWithoutUndo();
                    EditorUtility.SetDirty(helper);
                }
            }

            if (GUILayout.Button("删除", GUILayout.Width(40)))
            {
                Undo.RecordObject(helper, "Remove");
                tweenProperties.DeleteArrayElementAtIndex(i--);
                serializedObject.ApplyModifiedPropertiesWithoutUndo();
                EditorUtility.SetDirty(helper);
            }
        }
        EditorGUILayout.EndHorizontal();
    }

    void ShowAndSetTween(int i)
    {
        SerializedProperty tween = tweenProperties.GetArrayElementAtIndex(i);

        SerializedProperty controlType = tween.FindPropertyRelative("controlType");
        SetField((v) =>
        {
            if (helper.tweens[i].body is Renderer || helper.tweens[i].body is UIMeshRenderer || helper.tweens[i].body is MaskableGraphic)
            {
                if (v != EffectControlHelper.ControlType.TintColor
                    && v != EffectControlHelper.ControlType.UV
                    && v != EffectControlHelper.ControlType.ShaderValue
                    && v != EffectControlHelper.ControlType.SequenceTex)
                    v = EffectControlHelper.ControlType.TintColor;
            }
            else if (helper.tweens[i].body is Transform)
            {
                if (v != EffectControlHelper.ControlType.Position
                    && v != EffectControlHelper.ControlType.Rotation
                    && v != EffectControlHelper.ControlType.Scale)
                    v = EffectControlHelper.ControlType.Position;
            }
            else if (helper.tweens[i].body is GameObject)
                v = EffectControlHelper.ControlType.Visible;
            else if (helper.tweens[i].body is ProjectorPlane)
                v = EffectControlHelper.ControlType.ProjectorPlane;

            controlType.intValue = (int)v;
            serializedObject.ApplyModifiedPropertiesWithoutUndo();
        }, () => 
        (EffectControlHelper.ControlType)EditorGUILayout.EnumPopup("控制方式", (EffectControlHelper.ControlType)controlType.enumValueIndex), "Set ControlType");

        switch ((EffectControlHelper.ControlType)controlType.enumValueIndex)
        {
            case EffectControlHelper.ControlType.Visible:
                {
                    #region 显示
                    SetProperty("延迟(不再使用)", tween.FindPropertyRelative("delay"));
                    SetProperty("可见(不再使用)", tween.FindPropertyRelative("visible"));
                    SerializedProperty visibles = tween.FindPropertyRelative("visibles");

                    SetField((v) => 
                    {
                        visibles.arraySize = Mathf.Max(0, v);
                        serializedObject.ApplyModifiedPropertiesWithoutUndo();
                    }, () => EditorGUILayout.IntField("可视组", visibles.arraySize), "Set Size");

                    for (int s = 0; s < visibles.arraySize; s++)
                    {
                        EditorGUILayout.BeginHorizontal();
                        {
                            SerializedProperty visible = visibles.GetArrayElementAtIndex(s);
                            SetField((v) =>
                            {
                                visible.FindPropertyRelative("delay").floatValue = Mathf.Max(0, v);
                                serializedObject.ApplyModifiedPropertiesWithoutUndo();
                            }, () => EditorGUILayout.DelayedFloatField("时间", visible.FindPropertyRelative("delay").floatValue), "Set Delay");
                            SetField((v) =>
                            {
                                visible.FindPropertyRelative("visible").boolValue = v;
                                serializedObject.ApplyModifiedPropertiesWithoutUndo();
                            }, () => EditorGUILayout.ToggleLeft("可见", visible.FindPropertyRelative("visible").boolValue, GUILayout.Width(60)), "Set Visible");
                        }
                        EditorGUILayout.EndHorizontal();
                    }
                    #endregion

                    break;
                }
            case EffectControlHelper.ControlType.Position:
            case EffectControlHelper.ControlType.Rotation:
            case EffectControlHelper.ControlType.Scale:
                {
                    #region 变换动画
                    SetProperty("延迟", tween.FindPropertyRelative("delay"));
                    SetProperty("持续时间", tween.FindPropertyRelative("duration"));
                    SetProperty("循环(-1=无限循环)", tween.FindPropertyRelative("loop"));
                    SetProperty("循环方式", tween.FindPropertyRelative("loopType"));

                    SerializedProperty vectorCtrl = tween.FindPropertyRelative("vectorCtrl");

                    SetProperty("开始于", vectorCtrl.FindPropertyRelative("from"));
                    SetProperty("结束于", vectorCtrl.FindPropertyRelative("to"));

                    SetProperty("使用缓动代替曲线", tween.FindPropertyRelative("useEase"));
                    if (tween.FindPropertyRelative("useEase").boolValue)
                        SetProperty("缓动类型", tween.FindPropertyRelative("ease"));
                    else
                    {
                        SerializedProperty curves = tween.FindPropertyRelative("curves");
                        if (curves.arraySize < 3)
                        {
                            int oldSize = curves.arraySize;
                            curves.arraySize = 3;
                            for (int c = oldSize; c < curves.arraySize; c++)
                                curves.GetArrayElementAtIndex(c).animationCurveValue = new AnimationCurve(k0, k1);
                            serializedObject.ApplyModifiedPropertiesWithoutUndo();
                            EditorUtility.SetDirty(helper);
                        }
                        SetCurve("X曲线", curves.GetArrayElementAtIndex(0));
                        SetCurve("Y曲线", curves.GetArrayElementAtIndex(1));
                        SetCurve("Z曲线", curves.GetArrayElementAtIndex(2));
                    }
                    #endregion

                    break;
                }
            case EffectControlHelper.ControlType.TintColor:
                {
                    #region 颜色动画
                    SetProperty("延迟", tween.FindPropertyRelative("delay"));
                    SetProperty("持续时间", tween.FindPropertyRelative("duration"));
                    SetProperty("循环(-1=无限循环)", tween.FindPropertyRelative("loop"));
                    SetProperty("循环方式", tween.FindPropertyRelative("loopType"));

                    SerializedProperty colorCtrl = tween.FindPropertyRelative("colorCtrl");
                    if (tween.FindPropertyRelative("body").objectReferenceValue is Renderer)
                    {
                        Renderer renderer = tween.FindPropertyRelative("body").objectReferenceValue as Renderer;
                        SelectProperty("颜色属性名", colorCtrl.FindPropertyRelative("colorName"),
                            GetShaderProperties(renderer.sharedMaterial, ShaderUtil.ShaderPropertyType.Color));
                    }

                    SetGradient("颜色变化", helper.tweens[i].colorCtrl.gradient, colorCtrl.FindPropertyRelative("gradient"));
                    #endregion

                    break;
                }
            case EffectControlHelper.ControlType.UV:
                {
                    #region UV动画
                    SetProperty("延迟", tween.FindPropertyRelative("delay"));
                    SetProperty("持续时间", tween.FindPropertyRelative("duration"));
                    SetProperty("循环(-1=无限循环)", tween.FindPropertyRelative("loop"));
                    SetProperty("循环方式", tween.FindPropertyRelative("loopType"));

                    SerializedProperty uvCtrl = tween.FindPropertyRelative("uvCtrl");

                    if (tween.FindPropertyRelative("body").objectReferenceValue is Renderer)
                    {
                        Renderer renderer = tween.FindPropertyRelative("body").objectReferenceValue as Renderer;
                        if (renderer.sharedMaterial)
                            SelectProperty("UV属性名", uvCtrl.FindPropertyRelative("propertyName"),
                                GetShaderProperties(renderer.sharedMaterial, ShaderUtil.ShaderPropertyType.Vector, ShaderUtil.ShaderPropertyType.TexEnv));
                    }
                    else if (tween.FindPropertyRelative("body").objectReferenceValue is UIMeshRenderer)
                    {
                        UIMeshRenderer renderer = tween.FindPropertyRelative("body").objectReferenceValue as UIMeshRenderer;
                        if (renderer.material)                        
                            SelectProperty("UV属性名", uvCtrl.FindPropertyRelative("propertyName"),
                                GetShaderProperties(renderer.material, ShaderUtil.ShaderPropertyType.Vector, ShaderUtil.ShaderPropertyType.TexEnv));
                    }
                    else if (tween.FindPropertyRelative("body").objectReferenceValue is MaskableGraphic)
                    {
                        MaskableGraphic renderer = tween.FindPropertyRelative("body").objectReferenceValue as MaskableGraphic;
                        if (renderer.material)
                            SelectProperty("UV属性名", uvCtrl.FindPropertyRelative("propertyName"),
                                GetShaderProperties(renderer.material, ShaderUtil.ShaderPropertyType.Vector));
                    }

                    SerializedProperty uvInfoFrom = uvCtrl.FindPropertyRelative("from");
                    SerializedProperty uvInfoTo = uvCtrl.FindPropertyRelative("to");

                    SetField((v) =>
                    {
                        uvInfoFrom.vector4Value = new Vector4(v.x, v.y, uvInfoFrom.vector4Value.z, uvInfoFrom.vector4Value.w);
                        serializedObject.ApplyModifiedPropertiesWithoutUndo();
                    }, () => EditorGUILayout.Vector2Field("Tiling From", new Vector2(uvInfoFrom.vector4Value.x, uvInfoFrom.vector4Value.y)), "Set UV Tiling");

                    SetField((v) =>
                    {
                        uvInfoTo.vector4Value = new Vector4(v.x, v.y, uvInfoTo.vector4Value.z, uvInfoTo.vector4Value.w);
                        serializedObject.ApplyModifiedPropertiesWithoutUndo();
                    }, () => EditorGUILayout.Vector2Field("Tiling To", new Vector2(uvInfoTo.vector4Value.x, uvInfoTo.vector4Value.y)), "Set UV Tiling");

                    SetField((v) =>
                    {
                        uvInfoFrom.vector4Value = new Vector4(uvInfoFrom.vector4Value.x, uvInfoFrom.vector4Value.y, v.x, v.y);
                        serializedObject.ApplyModifiedPropertiesWithoutUndo();
                    }, () => EditorGUILayout.Vector2Field("Offset From", new Vector2(uvInfoFrom.vector4Value.z, uvInfoFrom.vector4Value.w)), "Set UV Offset");

                    SetField((v) =>
                    {
                        uvInfoTo.vector4Value = new Vector4(uvInfoTo.vector4Value.x, uvInfoTo.vector4Value.y, v.x, v.y);
                        serializedObject.ApplyModifiedPropertiesWithoutUndo();
                    }, () => EditorGUILayout.Vector2Field("Offset To", new Vector2(uvInfoTo.vector4Value.z, uvInfoTo.vector4Value.w)), "Set UV Offset");

                    SetProperty("使用缓动代替曲线", tween.FindPropertyRelative("useEase"));
                    if (tween.FindPropertyRelative("useEase").boolValue)
                        SetProperty("缓动类型", tween.FindPropertyRelative("ease"));
                    else
                    {
                        SerializedProperty curves = tween.FindPropertyRelative("curves");
                        if (curves.arraySize < 4)
                        {
                            int oldSize = curves.arraySize;
                            curves.arraySize = 4;
                            for (int c = oldSize; c < curves.arraySize; c++)
                                curves.GetArrayElementAtIndex(c).animationCurveValue = new AnimationCurve(k0, k1);
                            serializedObject.ApplyModifiedPropertiesWithoutUndo();
                            EditorUtility.SetDirty(helper);
                        }
                        SetCurve("U Tiling Curve", curves.GetArrayElementAtIndex(0));
                        SetCurve("V Tiling Curve", curves.GetArrayElementAtIndex(1));
                        SetCurve("U Offset Curve", curves.GetArrayElementAtIndex(2));
                        SetCurve("V Offset Curve", curves.GetArrayElementAtIndex(3));
                    }
                    #endregion

                    break;
                }
            case EffectControlHelper.ControlType.SequenceTex:
                {
                    #region 序列动画                    
                    SetProperty("延迟", tween.FindPropertyRelative("delay"));
                    SetProperty("持续时间", tween.FindPropertyRelative("duration"));
                    SetProperty("循环(-1=无限循环)", tween.FindPropertyRelative("loop"));

                    SerializedProperty uvCtrl = tween.FindPropertyRelative("uvCtrl");

                    if (tween.FindPropertyRelative("body").objectReferenceValue is Renderer)
                    {
                        Renderer renderer = tween.FindPropertyRelative("body").objectReferenceValue as Renderer;
                        if (renderer.sharedMaterial)
                            SelectProperty("序列纹理属性名", uvCtrl.FindPropertyRelative("propertyName"),
                                GetShaderProperties(renderer.sharedMaterial, ShaderUtil.ShaderPropertyType.TexEnv));
                    }
                    else if (tween.FindPropertyRelative("body").objectReferenceValue is UIMeshRenderer)
                    {
                        UIMeshRenderer renderer = tween.FindPropertyRelative("body").objectReferenceValue as UIMeshRenderer;
                        if (renderer.material)
                            SelectProperty("序列纹理UV", uvCtrl.FindPropertyRelative("propertyName"),
                                GetShaderProperties(renderer.material, ShaderUtil.ShaderPropertyType.Vector));
                    }
                    else if (tween.FindPropertyRelative("body").objectReferenceValue is MaskableGraphic)
                    {
                        MaskableGraphic renderer = tween.FindPropertyRelative("body").objectReferenceValue as MaskableGraphic;
                        if (renderer.material)
                            SelectProperty("序列纹理UV", uvCtrl.FindPropertyRelative("propertyName"),
                                GetShaderProperties(renderer.material, ShaderUtil.ShaderPropertyType.Vector));
                    }

                    SerializedProperty uvInfoFrom = uvCtrl.FindPropertyRelative("from");
                    SerializedProperty uvInfoTo = uvCtrl.FindPropertyRelative("to");

                    //X,Y = 列数，行数
                    SetField((v) =>
                    {
                        v.x = v.x == 0 ? v.x = 1 : v.x;
                        v.y = v.y == 0 ? v.y = 1 : v.y;
                        uvInfoFrom.vector4Value = new Vector4(1 / v.x, 1 / v.y, 0, 1 - 1 / v.y);
                        uvInfoTo.vector4Value = new Vector4(1 / v.x, 1 / v.y, 1 - 1 / v.x, 0);
                        serializedObject.ApplyModifiedPropertiesWithoutUndo();
                    }, () =>
                    {
                        int xCount = (int)(1 / uvInfoFrom.vector4Value.x + 0.5f);
                        int yCount = (int)(1 / uvInfoFrom.vector4Value.y + 0.5f);
                        return EditorGUILayout.Vector2Field("U Count | V Count", new Vector2(xCount, yCount));
                    }, "Set Col And Row");
                    #endregion

                    break;
                }
            case EffectControlHelper.ControlType.ShaderValue:
                {
                    #region 值变化
                    SetProperty("延迟", tween.FindPropertyRelative("delay"));
                    SetProperty("持续时间", tween.FindPropertyRelative("duration"));
                    SetProperty("循环(-1=无限循环)", tween.FindPropertyRelative("loop"));
                    SetProperty("循环方式", tween.FindPropertyRelative("loopType"));

                    SerializedProperty floatCtrol = tween.FindPropertyRelative("floatCtrl");

                    if (tween.FindPropertyRelative("body").objectReferenceValue is Renderer)
                    {
                        Renderer renderer = tween.FindPropertyRelative("body").objectReferenceValue as Renderer;
                        if (renderer.sharedMaterial)
                            SelectProperty("值属性名", floatCtrol.FindPropertyRelative("propertyName"),
                                GetShaderProperties(renderer.sharedMaterial, ShaderUtil.ShaderPropertyType.Float, ShaderUtil.ShaderPropertyType.Range));
                    }
                    else if (tween.FindPropertyRelative("body").objectReferenceValue is UIMeshRenderer)
                    {
                        UIMeshRenderer renderer = tween.FindPropertyRelative("body").objectReferenceValue as UIMeshRenderer;
                        if (renderer.material)
                            SelectProperty("值属性名", floatCtrol.FindPropertyRelative("propertyName"),
                                GetShaderProperties(renderer.material, ShaderUtil.ShaderPropertyType.Float, ShaderUtil.ShaderPropertyType.Range));
                    }
                    else if(tween.FindPropertyRelative("body").objectReferenceValue is MaskableGraphic)
                    {
                        MaskableGraphic renderer = tween.FindPropertyRelative("body").objectReferenceValue as MaskableGraphic;
                        if (renderer.material)
                            SelectProperty("值属性名", floatCtrol.FindPropertyRelative("propertyName"),
                                GetShaderProperties(renderer.material, ShaderUtil.ShaderPropertyType.Float, ShaderUtil.ShaderPropertyType.Range));
                    }

                    SetProperty("开始于", floatCtrol.FindPropertyRelative("from"));
                    SetProperty("结束于", floatCtrol.FindPropertyRelative("to"));

                    SetProperty("使用缓动代替曲线", tween.FindPropertyRelative("useEase"));
                    if (tween.FindPropertyRelative("useEase").boolValue)
                        SetProperty("缓动类型", tween.FindPropertyRelative("ease"));
                    else
                    {
                        SerializedProperty curves = tween.FindPropertyRelative("curves");
                        if (curves.arraySize < 1)
                        {
                            curves.arraySize = 1;
                            curves.GetArrayElementAtIndex(0).animationCurveValue = new AnimationCurve(k0, k1);
                            serializedObject.ApplyModifiedPropertiesWithoutUndo();
                            EditorUtility.SetDirty(helper);
                        }                       
                        SetCurve("曲线", curves.GetArrayElementAtIndex(0));
                    }
                    #endregion

                    break;                  
                }
            case EffectControlHelper.ControlType.ProjectorPlane:
                {
                    #region 投影面
                    SetProperty("延迟", tween.FindPropertyRelative("delay"));
                    SetProperty("持续时间", tween.FindPropertyRelative("duration"));
                    EditorGUILayout.LabelField("", "持续时间为0时，仅创建不更新，为-1时，每帧更新直到本体消亡，大于0时保持每帧更新", "Helpbox");
                    #endregion

                    break;
                }
        }
    }

    void InitTween(int i, int controlType)
    {
        SerializedProperty tween = tweenProperties.GetArrayElementAtIndex(tweenProperties.arraySize - 1);
        tween.FindPropertyRelative("controlType").intValue = controlType;
        tween.FindPropertyRelative("duration").floatValue = 1f;
        tween.FindPropertyRelative("loop").intValue = 1;
        SerializedProperty colorCtrl = tween.FindPropertyRelative("colorCtrl");
        colorCtrl.FindPropertyRelative("colorName").stringValue = "_TintColor";
        SerializedProperty unCtrl = tween.FindPropertyRelative("uvCtrl");
        unCtrl.FindPropertyRelative("propertyName").stringValue = "_UV";
        unCtrl.FindPropertyRelative("from").vector4Value = new Vector4(1, 1, 0, 0);
        unCtrl.FindPropertyRelative("to").vector4Value = new Vector4(1, 1, 0, 0);
        SerializedProperty curves = tween.FindPropertyRelative("curves");
        switch (controlType)
        {
            case 0:
            case 1:
            case 2:
                {
                    curves.arraySize = 3;
                    for (int c = 0; c < 3; c++)
                        curves.GetArrayElementAtIndex(c).animationCurveValue = new AnimationCurve(k0, k1);
                    serializedObject.ApplyModifiedPropertiesWithoutUndo();
                }
                break;
            case 3:
                {
                    serializedObject.ApplyModifiedPropertiesWithoutUndo();
                    helper.tweens[i].colorCtrl.gradient = new Gradient();
                    helper.tweens[i].colorCtrl.gradient.SetKeys(
                        new GradientColorKey[2] { new GradientColorKey(Color.white, 0), new GradientColorKey(Color.white, 1) },
                        new GradientAlphaKey[2] { new GradientAlphaKey(1, 0), new GradientAlphaKey(1, 1) });
                    EditorUtility.SetDirty(helper);
                    serializedObject.Update();
                }
                break;
            case 4:
                {
                    curves.arraySize = 4;
                    for (int c = 0; c < 4; c++)
                        curves.GetArrayElementAtIndex(c).animationCurveValue = new AnimationCurve(k0, k1);
                    serializedObject.ApplyModifiedPropertiesWithoutUndo();
                }
                break;
        }     
    }

    List<string> GetShaderProperties(Material material, params ShaderUtil.ShaderPropertyType[] type)
    {
        Shader shader = material.shader;
        List<string> properties = new List<string>();

        int count = ShaderUtil.GetPropertyCount(shader);

        for (int i = 0; i < count; i++)
        {
            ShaderUtil.ShaderPropertyType propertyType = ShaderUtil.GetPropertyType(shader, i);

            for (int j = 0; j < type.Length; j++)
                if (propertyType == type[j])
                    properties.Add(ShaderUtil.GetPropertyName(shader, i));
        }

        return properties;
    }

    string[] GetTweenNames()
    {
        List<string> names = new List<string>();
        for (int i = 0; i < helper.tweens.Count; i++)
        {
            if (!helper.tweens[i].body)
            {
                helper.tweens.RemoveAt(i--);
                EditorUtility.SetDirty(helper);
                serializedObject.Update();
                continue;
            }

            names.Add(string.Format("{0}<{1}>", helper.tweens[i].body.name, helper.tweens[i].controlType.ToString()));
        }

        return names.ToArray();
    }

    void SetField<T>(System.Action<T> value, System.Func<T> func, string undoTip = null)
    {
        EditorGUI.BeginChangeCheck();
        T result = func();
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(helper, undoTip == null ? "Field Change" : undoTip);
            value(result);
            EditorUtility.SetDirty(helper);
        }
    }

    void SetProperty(string label, SerializedProperty property, string undoTip = null)
    {
        EditorGUI.BeginChangeCheck();
        content.text = label == null ? "" : label;
        EditorGUILayout.PropertyField(property, content, true);

        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(helper, undoTip == null ? "Property Change" : undoTip);
            serializedObject.ApplyModifiedPropertiesWithoutUndo();         
            EditorUtility.SetDirty(helper);
        }
    }

    void SelectProperty(string label, SerializedProperty nameProperty, List<string> filterProperties)
    {
        EditorGUILayout.BeginHorizontal();
        {
            SetProperty(label, nameProperty);
            SetField((v) =>
            {
                nameProperty.stringValue = v;
                serializedObject.ApplyModifiedPropertiesWithoutUndo();
            }, () =>
            {
                int d = filterProperties.FindIndex(n => n.Equals(nameProperty.stringValue));
                int p = EditorGUILayout.Popup(d, filterProperties.ToArray(), GUILayout.Width(80));
                if (p != d) return filterProperties[p];
                return nameProperty.stringValue;
            });
        }
        EditorGUILayout.EndHorizontal();
    }

    void SetCurve(string label, SerializedProperty curve)
    {
        EditorGUILayout.BeginHorizontal();
        {
            SetProperty(label, curve);
            GUI.color = copyCurve == null ? Color.white : Color.yellow;
            if (GUILayout.Button(copyCurve == null ? "复制曲线" : "粘贴曲线", GUILayout.Width(54)))
            {
                if (copyCurve == null)
                {
                    copyCurve = curve.animationCurveValue;
                }
                else
                {
                    curve.animationCurveValue = new AnimationCurve(copyCurve.keys);
                    copyCurve = null;
                    serializedObject.ApplyModifiedProperties();
                    EditorUtility.SetDirty(helper);
                }
            }
            GUI.color = Color.white;
        }
        EditorGUILayout.EndHorizontal();
    }

    void SetGradient(string label, Gradient gradient, SerializedProperty gradProperty)
    {
        EditorGUILayout.BeginHorizontal();
        {
            SetProperty(label, gradProperty);
            GUI.color = copyGradient == null ? Color.white : Color.yellow;
            if (GUILayout.Button(copyGradient != null ? "粘贴" : "复制", GUILayout.Width(54)))
            {
                if (copyGradient == null)
                {
                    copyGradient = new Gradient();
                    copyGradient.SetKeys(gradient.colorKeys, gradient.alphaKeys);
                }
                else
                {
                    Undo.RecordObject(helper, "Copy");
                    gradient.SetKeys(copyGradient.colorKeys, copyGradient.alphaKeys);
                    EditorUtility.SetDirty(helper);
                    serializedObject.Update();
                    copyGradient = null;
                    Repaint();
                }
            }
            GUI.color = Color.white;
        }
        EditorGUILayout.EndHorizontal();
    }

    void SampleAnimation()
    {
        if (string.IsNullOrEmpty(helper.animationPath))
        {
            string relativePath = AssetDatabase.GetAssetPath(PrefabUtility.GetPrefabParent(helper));
            helper.animationPath = string.IsNullOrEmpty(relativePath) ? helper.animationPath : relativePath.Split('.')[0];
        }

        SetField((v) => helper.animationPath = v, () => EditorGUILayout.DelayedTextField("采样动画路径", helper.animationPath), "Set Bake Path");

        if (GUILayout.Button("采样到动画"))
        {
            if (!Application.isPlaying)
                return;

            string relativePath = helper.animationPath;
            if (System.IO.Directory.Exists(relativePath.Substring(0, relativePath.LastIndexOf('/'))))
                helper.BakeToClip((c) =>
                {
                    AnimationClip clip;
                    if (clip = (AnimationClip)AssetDatabase.LoadAssetAtPath(relativePath + ".anim", typeof(AnimationClip)))
                    {
                        EditorUtility.CopySerialized(c, clip);
                        EditorUtility.SetDirty(clip);
                    }
                    else
                        AssetDatabase.CreateAsset(c, relativePath + ".anim");
                });
        }
    }
}

#endif

public class EffectControlHelper : MonoBehaviour
{
    public enum ControlType
    {
        Position = 0,
        Rotation = 1,
        Scale = 2,
        TintColor = 3,
        UV = 4,
        Visible = 5,
        ShaderValue = 6,
        SequenceTex = 7,
        ProjectorPlane = 8
    }

    [System.Serializable]
    public class TweenGroup
    {
        public Object body;
        public ControlType controlType;
        public float delay;
        public float duration;
        public int loop;
        public LoopType loopType;
        public Vector3Control vectorCtrl;
        public ColorControl colorCtrl;
        public UVControl uvCtrl;
        public FloatControl floatCtrl;
        public bool useEase;
        public AnimationCurve[] curves;
        public Ease ease;
        public bool visible;
        public VisableGroup[] visibles;

        public TweenGroup() { }

        public TweenGroup(TweenGroup source)
        {
            body = source.body;
            controlType = source.controlType;
            delay = source.delay;
            duration = source.duration;
            loop = source.loop;
            loopType = source.loopType;
            vectorCtrl = new Vector3Control();
            vectorCtrl.from = source.vectorCtrl.from;
            vectorCtrl.to = source.vectorCtrl.to;
            colorCtrl = new ColorControl();
            colorCtrl.colorName = source.colorCtrl.colorName;
            colorCtrl.gradient = new Gradient();
            colorCtrl.gradient.SetKeys(source.colorCtrl.gradient.colorKeys, source.colorCtrl.gradient.alphaKeys);
            uvCtrl = new UVControl();
            uvCtrl.propertyName = source.uvCtrl.propertyName;
            uvCtrl.from = source.uvCtrl.from;
            uvCtrl.to = source.uvCtrl.to;
            floatCtrl = new FloatControl();
            floatCtrl.propertyName = source.floatCtrl.propertyName;
            floatCtrl.from = source.floatCtrl.from;
            floatCtrl.to = source.floatCtrl.to;
            useEase = source.useEase;
            curves = new AnimationCurve[source.curves.Length];
            for (int i = 0; i < curves.Length; i++)
                curves[i] = new AnimationCurve(source.curves[i].keys);
            ease = source.ease;
            visible = source.visible;
        }

        public TweenGroup(Object body)
        {
            this.body = body;
            if (body is Transform)
                controlType = ControlType.Position;
            else if (body is Renderer || body is MaskableGraphic || body is UIMeshRenderer)
                controlType = ControlType.TintColor;
            else
                controlType = ControlType.Visible;
        }

        public void SetValue(float t)
        {
            if (body is Transform)
            {
                Vector3 v;
                Evalute(out v, t);

                switch (controlType)
                {
                    case ControlType.Position:
                        (body as Transform).localPosition = v;
                        break;
                    case ControlType.Rotation:
                        (body as Transform).localEulerAngles = v;
                        break;
                    case ControlType.Scale:
                        (body as Transform).localScale = v;
                        break;
                }
            }
            else if (body is Renderer)
            {
                Renderer r = body as Renderer;
                if (r.material)
                {
                    switch (controlType)
                    {
                        case ControlType.TintColor:
                            if (colorCtrl.gradient != null)
                                r.material.SetColor(colorCtrl.colorName, colorCtrl.gradient.Evaluate(t));
                            break;
                        case ControlType.UV:
                            {
                                Vector4 v;
                                Evalute(out v, t);
                                r.material.SetVector(uvCtrl.propertyName, v);
                            }
                            break;
                        case ControlType.ShaderValue:
                            {
                                float v;
                                Evalute(out v, t);
                                r.material.SetFloat(floatCtrl.propertyName, v);
                            }                          
                            break;
                    }
                }
            }
            else if (body is MaskableGraphic)
            {
                MaskableGraphic i = body as MaskableGraphic;
                switch (controlType)
                {
                    case ControlType.TintColor:
                        if (colorCtrl.gradient != null)
                            i.color = colorCtrl.gradient.Evaluate(t);
                        break;
                    case ControlType.UV:
                        {
                            Vector4 v;
                            Evalute(out v, t);
                            if (i.material)
                                i.material.SetVector(uvCtrl.propertyName, v);
                            else if (i is RawImage)
                                (i as RawImage).uvRect = new Rect(v.z, v.w, v.x, v.y);
                        }
                        break;
                    case ControlType.ShaderValue:
                        {
                            if (i.material)
                            {
                                float v;
                                Evalute(out v, t);
                                i.material.SetFloat(floatCtrl.propertyName, v);
                            }
                        }
                        break;
                }
            }
            else if (body is UIMeshRenderer)
            {
                UIMeshRenderer r = body as UIMeshRenderer;
                switch (controlType)
                {
                    case ControlType.TintColor:
                        if (colorCtrl.gradient != null)
                            r.SetColor(colorCtrl.gradient.Evaluate(t));
                        break;
                    case ControlType.UV:
                        {
                            Vector4 cur;
                            Evalute(out cur, t);
                            r.SetUV(uvCtrl.propertyName, cur);
                        }
                        break;
                    case ControlType.ShaderValue:
                        {
                            float cur;
                            Evalute(out cur, t);
                            r.SetValue(floatCtrl.propertyName, cur);
                        }
                        break;
                }
            }
        }

        public void SetValue(Vector4 v)
        {
            if (controlType != ControlType.SequenceTex)
                return;

            if (body is Renderer)
            {
                Renderer r = body as Renderer;
                if (r.material)
                {
                    r.material.SetTextureScale(uvCtrl.propertyName, new Vector2(v.x, v.y));
                    r.material.SetTextureOffset(uvCtrl.propertyName, new Vector2(v.z, v.w));
                }
            }
            else if (body is MaskableGraphic)
            {
                MaskableGraphic i = body as MaskableGraphic;
                if (i.material)
                    i.material.SetVector(uvCtrl.propertyName, v);
                else if (i is RawImage)
                    (i as RawImage).uvRect = new Rect(v.z, v.w, v.x, v.y);
            }
            else if (body is UIMeshRenderer)
            {
                (body as UIMeshRenderer).SetUV(uvCtrl.propertyName, v);
            }
        }

        void Evalute(out Vector3 v, float t)
        {
            if (useEase)
                v = Vector3.LerpUnclamped(vectorCtrl.from, vectorCtrl.to, t);
            else
            {
                v.x = Mathf.LerpUnclamped(vectorCtrl.from.x, vectorCtrl.to.x, curves[0].Evaluate(t));
                v.y = Mathf.LerpUnclamped(vectorCtrl.from.y, vectorCtrl.to.y, curves[1].Evaluate(t));
                v.z = Mathf.LerpUnclamped(vectorCtrl.from.z, vectorCtrl.to.z, curves[2].Evaluate(t));
            }
        }

        void Evalute(out Vector4 v, float t)
        {
            if (useEase)
                v = Vector4.LerpUnclamped(uvCtrl.from, uvCtrl.to, t);
            else
            {
                v.x = Mathf.LerpUnclamped(uvCtrl.from.x, uvCtrl.to.x, curves[0].Evaluate(t));
                v.y = Mathf.LerpUnclamped(uvCtrl.from.y, uvCtrl.to.y, curves[1].Evaluate(t));
                v.z = Mathf.LerpUnclamped(uvCtrl.from.z, uvCtrl.to.z, curves[2].Evaluate(t));
                v.w = Mathf.LerpUnclamped(uvCtrl.from.w, uvCtrl.to.w, curves[3].Evaluate(t));
            }
        }

        void Evalute(out float v, float t)
        {
            if (useEase)
                v = Mathf.LerpUnclamped(floatCtrl.from, floatCtrl.to, t);
            else
                v = Mathf.LerpUnclamped(floatCtrl.from, floatCtrl.to, curves[0].Evaluate(t));
        }

        public void HideColor()
        {
            if (controlType != ControlType.TintColor)
                return;

            if (body is Renderer)
            {
                Renderer r = body as Renderer;
                if (r.material)
                    r.material.SetColor(colorCtrl.colorName, new Color(0, 0, 0, 0));
            }
            else if (body is MaskableGraphic)
            {
                MaskableGraphic r = body as MaskableGraphic;
                if (r.material)
                    r.material.SetColor(colorCtrl.colorName, new Color(0, 0, 0, 0));
            }
            else if (body is UIMeshRenderer)
            {
                UIMeshRenderer r = body as UIMeshRenderer;
                if (r.material)
                    r.material.SetColor(colorCtrl.colorName, new Color(0, 0, 0, 0));
            }
        }
    }

    [System.Serializable]
    public class Vector3Control
    {
        public Vector3 from;
        public Vector3 to;
        public string followHandler;
    }

    [System.Serializable]
    public class ColorControl
    {
        public string colorName;
        public Gradient gradient;
    }

    [System.Serializable]
    public class UVControl
    {
        public string propertyName;
        public Vector4 from;
        public Vector4 to;
    }

    [System.Serializable]
    public class FloatControl
    {
        public string propertyName;
        public float from;
        public float to;
    }

    [System.Serializable]
    public struct VisableGroup
    {
        public float delay;
        public bool visible;
    }

    [System.Serializable]
    public class Avatar
    {
        public float delay;
        public Vector3 position;
        public Vector3 rotation;
        public Vector3 scale = Vector3.one;
        public float duration = 1;
    }

    public float delay;
    public float[] repeatTimes;
    public Vector3 offset;
    public Vector3 angleOff;
    public Vector3 scale = Vector3.one;
    public float instanceExistTime = 1;
    public Avatar[] avatars;
    public string animationPath;

    private bool isBase = true;
    public bool IsBase
    {
        get { return isBase; }
        set { isBase = value; }
    }
    public bool isOriginal = true;

    public List<TweenGroup> tweens = new List<TweenGroup>();

    private List<Tweener> tweeners = new List<Tweener>();

    private Dictionary<Transform, Vector3> posOrgState;
    private Dictionary<Transform, Vector3> rotOrgState;
    private Dictionary<Transform, Vector3> sclOrgState;
    private Dictionary<GameObject, bool> goOrgState;

    private Vector3 orgPos = Vector3.zero;
    private Vector3 orgRot = Vector3.zero;
    private Vector3 orgScl = Vector3.one;

    private void OnEnable()
    {
        RePlay();
    }

    private static int Compare(TweenGroup a, TweenGroup b)
    {
        return a.delay.CompareTo(b.delay);
    }

    private void OnDisable()
    {
        for (int i = 0; i < tweeners.Count; i++)
        {
            tweeners[i].Kill();
            tweeners.RemoveAt(i--);
        }
    }

    public void RePlay()
    {
        ResetOriginalState();
        if (IsBase)
        {
            orgPos = transform.localPosition;
            orgRot = transform.localEulerAngles;
            orgScl = transform.localScale;
            tweens.Sort(Compare);
        }
        Delay(delay, Play);
    }

    private void Play()
    {
        for (int i = 0; i < tweens.Count; i++)
        {
            TweenGroup tg = tweens[i];

            if (!tg.body)
                continue;

            switch (tg.controlType)
            {
                case ControlType.Position:
                    {
                        if (tg.vectorCtrl == null)
                            continue;

                        Transform tr = tg.body as Transform;

                        if (posOrgState == null)
                            posOrgState = new Dictionary<Transform, Vector3>();
                        if (!posOrgState.ContainsKey(tr))
                            posOrgState.Add(tr, tr.localPosition);

                        DoValue(tg);
                        break;
                    }
                case ControlType.Rotation:
                    {
                        if (tg.vectorCtrl == null)
                            continue;

                        Transform tr = tg.body as Transform;

                        if (rotOrgState == null)
                            rotOrgState = new Dictionary<Transform, Vector3>();
                        if (!rotOrgState.ContainsKey(tr))
                            rotOrgState.Add(tr, tr.localEulerAngles);

                        DoValue(tg);
                        break;
                    }
                case ControlType.Scale:
                    {
                        if (tg.vectorCtrl == null)
                            continue;

                        Transform tr = tg.body as Transform;

                        if (sclOrgState == null)
                            sclOrgState = new Dictionary<Transform, Vector3>();
                        if (!sclOrgState.ContainsKey(tr))
                            sclOrgState.Add(tr, tr.localScale);

                        DoValue(tg);
                        break;
                    }              
                case ControlType.TintColor:
                    {
                        if (tg.colorCtrl == null)
                            continue;

                        DoColor(tg);
                        break;
                    }
                case ControlType.UV:
                    {
                        if (tg.uvCtrl == null)
                            continue;

                        DoValue(tg);
                        break;
                    }
                case ControlType.SequenceTex:
                    {
                        if (tg.uvCtrl == null)
                            continue;

                        DoTexSequence(tg);
                        break;
                    }
                case ControlType.ShaderValue:
                    {
                        if (tg.floatCtrl == null)
                            continue;

                        DoValue(tg);
                        break;
                    }
                case ControlType.Visible:
                    {
                        if (tg.visibles == null)
                            continue;

                        GameObject go = tg.body as GameObject;

                        if (goOrgState == null)
                            goOrgState = new Dictionary<GameObject, bool>();
                        if (!goOrgState.ContainsKey(go))
                            goOrgState.Add(go, go.activeSelf);

                        DoVisible(tg);
                        break;
                    }
                case ControlType.ProjectorPlane:
                    {
                        if (tg.visibles == null)
                            continue;

                        DoProjector(tg);
                        break;
                    }
            }
        }

        if (IsBase)
        {
            SpawnRepeatSimple();
            SpawnAvatar();
        }
    }

    #region 控制变化

    private void DoValue(TweenGroup tg)
    {
        tweeners.Add(DOTween.To(tg.SetValue, 0f, 1f, tg.duration)
            .SetDelay(tg.delay)
            .SetEase(tg.useEase ? tg.ease : Ease.Linear)
            .SetLoops(tg.loop, tg.loopType));
    }

    private void DoColor(TweenGroup tg)
    {
        tweeners.Add(DOTween.To(tg.SetValue, 0f, 1f, tg.duration)
            .SetDelay(tg.delay)
            .SetEase(Ease.Linear)
            .SetLoops(tg.loop, tg.loopType)
            .OnKill(tg.HideColor));
    }

    private void DoTexSequence(TweenGroup tg)
    {
        int xCount = (int)(1 / tg.uvCtrl.from.x + 0.5f);
        int totalCount = xCount * (int)(1 / tg.uvCtrl.from.y + 0.5f);

        if (xCount != 0 && totalCount != 0)
        {
            tg.SetValue(new Vector4(tg.uvCtrl.from.x, tg.uvCtrl.from.y, 0, 1 - tg.uvCtrl.from.y));
            Delay(tg.delay, delegate { DoTexSequence(tg, xCount, totalCount, tg.duration / totalCount, 1, tg.loop); });
        }          
    }

    private void DoTexSequence(TweenGroup tg, int xCount, int totalCount, float intervalTime, int focus, int loop = -1)
    {      
        if (focus == totalCount && loop > 0)
            loop--;

        if (loop != 0)
        {
            Delay(intervalTime, delegate
            {
                focus %= totalCount;
                tg.SetValue(new Vector4(tg.uvCtrl.from.x, tg.uvCtrl.from.y, focus % xCount * tg.uvCtrl.from.x, 1 - (1 + focus / xCount) * tg.uvCtrl.from.y));
                DoTexSequence(tg, xCount, totalCount, intervalTime, ++focus, loop);
            });
        }
    }

    private void DoVisible(TweenGroup tg)
    {
        GameObject go = tg.body as GameObject;
        for (int i = 0; i < tg.visibles.Length; i++)
        {
            VisableGroup vg = tg.visibles[i];
            Delay(vg.delay, delegate
            {
                if (vg.visible)
                {
                    ParticleSystem[] pss = go.GetComponentsInChildren<ParticleSystem>(true);
                    for (int p = 0; p < pss.Length; p++)
                        pss[p].Clear();
                    TrailRenderer[] trs = go.GetComponentsInChildren<TrailRenderer>(true);
                    for (int l = 0; l < trs.Length; l++)
                        trs[l].Clear();
                }
                go.SetActive(vg.visible);
            });
        }
    }

    private void DoProjector(TweenGroup tg)
    {
        ProjectorPlane pp = tg.body as ProjectorPlane;

        Delay(tg.delay, delegate
        {
            if (tg.duration == 0)
                pp.Projector();
            else if (tg.duration == -1)
                tweeners.Add(DOTween.To(delegate (float t) { pp.Projector(); }, 0, 1, 1).SetLoops(-1));
            else
                tweeners.Add(DOTween.To(delegate (float t) { pp.Projector(); }, 0, 1, tg.duration));
        });
    }
    #endregion

    #region 控制复生
    private void SpawnRepeatSimple()
    {
        if (repeatTimes != null)
            for (int i = 0; i < repeatTimes.Length; i++)
                SpawnRepeatSimple(repeatTimes[i], i + 1);
    }

    private void SpawnRepeatSimple(float delay, int index)
    {
        Delay(Mathf.Max(0.01f, delay), delegate
        {
            if (!IsBase)
                return;

            GameObject ins = Instantiate(this.gameObject);
            ins.transform.SetParent(transform.parent);
            ins.transform.localScale = new Vector3()
            {
                x = orgScl.x * Mathf.Pow(scale.x, index),
                y = orgScl.y * Mathf.Pow(scale.y, index),
                z = orgScl.z * Mathf.Pow(scale.z, index)
            };
            ins.transform.localPosition = orgPos + offset * index;
            ins.transform.localEulerAngles = orgRot + angleOff * index;
            EffectControlHelper ech = ins.GetComponent<EffectControlHelper>();
            ech.IsBase = false;

            Delay(ech.delay + instanceExistTime, delegate { DeepCore.Unity3D.UnityHelper.Destroy(ins); }, delegate { DeepCore.Unity3D.UnityHelper.Destroy(ins); });
        });
    }

    private void SpawnAvatar()
    {
        if (avatars != null)
            for (int i = 0; i < avatars.Length; i++)
                SpawnAvatar(avatars[i]);
    }

    private void SpawnAvatar(Avatar avatar)
    {
        Delay(Mathf.Max(0.01f, avatar.delay), delegate
        {
            if (!IsBase)
                return;

            GameObject ins = Instantiate(this.gameObject);
            ins.transform.SetParent(transform.parent);
            ins.transform.localPosition = avatar.position;
            ins.transform.localEulerAngles = avatar.rotation;
            ins.transform.localScale = avatar.scale;
            EffectControlHelper ech = ins.GetComponent<EffectControlHelper>();
            ech.IsBase = false;
            Delay(ech.delay + avatar.duration, delegate { DeepCore.Unity3D.UnityHelper.Destroy(ins); }, delegate { DeepCore.Unity3D.UnityHelper.Destroy(ins); });
        });
    }
    #endregion

    private static void EmptySet(float v)
    {

    }

    private void Delay(float time, TweenCallback onComplete, TweenCallback onKill = null)
    {
        if (time > 0)
            tweeners.Add(
                DOTween.To(EmptySet, 0, time, time)
                .SetEase(Ease.Linear)
                .OnComplete(onComplete)
                .OnKill(onKill));
        else onComplete();
    }

    public void ResetOriginalState()
    {
        if (posOrgState != null)
            foreach (KeyValuePair<Transform, Vector3> state in posOrgState)
            {
                if (state.Key)
                    state.Key.localPosition = state.Value;
            }

        if (rotOrgState != null)
            foreach (KeyValuePair<Transform, Vector3> state in rotOrgState)
            {
                if (state.Key)
                    state.Key.localEulerAngles = state.Value;
            }

        if (sclOrgState != null)
            foreach (KeyValuePair<Transform, Vector3> state in sclOrgState)
            {
                if (state.Key)
                    state.Key.localScale = state.Value;
            }

        if (goOrgState != null)
            foreach (KeyValuePair<GameObject, bool> state in goOrgState)
            {
                if (state.Key)
                    state.Key.SetActive(state.Value);
            }
    }

    public void BakeToClip(System.Action<AnimationClip> clipCallback)
    {
        AnimationClip clip = new AnimationClip();
        clip.frameRate = 30;

        List<TweenGroup> longTimeTweens = new List<TweenGroup>(tweens);
        longTimeTweens.Sort((a, b) => (b.delay + b.duration).CompareTo(a.delay + a.duration));
        float waitTime = longTimeTweens[0].delay + longTimeTweens[0].duration;

        OnDisable();
        OnEnable();

        Delay(delay, () => 
        {
            for (int i = 0; i < tweens.Count; i++)
            {
                TweenGroup tween = tweens[i];

                switch (tween.controlType)
                {
                    case ControlType.Position:
                        {
                            Transform tr = tween.body as Transform;
                            string relativePath = GetRelativePath(tr);

                            RecordCurve(tween, new AnimationCurve(), tween.delay + tween.duration, () => tr.localPosition.x,
                                (cx) => clip.SetCurve(relativePath, typeof(Transform), "localPosition.x", cx));
                            RecordCurve(tween, new AnimationCurve(), tween.delay + tween.duration, () => tr.localPosition.y,
                                (cy) => clip.SetCurve(relativePath, typeof(Transform), "localPosition.y", cy));
                            RecordCurve(tween, new AnimationCurve(), tween.delay + tween.duration, () => tr.localPosition.z,
                                (cz) => clip.SetCurve(relativePath, typeof(Transform), "localPosition.z", cz));
                            break;
                        }
                    case ControlType.Rotation:
                        {
                            Transform tr = tween.body as Transform;
                            string relativePath = GetRelativePath(tr);

                            RecordCurve(tween, new AnimationCurve(), tween.delay + tween.duration, () => tr.localEulerAngles.x,
                                (cx) => clip.SetCurve(relativePath, typeof(Transform), "localEulerAngles.x", cx));
                            RecordCurve(tween, new AnimationCurve(), tween.delay + tween.duration, () => tr.localEulerAngles.y,
                                (cy) => clip.SetCurve(relativePath, typeof(Transform), "localEulerAngles.y", cy));
                            RecordCurve(tween, new AnimationCurve(), tween.delay + tween.duration, () => tr.localEulerAngles.z,
                                (cz) => clip.SetCurve(relativePath, typeof(Transform), "localEulerAngles.z", cz));
                            break;
                        }
                    case ControlType.Scale:
                        {
                            Transform tr = tween.body as Transform;
                            string relativePath = GetRelativePath(tr);

                            RecordCurve(tween, new AnimationCurve(), tween.delay + tween.duration, () => tr.localScale.x,
                                (cx) => clip.SetCurve(relativePath, typeof(Transform), "localScale.x", cx));
                            RecordCurve(tween, new AnimationCurve(), tween.delay + tween.duration, () => tr.localScale.y,
                                (cy) => clip.SetCurve(relativePath, typeof(Transform), "localScale.y", cy));
                            RecordCurve(tween, new AnimationCurve(), tween.delay + tween.duration, () => tr.localScale.z,
                                (cz) => clip.SetCurve(relativePath, typeof(Transform), "localScale.z", cz));
                            break;
                        }
                    case ControlType.TintColor:
                        {
                            if (tween.body is Renderer)
                            {
                                Renderer rd = tween.body as Renderer;
                                string relativePath = GetRelativePath(rd.transform);

                                RecordCurve(tween, new AnimationCurve(), tween.delay + tween.duration,
                                    () => rd.material.GetColor(tween.colorCtrl.colorName).r,
                                    (cr) => clip.SetCurve(relativePath, typeof(Renderer), "material." + tween.colorCtrl.colorName + ".r", cr));
                                RecordCurve(tween, new AnimationCurve(), tween.delay + tween.duration,
                                    () => rd.material.GetColor(tween.colorCtrl.colorName).g,
                                    (cg) => clip.SetCurve(relativePath, typeof(Renderer), "material." + tween.colorCtrl.colorName + ".g", cg));
                                RecordCurve(tween, new AnimationCurve(), tween.delay + tween.duration,
                                    () => rd.material.GetColor(tween.colorCtrl.colorName).b,
                                    (cb) => clip.SetCurve(relativePath, typeof(Renderer), "material." + tween.colorCtrl.colorName + ".b", cb));
                                RecordCurve(tween, new AnimationCurve(), tween.delay + tween.duration,
                                    () => rd.material.GetColor(tween.colorCtrl.colorName).a,
                                    (ca) => clip.SetCurve(relativePath, typeof(Renderer), "material." + tween.colorCtrl.colorName + ".a", ca));
                            }
                            break;
                        }
                    case ControlType.UV:
                        {
                            if (tween.body is Renderer)
                            {
                                Renderer rd = tween.body as Renderer;
                                string relativePath = GetRelativePath(rd.transform);

                                RecordCurve(tween, new AnimationCurve(), tween.delay + tween.duration,
                                    () => rd.material.GetColor(tween.uvCtrl.propertyName).r,
                                    (cx) => clip.SetCurve(relativePath, typeof(Renderer), "material." + tween.uvCtrl.propertyName + ".x", cx));
                                RecordCurve(tween, new AnimationCurve(), tween.delay + tween.duration,
                                    () => rd.material.GetColor(tween.uvCtrl.propertyName).g,
                                    (cy) => clip.SetCurve(relativePath, typeof(Renderer), "material." + tween.uvCtrl.propertyName + ".y", cy));
                                RecordCurve(tween, new AnimationCurve(), tween.delay + tween.duration,
                                    () => rd.material.GetColor(tween.uvCtrl.propertyName).b,
                                    (cz) => clip.SetCurve(relativePath, typeof(Renderer), "material." + tween.uvCtrl.propertyName + ".z", cz));
                                RecordCurve(tween, new AnimationCurve(), tween.delay + tween.duration,
                                    () => rd.material.GetColor(tween.uvCtrl.propertyName).a,
                                    (cw) => clip.SetCurve(relativePath, typeof(Renderer), "material." + tween.uvCtrl.propertyName + ".w", cw));
                            }
                            break;
                        }
                    case ControlType.SequenceTex:
                        {
                            if (tween.body is Renderer)
                            {
                                Renderer rd = tween.body as Renderer;
                                string relativePath = GetRelativePath(rd.transform);

                                RecordCurve(tween, new AnimationCurve(), tween.delay + tween.duration,
                                    () => rd.material.GetTextureScale(tween.uvCtrl.propertyName).x,
                                    (cx) => clip.SetCurve(relativePath, typeof(Renderer), "material." + tween.uvCtrl.propertyName + "_ST.x", cx), true);
                                RecordCurve(tween, new AnimationCurve(), tween.delay + tween.duration,
                                    () => rd.material.GetTextureScale(tween.uvCtrl.propertyName).y,
                                    (cy) => clip.SetCurve(relativePath, typeof(Renderer), "material." + tween.uvCtrl.propertyName + "_ST.y", cy), true);
                                RecordCurve(tween, new AnimationCurve(), tween.delay + tween.duration,
                                    () => rd.material.GetTextureOffset(tween.uvCtrl.propertyName).x,
                                    (cz) => clip.SetCurve(relativePath, typeof(Renderer), "material." + tween.uvCtrl.propertyName + "_ST.z", cz), true);
                                RecordCurve(tween, new AnimationCurve(), tween.delay + tween.duration,
                                    () => rd.material.GetTextureOffset(tween.uvCtrl.propertyName).y,
                                    (cw) => clip.SetCurve(relativePath, typeof(Renderer), "material." + tween.uvCtrl.propertyName + "_ST.w", cw), true);
                            }
                            break;
                        }
                    case ControlType.ShaderValue:
                        {
                            Renderer rd = tween.body as Renderer;
                            string relativePath = GetRelativePath(rd.transform);

                            RecordCurve(tween, new AnimationCurve(), tween.delay + tween.duration,
                                () => rd.material.GetFloat(tween.floatCtrl.propertyName),
                                (cv) => clip.SetCurve(relativePath, typeof(Renderer), "material." + tween.floatCtrl.propertyName, cv));
                            break;
                        }
                    case ControlType.Visible:
                        {
                            GameObject go = tween.body as GameObject;
                            string relativePath = GetRelativePath(go.transform);

                            RecordCurve(tween, new AnimationCurve(), tween.delay + tween.duration,
                                () => go.activeSelf ? 1 : 0,
                                (cv) => clip.SetCurve(relativePath, typeof(GameObject), "m_IsActive", cv), true);
                            break;
                        }
                }
            }

            Delay(waitTime + 1, () => clipCallback(clip));
        });
    }

    private string GetRelativePath(Transform tr)
    {
        string relativePath = "";
        if (tr != transform)
        {
            relativePath = tr.name;
            while (tr.parent != transform)
            {
                tr = tr.parent;
                relativePath = string.Format("{0}/{1}", tr.name, relativePath);
            }
        }
        return relativePath;
    }

    private void RecordCurve(TweenGroup tween, AnimationCurve curve, float duration, System.Func<float> value, System.Action<AnimationCurve> result, bool constant = false)
    {
        int frame = (int)(duration * 30 + 0.5f);
        for (int count = 0; count <= frame; count++)
        {
            float time = Mathf.Lerp(0, duration, (float)count / frame);
            Delay(time, () =>
            {
                Keyframe key = new Keyframe(time, value());
                key.tangentMode = constant ? 103 : 34;//103 is Constant , 34 is Auto
                if (curve.keys.Length > 1)
                {
                    key.inTangent = constant ? float.PositiveInfinity : (value() - curve.keys[curve.keys.Length - 1].value) / (time - curve.keys[curve.keys.Length - 1].time);
                    key.outTangent = constant ? float.PositiveInfinity : (value() - curve.keys[curve.keys.Length - 1].value) / (time - curve.keys[curve.keys.Length - 1].time);
                }
                curve.AddKey(key);
            });
        }

        Delay(duration + 0.5f, () => result(curve));
    }
}