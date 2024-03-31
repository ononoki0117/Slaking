using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadyButton : MonoBehaviour
{
   public static void OnClickReadyButton()
    {
        GameManager.ChangeState(STATE.TUTORIAL);
    }
}
