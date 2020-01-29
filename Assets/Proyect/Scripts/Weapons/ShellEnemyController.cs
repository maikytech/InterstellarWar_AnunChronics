using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellEnemyController : MonoBehaviour
{
    [SerializeField] float speedShell;                  //Rapidez del misil enemigo.
    [SerializeField] float speedFollowTarget;			//Rapidez de seguimiento del target.

	private Rigidbody rigidbodyShellReference;		    //Referencia al componente Rigidbody del proyectil.
	private Transform playerTransformReference;		    //Referencia al transform del Player.
	private Vector3 targetPosition;					    //Posicion del target.
	private UXController UXControllerClassReference;	//Referencia a la clase "UXController".

	void Awake()
	{
		rigidbodyShellReference = GetComponent<Rigidbody> ();
		playerTransformReference = GameObject.FindWithTag("Player").GetComponent<Transform>();
		UXControllerClassReference = GameObject.FindWithTag ("GameController").GetComponent<UXController> ();
	}

	void Start ()
	{
		ShellEnemyMovement ();
	}

	void ShellEnemyMovement()						// Controla la velocidad del laser.
	{
		rigidbodyShellReference.velocity = transform.forward * speedShell;
	}

	void FollowTarget()		//Controla el seguimiento de calor del misil.
	{
		if (gameObject.tag == "ShellEnemy1" && !UXController.isGameOver )		//Si es el misil 1, su target estara corrido 4 unidades hacia la derecha.
		{	
			targetPosition = new Vector3 (playerTransformReference.position.x + 1, playerTransformReference.position.y, gameObject.transform.position.z);

		}

		if (gameObject.tag == "ShellEnemy2" && !UXController.isGameOver)
		{
			targetPosition = new Vector3 (playerTransformReference.position.x - 1, playerTransformReference.position.y, gameObject.transform.position.z);
		}

		transform.position = Vector3.Lerp (transform.position, targetPosition, Time.deltaTime * speedFollowTarget);
	}

	void FixedUpdate()
	{
		FollowTarget ();
	}
}