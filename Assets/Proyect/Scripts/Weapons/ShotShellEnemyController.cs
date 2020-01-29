using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotShellEnemyController : MonoBehaviour
{
    [SerializeField] float minTimeShot;                     //Valor minimo de tiempo para disparar un misil.
    [SerializeField] float maxTimeShot;                     //Valor maximo de tiempo para disparar un misil.
    [SerializeField] GameObject shootGameObjectReference;   //Referencia al gameobject del objeto a disparar.

    private float nextFire;									//Tiempo para el proximo disparo.
	private UXController UXControllerClassReference;		//Referencia a la clase "UXController".
	private GameController gameControllerClassReference;	//Referencia a la clase "GameController".

	void Awake()
	{
		UXControllerClassReference = GameObject.FindWithTag ("GameController").GetComponent<UXController> ();
	}

	void Start()
	{
		CyclicShotGeneration ();
		nextFire = 0f;
	}

	void CyclicShotGeneration()			//Genera el disparo de manera repetitiva y aleatoria.
	{
        if(gameObject.tag == "ShotMissileEnemy1")
        {
            InvokeRepeating("ShootShellEnemy", 5f, 1f);
        }else if(gameObject.tag == "ShotMissileEnemy2")
        {
            InvokeRepeating("ShootShellEnemy", 7f, 1f);
        }
    }

	void ShootShellEnemy()									// Controla el disparo del misil del Player.		
	{
		if(Time.time > nextFire)
		{
			nextFire = Time.time + Random.Range(minTimeShot, maxTimeShot);															//Controla el delay de disparo.
			Instantiate (shootGameObjectReference, gameObject.transform.position, gameObject.transform.rotation);	//Dispara.
		}
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
		LaserCancellation();
	}
}
