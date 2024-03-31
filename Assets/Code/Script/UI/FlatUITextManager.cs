using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FlatUITextManager : MonoBehaviour
{
    public TextMeshProUGUI TargetText;

    [SerializeField] private string wearing;
    [SerializeField] private string tutorial;
    [SerializeField] private string communication;
    [SerializeField] private string game;
    [SerializeField] private string request_encore;


    private void Awake()
    {
        TargetText.color = new Color(TargetText.color.r, TargetText.color.g, TargetText.color.b, 0);
        GameManager.ToWearing += delegate() { StartCoroutine(ChangeInto(wearing)); };
        GameManager.ToTutorial += delegate () { StartCoroutine(ChangeInto(tutorial)); };
        GameManager.ToCommunication += delegate () { StartCoroutine(ChangeInto(communication)); };
        GameManager.ToGame += delegate () { StartCoroutine(ChangeInto(game)); };
        GameManager.ToRequestEncore += delegate { StartCoroutine(ChangeInto(request_encore)); };
    }

    IEnumerator ChangeInto(string text)
    {
        while(TargetText.color.a > 0.01)
        {
            TargetText.color = new Color(TargetText.color.r, TargetText.color.g, TargetText.color.b, TargetText.color.a - Time.deltaTime);
            yield return null;
        }
        TargetText.text = text;
        while (TargetText.color.a <= 0.99)
        {
            TargetText.color = new Color(TargetText.color.r, TargetText.color.g, TargetText.color.b, TargetText.color.a + Time.deltaTime);
            yield return null;
        }
    }
}
