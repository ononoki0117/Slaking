using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetHMDPosition : MonoBehaviour
{
    public GameObject LeftEyeSurface;
    public GameObject RightEyeSurface;
    public GameObject Head;
    public bool isVrActive = false;

    private Vector3 cameraPosition;
    // Start is called before the first frame update
    void Start()
    {
        if(LeftEyeSurface != null && RightEyeSurface != null)
        {
            isVrActive = true;
            cameraPosition = (LeftEyeSurface.transform.position + RightEyeSurface.transform.position) / 2;
            transform.position = cameraPosition;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isVrActive)
        {
            cameraPosition = (LeftEyeSurface.transform.position + RightEyeSurface.transform.position) / 2;
            transform.position = cameraPosition;
            Head.transform.rotation = transform.rotation;
        }
    }
}
