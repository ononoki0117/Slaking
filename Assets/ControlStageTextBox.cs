using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ControlStageTextBox : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI TargetText;

    [SerializeField][TextArea] private string wearing;
    [SerializeField][TextArea] private string[] tutorialFront;
    [SerializeField][TextArea] private string tutorialBack;
    [SerializeField][TextArea] private string communication;
    [SerializeField] private ControlStageButton ButtonControl;
    [SerializeField][TextArea] private string tutorialEnd;
    [SerializeField][TextArea] private string enterCommunication;
    [SerializeField] private Image textBox;
    [SerializeField] private StageScreenSwitcher switcher;

    private bool VideoEnded = false;

    private void Awake()
    {
        TargetText.color = new Color(TargetText.color.r, TargetText.color.g, TargetText.color.b, 0);

        GameManager.ToWearing += delegate () { StartCoroutine(ChangeText(wearing)); };
        GameManager.ToTutorial += delegate () { StartCoroutine(ChangeTutorialTexts(tutorialFront, tutorialBack)); };
        GameManager.ToSelectMusic += delegate () { StartCoroutine(ClearAll()); };
        GameManager.ToGame += delegate () { StartCoroutine(Gaming()); };
        GameManager.ToResult += delegate () { StartCoroutine(ReadyCommunicating()); };
    }

    IEnumerator ChangeText(string text)
    {
        if (textBox.color.a < 0.01)
        {
            while(textBox.color.a <= 0.999)
            {
                textBox.color = new Color(textBox.color.r, textBox.color.g, textBox.color.b, textBox.color.a + Time.deltaTime);
                yield return null;
            }
        }
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

    IEnumerator ChangeTutorialTexts(string[] front, string back)
    {
        foreach(string s in front)
        {
            yield return ChangeText(s);
            yield return new WaitForSeconds(2.5f);
        }
        yield return ClearAll();
        yield return switcher.ChangeTexture(switcher.VideoTexture, switcher.TutorialVideoClip);
        // 비디오 재생 코루틴 실행

        switcher.VideoPlayer.loopPointReached += IsVideoEnded;
        VideoEnded = false;

        yield return new WaitUntil(() => VideoEnded);

        while (textBox.color.a <= 0.999)
        {
            textBox.color = new Color(textBox.color.r, textBox.color.g, textBox.color.b, textBox.color.a + Time.deltaTime);
            yield return null;
        }
        yield return ChangeText(back);

        yield return ButtonControl.ShowButton(tutorialEnd);
    }

    public IEnumerator Gaming()
    {
        yield return switcher.ChangeTexture(switcher.VideoTexture, switcher.GameVideoClip);
        switcher.VideoPlayer.loopPointReached += IsVideoEnded;
        VideoEnded = false;
        yield return new WaitForSeconds(2f);
        yield return new WaitUntil(() => VideoEnded);
        GameManager.ChangeState(STATE.RESULT);
        yield break;
    }

    public IEnumerator ReadyCommunicating()
    {
        yield return switcher.ChangeTexture(switcher.WebcamTexture, null, true);
        while (textBox.color.a <= 0.999)
        {
            textBox.color = new Color(textBox.color.r, textBox.color.g, textBox.color.b, textBox.color.a + Time.deltaTime);
            yield return null;
        }
        yield return ChangeText(communication);
        yield return ButtonControl.ShowButton(enterCommunication);
    }

    public IEnumerator ClearAll()
    {
        yield return ButtonControl.HideButton();
        while (TargetText.color.a > 0.001 || textBox.color.a > 0.001)
        {
            float timer = Time.deltaTime;
            TargetText.color = new Color(TargetText.color.r, TargetText.color.g, TargetText.color.b, TargetText.color.a - timer);
            textBox.color = new Color(textBox.color.r, textBox.color.g, textBox.color.b, textBox.color.a - timer);
            
            yield return null;
        }
    }

    private void IsVideoEnded(UnityEngine.Video.VideoPlayer vp)
    {
        MusicManager.Instance.musicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        VideoEnded = true;
    }
}
