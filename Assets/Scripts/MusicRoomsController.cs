using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicRoomsController : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip defaultBackgroundMusic;
    public AudioRooms roomSettings;

    private bool isInsideRoom = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !isInsideRoom)
        {
            isInsideRoom = true;
            MusicRoom();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" && isInsideRoom)
        {
            isInsideRoom = false;
            audioSource.clip = defaultBackgroundMusic;
            audioSource.Play();
        }
    }

    void MusicRoom()
    {
        AudioClip currentRoomMusic = defaultBackgroundMusic;

        if (roomSettings != null && roomSettings.backgroundMusic != null)
        {
            currentRoomMusic = roomSettings.backgroundMusic;
        }

        audioSource.clip = currentRoomMusic;
        audioSource.Play();
    }

}
