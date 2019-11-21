using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{

    public AudioClip healthItem, jump, impact;

    private AudioSource audioS;

    // Start is called before the first frame update
    void Start()
    {

        audioS = GetComponent<AudioSource>();

    }

    public void PlaySong(AudioClip clip)
    {

        audioS.clip = clip;
        audioS.Play();

    }

}
