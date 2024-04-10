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

    [SerializeField] private string RequestText;

    [SerializeField] private GameObject MusicSelectBoxControl;

    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        TargetText.color = new Color(TargetText.color.r, TargetText.color.g, TargetText.color.b, 0);
        TargetImage.color = new Color(TargetImage.color.r, TargetImage.color.g, TargetImage.color.b, 0);

        GameManager.ToWearing += delegate () { StartCoroutine(ShowButton(ReadyText)); AudioManager.Instance.PlaySFX(SFX.Notice); };
        GameManager.ToTutorial += delegate () { StartCoroutine(HideButton()); };
        //GameManager.ToRequestEncore += delegate () { StartCoroutine(ShowButton(RequestText)); };
    }

    public IEnumerator ShowButton(string text)
    {
        button.interactable = true;
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
        AudioManager.Instance.PlaySFX(SFX.Click);
        switch (GameManager.CURRENT_STATE)
        {
            case STATE.WEARING:
                button.interactable = false;
                ControlStageTextBox control = FindAnyObjectByType<ControlStageTextBox>();
                StartCoroutine(control.ClearAll());
                Recenter recenter = FindAnyObjectByType<Recenter>();
                StartCoroutine(recenter.WaitR4ResetPosition());

                break;
            case STATE.TUTORIAL:
                button.interactable = false;

                GameManager.ChangeState(STATE.SELECT_MUSIC);
                StartCoroutine(MusicSelectBoxControl.GetComponent<MusicSelectBoxControl>().Blink());  

                break;
            case STATE.RESULT:
                button.interactable = false;
                StartCoroutine(HideButton());
                ControlStageTextBox control2 = FindAnyObjectByType<ControlStageTextBox>();
                StartCoroutine(control2.ClearAll());
                GameManager.ChangeState(STATE.COMMUNICATION);

                break;
            case STATE.REQUEST_ENCORE:
                button.interactable = false;
                StartCoroutine (HideButton());
                ControlStageTextBox control3 = FindAnyObjectByType<ControlStageTextBox>();
                StartCoroutine(control3.ClearAll());
                GameManager.HadEncore = true;

                //FlatUITextManager flat = FindAnyObjectByType<FlatUITextManager>();
                //GameManager.ToGame += delegate () { StartCoroutine(flat.HideText()); };
                //GameManager.ToResult += delegate () { StartCoroutine(flat.ChangeText("버추얼 아이돌에게 인사해 주세요!!")); };
                //UIBackground ui = FindAnyObjectByType<UIBackground>();
                //GameManager.ToGame += delegate () { StartCoroutine(ui.FadeOut()); };

                //ControlStageTextBox box = FindAnyObjectByType<ControlStageTextBox>();
                //GameManager.ToGame += delegate () { StartCoroutine(box.Gaming()); };
                //GameManager.ToResult += delegate () { StartCoroutine(box.ReadyCommunicating()); };

                //FlatUIBoxManager ma =FindAnyObjectByType<FlatUIBoxManager>();
                //GameManager.ToGame += delegate () { StartCoroutine(ma.Hide()); };
                //GameManager.ToResult += delegate () { StartCoroutine(ma.Show()); };
                
                //GameManager.ChangeState(STATE.GAMEOVER);
                break;
            default:
                break;
        }
    }
}
