using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class EnemyCollisionController : MonoBehaviour
{
    [SerializeField] int scoreForDestroy;                           //Puntaje por destrucion de la nave enemiga.
    [SerializeField] float LaserPlayerDamage;                       //Daño recibido por el Laser Player.
    [SerializeField] float ShellPlayerDamage;                       //Daño recibido por el misil del Player.
    [SerializeField] GameObject softEnemyExplosion;             //Referencia a la explosion suave de la nave enemiga.
    [SerializeField] GameObject DestructionEnemyExplosion;      //Referencia a la explosion suave de la nave enemiga.
    [SerializeField] GameObject shieldEnemy;						//Referencia al escudo de la nave enemiga.

    private int indexCurrentScene;
    private UXController UXControllerClassReference;			//Referencia a la clase "UXController".
	private SpawnEnemies spawnEnemiesClassReference;			//Referencia a la clase "SpawnEnemies".
    private NextScene nextSceneClass;                           
	private HealthController healthControllerClassReference;	//Referencia a la clase "HealthController".

	void Awake()
	{
		UXControllerClassReference = GameObject.FindWithTag ("GameController").GetComponent<UXController> ();
		spawnEnemiesClassReference = GameObject.FindWithTag ("GameController").GetComponent<SpawnEnemies> ();
        nextSceneClass = GameObject.FindWithTag("GameController").GetComponent<NextScene>();
		healthControllerClassReference = gameObject.GetComponent<HealthController>();
	}

    private void Start()
    {
        indexCurrentScene = SceneManager.GetActiveScene().buildIndex;
    }

    void OnTriggerEnter(Collider other)					
	{
			EnemiesCollisionSetup (other);
	}
		
	void EnemiesCollisionSetup(Collider other)
	{
		if (other.tag == "LaserPlayer")										//Si la nave enemiga es colisionada por el laser del player.
		{
			healthControllerClassReference.Damage(LaserPlayerDamage);		//Daño en la nave enemiga por cuenta del laser player.			
			StartCoroutine (ShieldConfiguration ());						//Se activa y desactiva la vidualizacion del escudo.
			CollisionController(other);
		}

		if (other.tag == "Shell")											//Si la nave enemiga es colisionada por el misil del player.
		{
			healthControllerClassReference.Damage(ShellPlayerDamage);		//Daño en la nave enemiga por cuenta del misil del player.				
			StartCoroutine (ShieldConfiguration ());						//Se activa y desactiva la vidualizacion del escudo.
			CollisionController(other);
			Instantiate(DestructionEnemyExplosion, other.transform.position, Quaternion.identity);	//Explosion grande por misil.
		}
	}

	void CollisionController(Collider other)													//Funcion que configura las colisiones con el Player.
	{
		Instantiate(softEnemyExplosion, other.transform.position, Quaternion.identity);		//Se instancia la explosion de daño de la nave.
		DestroyCollider (other);																//Destruye el objeto colisionador.			

		if(healthControllerClassReference.currentHealth <= 0)			//Si la salud de la nave es menor o igual a cero...
		{
			
			UXControllerClassReference.AddScore(scoreForDestroy);		
			spawnEnemiesClassReference.isEnemyOnScene = false;
			Instantiate(DestructionEnemyExplosion, other.transform.position, Quaternion.identity);
            spawnEnemiesClassReference.attackInvoke = true;

            if (gameObject.tag == "LevelBoss")
            {
                spawnEnemiesClassReference.isLevelBossOnScene = false;
                spawnEnemiesClassReference.isLevelBossDestroyed = true;


                if (indexCurrentScene < 6)
                {
                    nextSceneClass.goToNextScene = true;
                    nextSceneClass.InvokeNextStage();
                    UXControllerClassReference.InvokeMissionCompletedUXActive();
                }
            }

            if (gameObject.tag == "FinalBoss")
            {
                spawnEnemiesClassReference.isFinalBossOnScene = false;
                spawnEnemiesClassReference.finalBossDestroyed = true;
                nextSceneClass.goToNextScene = true;
                nextSceneClass.InvokeNextStage();
                UXControllerClassReference.GameOver ();
            }

            Destroy(gameObject);
        }
	}

	IEnumerator ShieldConfiguration()				//Configura la visualizacion del escudo.
	{
		shieldEnemy.SetActive(true);				//Activa la visualizacion del escudo.
		yield return new WaitForSeconds (2);		//Espera unos momentos.
		shieldEnemy.SetActive(false);				//Desactiva la visualizacion del escudo.
	}

	void DestroyCollider(Collider other)			//Destruye el objeto externo que colisiona.		
	{	
		Destroy (other.gameObject);			
	}

}
