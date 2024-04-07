using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TextBreathingEffect : MonoBehaviour
{
    [SerializeField][Range (0.5f, 2.5f)]
    private float breathSpeed = 1.5f;
    private TextMeshProUGUI text;
    private void Start()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        StartCoroutine(BreathingEffect());
    }
    

    IEnumerator BreathingEffect()
    {
        while(true)
        {
            if (Input.anyKey)
            {
                text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
                yield return new WaitForSeconds(0.3f);
                text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
                yield return new WaitForSeconds(0.3f);
                text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
                yield return new WaitForSeconds(0.3f);
                text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
                yield return new WaitForSeconds(0.3f);
                text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
                yield return new WaitForSeconds(0.3f);
                text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
                yield return new WaitForSeconds(0.3f);
            }
            var alpha = (Mathf.Sin(Time.time * breathSpeed) + 1) / 2;
            text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
            yield return null;
        }
    }
}
