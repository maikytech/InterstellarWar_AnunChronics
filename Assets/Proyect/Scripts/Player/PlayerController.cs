//
// Programa que controla el comportamiento del player.
//
// Created by Poseto Studio on 2/14/17.
// Copyright © 2017 Poseto Studio. All rights reserved.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;       //Libreria para el uso de controles virtuales.

public class PlayerController : MonoBehaviour
{
    public bool IsPlayerStatic;

    [SerializeField] float tilting;                 //Inclinacion de la nave cuando realiza el movimiento horizontal.
    [SerializeField] float speed;                   //Rapidez del movimiento.
    [SerializeField] Vector3 velocityVector;        //Vector velocidad del Player.

    private float horizontalMovement;               //Variable que controla el movimiento derecha e izquierda del jugador.
    private float verticalMovement;                 //Variable que controla el movimiento arriba y abajo del jugador.
    private Vector3 vectorPosition;                 //Vector posicion del Player.
    private Rigidbody rigidbodyPlayerReference;     //Referencia al componente Rigidbody del jugador.
    private float timer;
    private float realTimeSinceStartGame;

    void Awake()
    {
        rigidbodyPlayerReference = gameObject.GetComponent<Rigidbody>();        //Se referencia el componente Rigidbody del jugador.
    }

    public void Movement()                                                      //Controla el movimiento del Player.
    {
        realTimeSinceStartGame = Time.realtimeSinceStartup;

        horizontalMovement = CrossPlatformInputManager.GetAxis("Horizontal");
        verticalMovement = CrossPlatformInputManager.GetAxis("Vertical");

        //Mathf.Clamp restringe la posicion del Player para que no se salga de la pantalla.
        velocityVector = new Vector3(horizontalMovement, verticalMovement, 0f);
        vectorPosition = new Vector3(Mathf.Clamp(rigidbodyPlayerReference.position.x, -25, 25), Mathf.Clamp(rigidbodyPlayerReference.position.y, 2, 28), 0);


        rigidbodyPlayerReference.position = vectorPosition;
        rigidbodyPlayerReference.velocity = velocityVector * speed;

        //Efecto de rotacion de la nave cuando se mueve horizontalmente.
        rigidbodyPlayerReference.rotation = Quaternion.Euler(0f, 0f, horizontalMovement * tilting);

        //Is true whether the player is static

        //IsPlayerStatic = velocityVector.x <= 0.01f && velocityVector.x >= -0.01 && velocityVector.y <= 0.01f && velocityVector.y >= -0.01;

        if((velocityVector.x <= 0.01f && velocityVector.x >= -0.01) && (velocityVector.y <= 0.01f && velocityVector.y >= -0.01) && realTimeSinceStartGame > 18f)
        { 
            timer += Time.fixedDeltaTime;


            if(timer > 3f)
            {
                IsPlayerStatic = true;
                timer = 0f;
            }
        }
        else
        {
            IsPlayerStatic = false;
            timer = 0f;
        }
    }

    void FixedUpdate()
    {
        Movement();
    }
}