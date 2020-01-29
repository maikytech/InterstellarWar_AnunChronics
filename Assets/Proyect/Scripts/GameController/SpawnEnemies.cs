//
// Programa que controla el comportamiento del player.
//
// Created by Poseto Studio on 2/14/17.
// Copyright © 2017 Poseto Studio. All rights reserved.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnEnemies : MonoBehaviour
{
    public bool attackInvoke = true;
    public bool isEnemyOnScene;							//Esta el enemigo en la escena?
	public bool isFinalBossOnScene;						//Esta el Boss Final en la escena?.
    public bool isLevelBossOnScene;
    public bool isLevelBossDestroyed;           
	public bool finalBossDestroyed;						//El Boss Final fue destruido?

    [SerializeField] int enemyCount;                    //Variable que contiene la cantidad de enemigos que han sido instanciados.
    [SerializeField] int finalBossCount;                //Variable que evita que se instancie infinitamente el Final Boss.
    [SerializeField] float delaySpawnEnemies;
    [SerializeField] GameObject bossLevel;              //Array de naves enemigas del nivel 1.
    [SerializeField] GameObject finalBoss;              //Referencia al Boss final.
    [SerializeField] GameObject[] enemyShipsLevel1;     //Array de naves enemigas del nivel 1.

    public int indexCurrentScene;
    //private NextScene nextSceneClass;
    private UXController UXControllerClassReference;	//Referencia a la clase "UXController".

	void Awake()
	{
        //nextSceneClass = GetComponent<NextScene>();
		UXControllerClassReference = GameObject.FindWithTag ("GameController").GetComponent<UXController> ();
	}

	void Start()
	{
		enemyCount = 0;
		finalBossCount = 0;
		isEnemyOnScene = false;
        isLevelBossDestroyed = false;
		isFinalBossOnScene = false;
		finalBossDestroyed = false;
        indexCurrentScene = SceneManager.GetActiveScene().buildIndex;
        delaySpawnEnemies = 50f;
    }
		
	void EnemyAttack()									
	{
        Invoke("SpawnEnemyShips", delaySpawnEnemies);
    }

	void SpawnEnemyShips()								//Instancia las naves enemigas en un punto lejano.
	{
        SpawnEnemySpacecrafts();
        SpawnLevelBoss();
        SpawnFinalBoss();
    }

    void SpawnEnemySpacecrafts ()
    {
        if (enemyCount < enemyShipsLevel1.Length && isEnemyOnScene == false)
        {
            Vector3 positionEnemies = new Vector3(0f, 1f, 4000f);
            Instantiate(enemyShipsLevel1[enemyCount], positionEnemies, Quaternion.identity);
            isEnemyOnScene = true;
            enemyCount++;
        }
    }

    void SpawnLevelBoss()
	{
        if(enemyCount >= enemyShipsLevel1.Length && isEnemyOnScene == false && isLevelBossDestroyed == false)
        { 
            Vector3 positionBoss = new Vector3 (0f, 1f, 4000f);
			Instantiate (bossLevel, positionBoss, Quaternion.identity);
			isEnemyOnScene = true;
            isLevelBossOnScene = true;
		}
	}

	void SpawnFinalBoss()
	{
		
		if (isLevelBossDestroyed == true && isLevelBossOnScene == false && isEnemyOnScene == false && indexCurrentScene == 6)
		{
			Vector3 positionFinalBoss = new Vector3 (0f, 5f, 4000f);
			Instantiate (finalBoss, positionFinalBoss, Quaternion.identity);
			isEnemyOnScene = true;
			isFinalBossOnScene = true;
			finalBossCount++;
		}
	}

	void AttackCancellation()							//Cancela la invocacion de la funcion que instancia las naves enemigas cuando se acabe el juego.
	{
		if (UXController.isGameOver)
		{
			CancelInvoke ();
		}
	}

	void Update()
	{
        if(attackInvoke)
        {
            EnemyAttack();
            attackInvoke = false;
        }
	}
}
