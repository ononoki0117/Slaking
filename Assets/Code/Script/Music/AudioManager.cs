using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{

    [Header("SFX")]
    FMOD.ChannelGroup sfxChannelGroup;
    FMOD.Sound[] sfxs;
    FMOD.Channel[] sfxChannels;

    [Header("Volume")]
    [SerializeField]
    private int sfxVolume = 1;

    [SerializeField]
    private int masterVolume = 1;

    [Header("Effect")]
    [SerializeField]
    private string start;
    [SerializeField]
    private string click;
    [SerializeField]
    private string notice;
    [SerializeField]
    private string confirm;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        LoadSFX();
    }

    private void LoadSFX()
    {
        int count = System.Enum.GetValues(typeof(SFX)).Length;

        sfxChannelGroup = new FMOD.ChannelGroup();
        sfxChannels = new FMOD.Channel[count];
        sfxs = new FMOD.Sound[count];

        //string metronomeName = "Metronome.mp3";

        // 일단은 수동으로 로드함
        //var Result1 = FMODUnity.RuntimeManager.CoreSystem.createSound(Path.Combine(Application.streamingAssetsPath, "Music", musicName), FMOD.MODE.CREATESAMPLE, out sfxs[0]);
        //var Result2 = FMODUnity.RuntimeManager.CoreSystem.createSound(Path.Combine(Application.streamingAssetsPath, "Music", metronomeName), FMOD.MODE.CREATESAMPLE, out sfxs[1]);

        //Debug.Log(Result1);
        //Debug.Log(Result2);

        FMODUnity.RuntimeManager.CoreSystem.createSound(Path.Combine(Application.streamingAssetsPath, "Effect", start), FMOD.MODE.CREATESAMPLE, out sfxs[0]);
        FMODUnity.RuntimeManager.CoreSystem.createSound(Path.Combine(Application.streamingAssetsPath, "Effect", click), FMOD.MODE.CREATESAMPLE, out sfxs[1]);
        FMODUnity.RuntimeManager.CoreSystem.createSound(Path.Combine(Application.streamingAssetsPath, "Effect", notice), FMOD.MODE.CREATESAMPLE, out sfxs[2]);
        FMODUnity.RuntimeManager.CoreSystem.createSound(Path.Combine(Application.streamingAssetsPath, "Effect", confirm), FMOD.MODE.CREATESAMPLE, out sfxs[3]);

        for (int i = 0; i < count; i++)
        {
            sfxChannels[i].setChannelGroup(sfxChannelGroup);
        }
    }

    public FMOD.Channel PlaySFX(SFX _sfx, float _volume = 1)
    {
        sfxChannels[(int)_sfx].stop();

        FMODUnity.RuntimeManager.CoreSystem.playSound(sfxs[(int)_sfx], sfxChannelGroup, false, out sfxChannels[(int)_sfx]);

        sfxChannels[(int)_sfx].setPaused(true);
        sfxChannels[(int)_sfx].setVolume((_volume * sfxVolume) * masterVolume);
        sfxChannels[(int)_sfx].setPaused(false);

        return sfxChannels[(int)_sfx];
    }

    public void StopSFX(SFX _sfx)
    {
        sfxChannels[(int)_sfx].stop();
    }

}

public enum SFX
{
    Start,
    Click,
    Notice,
    Confirm,
}
