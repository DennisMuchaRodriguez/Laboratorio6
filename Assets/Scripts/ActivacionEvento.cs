using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivacionEvento : MonoBehaviour
{
   public BotDialogo botDialogo;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == ("Player"))
        {
            botDialogo.StartPatrol(); 
            gameObject.SetActive(false); 
        }
    }
}
