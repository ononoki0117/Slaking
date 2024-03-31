using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    void Awake()
    {
        GameManager.ToWearing += WearingEvent;
    }

    void Update()
    {
        switch (GameManager.NEXT_STATE)
        {
            case STATE.WEARING:
                // 앞에 Tpose동작을 하도록 비디오 재생
            case STATE.TUTORIAL:
                // 튜토리얼 비디오 재생
            case STATE.SELECT_MUSIC:
                // 컨트롤러의 조이스틱을 통해 앞 UI 곡 선택
            case STATE.GAME:
                // 게임 비디오 재생 
            case STATE.COMMUNICATION:
                // 웹캠 재생
            case STATE.RESULT:
                // 결과 화면?
            case STATE.REQUEST_ENCORE:
                // 앙코르 요청 
                break;
        }
    }

    private void WearingEvent()
    {
        Debug.Log("Wearing Video Play");
    }
}
