using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Metronome : MonoBehaviour
{
    [SerializeField] private short signatureHI;
    [SerializeField] private short signatureLO;
    [SerializeField] private uint startDelay; // MS
    [SerializeField] private uint BPM;
    [SerializeField] public float frequency { get; private set; }

    private MusicPlayer music;
    private bool tickSound { get; set; }

    [SerializeField] public uint currentTime_PCM = 0;
    [SerializeField] public uint currentTime_MS = 0;

    [SerializeField] public uint previousTime_PCM = 0;
    [SerializeField] public uint previousTime_MS = 0;

    [SerializeField] public float deltaTime_PCM = 0;
    [SerializeField] public float deltaTime_MS = 0;

    [SerializeField] private short currentBeat;

    [SerializeField] public uint intervalLength_PCM { get; private set; }
    [SerializeField] public uint intervalLength_MS { get; private set; }
    [SerializeField] public uint nextMetronomeTime_PCM { get; private set; }
    [SerializeField] public uint nextMetronomeTime_MS { get; private set; }

    private void Awake()
    {
        Sheet sheet = Parser.Load("SongInfo/Kanade");

        tickSound = true;
        music = GetComponent<MusicPlayer>();

        signatureHI = sheet.signatureHI;
        signatureLO = sheet.signatureLO;

        startDelay = sheet.offset;
        BPM = sheet.BPM;

        currentBeat = signatureHI;
    }

    private void Start()
    {
        nextMetronomeTime_PCM = startDelay * (uint)frequency / 1000;
        nextMetronomeTime_MS = startDelay;

        intervalLength_PCM = 60 * (uint)frequency / BPM;
        intervalLength_MS = 60000 / BPM;

        currentTime_PCM = music.GetTime_PCM();
        currentTime_MS = music.GetTime_MS();
    }

    public void SetFrequency(float _frequency)
    {
        frequency = _frequency;
    }

    
    public void Tick()
    {
        currentBeat--;
        if (currentBeat == 0) currentBeat = signatureHI;

        //if (tickSound)
        //{
        //    AudioManager.Instance.PlaySFX(SFX.Metronome);   
        //}
    }



    private void Update()
    {   
        currentTime_PCM = music.GetTime_PCM();
        currentTime_MS = music.GetTime_MS();

        deltaTime_PCM = ((float)(currentTime_PCM - previousTime_PCM) / (float)frequency);
        deltaTime_MS = currentTime_MS - previousTime_MS;

        if (currentTime_PCM >= nextMetronomeTime_PCM)
        {
            Tick();
            nextMetronomeTime_PCM += intervalLength_PCM;
        }

        previousTime_PCM = currentTime_PCM;
        previousTime_MS = currentTime_MS;

       // Debug.Log("deltaTime_ms : " + deltaTime_MS);


    }

    private void FixedUpdate()
    {
        Debug.Log("deltatime_ms : " + deltaTime_MS);
    }

#if UNITY_EDITOR
    private void OnGUI()
    {
        GUILayout.Box($"Current Beat = {currentBeat} / {signatureLO} \n" +
            $"Current Music Position = {music.GetTime_MS()}\n" +
            $"Next Tick = {nextMetronomeTime_PCM} \n" +
            $"frequency = {frequency}hz\n" +
            $"Length = {music.GetLength()}\n" 
            // + $"Fmod DeltaTime = {(float)deltaTime_PCM}, Unity DeltaTime = {Time.deltaTime}"
            );
    }
#endif
}
