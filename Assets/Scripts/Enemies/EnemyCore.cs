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

    [Header("Stats")]
    [SerializeField] private float maxHp;
    [SerializeField] private float scale;

    [SerializeField] public int damage;
    [SerializeField] public float speed;

    [Header("Hit Reaction")]
    [SerializeField] private float hitForce = 0.25f;
    [SerializeField] private float springStrength = 80f;
    [SerializeField] private float damping = 12f;

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
                color = Color.yellow;
                scale = 1f;
                break;

            case 1:
                speed *= 1.5f;
                color = Color.blue;
                scale = 1.2f;
                break;

            case 2:
                speed *= 2.5f;
                color = Color.red;
                scale = 0.75f;
                break;
            case 3:
                speed *= 2.0f;
                color = Color.green;
                scale = 0.8f;
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


