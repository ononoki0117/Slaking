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
        GameManager.ToWearing += delegate() { StartCoroutine(ChangeText(wearing)); };
        GameManager.ToTutorial += delegate () { StartCoroutine(ChangeText(tutorial)); };
        GameManager.ToCommunication += delegate () { StartCoroutine(ChangeText(communication)); };
        GameManager.ToGame += delegate () { StartCoroutine(ChangeText(game)); };
        GameManager.ToRequestEncore += delegate { StartCoroutine(ChangeText(request_encore)); };
    }

    IEnumerator ChangeText(string text)
    {
        while(TargetText.color.a > 0.001)
        {
            TargetText.color = new Color(TargetText.color.r, TargetText.color.g, TargetText.color.b, TargetText.color.a - Time.deltaTime);
            yield return null;
        }
        TargetText.text = text;
        while (TargetText.color.a <= 0.999)
        {
            TargetText.color = new Color(TargetText.color.r, TargetText.color.g, TargetText.color.b, TargetText.color.a + Time.deltaTime);
            yield return null;
        }
    }
}
