using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SyncRotation : MonoBehaviour
{
    public Transform Head;
    public bool isVrActive = false;

    // Start is called before the first frame update
    void Start()
    {
        if (Head != null)
        {
            isVrActive = true;
            Head.rotation = transform.rotation;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Head.rotation = transform.rotation;
    }
}
