using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Instance")]
    public static AudioManager instance;

    [Header("SFX")]
    FMOD.ChannelGroup sfxChannelGroup;
    FMOD.Sound[] sfxs;
    FMOD.Channel[] sfxChannels;

    public FMOD.Channel musicChannel;
    public float sfxVolume = 1;
    public float masterVolume = 1;

    void LoadSFX()
    {
        int count = System.Enum.GetValues(typeof(SFX)).Length;

        sfxChannelGroup = new FMOD.ChannelGroup();
        sfxChannels = new FMOD.Channel[count];
        sfxs = new FMOD.Sound[count];

        for (int i = 0; i < count; i++)
        {
            string sfxFilename = System.Enum.GetName(typeof(SFX), i);
            string audioType = "mp3";

            FMODUnity.RuntimeManager.CoreSystem.createSound(Path.Combine(Application.streamingAssetsPath, "SFXS", sfxFilename + "." + audioType), FMOD.MODE.CREATESAMPLE, out sfxs[i]);
        }

        for (int i = 0;i < count; i++)
        {
            sfxChannels[i].setChannelGroup(sfxChannelGroup);
        }
    }

    // 효과음 재생 함수
    public void PlaySFX(SFX _sfx, float _volume = 1)
    {
        sfxChannels[(int)_sfx].stop();
        
        FMODUnity.RuntimeManager.CoreSystem.playSound(sfxs[(int)_sfx], sfxChannelGroup, false, out sfxChannels[(int)_sfx]);

        sfxChannels[(int)_sfx].setPaused(true);
        sfxChannels[(int)_sfx].setVolume((_volume * sfxVolume) * masterVolume);
        sfxChannels[(int)_sfx].setPaused(false);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
