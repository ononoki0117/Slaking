using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetHeadRotation : MonoBehaviour
{
    public Camera HMD;

    private void LateUpdate()
    {
        transform.rotation = HMD.transform.rotation; 
    }
}
