using System.Collections;
using System.Collections.Generic;
using DeepCore.Unity3D;
using UnityEngine;

public class AutoRemoveAnimation: MonoBehaviour {

    private Animator animator;
	
	void Start () {
        animator = GetComponentInChildren<Animator>();
	}
	
	void Update () {
		if (animator != null)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                FuckAssetObject.Unload(gameObject);
            }
        }
	}
}
