using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class YesNNoControl : MonoBehaviour
{
    [SerializeField]
    private Button YesButton;
    [SerializeField]
    private TextMeshProUGUI YesButtonText;

    
    
    [SerializeField]
    private Button NoButton;
    [SerializeField]
    private TextMeshProUGUI NoButtonText;

    [SerializeField]
    private List<Image> ButtonImages;

    [SerializeField] 
    private GameObject MusicSelectBoxControl;

    

    public void OnClickYes()
    {
        YesButton.interactable = false;
        NoButton.interactable = false;
        

        AudioManager.Instance.PlaySFX(SFX.Click);
        GameManager.ChangeState(STATE.SELECT_MUSIC);
        StartCoroutine(MusicSelectBoxControl.GetComponent<MusicSelectBoxControl>().Blink());

    }

    public void OnClickNo()
    {
        YesButton.interactable = false;
        NoButton.interactable = false;
        AudioManager.Instance.PlaySFX(SFX.Click);
        GameManager.ChangeState(STATE.TUTORIAL);
    }

    IEnumerator HideButton()
    {
        YesButton.interactable = false;
        NoButton.interactable = false;
        while (YesButtonText.color.a > 0.001)
        {
            YesButtonText.color = new Color(YesButtonText.color.r, YesButtonText.color.g, YesButtonText.color.b, YesButtonText.color.a - Time.deltaTime);
            NoButtonText.color = new Color(YesButtonText.color.r, YesButtonText.color.g, YesButtonText.color.b, YesButtonText.color.a);
            foreach (Image image in ButtonImages)
            {
                if (image != null) { image.color = new Color(image.color.r, image.color.g, image.color.b, YesButtonText.color.a); }
            }
            yield return null;
        }
        YesButtonText.color = new Color(YesButtonText.color.r, YesButtonText.color.g, YesButtonText.color.b, 0);
        NoButtonText.color = new Color(YesButtonText.color.r, YesButtonText.color.g, YesButtonText.color.b, 0);
        yield return null;
    }

    private void Awake()
    {
        YesButtonText.color = new Color(YesButtonText.color.r, YesButtonText.color.g, YesButtonText.color.b, 0);
        NoButtonText.color = new Color(YesButtonText.color.r, YesButtonText.color.g, YesButtonText.color.b, 0);
        
        foreach(Image image in ButtonImages)
        {
            if(image != null) { image.color = new Color(image.color.r, image.color.g, image.color.b, 0);}
        }
        
        YesButton.interactable = false;
        NoButton.interactable = false;

        GameManager.ToSkip += delegate () { StartCoroutine(ShowButtons()); };
        GameManager.ToSelectMusic += delegate { StartCoroutine(HideButton()); };
        GameManager.ToTutorial += delegate { StartCoroutine(HideButton()); };

    }
    public IEnumerator ShowButtons()
    {
        while (YesButtonText.color.a <=0.999)
        {
            YesButtonText.color = new Color(YesButtonText.color.r, YesButtonText.color.g, YesButtonText.color.b, YesButtonText.color.a + Time.deltaTime);
            NoButtonText.color = new Color(YesButtonText.color.r, YesButtonText.color.g, YesButtonText.color.b, YesButtonText.color.a);
            foreach (Image image in ButtonImages)
            {
                if (image != null) { image.color = new Color(image.color.r, image.color.g, image.color.b, YesButtonText.color.a); }
            }
            yield return null;
        }
        YesButtonText.color = new Color(YesButtonText.color.r, YesButtonText.color.g, YesButtonText.color.b, 1);
        NoButtonText.color = new Color(YesButtonText.color.r, YesButtonText.color.g, YesButtonText.color.b, 1);
        YesButton.interactable = true;
        NoButton.interactable = true;
        yield return null;
    }
}
