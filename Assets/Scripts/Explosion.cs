using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion
{
    public static void Create(Vector3 position, GameObject prefab)
    {
        GameObject explosion = MonoBehaviour.Instantiate(prefab, position,
            Quaternion.identity) as GameObject;

        MonoBehaviour.Destroy(explosion, 
            explosion.GetComponent<ParticleSystem>().startLifetime);
    }
}
