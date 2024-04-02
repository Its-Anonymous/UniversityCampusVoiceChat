using UnityEngine;
using Photon.Pun;
using Photon.Voice.Unity;
using UnityEngine.UI;

public class VoiceManager : MonoBehaviourPun
{

    public Recorder recorder;
    public Image micImage;

    public void EnableDisableVoiceManager()
    {

        if (recorder != null)
        {
            gameObject.SetActive(true);
            recorder.TransmitEnabled = true; // Enable voice transmission
        }
    }


    public void ResetMicIcon()
    {
        Color color = micImage.color;
        color.a = 1;
        micImage.color =  color;
    }

    public void EnableDisableAudioTransmition()
    {
        if (recorder != null)
        {
            recorder.TransmitEnabled = !recorder.TransmitEnabled;
        }

        Color color = micImage.color;
        color.a = recorder.TransmitEnabled ? 1: .3f;
        micImage.color = color;
    }
}