using UnityEngine;
using System.Collections;

public class BillBoard : MonoBehaviour 
{
    void Start()
    {

    }

    void LateUpdate()
    {

        if (Camera.main != null)
        {
            transform.LookAt(Camera.main.transform);
        }
 
    }


}
