using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;					//Libreria para el manejo de UX.

public class Title : MonoBehaviour
{
    [SerializeField] Text title;					//Referencia al titulo.

	void Start ()
	{
		Invoke ("TitleSetup", 10f);
	}

	void TitleSetup()
	{

		title.enabled = false;
	}
}
