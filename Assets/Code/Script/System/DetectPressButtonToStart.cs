using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPressButtonToStart : MonoBehaviour
{
    
    void Update()
    {
        if (Input.anyKeyDown)
        {
            LoadingManager.LoadScene("Wearing");
        }
    }
}
