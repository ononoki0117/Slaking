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
    private void Start()
    {
        StartCoroutine(HMDDetection());
    }


    private void Update()
    {
        // 현재는 HMD를 Detect하는 것이 아닌 그냥 들어가는;;;
        if (Input.anyKeyDown)
        {
            GameManager.ChangeState(STATE.WEARING);
        }
    }

    IEnumerator HMDDetection()
    {
        yield return null;

        while (Input.GetKeyDown(KeyCode.Space))
        {
            if (HMDDetector.isPresent())
            {
                Debug.Log("Move");

                GameManager.ChangeState(STATE.WEARING);

                yield return null;
                break;
            }
            else
            {
                Debug.Log("No HMD Detected!");
            }
            yield return null;
        }
        yield return null;
    }
}
