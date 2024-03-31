using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingManager : Singleton<LoadingManager>
{
    public static string nextScene;
    [SerializeField] Image progressBar;

    void Start()
    {
        StartCoroutine(LoadScene());
    }

    public static void LoadScene(string sceneName, STATE state)
    {
        if (nextScene == sceneName)
        {
            Debug.Log("Loading Manager : Stay in Currnet Scene");
            try
            {
                GameManager.UpdateState(state);
                Debug.Log("Loading Mananger : Update State");
            }
            catch (System.Exception)
            {
                Debug.Log("Loading Manager : No Event exist");
            }
            return;
        }
        Debug.Log("Loading Manager : Load " + sceneName);
        nextScene = sceneName;
        SceneManager.LoadScene("Loading");
    }

    IEnumerator LoadScene()
    {
        yield return null;
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;
        float timer = 0.0f;
        while(!op.isDone)
        {
            yield return null;
            timer += Time.deltaTime;
            if (op.progress < 0.9f)
            {
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, op.progress, timer);
                if (progressBar.fillAmount >= op.progress)
                {
                    timer = 0.0f;
                }
            }
            else
            {
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, 1f, timer);
                if (progressBar.fillAmount == 1.0f)
                {
                    op.allowSceneActivation = true;
                    GameManager.IsStateChanged = true;
                    yield break;
                }
            }
        }
    }
}
