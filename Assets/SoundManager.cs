using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip wallHit;
    public AudioClip paddleHit;
    public AudioClip tileDestroyed;
    public AudioClip tileInjured;
    public AudioClip nextLevel;
    public AudioClip gameOver;

    AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Play(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
