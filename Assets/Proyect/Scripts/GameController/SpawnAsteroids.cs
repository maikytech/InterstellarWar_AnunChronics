using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAsteroids : MonoBehaviour
{
	public int cycleCounterExtern;							    //Cantidad de ciclos para la generacion de asteroides externos.
	public int cycleCounterIntern;                              //Cantidad de ciclos para la generacion de asteroides internos.

    [SerializeField] int StationaryAsteroidsPerWave;         //Cantidad de ciclos para la generacion de asteroides estacionarios.
    [SerializeField] float startSpawnDelay;                        //Tiempo de espera para que se comiencen a generar los asteroides.
    [SerializeField] float waveDelay;                              //Tiempo de espera entre la generación de cada horda de asteroides.
    //[SerializeField] float spawnDelayDirectAsteroids;
    [SerializeField] float spawnDelay;                             //Tiempo de espera en el ciclo for para la instanciacion de cada asteroide.
    [SerializeField] float spawnAsteroidDistance;                  //Distancia donde se instanciarán los asteroides.
    [SerializeField] GameObject[] asteroidsReferences;             //Array que contiene los diferentes asteroides.
    [SerializeField] GameObject[] externalAsteroids;
    [SerializeField] GameObject[] stationaryAsteroidsReferences;    //Array que contiene los diferentes asteroides.

    private Vector3[] positionsCloseToPlayer;
    private NextScene nextSceneClass;
	private UXController uxControllerClassReference;		//Referencia a la clase "UXController".
	private SpawnEnemies spawnEnemiesClassReference;		//Referencia a la clase "SpawnEnemies".
	private Transform playerTransformReference;				//Referencia al transform del Player.
    private PlayerController playerController;
    private Vector3 PositionClosePlayerStatic;


    void Awake()
	{
		uxControllerClassReference = GetComponent<UXController> ();
        nextSceneClass = GetComponent<NextScene>();
		spawnEnemiesClassReference = GameObject.FindWithTag ("GameController").GetComponent<SpawnEnemies> ();
		playerTransformReference = GameObject.FindWithTag("Player").GetComponent<Transform>();
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    public IEnumerator Spawn()
	{
		int overallLimit1;				//Limite inferior del intervalo para la generacion aleatoria de los asteroides globales.
		int overallLimit2;				//Limite superior del intervalo para la generacion aleatoria de los asteroides globales.
		int localLimit1;				//Limite inferior del intervalo para la generacion aleatoria de los asteroides locales.
		int localLimit2;				//Limite superior del intervalo para la generacion aleatoria de los asteroides locales.	

		overallLimit1 = -600;
		overallLimit2 = 600;
		localLimit1 = -50;
		localLimit2 = 50;

		yield return new WaitForSeconds (startSpawnDelay);

		//Ciclo de instanciacion de los asteroides.
		while (true)
		{
			StartCoroutine (InstantiateExternalAsteroids (overallLimit1, overallLimit2, cycleCounterExtern));
			StartCoroutine (InstantiateAsteroids (localLimit1, localLimit2, cycleCounterIntern));
			StartCoroutine (InstantiateDirectAsteroid());
            StartCoroutine(InstantiateWanderingAsteroids(localLimit1, localLimit2, StationaryAsteroidsPerWave));

            yield return new WaitForSeconds (waveDelay);

			if (UXController.isGameOver)
			{
				//Se activa el boton de Restart despues de que se terminan de generar los asteroides.
				uxControllerClassReference.RestartGameobjectReference.SetActive (true);

				break;					//Esta instruccion nos saca fuera de la instruccion While, por los cual, se dejan de generar asteriodes.
			}

            if(nextSceneClass.goToNextScene)
            {
                break;
            }
        }
	}

	//Función que controla la instanciacion de asteroides.
	IEnumerator InstantiateAsteroids(int limit1, int limit2, int cycleCounter)
	{
		for (int i = 0; i < cycleCounter; i++)
		{	
			Vector3 positionSpawnAsteroids = new Vector3 (Random.Range (Random.Range (limit1, limit2), Random.Range (limit1, limit2)), Random.Range (Random.Range (limit1, limit2), Random.Range (limit1, limit2)), spawnAsteroidDistance);
			Instantiate (asteroidsReferences [Random.Range (0, asteroidsReferences.Length)], positionSpawnAsteroids, Quaternion.identity);

			yield return new WaitForSeconds (spawnDelay);
		}
	}

    //Función que controla la instanciacion de asteroides.
    IEnumerator InstantiateExternalAsteroids(int limit1, int limit2, int cycleCounter)
    {
        for (int i = 0; i < cycleCounter; i++)
        {
            int limite = 30;

            Vector3 positionSpawnAsteroids = new Vector3(Random.Range(Random.Range(limit1, limit2), Random.Range(limit1, limit2)), Random.Range(Random.Range(limit1, limit2), Random.Range(limit1, limit2)), spawnAsteroidDistance);

            if ((positionSpawnAsteroids.x > limite || positionSpawnAsteroids.x < -limite) && (positionSpawnAsteroids.y > limite || positionSpawnAsteroids.y < -limite))
            {
                //Instantiate(stationaryAsteroidsReferences[Random.Range(0, stationaryAsteroidsReferences.Length)], positionSpawnAsteroids, Quaternion.identity);
                Instantiate(externalAsteroids[Random.Range(0, externalAsteroids.Length)], positionSpawnAsteroids, Quaternion.identity);
            }

            yield return new WaitForSeconds(spawnDelay);
        }
    }

    //Función que controla la instanciacion de asteroides errantes, con direcciones aleatorias.
    IEnumerator InstantiateWanderingAsteroids(int limit1, int limit2, int cycleCounter)
	{
		for (int i = 0; i < cycleCounter; i++)
		{
			int limite = 20;

			Vector3 positionSpawnAsteroids = new Vector3 (Random.Range (Random.Range (limit1, limit2), Random.Range (limit1, limit2)), 
                                                            Random.Range (Random.Range (limit1, limit2), Random.Range (limit1, limit2)), 
                                                            Random.Range(limite, -35));

            if (UXController.isGameOver == false)
            {
                PositionClosePlayerStatic = new Vector3(playerTransformReference.position.x, playerTransformReference.position.y, playerTransformReference.position.z - 20);
            }
            else
            {
                PositionClosePlayerStatic = new Vector3(0, 0, 0);
            }

            if ((positionSpawnAsteroids.x > limite || positionSpawnAsteroids.x < -limite) && (positionSpawnAsteroids.y > limite || positionSpawnAsteroids.y < -limite))
			{
				Instantiate (stationaryAsteroidsReferences [Random.Range (0, stationaryAsteroidsReferences.Length)], positionSpawnAsteroids, Quaternion.identity);
			}

            if (playerController.IsPlayerStatic && !spawnEnemiesClassReference.isEnemyOnScene)
            {
                Instantiate(stationaryAsteroidsReferences[Random.Range(0, stationaryAsteroidsReferences.Length)], PositionClosePlayerStatic, Quaternion.identity);
            }

            yield return new WaitForSeconds (spawnDelay);
		}
	}

	//Funcion que genera un asteroide directo a la posicion del Player
	IEnumerator InstantiateDirectAsteroid()
	{
		Vector3 playerPosition = new Vector3(playerTransformReference.position.x, playerTransformReference.position.y, spawnAsteroidDistance);
        Instantiate (asteroidsReferences [Random.Range (0, asteroidsReferences.Length)], playerPosition, Quaternion.identity);

        Vector3 ToRightPlayerPosition = new Vector3(playerTransformReference.position.x + 3f, playerTransformReference.position.y, spawnAsteroidDistance);
        Vector3 ToLeftPlayerPosition = new Vector3(playerTransformReference.position.x - 3f, playerTransformReference.position.y, spawnAsteroidDistance);
        Vector3 ToUpPlayerPosition = new Vector3(playerTransformReference.position.x, playerTransformReference.position.y + 3f, spawnAsteroidDistance);
        Vector3 ToDownPlayerPosition = new Vector3(playerTransformReference.position.x, playerTransformReference.position.y - 3f, spawnAsteroidDistance);

        positionsCloseToPlayer = new Vector3[] { ToRightPlayerPosition, ToLeftPlayerPosition, ToUpPlayerPosition, ToDownPlayerPosition };
        Instantiate(asteroidsReferences[Random.Range(0, asteroidsReferences.Length)], positionsCloseToPlayer[Random.Range(0, positionsCloseToPlayer.Length)], Quaternion.identity);

        yield return new WaitForSeconds (spawnDelay);
	}


    void decreaseAsteroidWave()		//Decremento de la oleada de asteroides cuando alguna nave enemiga esta en escena.
	{

		if (spawnEnemiesClassReference.isEnemyOnScene)
		{
			waveDelay = 10;

		} else if (!spawnEnemiesClassReference.isEnemyOnScene)
			{
				waveDelay = 5;
			}
	}

	void Update()
	{
		decreaseAsteroidWave ();
	}
}
