using System;
using UnityEngine;

public static class UnityExtenstion
{
    class GameObjectDeactivateSection : IDisposable
    {
        GameObject go;
        bool oldState;
        public GameObjectDeactivateSection(GameObject aGo)
        {
            go = aGo;
            oldState = go.activeSelf;
            go.SetActive(false);
        }
        public void Dispose()
        {
            go.SetActive(oldState);
        }
    }

    public static IDisposable Deactivate(this GameObject obj)
    {
        return new GameObjectDeactivateSection(obj);
    }

    // 设置属性, 在Awake执行之前
    public static T AddComponent<T>(this GameObject gameObject, Action<T> action) where T : Component
    {
        using (gameObject.Deactivate())
        {
            T component = gameObject.AddComponent<T>();
            if (action != null) action(component);
            return component;
        }
    }

    public static void SetPivot(this RectTransform rectTransform, Vector2 pivot)
    {
        if (rectTransform == null) return;

        Vector2 size = Vector2.Scale(rectTransform.rect.size, rectTransform.localScale);
        Vector2 deltaPivot = rectTransform.pivot - pivot;
        Vector3 deltaPosition = new Vector3(deltaPivot.x * size.x, deltaPivot.y * size.y);
        rectTransform.pivot = pivot;
        rectTransform.localPosition -= deltaPosition;
    }
}