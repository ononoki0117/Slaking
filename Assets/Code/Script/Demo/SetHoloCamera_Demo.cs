using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetHoloCamera_Demo : MonoBehaviour
{
    [Header("Target Object")]
    [SerializeField]
    private GameObject Target;

    [Header("Camera Smoothness")]
    [SerializeField]
    [Range(0f, 1f)]
    private float CameraSmoothness;

    [Header("Camera Velocity")]
    [SerializeField]
    private Vector3 CameraVelocity = Vector3.zero;

    [Header("Camera Offset")]
    [SerializeField]
    private Vector3 CameraOffset;

    private void Update()
    {
        Vector3 target_position = Target.transform.position + CameraOffset;
        transform.position = Vector3.SmoothDamp(transform.position, target_position, ref CameraVelocity, CameraSmoothness);
    }
}
