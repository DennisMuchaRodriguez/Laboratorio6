using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
public class BotDialogo : MonoBehaviour
{
    [SerializeField] private List<Transform> puntosDestino;
    [SerializeField] private float velocidadMovimiento = 2f;
    [SerializeField] private float tiempoQuieto = 2f; 
    [SerializeField] private GameObject dialogo;
    [SerializeField] private GameObject dialogoPanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private GameObject dialogueInteraction;
    private bool isPlayerInRange;
    private int currentDestinationIndex = 0;
    private bool isMoving = false;

    private bool didDialogueStart;
    
    void Update()
    {
       
        if (!isMoving && !didDialogueStart)
        {
            StartCoroutine(MoveToNextDestination());
        }
    }
    private IEnumerator MoveToNextDestination()
    {
        isMoving = true;
        Vector3 nextDestination = puntosDestino[currentDestinationIndex].position;
        while (transform.position != nextDestination)
        {
            transform.position = Vector3.MoveTowards(transform.position, nextDestination, velocidadMovimiento * Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(tiempoQuieto); 

        currentDestinationIndex = (currentDestinationIndex + 1) % puntosDestino.Count;
        isMoving = false;
    }
    private void StartDialogo()
    {
        didDialogueStart = true;
        dialogoPanel.SetActive(true);
        dialogo.SetActive(false);
        dialogueInteraction.SetActive(false);
        Time.timeScale = 0;
    }
    private void EndDialogo()
    {
        didDialogueStart = false;
        dialogoPanel.SetActive(false);
        dialogo.SetActive(true);
        dialogueInteraction.SetActive(true);
        Time.timeScale = 1;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            isPlayerInRange = true;
            dialogo.SetActive(true);
            dialogueInteraction.SetActive(true);
        }
       
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            isPlayerInRange = false;
            dialogo.SetActive(false);
            dialogueInteraction.SetActive(false);
        }
       
    }
    public void OnInteract(InputAction.CallbackContext context)
    {
            if (context.started && !didDialogueStart && isPlayerInRange)
            {
                StartDialogo();
            }
            else if (context.started && didDialogueStart&& isPlayerInRange)
            {
                EndDialogo();
            }
        
    }
}
