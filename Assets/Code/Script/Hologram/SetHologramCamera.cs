using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetHologramCamera : MonoBehaviour
{

    public GameObject targetObject;
    
    public Vector3 offset;

    [Range(0f, 1f)]
    public float smoothness;
    
    private Vector3 velocity = Vector3.zero;
    
    void Update()
    {
        Vector3 targetPosition = targetObject.transform.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothness);
    }
}
