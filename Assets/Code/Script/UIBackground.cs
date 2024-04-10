using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBackground : MonoBehaviour
{
    [SerializeField]
    private Image background;

    private void Awake()
    {
        GameManager.ToGame += delegate () { StartCoroutine(FadeOut()); };
    }

    public IEnumerator FadeOut()
    {
        while(background.color.a > 0.001)
        {
            background.color = new Color(0f, 0f, 0f, background.color.a - Time.deltaTime);
            yield return null;
        }
        background.enabled = false;
    }
}
