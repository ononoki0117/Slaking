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
        try
        {
            GameManager.UpdateState(state);
            Debug.Log("Loading Mananger : Update State");
        }
        catch (System.Exception)
        {
            Debug.Log("Loading Manager : No Event exist");
        }
        Debug.Log("Loading Manager : Load " + sceneName);
        nextScene = sceneName;
        SceneManager.LoadScene("Loading");
    }

    IEnumerator FadeIn()
    {
        GameObject[] LoadingBlacks = GameObject.FindGameObjectsWithTag("LoadingBlack");
        List<Image> BlackScreen = new List<Image>();
        foreach(GameObject obj in LoadingBlacks)
        {
            BlackScreen.Add(obj.GetComponent<Image>());
        }
        while(BlackScreen[0].color.a < 0.999)
        {
           foreach(Image img in BlackScreen)
           {
                img.color = new Color(img.color.r, img.color.g, img.color.b, img.color.a + Time.deltaTime);
           }
           yield return null;
        }
        SceneManager.LoadScene("Loading");
        yield return null;
    }

    IEnumerator FadeOut()
    {
        GameObject[] LoadingBlacks = GameObject.FindGameObjectsWithTag("LoadingBlack");
        List<Image> BlackScreen = new List<Image>();
        foreach (GameObject obj in LoadingBlacks)
        {
            BlackScreen.Add(obj.GetComponent<Image>());
        }
        while (BlackScreen[0].color.a < 0.999)
        {
            foreach (Image img in BlackScreen)
            {
                img.color = new Color(img.color.r, img.color.g, img.color.b, img.color.a + Time.deltaTime);
            }
            yield return null;
        }
        SceneManager.LoadScene("Loading");
        yield return null;
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
