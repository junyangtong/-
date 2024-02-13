using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Random = System.Random;
using Range = UnityEngine.SocialPlatforms.Range;


public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    private static AudioManager curent;

    public AudioClip RunAudio;

    private AudioSource RunSource;
    private void Awake()
    {
        curent = this;
        DontDestroyOnLoad(gameObject);

        RunSource = gameObject.AddComponent<AudioSource>();
        RunSource.volume = 0.4f;    
    }

    public static void PlayFootStepAudio()
    {
        curent.RunSource.clip = curent.RunAudio;
        curent.RunSource.Play();
    }
}
