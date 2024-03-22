using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.XR.OpenVR;
using UnityEngine;
using UnityEngine.XR;

/// <summary>
/// 착용시에 게임 진행, 착용하지 않은 상태면 계속 타이틀 스크린에 있도록 하고 싶음...
/// </summary>
public class DetectHMD : MonoBehaviour
{
    


    private void Start()
    {
        StartCoroutine(HMDDetection());
    }

    IEnumerator HMDDetection()
    {
        yield return null;

        //while()
        //{
        //    InputDevices.GetDevices(inputDevices);

        //}
        
        
        while (true)
        {
            var inputDevices = new List<UnityEngine.XR.InputDevice>();
            InputDevices.GetDevices(inputDevices);
            foreach (var device in inputDevices)
            {
                Debug.Log(string.Format("Device found with name'{0}' and role '{1}'", device.name, device.role.ToString()));

            }
            yield return new WaitForSeconds(2f);
        }

    }
}
