using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyLevel : MonoBehaviour
{
	private SpawnAsteroids spawnAsteroidsClassReference;		//Referencia a la clase "UXController".

	void Awake()
	{
		spawnAsteroidsClassReference = GameObject.FindWithTag ("GameController").GetComponent<SpawnAsteroids> ();
	}

	void LevelConfiguration()
	{
		spawnAsteroidsClassReference.cycleCounterIntern++;					//Aumenta la instanciación interna de asteriodes.
		spawnAsteroidsClassReference.cycleCounterExtern += 5;					//Aumenta la instanciación externa de asteriodes.
	}

	void Start()
	{
		InvokeRepeating("LevelConfiguration", 100f, 100f);
	}
}
