using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileCollisionControler : MonoBehaviour
{
    [SerializeField] GameObject DestructionPlayerExplosion;

    void OnTriggerEnter(Collider other)
    {
        MissileCollisionConf(other);

    }

    void MissileCollisionConf(Collider other)
    {
       if(other.tag == "Shell" || other.tag == "ShellEnemy1" || other.tag == "ShellEnemy2")
        {
            Instantiate(DestructionPlayerExplosion, other.transform.position, Quaternion.identity);
            Destroy(other);
            Destroy(gameObject);
        }
    }
}
