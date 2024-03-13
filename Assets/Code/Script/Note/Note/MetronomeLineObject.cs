using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetronomeLineObject : NoteObject
{
    static public float speed = 15f;


    static private Metronome metronome;
    private void Awake()
    {
        metronome = FindAnyObjectByType<Metronome>();
    }

    override public bool IsMissed()
    {
        if (this.transform.position.x >= Player.Instance.MissPoint.position.x)
        {
            return true;
        }

        return false;
    }

    override public void MoveObject()
    {
        this.transform.Translate(Vector3.right * speed * metronome.deltaTime_MS / 1000);
    }
}
