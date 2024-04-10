using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSignal : MonoBehaviour
{
    public SerialController serialController;

    private void Awake()
    {
        MusicManager.markerUpdated += SendI;
    }
    // Start is called before the first frame update
    public void SendI()
    {
        serialController.SendSerialMessage("I");
    }
    public void SendO()
    {
        serialController.SendSerialMessage("O");
    }
}
