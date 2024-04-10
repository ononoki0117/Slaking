using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MusicSelectBoxControl : MonoBehaviour
{
    private Image[] images;

    private Button button;
    private void Awake()
    {

        button = GetComponent<Button>();
        button.interactable = false;
        images = GetComponentsInChildren<Image>();
        foreach(Image image in images)
        {
            image.color = new Color(1, 1, 1, 0);
        }
        //gameObject.SetActive(false);
        GameManager.ToGame += delegate () { gameObject.SetActive(false); };
    }

    public IEnumerator Blink()
    {
        yield return new WaitForSeconds(2f);
        yield return FadeIn();
        yield return new WaitForSeconds(2f);
        yield return FadeOut();
        //yield return new WaitForEndOfFrame();
        Debug.Log("Music Select box");
        GameManager.ChangeState(STATE.GAME);
    }

    IEnumerator FadeIn()
    {
        yield return new WaitForSeconds(2f);

        while (images.Last().color.a < 1)
        {
            float timer = Time.deltaTime * 6;
            foreach (Image image in images)
            {
                image.color = new Color(1, 1, 1, image.color.a + timer);
                yield return null;
            }
        }
    }

    IEnumerator FadeOut()
    {
        while (images.Last().color.a > 0)
        {
            float timer = Time.deltaTime * 6;
            foreach (Image image in images)
            {
                image.color = new Color(1, 1, 1, image.color.a - timer);
                yield return null;
            }
        }
    }
}
