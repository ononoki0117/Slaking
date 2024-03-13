using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDownObject : NoteObject
{
    static public float speed = 15f;

    static private Metronome metronome;
    private void Awake()
    {
        metronome = FindAnyObjectByType<Metronome>();
    }

    public override bool IsMissed()
    {
        if (this.transform.position.x >= Player.Instance.MissPoint.position.x)
        {
            //Debug.Log("key down note missed!!");

            return true;
        }

        return false;
    }

    public override void MoveObject()
    {
        this.transform.Translate(Vector3.right * speed * Time.deltaTime);
    }
}
