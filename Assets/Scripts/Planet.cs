using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Planet : MonoBehaviour
{
    [SerializeField] PlayerStats stats;
    [SerializeField] GameManager gameManager;
    [SerializeField] private SpriteRenderer background;

    [SerializeField] private SpriteRenderer health;
    [SerializeField] Color currentColor = Color.white;

    private MaterialPropertyBlock propBlock;

    private Coroutine hpTween;
    private Coroutine colorTween;

    private float colorFactor = 0.6f;

    private float maxHp = 5;
    private float currentHp = 5;

    private float currentHp01 = 1.0f;

    private readonly Color[] colorPath =
    {
        new Color(0f, 120f/255f, 0f),
        new Color(120f/255f, 120f/255f, 0f),
        new Color(120f/255f, 0f, 0f),
        new Color(120f/255f, 0f, 120f/255f),
        new Color(0f, 0f, 120f/255f),
        new Color(0f, 120f/255f, 120f/255f),
    };


    void Awake()
    {
        propBlock = new MaterialPropertyBlock();
        ApplyProperties();
    }

    void ApplyProperties()
    {
        health.GetPropertyBlock(propBlock);

        propBlock.SetFloat("_HP", currentHp01);
        propBlock.SetColor("_Color", currentColor);
        background.material.color = new Color(currentColor.r * colorFactor, currentColor.g * colorFactor, currentColor.b * colorFactor);

        health.SetPropertyBlock(propBlock);
    }

    public void Restart()
    {
        maxHp = 5 + stats.maxHp;
        currentHp = maxHp;

        SetHealthSmooth();
        SetColorSmooth(RandomBaseColor());
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
            SetHealthSmooth();

            Destroy(enemy.gameObject);

            if (currentHp <= 0)
            {
                EndSession();
            }

            return;
        }
    }

    private void EndSession()
    {
        gameManager.EndSession();
    }


    public void SetHealthSmooth(float duration = 0.5f)
    {
        float target = Mathf.Clamp01(currentHp / maxHp);

        if (hpTween != null)
            StopCoroutine(hpTween);

        hpTween = StartCoroutine(TweenHP(currentHp01, target, duration));
    }

    public void SetColorSmooth(Color target, float duration = 0.5f)
    {
        if (colorTween != null)
            StopCoroutine(colorTween);

        colorTween = StartCoroutine(TweenColor(currentColor, target, duration));
    }


    IEnumerator TweenHP(float from, float to, float duration)
    {
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;

            // ease out (smooth, naturalny)
            t = 1f - Mathf.Pow(1f - t, 3f);

            currentHp01 = Mathf.Lerp(from, to, t);

            health.GetPropertyBlock(propBlock);
            propBlock.SetFloat("_HP", currentHp01);
            health.SetPropertyBlock(propBlock);

            yield return null;
        }

        currentHp01 = to;
        ApplyProperties();
    }

    IEnumerator TweenColor(Color from, Color to, float duration)
    {
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;
            t = 1f - Mathf.Pow(1f - t, 3f);

            currentColor = Color.Lerp(from, to, t);

            health.GetPropertyBlock(propBlock);
            propBlock.SetColor("_Color", currentColor);
            background.material.color = new Color(currentColor.r * colorFactor, currentColor.g * colorFactor, currentColor.b * colorFactor);
            health.SetPropertyBlock(propBlock);

            yield return null;
        }

        currentColor = to;
        ApplyProperties();
    }

    Color RandomBaseColor()
    {
        float max = 120f / 255f;

        int zeroChannel = Random.Range(0, 3);
        int maxChannel = Random.Range(0, 2);

        float r;
        float g;
        float b;

        if (zeroChannel == 0)
        {
            r = 0f;
            g = maxChannel == 0 ? max : Random.Range(0f, max);
            b = maxChannel == 1 ? max : Random.Range(0f, max);
        }
        else if (zeroChannel == 1)
        {
            r = maxChannel == 0 ? max : Random.Range(0f, max);
            g = 0f;
            b = maxChannel == 1 ? max : Random.Range(0f, max);
        }
        else
        {
            r = maxChannel == 0 ? max : Random.Range(0f, max);
            g = maxChannel == 1 ? max : Random.Range(0f, max);
            b = 0f;
        }

            return new Color(r, g, b, 1f);
    }
}
