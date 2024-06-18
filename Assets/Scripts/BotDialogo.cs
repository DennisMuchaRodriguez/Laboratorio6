using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;
using UnityEngine.AI;
public class BotDialogo : MonoBehaviour
{
     [SerializeField] private List<Transform> puntosDestino;
    [SerializeField] private float velocidadMovimiento = 2f;
    [SerializeField] private float tiempoEspera = 2f;
    [SerializeField] private GameObject dialogo;
    [SerializeField] private GameObject dialogoPanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private GameObject dialogueInteraction;

    private NavMeshAgent navMeshAgent;
    private int currentDestinationIndex = 0;
    private bool isPatrolling = false;
    private bool isPlayerInRange = false;
    private bool didDialogueStart = false;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        
    }

    public void StartPatrol()
    {
        if (puntosDestino.Count > 0)
        {
            isPatrolling = true;
            SetDestination(puntosDestino[currentDestinationIndex].position);
        }
        else
        {
            Debug.Log("No hay puntos de destino asignados para el patrullaje.");
        }
    }

    void SetDestination(Vector3 destination)
    {
        navMeshAgent.SetDestination(destination);
    }

    void Update()
    {
        if (isPatrolling && !navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.1f)
        {
            StartCoroutine(WaitAtDestination());
        }
    }

    IEnumerator WaitAtDestination()
    {
        isPatrolling = false;
        yield return new WaitForSeconds(tiempoEspera);
        currentDestinationIndex = (currentDestinationIndex + 1) % puntosDestino.Count;
        SetDestination(puntosDestino[currentDestinationIndex].position);
        isPatrolling = true;
    }

    void StartDialogo()
    {
        didDialogueStart = true;
        dialogoPanel.SetActive(true);
        dialogo.SetActive(true);
        dialogueInteraction.SetActive(false);
        Time.timeScale = 0;
    }

    void EndDialogo()
    {
        didDialogueStart = false;
        dialogoPanel.SetActive(false);
        dialogo.SetActive(false);
        dialogueInteraction.SetActive(true);
        Time.timeScale = 1;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = true;
            dialogo.SetActive(true);
            dialogueInteraction.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
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
        else if (context.started && didDialogueStart && isPlayerInRange)
        {
            EndDialogo();
        }
    }
}
