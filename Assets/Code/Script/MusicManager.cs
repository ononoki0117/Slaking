using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;
using FMODUnity;
using FMOD;
public class MusicManager : Singleton<MusicManager>
{
    public TimelineInfo timelineInfo = null;
    private GCHandle timelineHandle;
    public FMOD.Studio.EventInstance musicInstance;
    private FMOD.Studio.EVENT_CALLBACK beatCallback;
    public bool isPlaying = false;

    public delegate void BeatEventDelegate();
    public static event BeatEventDelegate beatUpdate;

    public delegate void MarkerListnerDelegate();
    public static event MarkerListnerDelegate markerUpdated;

    public static int lastBeat = 0;
    public static string lastMarkerString = null;

    [SerializeField] private static LightControl lightControler;

    [StructLayout(LayoutKind.Sequential)]
    public class TimelineInfo
    {
        public int currentBeat = 0;
        public FMOD.StringWrapper lastMarker = new FMOD.StringWrapper();
    }

    private void Awake()
    {
        //if (!GameMusic.IsNull)
        //{
        //    musicInstance = RuntimeManager.CreateInstance(GameMusic);
        //    timelineInfo = new TimelineInfo();
        //    beatCallback = new FMOD.Studio.EVENT_CALLBACK(BeatEventCallback);
        //    timelineHandle = GCHandle.Alloc(timelineInfo, GCHandleType.Pinned);
        //    musicInstance.setUserData(GCHandle.ToIntPtr(timelineHandle));
        //    musicInstance.setCallback(beatCallback, FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_BEAT | FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_MARKER);
        //}
    }

    public void SetMusic(EventReference GameMusic)
    {
        if (!GameMusic.IsNull)
        {
            musicInstance = RuntimeManager.CreateInstance(GameMusic);
            timelineInfo = new TimelineInfo();
            beatCallback = new FMOD.Studio.EVENT_CALLBACK(BeatEventCallback);
            timelineHandle = GCHandle.Alloc(timelineInfo, GCHandleType.Pinned);
            musicInstance.setUserData(GCHandle.ToIntPtr(timelineHandle));
            musicInstance.setCallback(beatCallback, FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_BEAT | FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_MARKER);
        }
    }

    public void StartMusic()
    {
        musicInstance.start();

        isPlaying = true;
    }


    private void Update()
    {
        if (isPlaying)
        {
            if (lastMarkerString != timelineInfo.lastMarker)
            {
                lastMarkerString = timelineInfo.lastMarker;

                if(markerUpdated != null)
                {
                    markerUpdated();
                }
            }
            if (lastBeat != timelineInfo.currentBeat)
            {
                lastBeat = timelineInfo.currentBeat;

                if(beatUpdate != null)
                {
                    beatUpdate();
                }
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
        if(isPlaying)
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