using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ControlStageButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI TargetText;

    [SerializeField] private Image TargetImage;

    [SerializeField] private string ReadyText;

    private void Awake()
    {
        TargetText.color = new Color(TargetText.color.r, TargetText.color.g, TargetText.color.b, 0);
        TargetImage.color = new Color(TargetImage.color.r, TargetImage.color.g, TargetImage.color.b, 0);

        GameManager.ToWearing += delegate () { StartCoroutine(ShowButton(ReadyText)); };
        GameManager.ToTutorial += delegate () { StartCoroutine(HideButton()); };
    }

    public IEnumerator ShowButton(string text)
    {
        TargetText.text = text;
        while (TargetText.color.a <= 0.999)
        {
            TargetText.color = new Color(TargetText.color.r, TargetText.color.g, TargetText.color.b, TargetText.color.a + Time.deltaTime);
            TargetImage.color = new Color(TargetImage.color.r, TargetImage.color.g, TargetImage.color.b, TargetImage.color.a + Time.deltaTime);
            yield return null;
        }
    }

    public IEnumerator HideButton()
    {
        while (TargetText.color.a > 0.001)
        {
            TargetText.color = new Color(TargetText.color.r, TargetText.color.g, TargetText.color.b, TargetText.color.a - Time.deltaTime);
            TargetImage.color = new Color(TargetImage.color.r, TargetImage.color.g, TargetImage.color.b, TargetImage.color.a - Time.deltaTime);
            yield return null;
        }
    }

    public void OnClick()
    {
        switch (GameManager.CURRENT_STATE)
        {
            case STATE.WEARING:
                ControlStageTextBox control = FindAnyObjectByType<ControlStageTextBox>();
                StartCoroutine(control.ClearAll());
                //GameManager.ChangeState(STATE.TUTORIAL); 
                break;
            case STATE.TUTORIAL:
                GameManager.ChangeState(STATE.SELECT_MUSIC);
                break;
            default:
                break;
        }
    }
}
