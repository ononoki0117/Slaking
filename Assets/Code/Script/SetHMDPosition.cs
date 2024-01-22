using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetHMDPosition : MonoBehaviour
{
    public GameObject LeftEyeSurface;
    public GameObject RightEyeSurface;
    public GameObject Head;

    private Vector3 cameraPosition;
    // Start is called before the first frame update
    void Start()
    {
        cameraPosition = (LeftEyeSurface.transform.position + RightEyeSurface.transform.position) / 2;
        transform.position = cameraPosition;
    }

    // Update is called once per frame
    void Update()
    {
        cameraPosition = (LeftEyeSurface.transform.position + RightEyeSurface.transform.position) / 2;
        transform.position = cameraPosition;
        Head.transform.rotation = transform.rotation;
    }
}
