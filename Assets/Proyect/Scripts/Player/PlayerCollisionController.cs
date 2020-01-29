//
// Programa que controla el comportamiento del player.
//
// Created by Poseto Studio on 2/14/17.
// Copyright © 2017 Poseto Studio. All rights reserved.


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionController : MonoBehaviour
{
    public int scoreForDestroyAsteriod;

    [SerializeField] int scoreDestroyPlayerForAsteroid;         //Puntaje por colision con asteroide..
    [SerializeField] int asteroidCollisionDamage;
    [SerializeField] float LaserEnemyDamage;
    [SerializeField] int ShellEnemyDamage;
    [SerializeField] GameObject softPlayerExplosion;            //Referencia al Gameobject de la explosion suave del Player.
    [SerializeField] GameObject DestructionPlayerExplosion;     //Referencia al Gameobject de la explosion suave del Player.
    [SerializeField] GameObject asteroidExplosion;              //Referencia a la explosion del asteroide.
    [SerializeField] GameObject shieldPlayer;					//Referencia al escudo del Player.

	private UXController UXControllerClassReference;			//Referencia a la clase "UXController".
	private HealthController healthControllerClassReference;	//Referencia a la clase "HealthController".

	void Awake()
	{
		UXControllerClassReference = GameObject.FindWithTag ("GameController").GetComponent<UXController> ();
		healthControllerClassReference = gameObject.GetComponent<HealthController>();
	}

	void OnTriggerEnter(Collider other)					
	{
		if (!UXController.isGameOver)			//Si el juego se encuentra activo...
		{
			PlayerCollisionSetup (other);
		}
	}

	void PlayerCollisionSetup(Collider other)
	{
		switch(other.tag)
		{
			case ("LaserEnemies"):											//Si el objeto colisionador es el laser enemigo.

				healthControllerClassReference.Damage(LaserEnemyDamage);	//Daño en la nave provocado por laser enemigo.
				CollisionController(other);
				StartCoroutine( ShieldConfiguration());
				break;

			case ("ShellEnemy1"):
			case ("ShellEnemy2"):

				healthControllerClassReference.Damage (ShellEnemyDamage);	//Daño en la nave provocado por misil enemigo.
				CollisionController (other);
				StartCoroutine (ShieldConfiguration ());
				Instantiate(DestructionPlayerExplosion, other.transform.position, Quaternion.identity);		//Destruccion grande por misil.
				break;

			case ("Asteroid"):

				healthControllerClassReference.Damage(asteroidCollisionDamage);		//Daño en la nave provocado por colision con asteroide.
				CollisionController(other);
                UXControllerClassReference.AddScore(scoreForDestroyAsteriod);
                StartCoroutine( ShieldConfiguration());
				Instantiate(asteroidExplosion, other.transform.position, Quaternion.identity);		//Se instancia la explosion del asteroide.
				break;

			default:
				break;
		}
	}

	void CollisionController(Collider other)											//Funcion que configura las colisiones con el Player.
	{
		Instantiate(softPlayerExplosion, other.transform.position, Quaternion.identity);	//Se instancia la explosion de la nave.
		DestroyCollider (other);															//Destruye el objeto colisionador.
		UXControllerClassReference.damagePanelSetup();

		if(healthControllerClassReference.currentHealth <= 0)	//Si la salud de la nave es menor o igual a cero...
		{
			DestroyPlayer();									//Destruye el Player.
			UXControllerClassReference.GameOver();				//Llama a la funcion GameOver.
			Instantiate(DestructionPlayerExplosion, other.transform.position, Quaternion.identity);	//Se instancia la explosion de la nave.
		}
	}

	IEnumerator ShieldConfiguration()				//Configura la visualizacion del escudo.
	{
		shieldPlayer.SetActive(true);						//Activa la visualizacion del escudo.

		yield return new WaitForSeconds (2);		//Espera unos momentos.

		shieldPlayer.SetActive(false);					//Desactiva la visualizacion del escudo.
	}

	void DestroyCollider(Collider other)	//Destruye el objeto externo que colisiona.		
	{	
		Destroy (other.gameObject);			
	}

	void DestroyPlayer()			//Destruye el Player.		
	{
		Destroy (gameObject);			
	}
}
