using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingCircle : MonoBehaviour
{
    public static bool Active = false;
    public static bool IsActive() => Active;
    private Image Image;
    private void Awake()
    {
        Image = GetComponent<Image>();
        Image.color = new Color(1, 1, 1, 0);
        Active = false;
        LoadingManager.LoadStart += delegate () { StartCoroutine(FadeIn()); };
        LoadingManager.LoadFinish += delegate () { StartCoroutine(FadeOut()); };
        GameManager.ToGame += delegate () { StartCoroutine(FadeOut()); };
    }
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, -1));
    }

    IEnumerator FadeIn()
    {
        while(Image.color.a < 0.999)
        {
            Image.color = new Color(1, 1, 1, Image.color.a + Time.deltaTime);
            yield return null;
        }
        Active = true;
        yield break;
    }

    IEnumerator FadeOut()
    {
        while (Image.color.a > 0.001)
        {
            Image.color = new Color(1, 1, 1, Image.color.a - Time.deltaTime);
            yield return null;
        }
        Active = false;
        yield break;
    }
}
