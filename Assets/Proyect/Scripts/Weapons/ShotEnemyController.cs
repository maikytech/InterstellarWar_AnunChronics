using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotEnemyController : MonoBehaviour
{
    [SerializeField] GameObject shootGameObjectReference;	//Referencia al gameobject del objeto a disparar.

	private float nextFire;									//Tiempo para el proximo disparo.
	private float fireRateEnemySynchronized;				//Variable que contendrá la sincronización de fireRateEnemy.
	private float fireRateBoss1Synchronized;				//Variable que contendrá la sincronización de fireRateBoss1.
	private UXController UXControllerClassReference;		//Referencia a la clase "UXController".
	private GameController gameControllerClassReference;	//Referencia a la clase "GameController".

	void Awake()
	{
		UXControllerClassReference = GameObject.FindWithTag ("GameController").GetComponent<UXController> ();
		gameControllerClassReference = GameObject.FindWithTag ("GameController").GetComponent<GameController> ();
	}

	void Start()
	{
		CyclicShotGeneration();			//Genera el disparo de manera repetitiva y aleatoria.
		nextFire = 0f;
	}

	void CyclicShotGeneration()			//Genera el disparo de manera repetitiva y aleatoria.
	{
		InvokeRepeating ("ShootEnemy", 5f, 0.01f);
	}

	void ShootEnemy ()									//Configuración de disparo del enemigo.
	{
		if(Time.time > nextFire && gameObject.tag == "ShotSpawnEnemy")
		{
			nextFire = Time.time + fireRateEnemySynchronized;														//Controla el delay de disparo.
            Instantiate (shootGameObjectReference, gameObject.transform.position, gameObject.transform.rotation);	//Dispara.
					
		}
	}
		
	void synchronizationFireRate()			//Sincroniza la variable fireRateEnemySynchronized con el valor aleatorio que se encuentra en la variable fireRateEnemy.		
	{
		fireRateEnemySynchronized = gameControllerClassReference.fireRateEnemy;
	}

	void LaserCancellation()			//El enemigo deja de disparar despues de ser destruido.							
	{
		if (UXController.isGameOver)
		{
			CancelInvoke ();			//Cancela la invocacion de la función del Laser del enemigo si se acaba el juego.
		}
	}

	void Update()
	{
		synchronizationFireRate ();		//Constantemente esta sincronizando el numero aleatorio.
		LaserCancellation ();
	}
}
