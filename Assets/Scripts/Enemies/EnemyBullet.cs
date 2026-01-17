using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BulletEnemy : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;

    private float shotSpeed = 10f;
    private float damage = 1f;
    private float lifeTime = 3f;

    private Vector2 direction;

    public void Init(Vector2 shootDirection)
    {
        direction = shootDirection.normalized;
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.position += (Vector3)(direction * CalculateShootSpeed() * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        EnemyCore enemy = other.GetComponent<EnemyCore>();
        if (enemy != null)
        {
            enemy.TakeDamage(CalculateDamage());
            Destroy(gameObject);
        }
    }

    float CalculateShootSpeed()
    {
        return shotSpeed;
    }

    float CalculateDamage()
    {
        return damage;
    }
}
