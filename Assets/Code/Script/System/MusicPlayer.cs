using FMOD;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : Singleton<MusicPlayer>
{
    [SerializeField]
    private SFX playingMusic;

    [SerializeField]
    private bool isPlaying { get; set; } = false;

    private FMOD.Channel channel;


    public void PlayMusic(SFX _sfx)
    {
        if (isPlaying) return;

        playingMusic = _sfx;
        channel = AudioManager.Instance.PlaySFX(_sfx, 1);
        isPlaying = true;
    }

    public void StopMusic()
    {
        AudioManager.Instance.StopSFX(playingMusic);
        isPlaying = false;
    }
    
    public uint GetTime_PCM()
    {
        channel.getPosition(out uint pos, FMOD.TIMEUNIT.PCM);
        return pos;
    }

    public uint GetTime_MS()
    {
        channel.getPosition(out uint pos, FMOD.TIMEUNIT.MS);
        return pos;
    }

    public uint GetLength()
    {
        channel.getCurrentSound(out FMOD.Sound sound);
        sound.getLength(out uint length, FMOD.TIMEUNIT.PCM);
        return length;
    }

    public float GetFrequency()
    {
        channel.getFrequency(out float frequency);
        return frequency;
    }
}
