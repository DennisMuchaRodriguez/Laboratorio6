using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EfectosSonido : MonoBehaviour
{
    public AudioSource StepAudioSource;
    public AudioClip StepSound;

    private void Awake()
    {
        StepAudioSource = GetComponent<AudioSource>();
    }

    private void PlayFootStepSound()
    {
        if (!StepAudioSource.isPlaying)
        {
            StepAudioSource.clip = StepSound;
            StepAudioSource.Play();
        }
    }

    public void StartWalking()
    {
        PlayFootStepSound();
    }

    public void StopWalking()
    {
        StepAudioSource.Stop();
    }
}