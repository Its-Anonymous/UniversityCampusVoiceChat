using UnityEngine;
using Photon.Pun;
using Photon.Voice.Unity;
using UnityEngine.UI;

public class VoiceManager : MonoBehaviourPun
{
    public bool speakerEnable = true;

    public Recorder recorder;
    public Image micImage;
    public Image speakerImage;

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

    public void EnableDisableSpeaker()
    {

        foreach (var item in FindObjectsOfType<ClothDynamic>(true))
        {
            Debug.Log(item.name , item.gameObject);
            item.GetComponent<AudioSource>().enabled = item.GetComponent<Speaker>().enabled = !speakerEnable;
        }

        Color color = speakerImage.color;
        color.a = speakerEnable ? 1 : .3f;
        speakerImage.color = color;
    }
}