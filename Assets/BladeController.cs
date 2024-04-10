using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeController : MonoBehaviour
{
    public delegate void BladeControllerDelegate();
    public static event BladeControllerDelegate MessageEvent;

    private bool isEnterEncore = false;
    public int ShakeCount = 0;

    public LightSignal signal;


    private void Awake()
    {
        //GameManager.ToCommunication += delegate () {
        //    StartCoroutine(Wait4Shake());
        //};
    }

    IEnumerator Wait4Shake()
    {
        Debug.Log("Wait4Shake");
        //float timer = 0;
        ShakeCount = 0;

        yield return null;

        while (!isEnterEncore)
        {
            //timer += Time.deltaTime;

            //if (ShakeCount >= 5 && !GameManager.HadEncore)
            //{
            //    Debug.Log("Blade Controller : Encore requested");
            //    GameManager.ChangeState(STATE.REQUEST_ENCORE); 
            //    yield break;
            //}

            //if (timer > 10f)
            //{
            //    Debug.Log("Encore faild");
            //    GameManager.ChangeState(STATE.GAMEOVER);
            //    yield break;
            //}

            //GameManager.ChangeState(STATE.GAMEOVER);
            yield return null;
        }
    }

    void OnMessageArrived(string msg)
    {
        
        Debug.Log("Blade Controller : Shake count :" + ShakeCount);
        signal.SendO();
        AudioManager.Instance.PlaySFX(SFX.Call);
        MessageEvent();
        ShakeCount++;
    }

    void OnConnectionEvent(bool success)
    {
        return;
    }
}
