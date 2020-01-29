//
// Programa que controla el comportamiento de los asteriodes.
//
// Created by Poseto Studio on 2/14/17.
// Copyright © 2017 Poseto Studio. All rights reserved.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteriodController : MonoBehaviour
{
	[SerializeField] int maxSpeedAsteroid;      //Velocidad maxima que tendrá un asteroide instanciado.
    [SerializeField] int minSpeedAsteroid;      //Velocidad minima que tendrá un asteroide instanciado.
    [SerializeField] float speedRotation;			//Rapidez de rotacion del asteriode.

	private Rigidbody rigidbodyAsteroidReference;		//Referencia al componente Rigidbody del asteriode.

	void Awake()
	{
		rigidbodyAsteroidReference = GetComponent<Rigidbody> ();		//Se referencia el componente Rigidbody del asteroide.

	}

	void Start()
	{
		AsteriodMovement ();
		AsteroidRotation ();
	}

	void AsteriodMovement()			//Controla el movimiento del asteriode.
	{
		//rigidbodyAsteroidReference.velocity = transform.forward * -speedAsteriod;

		rigidbodyAsteroidReference.velocity = transform.forward * -(Random.Range(minSpeedAsteroid, maxSpeedAsteroid));

	}

	void AsteroidRotation()			//El asteriode rotará de forma aleatoria en todos los ejes.
	{
		// angularVelocity es un vector de la velocidad angular.
		// insideUnitSphere retorna un vector aleatorio dentro de una esfera de radio uno, cada vez que se llame a esta funcion el asteroide tendra un vector de giro distinto.
		// Para evitar que el asteriode detenga su movimiento, se modifica la variable "Angular Drag" del componente Rigidbbody del asteriode.

		rigidbodyAsteroidReference.angularVelocity = Random.insideUnitSphere * speedRotation;
	}
}
