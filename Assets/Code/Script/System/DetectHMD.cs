using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.XR;
internal static class HMDDetector
{
    public static bool isPresent()
    {
        var xrDisplaySubsystems = new List<XRDisplaySubsystem>();
        SubsystemManager.GetInstances<XRDisplaySubsystem>(xrDisplaySubsystems);
        foreach (var xrDisplay in xrDisplaySubsystems)
        {
            if (xrDisplay.running)
            {
                return true;
            }
        }
        return false;
    }
}

/// <summary>
/// 착용시에 게임 진행, 착용하지 않은 상태면 계속 타이틀 스크린에 있도록 하고 싶음...
/// </summary>
public class DetectHMD : MonoBehaviour
{
    private bool IsFoundHMD = false;

    private void Start()
    {
        StartCoroutine(HMDDetection());
    }

    IEnumerator HMDDetection()
    {
        yield return null;

        while (!HMDDetector.isPresent())
        {
            Debug.Log("HMD not Detected");
            yield return new WaitForSeconds(2f);
        }
        LoadingManager.LoadScene("Wearing");

        yield return null;
    }
}
