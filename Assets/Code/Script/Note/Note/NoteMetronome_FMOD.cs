using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteMetronome_FMOD : NoteObject
{
    [SerializeField]
    static Metronome metronome;
    [SerializeField]
    float speed = 6f;
    [SerializeField]
    const float missingPoint = 8.5f;

    private void Awake()
    {
        metronome = FindAnyObjectByType<Metronome>();
    }

    override public bool IsMissed()
    {
        if (this.transform.position.x >= missingPoint)
        {
            return true;
        }

        return false;
    }

    override public void MoveObject()
    {
        this.transform.Translate(Vector3.right * this.speed * (float)metronome.deltaTime_PCM);
    }
}
