//
// Programa que controla el comportamiento del player.
//
// Created by Poseto Studio on 2/14/17.
// Copyright © 2017 Poseto Studio. All rights reserved.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;				

public class HealthController : MonoBehaviour
{
    public float currentHealth;                         //Variable de estado de la salud actual.

    [SerializeField] float startingHealth;				//Variable que controla la salud del Player o del Enemigo.
    [SerializeField] float valueRegeneration;           //Valor de regeneracion que incrementa la salud del Player.
    [SerializeField] Slider sliderHealthReference;      //Referencia al slider de vida.
    [SerializeField] Image fillHealthImage;             //Referencia a la imagen de relleno.
    [SerializeField] Color fullHealthColor;             //Variable para el color de la salud maxima.
    [SerializeField] Color zeroHealthColor;				//Variable para el color de la salud minima.

	void Start()
	{
		currentHealth = startingHealth;							
		InvokeRepeating("healthRegeneration", 1f, 0.5f);			
		SetHealthUI();											
	}

	public void Damage(float amount)			
	{
		if(currentHealth > 0f)
		{
			currentHealth -= amount;					
		}
	}

	private void SetHealthUI()								//Funcion que actualiza la barra de salud del player o del enemigo.
	{
		sliderHealthReference.value = currentHealth;		//La barra de salud es igual al valor de la salud actual.

		//Color es una estructura
		//Lerp interpola gradualmente los colores.
		//currentHealth/startingHealth es el porcentaje de salud.
		fillHealthImage.color = Color.Lerp(zeroHealthColor, fullHealthColor, (currentHealth/startingHealth));
	}

	void healthRegeneration()
	{
		//Si la salud actual es menor que la maxima y el juego no ha terminado entonces....
		if ((currentHealth < startingHealth) && (!UXController.isGameOver))
		{
			currentHealth += valueRegeneration;		//Se regenera la salud del Player.
			SetHealthUI();							//Actualiza la barra de vida.
		}


	}
}
