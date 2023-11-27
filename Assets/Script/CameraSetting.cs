using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSetting : MonoBehaviour
{
    // Field for View Matrix
    public int x, y, z = 0;
    // Aspect Ratio Width
    public float ratioWidth = 9f;
    // Aspect Ratio Height
    private const float ratioHeight = 16f;
    // Component's Camera
    private Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        cam.aspect = ratioWidth/ratioHeight;
    }

    // Update is called once per frame
    void Update()
    {
        cam.aspect = ratioWidth / ratioHeight;
    }
}
