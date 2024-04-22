using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicRoomsController : MonoBehaviour
{
    public AudioClip Room1;
    public AudioClip Room2; 
    public AudioClip Room3;
    public AudioClip Room4;
    public AudioClip SoundPredeterminari;
    public AudioSource audioSource;
   
    private bool IsNotRoom = false;


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && !IsNotRoom)
        {
            IsNotRoom = true;
            MusicRoom();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player" && IsNotRoom)
        {
            IsNotRoom = false;
            audioSource.clip = SoundPredeterminari;
            audioSource.Play();
        }
    }

    void MusicRoom()
    {
        AudioClip currentRoomMusic = SoundPredeterminari;

        if(gameObject.name == "Room1")
        {
            currentRoomMusic = Room1;
        }
        else if (gameObject.name == "Room2")
        {
            currentRoomMusic = Room2;
        }
        else if (gameObject.name == "Room3")
        {
            currentRoomMusic = Room3;
        }
        else if (gameObject.name == "Room4")
        {
            currentRoomMusic = Room4;
        }

        audioSource.clip = currentRoomMusic;
        audioSource.Play();
    }
}
