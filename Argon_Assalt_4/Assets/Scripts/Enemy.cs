using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathFX;
    [SerializeField] Transform parent;
    // Start is called before the first frame update
    [SerializeField] int scorePerHit = 12;
    [SerializeField] int healthPoints = 100;
    ScoreBoard scoreBoard;
    void Start()
    {
        AddBoxCollider();
        scoreBoard = FindObjectOfType<ScoreBoard>();
    }

    private void AddBoxCollider()
    {
        Collider boxColider = gameObject.AddComponent<BoxCollider>();
        boxColider.isTrigger = false;
    }

    void OnParticleCollision(GameObject other)
    {
        ProcessHit();
        if (healthPoints <= 0)
        {
            KillEnemy();
        }
    }

    private void ProcessHit()
    {
        scoreBoard.ScoreHit(scorePerHit);
        healthPoints = healthPoints - 10;
    }

    private void KillEnemy()
    {
        GameObject fx = Instantiate(deathFX, transform.position, Quaternion.identity);
        fx.transform.parent = parent;
        Destroy(gameObject);
    }
}
