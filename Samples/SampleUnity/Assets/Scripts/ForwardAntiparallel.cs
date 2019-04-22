using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardAntiparallel : MonoBehaviour
{

    public Transform Target;

    // Use this for initialization
    void Start()
    {
        Target = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Target != null)
        {
            this.transform.forward = -Target.forward;
        }
    }
}
