using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TitleSceneManager : MonoBehaviour
{
    [SerializeField] private GameObject TitleLogo;
    [SerializeField] private GameObject Character;
    private bool triggerValue;
    private UnityEngine.XR.InputDevice LeftHandDevice;
    private UnityEngine.XR.InputDevice RightHandDevice;

    private void Awake()
    {
        var inputDevices = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevices(inputDevices);

        var leftHandDevices = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevicesAtXRNode(UnityEngine.XR.XRNode.LeftHand, leftHandDevices);

        var rightHandDevices = new List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevicesAtXRNode(UnityEngine.XR.XRNode.RightHand, rightHandDevices);


        if (leftHandDevices.Count == 1)
        {
            LeftHandDevice = leftHandDevices[0];
        }

        if (rightHandDevices.Count == 1)
        {
            RightHandDevice = rightHandDevices[0];
        }

        GameManager.HadEncore = false;

        GameManager.ToDemo += delegate () { StartCoroutine(PlayDemo()); };
        GameManager.ToTitle += delegate () { StartCoroutine(PlayTitle()); };
    }

    private void Start()
    {
        StartCoroutine(PlayTitle());
    }

    IEnumerator PlayDemo()
    {
        Debug.Log("TitleSceneManager : Enter Demo");
        TitleLogo.SetActive(false);
        Character.SetActive(true);

        yield return new WaitForSeconds(2f);

        Debug.Log("TitleSceneManager : Play Demo");
        Character.GetComponent<Animator>().Play("SoregaZenbu_Dance", -1, 0);
        
        while (true)
        {
            if (Input.anyKey)
            {
                break;
            }
            yield return null;
        }

        GameManager.ChangeState(STATE.TITLE);
        yield break;
    }

    IEnumerator PlayTitle()
    {
        Debug.Log("TitleSceneManager : Enter Title");
        TitleLogo.SetActive(true);
        Character.SetActive(false);

        yield return new WaitForSeconds(2f);

        Debug.Log("TitleSceneManager : Start Checking Key Input");

        bool leftTrigger;
        bool rightTrigger;

        float timer = 0;
        while (true)
        {
            timer += Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.Space) 
                || (LeftHandDevice.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out leftTrigger) && leftTrigger)
                || (RightHandDevice.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out rightTrigger) && rightTrigger))
            {
                AudioManager.Instance.PlaySFX(SFX.Start);
                yield return new WaitForSeconds(2f);
                GameManager.ChangeState(STATE.WEARING);
                yield break;
            }
            if (timer > 10f)
            {
                GameManager.ChangeState(STATE.DEMO);
                yield break;
            }
            yield return null;
        }
    }

}
