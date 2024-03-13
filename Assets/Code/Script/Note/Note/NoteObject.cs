using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UIElements;

public abstract class NoteObject : MonoBehaviour
{
    public IObjectPool<GameObject> pool;

    public void ReleaseObject()
    {
        pool.Release(gameObject);
    }

    private void Update()
    {
        if (IsMissed())
        {
            // GameObject�� NotePool�� �����ְ� NoteData�� finished�� ���� null�� �����...
            ReleaseObject();
        }

        if (IsPassedTimeWindow())
        {
            Metronome m = FindAnyObjectByType<Metronome>();
            Debug.Log(m.currentTime_MS);
        }

        MoveObject();
    }

    //public JUDGEMENT Judgement(uint currentTime, uint[] range)
    //{
    //    for (int i = 0; i < range.Length; i++)
    //    {
    //        if (currentTime - range[i] <= data.position && data.position <= currentTime + range[i])
    //            return JUDGEMENT.PERFECT + i;
    //    }

    //    return JUDGEMENT.NONOTE;
    //}

    public abstract bool IsMissed(); // ���� ��ġ�� ����� isMissed = true 
    public abstract void MoveObject(); // note�� ������ ����  

    public bool IsPassedTimeWindow()
    {
        if (this.transform.position.x >= Player.Instance.MissPoint.position.x)
        {
            return true;
        }
        return false;
    }
}
