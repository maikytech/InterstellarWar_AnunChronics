using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;               //Libreraia para el uso de botones virtuales.

public class ShotShellController : MonoBehaviour
{
    [SerializeField] float fireRate;                        // Tasa de disparo.
    [SerializeField] GameObject shootGameObjectReference;   // Referencia al gameobject del objeto a disparar.

	private float nextFire;
    private UXController UXControllerClassReference;

    void Awake()
    {
        UXControllerClassReference = GameObject.FindWithTag("GameController").GetComponent<UXController>();
    }

    void Start()
	{
		nextFire = 0f;
	}

	void ShootShellPlayer()									// Controla el disparo del misil del Player.		
	{
		if (gameObject.tag == "ShotShell")
		{
			//Cuando el jugador oprima la tecla de disparo del misil, se creara una instancia del prefab con un intervalo de tiempo.
            if (CrossPlatformInputManager.GetButton ("Fire2") && Time.time > nextFire && UXControllerClassReference.IsShellButtonActive)
			{
				nextFire = Time.time + fireRate;			//Controla el delay de disparo.
				Shoot ();
                UXControllerClassReference.IsShellButtonActive = false;
                UXControllerClassReference.hasShellBeenfired = true;
                UXControllerClassReference.shellButtonAnimator.SetBool("hasShellBeenfired", true);
                UXControllerClassReference.shellButtonAnimator.SetBool("IsShellButtonActivated", false);
                UXControllerClassReference.shellButtonImage.fillAmount = 0f;
                UXControllerClassReference.missileButtonImage.fillAmount = 0f;
            }
		}
	}

	void Shoot()									//Ejecuta el disparo.
	{
		Instantiate (shootGameObjectReference, gameObject.transform.position, Quaternion.identity);

    }

	void Update()
	{
		Invoke("ShootShellPlayer", 1f);
	}
}
