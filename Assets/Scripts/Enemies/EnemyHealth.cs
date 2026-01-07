using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sprite;

    private MaterialPropertyBlock propBlock;
    private float maxHp;

    void Awake()
    {
        propBlock = new MaterialPropertyBlock();
    }

    public void Init(float maxHp, Color color)
    {
        this.maxHp = maxHp;

        sprite.GetPropertyBlock(propBlock);
        propBlock.SetColor("_Color", color);
        sprite.SetPropertyBlock(propBlock);

        SetHealth(maxHp);
    }

    public void SetHealth(float currentHp)
    {
        float t = Mathf.Clamp01(currentHp / maxHp);

        sprite.GetPropertyBlock(propBlock);
        propBlock.SetFloat("_HP", t);          
        sprite.SetPropertyBlock(propBlock); 
    }
}
