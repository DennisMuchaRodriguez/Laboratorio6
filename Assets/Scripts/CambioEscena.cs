using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CambioEscena : MonoBehaviour
{
    public string nameEscena;

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == ("Player"))
        {
          
            SceneManager.LoadScene(nameEscena);
        }
    }
}
