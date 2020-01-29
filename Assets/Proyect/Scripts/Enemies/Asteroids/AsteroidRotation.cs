using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidRotation : MonoBehaviour
{
    [SerializeField] float speedRotation;							//Rapidez de rotacion del asteriode.

	private Rigidbody rigidbodyAsteroidReference;		//Referencia al componente Rigidbody del asteriode.

	void Awake()
	{
		rigidbodyAsteroidReference = GetComponent<Rigidbody> ();		//Se referencia el componente Rigidbody del asteroide.

	}

	void Rotation()									//El asteriode rotará de forma aleatoria en todos los ejes.
	{
		rigidbodyAsteroidReference.angularVelocity = Random.insideUnitSphere * speedRotation;
	}
		
	void Start()
	{
		Rotation ();
	}
}