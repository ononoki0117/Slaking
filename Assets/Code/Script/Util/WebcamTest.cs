using UnityEngine;
using UnityEngine.UI;

public class WebcamTest : MonoBehaviour
{
    public RawImage display1;
    public RawImage display2;
    WebCamTexture camTexture1;
    WebCamTexture camTexture2;
    private int currentIndex = 0;

    private void Start()
    {
        WebCamDevice[] devices = WebCamTexture.devices;

        for (int i = 0; i < devices.Length; i++)
        {
            Debug.Log(devices[i].name);
        }

        if (camTexture1 != null)
        {
            display1.texture = null;
            camTexture1.Stop();
            camTexture1 = null;
        }
        WebCamDevice device = WebCamTexture.devices[currentIndex];
        camTexture1 = new WebCamTexture(device.name);
        display1.texture = camTexture1;
        camTexture1.Play();


        if (camTexture2 != null)
        {
            display2.texture = null;
            camTexture2.Stop();
            camTexture2 = null;
        }
        WebCamDevice device2 = WebCamTexture.devices[currentIndex+1];
        camTexture2 = new WebCamTexture(device2 .name);
        display2.texture = camTexture2;
        camTexture2.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
