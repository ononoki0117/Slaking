using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMODMetronome : MonoBehaviour
{
    public int time;
    public int startDelay;

    uint GetTime()
    {
        AudioManager.instance.musicChannel.getPosition(out uint pos, FMOD.TIMEUNIT.MS);
        return pos;
    }
    
    public void SetMetronome()
    {
        int preBar = 10;
        int barTime = time - startDelay;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
