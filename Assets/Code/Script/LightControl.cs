using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightControl : MonoBehaviour
{
    //[SerializeField]
    //public Light[] PointLight;

    [SerializeField]
    [Range(2f, 20f)] private float markerInitialIntencity;
    [SerializeField]
    [Range(2f, 20f)] private float markerIntencity;
    [SerializeField]
    [Range(2f, 20f)] private float beatInitialIntencity;
    [SerializeField]
    [Range(2f, 20f)] private float beatIntencity;

    [SerializeField]
    private bool waitForString = false;

    [SerializeField]
    private string stringToWaitFor = "";

    [SerializeField]
    private Material markerMaterial;
    [SerializeField]
    private Material beatMaterial;

    private bool isFirstMarker = true;

    public bool turnOn;

    public void Control()
    {
        //if (turnOn)
        //{
        //    foreach (Light light in PointLight)
        //    {
        //        light.intensity = initialIntencity;
        //    }
        //}
        //else
        //{
        //    foreach(Light light in PointLight)
        //    {
        //        light.intensity = 0;
        //    }
        //}
    }

    private void Awake()
    {
        MusicManager.markerUpdated += MarkerFadeOut;
        MusicManager.beatUpdate += BeatFadeOut;
    }

    private void MarkerFadeOut()
    {
        if (isFirstMarker)
        {
            isFirstMarker = false;
            return;
        }
        markerIntencity = markerInitialIntencity;
        //Debug.Log("FadeOut");
        //foreach( Light light in PointLight)
        //{
        //    light.intensity = intencity;
        //}
        //StartCoroutine(LightFading());
        StartCoroutine(MarkerFade());
    }

    private void BeatFadeOut()
    {
        beatIntencity = beatInitialIntencity;
        StartCoroutine(BeatFade());
    }

    //private IEnumerator LightFading()
    //{
    //    while (PointLight[0].intensity > 0)
    //    {
    //        foreach (Light light in PointLight)
    //        {
    //            light.intensity -= Time.deltaTime * 2;
    //            Debug.Log($"{light.intensity}");
    //        }
    //        yield return null;
    //    }
    //}

    private IEnumerator MarkerFade()
    {
        while (markerIntencity > 4)
        {
            markerIntencity -= Time.deltaTime * 5;
            markerMaterial.SetColor("_EmissionColor", new Color(1,1,1) * markerIntencity);
            yield return null;
        }
    }

    private IEnumerator BeatFade()
    {
        while (beatIntencity > 5)
        {
            beatIntencity -= Time.deltaTime * 7;
            beatMaterial.SetColor("_EmissionColor", new Color(1,1,1) * beatIntencity);
            yield return null;
        }
    }
}
