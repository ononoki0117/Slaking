using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FlatUITextManager : MonoBehaviour
{
    public TextMeshProUGUI TargetText;

    [SerializeField][TextArea] private string wearing;
    [SerializeField][TextArea] private string tutorial;
    [SerializeField][TextArea] private string selectmusic;
    [SerializeField][TextArea] private string communication;


    private void Awake()
    {
        TargetText.color = new Color(TargetText.color.r, TargetText.color.g, TargetText.color.b, 0);
        GameManager.ToWearing += delegate() { StartCoroutine(ChangeText(wearing)); };
        GameManager.ToTutorial += delegate () { StartCoroutine(ChangeText(tutorial)); };
        GameManager.ToSelectMusic += delegate () { StartCoroutine(ChangeText(selectmusic)); };
        GameManager.ToGame += delegate () { StartCoroutine(HideText()); };
        GameManager.ToResult += delegate () {StartCoroutine(ChangeText(communication)); };
        GameManager.ToRequestEncore += delegate () { StartCoroutine(HideText()); };
    }

    public IEnumerator ChangeText(string text)
    {
        yield return HideText();

        TargetText.text = text;

        yield return ShowText();
    }

    public IEnumerator HideText()
    {
        while (TargetText.color.a > 0.001)
        {
            TargetText.color = new Color(TargetText.color.r, TargetText.color.g, TargetText.color.b, TargetText.color.a - Time.deltaTime);
            yield return null;
        }
    }

    IEnumerator ShowText()
    {
        while (TargetText.color.a <= 0.999)
        {
            TargetText.color = new Color(TargetText.color.r, TargetText.color.g, TargetText.color.b, TargetText.color.a + Time.deltaTime);
            yield return null;
        }
    }
}
