using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Planet : MonoBehaviour
{
    [SerializeField] PlayerStats stats;
    [SerializeField] GameManager gameManager;

    [SerializeField] private SpriteRenderer health;

    private MaterialPropertyBlock propBlock;

    private int maxHp = 5;
    private int currentHp = 5;

    void Awake()
    {
        propBlock = new MaterialPropertyBlock();
    }

    public void Restart()
    {
        maxHp = 5 + stats.maxHp;
        currentHp = maxHp;

        SetHealth(currentHp);
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
            SetHealth(currentHp);

            Destroy(enemy.gameObject);

            if (currentHp <= 0)
            {
                EndSession();
            }

            return;
        }
    }

    public void SetHealth(float currentHp)
    {
        float t = Mathf.Clamp01(currentHp / maxHp);

        health.GetPropertyBlock(propBlock);
        propBlock.SetFloat("_HP", t);
        health.SetPropertyBlock(propBlock);
    }

    private void EndSession()
    {
        gameManager.EndSession();
    }

}
