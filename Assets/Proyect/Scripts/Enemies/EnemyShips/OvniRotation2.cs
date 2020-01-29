using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OvniRotation2 : MonoBehaviour
{
    [SerializeField] float speedRotation;							//Rapidez de rotacion del asteriode.

	void Rotation()									//El asteriode rotará de forma aleatoria en todos los ejes.
	{
		transform.Rotate (Vector3.up * Time.deltaTime * speedRotation);
	}

	void FixedUpdate()
	{
		Rotation ();
	}
}
