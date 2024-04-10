using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OuterLEDController_Demo : MonoBehaviour
{
    [Header("LED")]
    [SerializeField]
    private SerialController LEDSerialController;

    void OnMessageArrived(string msg)
    {
        Debug.Log("Message arrived: " + msg);
        LEDSerialController.SendSerialMessage("O");
    }

    void OnConnectionEvent(bool success)
    {
        if (success)
            Debug.Log("Connection established");
        else
            Debug.Log("Connection attempt failed or disconnection detected");
    }
}
