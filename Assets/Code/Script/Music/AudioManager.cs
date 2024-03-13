using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{

    public bool metronome = true;

    [Header("SFX")]
    FMOD.ChannelGroup sfxChannelGroup;
    FMOD.Sound[] sfxs;
    FMOD.Channel[] sfxChannels;

    [SerializeField]
    private int sfxVolume = 1;

    [SerializeField]
    private int masterVolume = 1;


    void Awake()
    {
        LoadSFX(Parser.Load("SongInfo/Kanade").title);
    }


    private void LoadSFX(string musicName)
    {
        int count = System.Enum.GetValues(typeof(SFX)).Length;

        sfxChannelGroup = new FMOD.ChannelGroup();
        sfxChannels = new FMOD.Channel[count];
        sfxs = new FMOD.Sound[count];

        string metronomeName = "Metronome.mp3";

        // 일단은 수동으로 로드함
        var Result1 = FMODUnity.RuntimeManager.CoreSystem.createSound(Path.Combine(Application.streamingAssetsPath, "Music", musicName), FMOD.MODE.CREATESAMPLE, out sfxs[0]);
        var Result2 = FMODUnity.RuntimeManager.CoreSystem.createSound(Path.Combine(Application.streamingAssetsPath, "Music", metronomeName), FMOD.MODE.CREATESAMPLE, out sfxs[1]);

        Debug.Log(Result1);
        Debug.Log(Result2);

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
    Background,
    Metronome
}
