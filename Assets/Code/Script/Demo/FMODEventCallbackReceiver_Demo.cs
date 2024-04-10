using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD;
using FMODUnity;
using System.Runtime.InteropServices;
using System;

public class FMODEventCallbackReceiver_Demo : MonoBehaviour
{
    [Header("TimelineInfo From FMOD Project")]
    [SerializeField] private TimelineInfo timelineInfo = null;


    [Header("FMOD Event Reference")]
    [SerializeField] private EventReference DemoMusic;
    private FMOD.Studio.EventInstance musicInstance;


    [Header("BG Volume")]
    [SerializeField]
    [Range(0f, 1f)] private float Volume;
    private FMOD.Studio.EVENT_CALLBACK BeatCallback;
    public bool isPlaying = false;


    private GCHandle timelineHandle;

    public delegate void BeatEventDelegate();
    public static event BeatEventDelegate BeatUpdated;

    public delegate void MarkerListnerDelegate();
    public static event MarkerListnerDelegate MarkerUpdated;

    public static int LastBeat = 0;
    public static string LastMarkerString = null;

    public class TimelineInfo
    {
        public int currentBeat = 0;
        public StringWrapper lastMarker = new();
    }

    public void SetMusic(EventReference GameMusic)
    {
        if (!GameMusic.IsNull)
        {
            musicInstance = RuntimeManager.CreateInstance(GameMusic);
            timelineInfo = new TimelineInfo();
            BeatCallback = new FMOD.Studio.EVENT_CALLBACK(BeatEventCallback);
            timelineHandle = GCHandle.Alloc(timelineInfo, GCHandleType.Pinned);
            musicInstance.setUserData(GCHandle.ToIntPtr(timelineHandle));
            musicInstance.setCallback(BeatCallback, FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_BEAT | FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_MARKER);
        }
    }

    public void StartMusic()
    {
        musicInstance.start();
        musicInstance.setVolume(Volume);
        isPlaying = true;
    }

    private void Start()
    {
        SetMusic(DemoMusic);
        StartMusic();
    }
    private void Update()
    {
        if (isPlaying)
        {
            if (LastMarkerString != timelineInfo.lastMarker)
            {
                LastMarkerString = timelineInfo.lastMarker;
                MarkerUpdated?.Invoke();
            }
            if (LastBeat != timelineInfo.currentBeat)
            {
                LastBeat = timelineInfo.currentBeat;
                BeatUpdated?.Invoke();
            }
        }
    }

    private void OnDestroy()
    {
        musicInstance.setUserData(IntPtr.Zero);
        musicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        musicInstance.release();
        timelineHandle.Free();
    }

#if UNITY_EDITOR
    private void OnGUI()
    {
        if (isPlaying)
        {
            GUILayout.Box($"Current Beat = {timelineInfo.currentBeat}, Last Marker = {(string)timelineInfo.lastMarker}");
        }
    }
#endif

    [AOT.MonoPInvokeCallback(typeof(FMOD.Studio.EVENT_CALLBACK))]
    static FMOD.RESULT BeatEventCallback(FMOD.Studio.EVENT_CALLBACK_TYPE type, IntPtr instancePtr, IntPtr parameterPtr)
    {
        FMOD.Studio.EventInstance instance = new FMOD.Studio.EventInstance(instancePtr);

        IntPtr timelineInfoPtr;
        FMOD.RESULT result = instance.getUserData(out timelineInfoPtr);

        if (result != FMOD.RESULT.OK)
        {
            UnityEngine.Debug.LogError("Timeline Callback error: " + result);
        }
        else if (timelineInfoPtr != IntPtr.Zero)
        {
            GCHandle timelineHandle = GCHandle.FromIntPtr(timelineInfoPtr);
            TimelineInfo timelineInfo = (TimelineInfo)timelineHandle.Target;

            switch (type)
            {
                case FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_BEAT:
                    {
                        var parameter = (FMOD.Studio.TIMELINE_BEAT_PROPERTIES)Marshal.PtrToStructure(parameterPtr, typeof(FMOD.Studio.TIMELINE_BEAT_PROPERTIES));
                        timelineInfo.currentBeat = parameter.beat;
                    }
                    break;
                case FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_MARKER:
                    {
                        var parameter = (FMOD.Studio.TIMELINE_MARKER_PROPERTIES)Marshal.PtrToStructure(parameterPtr, typeof(FMOD.Studio.TIMELINE_MARKER_PROPERTIES));
                        timelineInfo.lastMarker = parameter.name;
                        //lightControler.turnOn = !lightControler.turnOn;
                    }
                    break;
            }
        }

        return FMOD.RESULT.OK;
    }
}
