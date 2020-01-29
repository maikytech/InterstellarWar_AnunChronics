using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;               //Libreria para el uso de controles virtuales.

public class ShotController : MonoBehaviour
{
    [SerializeField] float fireRate;                         // Tasa de disparo.
    [SerializeField] GameObject shootGameObjectReference;    // Referencia al gameobject del objeto a disparar.
                           
	private float nextFire;									// Tiempo para el proximo disparo.

    private void Start()
    {
        nextFire = 0f;
    }

    void ShootPlayer()                                      // Controla el disparo del laser del Player.		
    {
        if (gameObject.tag == "ShotSpawnPlayer")
        {
            if (CrossPlatformInputManager.GetButton("Fire1") && Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;            //Controla el delay de disparo.
                Shoot();
            }
        }
    }
		
	void Shoot()									//Ejecuta el disparo.
	{
        Instantiate(shootGameObjectReference, gameObject.transform.position, gameObject.transform.rotation);
	}
    	
	void Update()
	{
		Invoke ("ShootPlayer", 8f);
	}
}
