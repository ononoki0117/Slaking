using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectStart : MonoBehaviour
{
    
    void Update()
    {
        if (Input.anyKeyDown)
        {
            LoadingManager.LoadScene("Wearing");
        }
    }
}
