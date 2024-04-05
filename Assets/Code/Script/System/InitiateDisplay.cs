using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitiateDisplay : MonoBehaviour
{
    private void Awake()
    {
        if (Display.displays.Length > 1)
        {
            Display.displays[1].Activate();
            Display.displays[2].Activate();
        }
        LoadingManager.nextScene = "Title";
    }
}
