using UnityEngine;
using System.Collections;

public class BillBoardUICamera : MonoBehaviour 
{
    void Start()
    {

    }

    void LateUpdate()
    {

        if (UGUIMgr.UGUICamera != null)
        {
            transform.LookAt(UGUIMgr.UGUICamera.transform);
        }
 
    }


}
