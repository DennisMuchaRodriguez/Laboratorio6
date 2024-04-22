using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EfectoSalirEntrar : MonoBehaviour
{
    public AudioClip enterSound;
    public AudioClip exitSound;
    public AudioSource audioSource;

    private bool playerInsede = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && !playerInsede)
        {
            playerInsede = true;
            audioSource.PlayOneShot(enterSound);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player" && playerInsede)
        {
            playerInsede=false;
            audioSource.PlayOneShot(exitSound);
        }
    }
}
