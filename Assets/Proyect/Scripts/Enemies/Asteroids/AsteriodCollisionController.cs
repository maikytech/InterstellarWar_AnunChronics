using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/******     Configuracion de las colisiones de los asteriodes      ************/

public class AsteriodCollisionController : MonoBehaviour
{
	[SerializeField] GameObject asteroidExplosion;						//Referencia a la explosion del asteroide.

	private UXController UXControllerClass;								//Referencia a la clase "UXController".
	private SpawnEnemies spawnEnemiesClass;								//Referencia a la clase "SpawnEnemies".
	private HealthController healthControllerClass;						//Referencia a la clase "HealthController".
	private PlayerCollisionController playerCollisionControllerClass;	//Referencia a la clase "PlayerCollisioncontroller".
    private NextScene nextSceneClass;

    void Awake()
	{
		UXControllerClass = GameObject.FindWithTag ("GameController").GetComponent<UXController> ();
		spawnEnemiesClass = GameObject.FindWithTag ("GameController").GetComponent<SpawnEnemies> ();
        nextSceneClass = GameObject.FindWithTag("GameController").GetComponent<NextScene>();

        if (!UXController.isGameOver)
        {
            playerCollisionControllerClass = GameObject.FindWithTag("Player").GetComponent<PlayerCollisionController>();
        }
		
		healthControllerClass = gameObject.GetComponent<HealthController>();
	}

	void OnTriggerEnter(Collider other)					
	{
        if(nextSceneClass.goToNextScene == false)
        {
            CollisionController(other);
        }
	}

	void CollisionController(Collider other)
	{
		if (other.tag ==  "LaserPlayer" || other.tag ==  "Shell")
		{
			Instantiate(asteroidExplosion, gameObject.transform.position, Quaternion.identity);	//Se instancia la explosion del asteroide.
			UXControllerClass.AddScore(playerCollisionControllerClass.scoreForDestroyAsteriod);	//Se llama a la funcion AddScore para sumar puntos por destruccion del asteroide.
			DestroyCollider (other);															//Se destruye el laser o el proyectil.
			DestroyAsteroid();															//Se destruye el asteroide.
		}
	}
		
	void DestroyCollider(Collider other)	//Destruye el objeto externo que colisiona.		
	{	
		Destroy (other.gameObject);			
	}

	void DestroyAsteroid()					//Destruye el asteroide.		
	{
		Destroy (gameObject);			
	}
}
