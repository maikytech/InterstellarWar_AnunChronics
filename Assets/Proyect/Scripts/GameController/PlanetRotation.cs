using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetRotation : MonoBehaviour
{
    [SerializeField] float speedRotationPlanet;

    void Rotation()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * speedRotationPlanet);
    }

    private void FixedUpdate()
    {
        Rotation();
    }
}
