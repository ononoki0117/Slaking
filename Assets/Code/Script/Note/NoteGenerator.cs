using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class NoteGenerator : MonoBehaviour
{
    [SerializeField] private Metronome metronome;
    [SerializeField] private Sheet sheet;

    public Transform GeneratePoint;
    public Transform TimeWindowPoint;

    private uint noteGenerateTime_MS;
    private uint noteOffset_MS = 2000;

    [SerializeField]
    private long nextMetromoneLineTime_PCM = 0;
    private uint startDelay_MS;

    public Queue<Tuple<NoteData, GameObject>> notesOnDisplay;

    /// <summary>
    /// 첫번째 메트로놈 노트의 이상 수정 해야함
    /// </summary>
    private void Awake()
    {
        metronome = FindAnyObjectByType<Metronome>();
        GeneratePoint.transform.position = GetGeneratorPosition(MetronomeLineObject.speed);
        
        notesOnDisplay = new Queue<Tuple<NoteData, GameObject>>();
    }

    private void Start()
    {
        sheet = Parser.GetSheet();
        sheet.PrintInfo();

        startDelay_MS = sheet.offset;
        nextMetromoneLineTime_PCM = ((long)startDelay_MS - (long)noteOffset_MS) * (long)metronome.frequency / 1000;

    }

    private void Update()
    {
        // Generate Metronome Line
        if (metronome.currentTime_PCM >= nextMetromoneLineTime_PCM)
        {
            if (nextMetromoneLineTime_PCM >= 0)
                MakeMetronomeLine();
            nextMetromoneLineTime_PCM += metronome.intervalLength_PCM;
        }

        // Generate Spectator Note
        noteGenerateTime_MS = metronome.currentTime_MS + noteOffset_MS;

        if (sheet.spectatorQueue.TryPeek(out var data))
        {
            if (data.position <= noteGenerateTime_MS)
            {
                GameObject note = MakeKeyDownNote();
                NoteData noteData = sheet.spectatorQueue.Dequeue();

                Debug.Log("note Generated" + data.position + " " + metronome.currentTime_MS);

                Tuple<NoteData, GameObject> t = Tuple.Create(noteData, note);
                notesOnDisplay.Enqueue(t);
            }
        }

        if (notesOnDisplay.TryPeek(out var ondisplayNote))
        {
            NoteData noteData = ondisplayNote.Item1;
            GameObject gameObject = ondisplayNote.Item2;

            
            if (gameObject.transform.position.x >= Player.Instance.MissPoint.position.x)
            {
                notesOnDisplay.Dequeue();
                Debug.Log("note passed time window" + noteData.position + " " + metronome.currentTime_MS);
            }
        }
    }

    private Vector3 GetGeneratorPosition(float speed)
    {
        return new Vector3(TimeWindowPoint.position.x - noteOffset_MS / 1000 * speed, 0, 0);
    }

    public void MakeMetronomeLine()
    {
        var note_metronome = NoteObjectPool.instance.GetGameObject("MetronomeLine");
     
        note_metronome.transform.position = this.GeneratePoint.position;
    }

    public GameObject MakeKeyDownNote()
    {
        var note = NoteObjectPool.instance.GetGameObject("KeydownNote");

        note.transform.position = this.GeneratePoint.position;


        return note;
    }
}
