using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Collider boxColider = gameObject.AddComponent<BoxCollider>();
        boxColider.isTrigger = false;
    }


    void OnParticleCollision(GameObject other)
    {
        Destroy(gameObject);
    }
}
