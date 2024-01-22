using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRM;

public class LinkHMD : MonoBehaviour
{
    [SerializeField]
    Camera m_firstPersonCamera;

    [SerializeField]
    LayerMask m_firstPersonMask;

    [SerializeField]
    LayerMask m_otherMask;

    [SerializeField]
    VRMFirstPerson m_firstPerson;

    // Start is called before the first frame update
    void Start()
    {
        m_firstPerson = GameObject.FindAnyObjectByType<VRMFirstPerson>();
    }

    // Update is called once per frame
    void Update()
    {
        var camera = m_firstPersonCamera;

        camera.cullingMask = (camera == m_firstPersonCamera) ? m_firstPersonMask : m_otherMask;


        if (m_firstPerson != null)
            m_firstPerson.Setup();
    }
}
