using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.CullingGroup;

public class StageManager : MonoBehaviour
{
    void Awake()
    {
        GameManager.ToWearing += WearingEvent;
    }

    private void Start()
    {
        if (GameManager.IsStateChanged)
        {
            GameManager.IsStateChanged = false;
            try
            {
                GameManager.UpdateState(GameManager.CURRENT_STATE);
                Debug.Log("StageManager : Start : Update State");
            }
            catch (System.Exception)
            {
                Debug.Log("StageManager : Start : No Event Exist");
                Debug.Log(GameManager.CURRENT_STATE.ToString());
            }
        }
    }

    void Update()
    {
        //switch (GameManager.NEXT_STATE)
        //{
        //    case STATE.WEARING:
        //        // �տ� Tpose������ �ϵ��� ���� ���
        //    case STATE.TUTORIAL:
        //        // Ʃ�丮�� ���� ���
        //    case STATE.SELECT_MUSIC:
        //        // ��Ʈ�ѷ��� ���̽�ƽ�� ���� �� UI �� ����
        //    case STATE.GAME:
        //        // ���� ���� ��� 
        //    case STATE.COMMUNICATION:
        //        // ��ķ ���
        //    case STATE.RESULT:
        //        // ��� ȭ��?
        //    case STATE.REQUEST_ENCORE:
        //        // ���ڸ� ��û 
        //        break;
        //}
    }

    private void WearingEvent()
    {
        Debug.Log("Wearing Video Play");
    }
}
