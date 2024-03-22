using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public static CURRENT_STATE STATE;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}

public enum CURRENT_STATE
{
    TITLE, WEARING, BACKSTAGE, COMMUNICATION, GAME, RESULT, REQUEST_ENCORE, GAMEOVER
}