using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class VanilaMetronome : MonoBehaviour
{
    public double bpm = 60F;
    public float gain = 0.5f;
    public int signatureHi = 4;
    public int signatureLo = 4;

    private double nextTick = 0.0f;
    private float amp = 0.0f;
    private float phase = 0.0f;
    private double sampleRate = 0.0f;
    private int accent;
    
    public bool isPlaying = false;

    void Start()
    {
        accent = signatureHi;
        double startTick = AudioSettings.dspTime;
        sampleRate = AudioSettings.outputSampleRate;
        nextTick = startTick * sampleRate;
        isPlaying = true;
    }

    void Update()
    {

    }

    private void OnAudioFilterRead(float[] data, int channels)
    {
        if (!isPlaying)
            return;

        double samplesPerTick = sampleRate * 60f / bpm * 4.0f / signatureLo;
        double sample = AudioSettings.dspTime * sampleRate;
        int dataLen = data.Length / channels;
        int n = 0;

        while (n < dataLen)
        {
            float x = gain * amp * Mathf.Sin(phase);
            int i = 0;
            while (i < channels)
            {
                data[n * channels + i] += x;
                i++;
            }

            while (sample + n >= nextTick)
            {
                nextTick += samplesPerTick;
                amp = 1.0f;
                if (++accent > signatureHi)
                {
                    accent = 1;
                    amp *= 2.0f;
                }
                Debug.Log("Tick: " + accent + "/" + signatureHi);
            }
            phase += amp * 0.3f;
            amp *= 0.993f;
            n++;
        }
    }

    //public double bpm = 140.0F;
    //public bool isPlaying = false;

    //double nextTick = 0.0F; // The next tick in dspTime
    //double sampleRate = 0.0F;
    //bool ticked = false;

    //void Start()
    //{
    //    double startTick = AudioSettings.dspTime;
    //    sampleRate = AudioSettings.outputSampleRate;

    //    nextTick = startTick + (60.0 / bpm);
    //}

    //void LateUpdate()
    //{
    //    if (!ticked && nextTick >= AudioSettings.dspTime)
    //    {
    //        ticked = true;
    //        BroadcastMessage("OnTick");
    //    }
    //}

    //// Just an example OnTick here
    //void OnTick()
    //{
    //    Debug.Log("Tick");
    //    GetComponent<AudioSource>().Play();
    //}

    //void FixedUpdate()
    //{
    //    double timePerTick = 60.0f / bpm;
    //    double dspTime = AudioSettings.dspTime;

    //    while (dspTime >= nextTick)
    //    {
    //        ticked = false;
    //        nextTick += timePerTick;
    //    }

    //}
}
