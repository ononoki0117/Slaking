using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public static CURRENT_STATE STATE { get; private set; }

    public static CURRENT_SCENE SCENE { get; private set; }

    public static void ChangeState(string state)
    {
        switch(state)
        {
            case "Title":
                ChangeState(CURRENT_STATE.TITLE); break;
            case "Wearing":
                ChangeState(CURRENT_STATE.WEARING); break;
            case "Tutorial":
                ChangeState(CURRENT_STATE.TUTORIAL); break;
            case "SelectMusic":
                ChangeState(CURRENT_STATE.SELECT_MUSIC); break;
            case "Communication":
                ChangeState(CURRENT_STATE.COMMUNICATION); break;
            case "Game":
                ChangeState(CURRENT_STATE.GAME); break;
            case "Result":
                ChangeState(CURRENT_STATE.RESULT); break;
            case "RequestEncore":
                ChangeState(CURRENT_STATE.REQUEST_ENCORE); break;
            case "GameOver":
                ChangeState(CURRENT_STATE.GAMEOVER); break;
            default:
                break;
        }
    }

    private static void ChangeState(CURRENT_STATE state)
    {
        STATE = state;
        switch(state)
        {
            case CURRENT_STATE.TITLE:
                SCENE = CURRENT_SCENE.TITLE;
                MoveScene(CURRENT_SCENE.TITLE);
                break;
            case CURRENT_STATE.WEARING:
                SCENE = CURRENT_SCENE.WEARING;
                MoveScene(CURRENT_SCENE.WEARING);
                break;
            case CURRENT_STATE.TUTORIAL:
            case CURRENT_STATE.SELECT_MUSIC:
                SCENE = CURRENT_SCENE.BACKSTAGE;
                MoveScene(CURRENT_SCENE.BACKSTAGE);
                break;
            case CURRENT_STATE.COMMUNICATION:
            case CURRENT_STATE.GAME:
            case CURRENT_STATE.RESULT:
            case CURRENT_STATE.REQUEST_ENCORE:
                SCENE = CURRENT_SCENE.STAGE;
                MoveScene(CURRENT_SCENE.STAGE);
                break;
            case CURRENT_STATE.GAMEOVER:
                SCENE = CURRENT_SCENE.END;
                MoveScene(CURRENT_SCENE.END);
                break;
        }
    }

    private static void MoveScene(CURRENT_SCENE scene)
    {
        switch(scene)
        {
            case CURRENT_SCENE.TITLE:
                LoadingManager.LoadScene("Title");
                break;
            case CURRENT_SCENE.WEARING:
                LoadingManager.LoadScene("Wearing");
                break;
            case CURRENT_SCENE.BACKSTAGE:
                LoadingManager.LoadScene("BackStage");
                break;
            case CURRENT_SCENE.STAGE:
                LoadingManager.LoadScene("Stage");
                break;
            case CURRENT_SCENE.END:
                LoadingManager.LoadScene("End");
                break;
        }
    }

    private void Awake()
    {
        //DontDestroyOnLoad(gameObject);
    }
}

public enum CURRENT_STATE
{
    TITLE, // TITLE
    WEARING, // WEARING
    TUTORIAL, SELECT_MUSIC, // BACKSTAGE
    COMMUNICATION, GAME, RESULT, REQUEST_ENCORE, // STAGE
    GAMEOVER // END
}

public enum CURRENT_SCENE
{
    TITLE, WEARING, BACKSTAGE, STAGE, END, LOADING
}