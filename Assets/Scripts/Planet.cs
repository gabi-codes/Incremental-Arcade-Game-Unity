using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    [SerializeField] PlayerStats stats;


    private int maxHp = 5;
    private int currentHp = 5;


    private void Awake()
    {
        currentHp = maxHp + stats.maxHp;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        Vert vert = other.GetComponent<Vert>();
        if (vert != null)
        {
            stats.vertices += 1;
            Destroy(vert.gameObject);
            return;
        }

        EnemyCore enemy = other.GetComponentInParent<EnemyCore>();
        if (enemy != null)
        {
            currentHp -= enemy.damage;
            Destroy(enemy.gameObject);
            return;
        }
    }


}
