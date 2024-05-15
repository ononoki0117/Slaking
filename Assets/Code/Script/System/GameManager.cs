using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public static STATE NEXT_STATE { get; private set; }
    public static STATE CURRENT_STATE { get; private set; }
    public static SCENE CURRENT_SCENE { get; private set; }

    public STATE current_state;
    public SCENE current_scene;
    public static bool IsStateChanged = false;

    public static bool HadEncore = false;
    public static bool AcceptEncore = false;
    #region event
    public delegate void StateUpdate();
    public static event StateUpdate ToTitle;
    public static event StateUpdate ToDemo;
    public static event StateUpdate ToWearing;
    public static event StateUpdate ToSkip;
    public static event StateUpdate ToTutorial;
    public static event StateUpdate ToSelectMusic;
    public static event StateUpdate ToGame;
    public static event StateUpdate ToCommunication;
    public static event StateUpdate ToResult;
    public static event StateUpdate ToRequestEncore;
    public static event StateUpdate ToGameOver;
    #endregion

    public static void ChangeState(STATE state)
    {
        NEXT_STATE = state;
        switch(state)
        {
            case STATE.TITLE:
                CURRENT_SCENE = SCENE.TITLE;
                MoveScene(SCENE.TITLE, STATE.TITLE);
                break;
            case STATE.DEMO:
                CURRENT_SCENE = SCENE.TITLE;
                MoveScene(SCENE.TITLE, STATE.DEMO);
                break;
            case STATE.WEARING:
                CURRENT_SCENE = SCENE.STAGE;
                MoveScene(SCENE.STAGE, STATE.WEARING);
                break;
            case STATE.SKIP:
                CURRENT_SCENE = SCENE.STAGE;
                MoveScene(SCENE.STAGE, STATE.SKIP);
                break;
            case STATE.TUTORIAL:
                CURRENT_SCENE = SCENE.STAGE;
                MoveScene(SCENE.STAGE, STATE.TUTORIAL);
                break;
            case STATE.SELECT_MUSIC:
                CURRENT_SCENE = SCENE.STAGE;
                MoveScene(SCENE.STAGE, STATE.SELECT_MUSIC);
                break;
            case STATE.GAME:
                CURRENT_SCENE = SCENE.STAGE;
                MoveScene(SCENE.STAGE, STATE.GAME);
                break;
            case STATE.COMMUNICATION:
                CURRENT_SCENE = SCENE.STAGE;
                MoveScene(SCENE.STAGE, STATE.COMMUNICATION);
                break;
            case STATE.RESULT:
                CURRENT_SCENE = SCENE.STAGE;
                MoveScene(SCENE.STAGE, STATE.RESULT);
                break;
            case STATE.REQUEST_ENCORE:
                CURRENT_SCENE = SCENE.STAGE;
                MoveScene(SCENE.STAGE, STATE.REQUEST_ENCORE);
                break;
            case STATE.GAMEOVER:
                CURRENT_SCENE = SCENE.END;
                MoveScene(SCENE.END, STATE.GAMEOVER);
                break;
        }
    }

    private static void MoveScene(SCENE scene, STATE state)
    {
        switch(scene)
        {
            case SCENE.TITLE:
                LoadingManager.LoadScene("Title", state);
                break;
            case SCENE.STAGE:
                LoadingManager.LoadScene("Stage", state);
                break;
            case SCENE.END:
                LoadingManager.LoadScene("End", state);
                break;
        }
    }

    public static void UpdateState(STATE state)
    {
        CURRENT_STATE = state;
        switch(state)
        {
            case STATE.TITLE: ToTitle(); break;
            case STATE.DEMO: ToDemo(); break;
            case STATE.WEARING: ToWearing(); break;
            case STATE.TUTORIAL: ToTutorial(); break;
            case STATE.SKIP: ToSkip(); break;
            case STATE.SELECT_MUSIC: ToSelectMusic(); break;
            case STATE.GAME: Debug.Log("Game"); ToGame();break;
            case STATE.COMMUNICATION: ToCommunication(); break;
            case STATE.RESULT: ToResult(); break;
            case STATE.REQUEST_ENCORE: ToRequestEncore(); break;
            case STATE.GAMEOVER: ToGameOver(); break;
        }
    }

    private void Start()
    {
        if (CURRENT_STATE != NEXT_STATE)
        {
            CURRENT_STATE = NEXT_STATE;
            try
            {
                // STATE변경에 따른 이벤트 호출
                UpdateState(CURRENT_STATE);
                Debug.Log("GameManager : Start : Update State");
            }
            catch (System.Exception)
            {
                Debug.Log("GameManager : Start : No Event Exist");
                Debug.Log(CURRENT_STATE.ToString());
            }
        }
        
        current_state = CURRENT_STATE;
        current_scene = CURRENT_SCENE;
    }

    private void Update()
    {
        current_state = CURRENT_STATE;
        current_scene = CURRENT_SCENE;
    }
}

public enum STATE
{
    TITLE, DEMO,// TITLE
    WEARING, SKIP, TUTORIAL, SELECT_MUSIC, COMMUNICATION, GAME, RESULT, REQUEST_ENCORE, // STAGE
    GAMEOVER // END
}

public enum SCENE
{
    TITLE, STAGE, END, LOADING
}