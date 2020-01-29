    //
// Programa que controla el comportamiento general del juego.
//
// Created by Poseto Studio on 2/14/17.
// Copyright © 2017 Poseto Studio. All rights reserved.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
	//public GameObject enemyGameobjectReference;		//Referencia al Gameobject del enemigo.
	public float fireRateEnemy;							//Frecuencia de fuego enemigo, es aleatorio y sincronizado con dos lasers.

    [SerializeField] float minValueFrecuencyShotEnemy;
    [SerializeField] float maxValueFrecuencyShotEnemy;

	private UXController uxControllerClassReference;		//Referencia a la clase UXController.
	private SpawnAsteroids spawnAsteroidsClassReference;	//Referencia a la clase SpawnAsteroids.


	void Awake()
	{
		spawnAsteroidsClassReference = GetComponent<SpawnAsteroids> ();		//Referencia a la clase que controla la generacion de asteroides.
	}
				
	void Start()
	{
		StartCoroutine(spawnAsteroidsClassReference.Spawn ());				//Se llama a la funcion que genera los asteroides.
		synchronizationEnemy();										//Comienza la sincronizacion de los lasers enemigos.

	}
		
	/***************  Laser & Shell Enemy configuration  ********************/

	void randomGeneratorEnemy()		//Genera un valor aleatorio y se asigna a la variable fireRateEnemy.
	{
		fireRateEnemy = Random.Range (minValueFrecuencyShotEnemy, maxValueFrecuencyShotEnemy);
	}

	void synchronizationEnemy()				
	{
		//Invoca la funcion que genera el numero aleatorio desde el comienzo del juego, y lo sigue invocando cada 2 segundos.
		InvokeRepeating ("randomGeneratorEnemy", 1f, 2f); 
	}
}
