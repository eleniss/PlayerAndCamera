using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    CharacterController _characterController;
    ImputController _input;
    public float Speed = 1;

    public float JumpSpeed = 10;
    private Vector3 _lastVelocity;


    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _input = GetComponent<ImputController>();
    }

    // Update is called once per frame
    void Update()
    {
        
        Move();
    }

    private void Jump(ref Vector3 velocity)
    {
        velocity.y = JumpSpeed;
    }
    private bool ShouldJump()
    {
        return _input.Jump; //TO TO replace with new input
    }


    private void Move()
    {
        Vector3 direction = new Vector3(_input.Move.x, 0, _input.Move.y); //La y es 0 para que solo se mueva horizontalmente, pero aun así el
                                                                          //eje z tenemos y pq en InputController tenemos que coge un vector2, de manera que solo hay x e y
        //_characterController.SimpleMove(direction * Speed);   //Tiene en cuenta la gravedad de por si.

        Vector3 velocity = new Vector3();
        velocity.x = direction.x * Speed;
        velocity.y = _lastVelocity.y;
        velocity.z = direction.z * Speed;
        velocity.y = GetGravity();                  //Esta y la aterior  sirve para calcular la velocidad
        
        
        //Jump 

        if (ShouldJump())               //El salto tiene que ser depsues de la gravedad, porque sino la sobreescribe
        {
            Jump(ref velocity);
        }


       

        _characterController.Move(velocity  *Time.deltaTime);       //Esta para cambiar de posicion teniendo en cuenta la velocidad que emos calculado antes.


        

        //Turn
        if (direction.magnitude > 0)
        {
            Vector3 target = transform.position + direction;
            transform.LookAt(target);
        }
        _lastVelocity = velocity;
    }

    private float GetGravity()
    {
        return _lastVelocity.y + Physics.gravity.y * Time.deltaTime;
    } 
}
