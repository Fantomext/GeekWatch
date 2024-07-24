using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstStartVoice : MonoBehaviour
{
    string voiceName;
    [SerializeField]
    AudioClip clip;
    [SerializeField]
    AudioSource source;

    void Start()
    {
        source = GetComponent<AudioSource>();
        voiceName = clip.name;

        if (!PlayerPrefs.HasKey(voiceName))
            FirstVoice();
    }

    void FirstVoice()
    {
        source.PlayOneShot(clip);
        PlayerPrefs.SetInt(voiceName, 1);
    }
    
}
