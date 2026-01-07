using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BulletPlayer : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    
    private float shotSpeed = 10f;
    private float damage = 1f;
    
    private float lifeTine = 3f;

    private Vector2 direction;

    public void Init(Vector2 shootDirection)
    {
        direction = shootDirection.normalized;
        Destroy(gameObject, lifeTine);
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
        return shotSpeed + 2.0f * playerStats.shotSpeed;
    }

    float CalculateDamage()
    {
        return damage + playerStats.damage;
    }
}
