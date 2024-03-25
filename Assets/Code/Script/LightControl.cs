using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightControl : MonoBehaviour
{
    [SerializeField]
    public Light[] PointLight;

    [SerializeField][Range(2f, 5f)] private float intencity;

    [SerializeField]
    private bool waitForString = false;

    [SerializeField]
    private string stringToWaitFor = "";

    
    public bool turnOn;

    public void Control()
    {
        if (turnOn)
        {
            foreach (Light light in PointLight)
            {
                light.intensity = intencity;
            }
        }
        else
        {
            foreach(Light light in PointLight)
            {
                light.intensity = 0;
            }
        }
    }

    private void Awake()
    {
        MusicManager.markerUpdated += FadeOut;
    }

    private void FadeOut()
    {
        Debug.Log("FadeOut");
        foreach( Light light in PointLight)
        {
            light.intensity = intencity;
        }
        StartCoroutine(Fading());
    }

    private IEnumerator Fading()
    {
        while (PointLight[0].intensity > 0)
        {
            foreach (Light light in PointLight)
            {
                light.intensity -= Time.deltaTime * 2;
                Debug.Log($"{light.intensity}");
            }
            yield return null;
        }
    }
}
