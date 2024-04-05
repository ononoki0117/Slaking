using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TitleSceneManager : MonoBehaviour
{
    [SerializeField] private GameObject TitleLogo;
    [SerializeField] private GameObject Character;

    private void Awake()
    {
        GameManager.ToDemo += delegate () { StartCoroutine(PlayDemo()); };
        GameManager.ToTitle += delegate () { StartCoroutine(PlayTitle()); };
    }

    private void Start()
    {
        StartCoroutine(PlayTitle());
    }

    IEnumerator PlayDemo()
    {
        Debug.Log("TitleSceneManager : Enter Demo");
        TitleLogo.SetActive(false);
        Character.SetActive(true);

        yield return new WaitForSeconds(2f);

        Debug.Log("TitleSceneManager : Play Demo");
        Character.GetComponent<Animator>().Play("SoregaZenbu_Dance", -1, 0);
        
        while (true)
        {
            if (Input.anyKey)
            {
                break;
            }
            yield return null;
        }

        GameManager.ChangeState(STATE.TITLE);
        yield break;
    }

    IEnumerator PlayTitle()
    {
        Debug.Log("TitleSceneManager : Enter Title");
        TitleLogo.SetActive(true);
        Character.SetActive(false);

        yield return new WaitForSeconds(2f);

        Debug.Log("TitleSceneManager : Start Checking Key Input");

        float timer = 0;
        while (true)
        {
            timer += Time.deltaTime;
            if (Input.anyKey)
            {
                GameManager.ChangeState(STATE.WEARING);
                yield break;
            }
            if (timer > 10f)
            {
                GameManager.ChangeState(STATE.DEMO);
                yield break;
            }
            yield return null;
        }
    }

}
