using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Windows;

public class EndSceneManager : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI VrText;
    [SerializeField] private TextMeshProUGUI FlatText;

    // Start is called before the first frame update
    void Start()
    {
        VrText.color = new Color(1f, 1f, 1f, 0f);
        FlatText.color = new Color(1f, 1f, 1f, 0f);
        StartCoroutine(FadeIn());
        StartCoroutine(FadeIn2());
    }

    IEnumerator FadeIn()
    {
        while(VrText.color.a <= 0.999)
        {
            VrText.color = new Color(1f, 1f, 1f, VrText.color.a + Time.deltaTime * 0.7f);
            yield return null;
        }
    }
    IEnumerator FadeIn2()
    {
        while (FlatText.color.a <= 0.999)
        {
            FlatText.color = new Color(1f, 1f, 1f, FlatText.color.a + Time.deltaTime * 0.7f);
            yield return null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if(VrText.color.a >= 0.999)
        //{
        //    VrText.color = new Color(1f, 1f, 1f, VrText.color.a + Time.deltaTime);
        //}
        //if (FlatText.color.a >= 0.999)
        //{
        //    FlatText.color = new Color(1f, 1f, 1f, FlatText.color.a + Time.deltaTime);
        //}

        if (UnityEngine.Input.GetKeyDown(KeyCode.Space))
        {
            Application.Quit();
        }
    }
}
