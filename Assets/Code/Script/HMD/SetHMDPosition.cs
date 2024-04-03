using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetHMDPosition : MonoBehaviour
{
    public Transform HMDtransform;
    public GameObject Head;
    public bool isVrActive = false;

    void Start()
    {
        if(HMDtransform != null)
        {
           isVrActive = true;
           transform.position = HMDtransform.position;
        }
    }

    void Update()
    {
        if (isVrActive)
        {
            transform.position = HMDtransform.position;
            //Head.transform.rotation = transform.rotation;
        }
    }
}
