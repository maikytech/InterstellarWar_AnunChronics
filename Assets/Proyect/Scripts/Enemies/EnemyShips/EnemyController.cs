using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float speedFollowPlayerForAttack;            //Rapidez del seguimiento que hace la nave enemiga al player.
    [SerializeField] float attackDistance;					//Distancia de ataque.
    [SerializeField] float TimeUpdateEnemyPosition;
                              
    private Transform playerTransformReference;			//Referencia al transform del player.
	private UXController UXControllerClassReference;	//Referencia a la clase "UXController".
    private Rigidbody rigidbodyEnemyReference;     //Referencia al componente Rigidbody del jugador.

    void Awake()
	{
        if(UXController.isGameOver == false)
        {
            //Se utiliza "GameObject.FindWithTag" cuando el gameobject que contiene el script será instanciado.
            playerTransformReference = GameObject.FindWithTag("Player").GetComponent<Transform>();
        }

        rigidbodyEnemyReference = gameObject.GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Invoke("SpeedFollowConf", 5f);
    }

    IEnumerator EnemyMovement()							//Controla el movimiento de la nave hasta el punto de ataque y seguimiento del player.							
	{
		if (!UXController.isGameOver)		//Si el juego esta activo....
		{
			Vector3 attackPosition = new Vector3 (playerTransformReference.position.x, playerTransformReference.position.y, attackDistance);
			yield return new WaitForSeconds (TimeUpdateEnemyPosition);

            transform.position = Vector3.Lerp(transform.position, attackPosition, speedFollowPlayerForAttack * Time.deltaTime);
        }
	}

	void FixedUpdate()
	{
		StartCoroutine(EnemyMovement ());

	}

    void SpeedFollowConf()
    {
        speedFollowPlayerForAttack += 0.7f;
    }
}
