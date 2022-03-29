using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudioMgr : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip enemy01Die;
    // Start is called before the first frame update
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void PlayEnemy01Die()
    {
        //audioSource.clip = enemy01Die;
        if (!audioSource.isPlaying)
            audioSource.PlayOneShot(enemy01Die);
        
    }

}
