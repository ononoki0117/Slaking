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
/// ����ÿ� ���� ����, �������� ���� ���¸� ��� Ÿ��Ʋ ��ũ���� �ֵ��� �ϰ� ����...
/// </summary>
public class DetectHMD : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(HMDDetection());
    }


    private void Update()
    {
        // ����� HMD�� Detect�ϴ� ���� �ƴ� �׳� ����;;;
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
