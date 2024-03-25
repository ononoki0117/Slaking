using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebcamSwitcher : MonoBehaviour
{
    public GameObject Webcam;
    public GameObject Video;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.W))
        {
            if (Webcam.activeSelf)
            {
                Webcam.SetActive(false);
                Video.SetActive(true);
            }
            else
            {
                Webcam.SetActive(true);
                Video.SetActive(false);
            }
        }
    }
}
