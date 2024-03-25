using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class StartVideo : MonoBehaviour
{
    public VideoPlayer player;
    private void Awake()
    {
        player = GetComponent<VideoPlayer>();
    }

    private void Start()
    {
        
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyUp(KeyCode.Space)) 
        {
            player.Play();
        }
    }
}
