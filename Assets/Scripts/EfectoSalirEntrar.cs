using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EfectoSalirEntrar : MonoBehaviour
{
  public AudioClip enterSound;
    public AudioClip exitSound;
    public AudioSource audioSource;
    public Image fadeImage;
    private bool playerInside = false;
    public float fadeDuration = 1f;
    public float fadeDelay = 2f;
    private Color originalColor;
    private Coroutine fadeCoroutine;

    public GameObject[] objectsToActivate; 

    private void Start()
    {
        originalColor = fadeImage.color;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !playerInside)
        {
            playerInside = true;
            audioSource.PlayOneShot(enterSound);
            if (fadeCoroutine != null)
            {
                StopCoroutine(fadeCoroutine); 
            }
            fadeCoroutine = StartCoroutine(FadeAndReset());

            
            ActivateObjects();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && playerInside)
        {
            playerInside = false;
            audioSource.PlayOneShot(exitSound);
            if (fadeCoroutine != null)
            {
                StopCoroutine(fadeCoroutine); 
            }
            fadeCoroutine = StartCoroutine(FadeAndReset());
        }
    }

    private IEnumerator FadeAndReset()
    {
        yield return new WaitForSeconds(fadeDelay); 

        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration); 
            fadeImage.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        fadeImage.color = originalColor;
    }

    private void ActivateObjects()
    {
        for (int i = 0; i < objectsToActivate.Length; i++)
        {
            GameObject obj = objectsToActivate[i];

        
            BotDialogo npcMovement = obj.GetComponent<BotDialogo>();
            if (npcMovement != null)
            {
                npcMovement.StartPatrol(); 
            }

        }
    }
}
