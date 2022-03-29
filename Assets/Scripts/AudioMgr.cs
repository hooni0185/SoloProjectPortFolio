using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMgr : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip audioClip01;
    public AudioClip audioClip02;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.clip = audioClip01;
        audioPlay();
    }

    public void audioPlay()
    {
        if(!audioSource.isPlaying)
            audioSource.Play();
    }
}
