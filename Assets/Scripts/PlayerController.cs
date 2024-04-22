using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    private Rigidbody _comprigidbody3D;
    public float speedX;
    public float raycastDistance = 1f;
 
    public float _xMovement;
    public float _zMovement;
    public EfectosSonido EfectosSonido;




    private void Awake()
    {
        _comprigidbody3D = GetComponent<Rigidbody>();
        EfectosSonido = GetComponent<EfectosSonido>();


    }

    void FixedUpdate()
    {
        Move();
        if(_xMovement != 0 || _zMovement != 0)
        {
            EfectosSonido.StartWalking();
        }
        else
        {
            EfectosSonido.StopWalking();
        }

    }

    void Move()
    {
        _comprigidbody3D.velocity = new Vector3(_xMovement * speedX, _comprigidbody3D.velocity.y, _zMovement * speedX);
    }

    public void OnMovementX(InputAction.CallbackContext context)
    {
        _xMovement = context.ReadValue<float>();
    }

    public void OnMovementZ(InputAction.CallbackContext context)
    {
        _zMovement = context.ReadValue<float>();
    }

}
