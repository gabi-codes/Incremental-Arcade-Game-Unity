using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCore : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject root;

    [SerializeField] private SpriteRenderer outsideSprite;
    [SerializeField] private EnemyHealth enemyHealth;
    [SerializeField] private Vert vertPrefab;

    [Header("Stats")]
    [SerializeField] private float maxHp;
    [SerializeField] private float scale;

    [SerializeField] public int damage;
    [SerializeField] public float speed;

    [Header("Hit Reaction")]
    [SerializeField] private float hitForce = 0.25f;
    [SerializeField] private float springStrength = 80f;
    [SerializeField] private float damping = 12f;

    [Header("Colors")]
    [SerializeField] private Color[] colors; 

    private float currentHp;
    private Color color;

    private Vector2 hitOffset;
    private Vector2 hitVelocity;

    Coroutine flashRoutine;

    public void Init(int tier)
    {
        SetTier(tier);
        enemyHealth.Init(maxHp, color);
    }

    public void SetTier(int tier)
    {
        switch (tier)
        {
            case 0:
                color = colors[0];
                break;

            case 1:
                color = colors[1];
                scale = 1.1f;
                speed *= 1.1f;
                maxHp += 3;
                maxHp *= 1.1f;
                break;

            case 2:
                color = colors[2];
                scale = 1.2f;
                speed *= 1.25f;
                maxHp += 8;
                maxHp *= 1.2f;
                break;

            case 3:
                color = colors[3];
                scale = 1.3f;
                speed *= 1.35f;
                maxHp += 20;
                maxHp *= 1.3f;
                break;

            case 4:
                color = colors[4];
                scale = 1.0f;
                speed *= 1.75f;
                maxHp += 10;
                maxHp *= 1.2f;
                break;
        }

        outsideSprite.color = color;
        outsideSprite.transform.localScale = new Vector3(scale, scale, 1);

        currentHp = maxHp;
        
    }

    void Update()
    {
        UpdateHitReaction();
    }

    public void TakeDamage(float dmg)
    {
        currentHp -= dmg;
        enemyHealth.SetHealth(currentHp);

        ApplyHitReaction();

        if (currentHp <= 0)
        {
            Die();
        }

    }

    void Die()
    {
        int count = Random.Range(1, 3);

        for (int i = 0; i < count; i++)
        {
            Vert vert = Instantiate(vertPrefab, (Vector2)transform.position, Quaternion.identity);
        }
        
        Destroy(root);
    }

    void ApplyHitReaction()
    {
        Vector2 dir = (outsideSprite.transform.position).normalized;

        dir += Random.insideUnitCircle * 0.3f;
        dir.Normalize();

        hitVelocity += dir * hitForce;

        flashRoutine = StartCoroutine(FlashWhite());
    }

    void UpdateHitReaction()
    {
        Vector2 force = -hitOffset * springStrength;
        hitVelocity += force * Time.deltaTime;

        hitVelocity *= Mathf.Exp(-damping * Time.deltaTime);

        hitOffset += hitVelocity * Time.deltaTime;

        outsideSprite.transform.localPosition = hitOffset;
    }

    IEnumerator FlashWhite()
    {
        outsideSprite.color = Color.white;
        yield return new WaitForSeconds(0.06f);
        outsideSprite.color = color;
    }
}


