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
                // �տ� Tpose������ �ϵ��� ���� ���
            case STATE.TUTORIAL:
                // Ʃ�丮�� ���� ���
            case STATE.SELECT_MUSIC:
                // ��Ʈ�ѷ��� ���̽�ƽ�� ���� �� UI �� ����
            case STATE.GAME:
                // ���� ���� ��� 
            case STATE.COMMUNICATION:
                // ��ķ ���
            case STATE.RESULT:
                // ��� ȭ��?
            case STATE.REQUEST_ENCORE:
                // ���ڸ� ��û 
                break;
        }
    }

    private void WearingEvent()
    {
        Debug.Log("Wearing Video Play");
    }
}
