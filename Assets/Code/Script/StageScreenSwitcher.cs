using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class StageScreenSwitcher : MonoBehaviour
{
    [Header("Target RawImage")] 
    [SerializeField] private RawImage TargetImage;

    [Header("Textures")]
    [SerializeField] public Texture VideoTexture;
    [SerializeField] public Texture ImageTexture;

    [Header("Black Cover")]
    [SerializeField] private Image BlackScreen;

    [Header("Webcam properties")]
    [SerializeField] public WebCamTexture WebcamTexture;
    [SerializeField] public int WebcamIndex = 0;

    [Header("Video properties")]
    [SerializeField] public VideoPlayer VideoPlayer;
    [SerializeField] public VideoClip TutorialVideoClip;
    [SerializeField] public VideoClip GameVideoClip;

    [Header("Music properties")]
    [SerializeField] private EventReference GameMusic;
    [SerializeField] private EventReference TutorialMusic;

    private void Awake()
    {
        WebCamDevice[] devices = WebCamTexture.devices;
        for (int i = 0; i < devices.Length; i++)
        {
            Debug.Log(devices[i].name);
        }
        if (WebcamTexture != null)
        {
            WebcamTexture.Stop();
            WebcamTexture = null;
        }

        WebCamDevice device = WebCamTexture.devices[WebcamIndex];
        WebcamTexture = new WebCamTexture(device.name);
        
        BlackScreen.color = new Color(0, 0, 0, 1);

        GameManager.ToWearing += delegate () { StartCoroutine(ChangeTexture(ImageTexture)); };
        GameManager.ToTutorial += delegate () { StartCoroutine(FadeIn()); };
        GameManager.ToSelectMusic += delegate () { StartCoroutine(FadeIn()); };
        //GameManager.ToGame += delegate () { StartCoroutine(ChangeTexture(VideoTexture, GameVideoClip)); };
        GameManager.ToCommunication += delegate () { StartCoroutine(ChangeTexture(WebcamTexture)); };
        GameManager.ToResult += delegate () { StartCoroutine(FadeIn()); };
        GameManager.ToRequestEncore += delegate () { StartCoroutine(ChangeTexture(WebcamTexture)); };
    }

    IEnumerator FadeIn()
    {
        while(BlackScreen.color.a < 0.999)
        {
            BlackScreen.color = new Color(0, 0, 0, BlackScreen.color.a + Time.deltaTime);
            yield return null;
        }
        yield break;
    }

    IEnumerator FadeOut()
    {
        while (BlackScreen.color.a > 0.001)
        {
            BlackScreen.color = new Color(0, 0, 0, BlackScreen.color.a - Time.deltaTime);
            yield return null;
        }
        yield break;
    }

    public IEnumerator ChangeTexture(Texture texture, VideoClip clip = null, bool isWebcam = false)
    {
        yield return FadeIn();
        
        TargetImage.texture = texture;

        if (clip != null)
        {
            VideoPlayer.clip = clip;
        }

        if (isWebcam)
        {
            TargetImage.texture = WebcamTexture;
            WebcamTexture.Play();
        }

        yield return FadeOut();

        if(clip != null)
        {
            if(GameManager.CURRENT_STATE == STATE.TUTORIAL)
            {
                MusicManager.Instance.SetMusic(TutorialMusic);
            }
            if (GameManager.CURRENT_STATE == STATE.GAME)
            {
                MusicManager.Instance.SetMusic(GameMusic);
            }

            yield return new WaitForSeconds(2f);
            MusicManager.Instance.StartMusic();
            VideoPlayer.Play();
        }

        yield break;
    }



}
