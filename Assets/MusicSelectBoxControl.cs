using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MusicSelectBoxControl : MonoBehaviour
{
    private Image[] images;
    private void Awake()
    {
        images = GetComponentsInChildren<Image>();
        foreach(Image image in images)
        {
            image.color = new Color(1, 1, 1, 0);
        }
        gameObject.SetActive(false);
        GameManager.ToGame += delegate () { gameObject.SetActive(false); };
    }

    private void Start()
    {
        //StartCoroutine(Blink());
    }

    public IEnumerator Blink()
    {
        yield return new WaitForSeconds(2f);
        yield return FadeIn();
        yield return new WaitForSeconds(2f);
        yield return FadeOut();
        yield return new WaitForEndOfFrame();
        GameManager.ChangeState(STATE.GAME);
    }

    IEnumerator FadeIn()
    {
        
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
