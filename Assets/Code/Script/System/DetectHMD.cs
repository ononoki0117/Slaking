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
    private bool IsFoundHMD = false;

    private void Start()
    {
        StartCoroutine(HMDDetection());
    }


    private void Update()
    {
        // ����� HMD�� Detect�ϴ� ���� �ƴ� �׳� ����;;;
        if (Input.anyKeyDown)
        {
            LoadingManager.LoadScene("Wearing");
        }
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
