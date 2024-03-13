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
            // GameObject를 NotePool에 돌려주고 NoteData는 finished로 가든 null로 만들든...
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

    public abstract bool IsMissed(); // 일정 위치를 벗어나면 isMissed = true 
    public abstract void MoveObject(); // note의 움직임 제어  

    public bool IsPassedTimeWindow()
    {
        if (this.transform.position.x >= Player.Instance.MissPoint.position.x)
        {
            return true;
        }
        return false;
    }
}
