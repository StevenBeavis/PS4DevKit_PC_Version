using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float health = 100f;

    public float chaseSpeed;

    public int worth = 50;
    public static int strength = 50;

    public static bool damagedPlayer;

    Transform target;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;

        damagedPlayer = false;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, chaseSpeed * Time.deltaTime);

        if (damagedPlayer)
        {
            Die();
            damagedPlayer = false;
        }
    }

    public void TakeDamage(float amount)
    {
        health -= amount;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (!damagedPlayer)
        {
            PlayerController player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

            player.AddScore(worth);
        }
        Destroy(gameObject);    
    }
}
