using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationaryAsterodController : MonoBehaviour
{
		void AsteriodMovement()			//Controla el movimiento del asteriode.
		{
			transform.position += transform.forward * Time.deltaTime * -15.0f;
		}

		void AsteroRotation()
		{
			transform.rotation = Quaternion.Euler (0f, Random.Range (0f, 360f), 0f);

		}

		void Start()
		{
			AsteroRotation ();
		}

		void Update()
		{
			AsteriodMovement ();
		}
}
