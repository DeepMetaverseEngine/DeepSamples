using UnityEngine;

public class ResetTrailRenderer : MonoBehaviour {

	// Use this for initialization
	void Start () {
        OnEnable();


    }

    // Update is called once per frame
    void Update () {
		
	}

    private void OnEnable()
    {
        var tr = GetComponent<TrailRenderer>();
        if (tr)
        {
            tr.Clear();
        }
    }

    private void OnTransformParentChanged()
    {
        var tr = GetComponent<TrailRenderer>();
        if (tr)
        {
            tr.Clear();
        }
    }


}
