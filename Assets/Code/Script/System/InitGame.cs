using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitGame : MonoBehaviour
{
    private void Awake()
    {
        if (Display.displays.Length > 1)
        {
            Display.displays[1].Activate();
        }
        //GameManager.ChangeState("TITLE");
    }
}
