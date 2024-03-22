using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.XR.OpenVR;
using UnityEngine;
using UnityEngine.XR;

/// <summary>
/// ����ÿ� ���� ����, �������� ���� ���¸� ��� Ÿ��Ʋ ��ũ���� �ֵ��� �ϰ� ����...
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
