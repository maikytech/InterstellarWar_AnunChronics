using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellController : MonoBehaviour
{
    [SerializeField] float speedShell;

	private Rigidbody rigidbodyShellReference;		// Referencia al componente Rigidbody del proyectil.

	void Awake()
	{
		rigidbodyShellReference = GetComponent<Rigidbody> ();
	}

	void Start ()
	{
		ShellPlayerMovement ();
	}

	void ShellPlayerMovement()						// Controla la velocidad del laser.
	{
		rigidbodyShellReference.velocity = transform.forward * speedShell;
	}
}
