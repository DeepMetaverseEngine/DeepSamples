using DeepCore;
using UnityEngine;
 

public class BarManager{

	private static BarManager mInstance = null;

    public static BarManager Instance
    {
        get
        {
            if (mInstance == null)
            {
                mInstance = new BarManager();
            }
            return mInstance;
        }
    }

    private HashMap<int,Transform> barMap = new HashMap<int, Transform>();

    public void AddBarShowNode(Transform node,int index)
    {
        if (barMap.ContainsKey(index) == false)
        {
            barMap.Add(index, node);
            SetBarSibilingIndex();
        }
            
    }

    public void SetBarSibilingIndex()
    {
        foreach (int index in barMap.Keys)
        {
            Transform node = barMap.Get(index);
            node.SetSiblingIndex(index);
        }
    }

    public void Clear()
    {
        barMap.Clear();
    }
}
