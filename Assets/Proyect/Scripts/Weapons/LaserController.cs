//
// Programa que controla el comportamiento de los lasers.
//
// Created by Poseto Studio on 2/14/17.
// Copyright © 2017 Poseto Studio. All rights reserved.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    [SerializeField] float speedLaser;				// Rapidez del rayo laser.

	private Rigidbody rigidbodyLaserReference;		// Referencia al componente Rigidbody del laser.

	void Awake()
	{
		rigidbodyLaserReference = GetComponent<Rigidbody> ();
	}

	void Start ()
	{
		laserPlayerMovement ();
	}

	void laserPlayerMovement()						// Controla la velocidad del laser.
	{
		rigidbodyLaserReference.velocity = transform.forward * speedLaser;
	}
}