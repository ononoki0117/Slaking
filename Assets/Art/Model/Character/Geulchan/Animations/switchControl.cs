using FMODUnity;
using Mocopi.Receiver;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switchControl : MonoBehaviour
{
    public Animator animator;
    public RuntimeAnimatorController controller;
    public MocopiAvatar mocopi;

    [Header("Music properties")]
    [SerializeField] private EventReference Music; 
    [SerializeField] private EventReference DemoMusic;

    // Start is called before the first frame update
    void Start()
    {
        mocopi = FindAnyObjectByType<MocopiAvatar>();
        animator.runtimeAnimatorController = null;
        MusicManager.Instance.musicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        MusicManager.Instance.SetMusic(DemoMusic);
        MusicManager.Instance.StartMusic();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(animator.runtimeAnimatorController == null)
            {
                MusicManager.Instance.musicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                mocopi.enabled = false;
                animator.runtimeAnimatorController = controller;
                StartCoroutine(PlayAnimation(animator));
            }
            else
            {
                MusicManager.Instance.musicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                MusicManager.Instance.SetMusic(DemoMusic);
                MusicManager.Instance.StartMusic();
                mocopi.enabled = true;
                animator.runtimeAnimatorController = null;
            }
        }
    }

    private IEnumerator PlayAnimation(Animator animator)
    {
        MusicManager.Instance.musicInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        yield return new WaitForSeconds(1f);

        MusicManager.Instance.SetMusic(Music);
        MusicManager.Instance.StartMusic();

        yield return new WaitForSeconds(3f);
        animator.SetTrigger("DanceTrigger");

        yield return null;
    }
}
