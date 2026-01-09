using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour
{
    [Header("Skill")]
    [SerializeField] public SkillDefinition skill;

    [Header("PlayerStats")]
    [SerializeField] public PlayerStats stats;

    [Header("References")]
    [SerializeField] private GameObject root;
    [SerializeField] private Image icon;
    [SerializeField] private Image borderImage;
    [SerializeField] private Image fillImage;
    [SerializeField] private Image progressImage;
    [SerializeField] private Image branachImage;
    [SerializeField] private RectTransform branchPivot;
    [SerializeField] private Button button;

    [Header("Colors")]
    [SerializeField] private Color colorDisabled;
    [SerializeField] private Color colorLocked;
    [SerializeField] private Color colorCantBuy;
    [SerializeField] private Color colorCanBuy;
    [SerializeField] private Color colorBought;

    private Color currentColor;
    private float progress;

    private float colorTransitionTime = 0.6f;
    private Coroutine colorRoutine;

    private States state;
    private States lastState = States.Unknown;

    enum States
    {
        Disabled,
        Locked,
        CantBuy,
        CanBuy,
        Bought,
        Unknown
    }

    private void Awake()
    {
        icon.sprite = skill.icon;
        root.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public void SetInactive()
    {
        branachImage.color = colorDisabled;
        icon.color = colorDisabled;
        borderImage.color = colorDisabled;
        fillImage.color = colorDisabled;
        progressImage.color = colorDisabled;
    }

    public void OnClick()
    {
        SkillTreeManager.Instance.Buy(skill);
    }

    public void UpdateVisuals(int currentLevel, int levelOfReq)
    {
        if (!IsEnabled(levelOfReq)) { state = States.Disabled; }
        else if (IsBought(currentLevel)) { state = States.Bought; }
        else if (!IsUnlocked(levelOfReq)) { state = States.Locked; }
        else if (CanBuy(currentLevel)) { state = States.CanBuy; }
        else { state = States.CantBuy; }

        if (state == lastState) return;

        switch (state)
        {
            case States.Disabled:
                button.enabled = false;
                currentColor = colorDisabled;

                TransitionColors
                (
                    currentColor,
                    currentColor,
                    currentColor,
                    currentColor,
                    currentColor
                );

                progress = 0f;

                break;

            case States.Locked:
                button.enabled = false;
                currentColor = colorLocked;

                TransitionColors
                (
                    currentColor,
                    currentColor,
                    currentColor,
                    new Color(currentColor.r, currentColor.g, currentColor.b, 0.1f),
                    new Color(currentColor.r, currentColor.g, currentColor.b, 0.7f)
                );

                progress = 0f;
                break;

            case States.Bought:
                button.enabled = false;
                currentColor = colorBought;

                TransitionColors
                (
                    currentColor,
                    Color.white,
                    Color.white,
                    new Color(currentColor.r, currentColor.g, currentColor.b, 0.1f),
                    new Color(currentColor.r, currentColor.g, currentColor.b, 0.7f)
                );

                progress = 1f;
                break;

            case States.CanBuy:
                button.enabled = true;
                currentColor = colorCanBuy;

                TransitionColors
                (
                    currentColor,
                    Color.white,
                    currentColor,
                    new Color(currentColor.r, currentColor.g, currentColor.b, 0.1f),
                    new Color(currentColor.r, currentColor.g, currentColor.b, 0.7f)
                );

                progress = (float)currentLevel / (float)skill.maxPoints;
                break;

            case States.CantBuy:
                button.enabled = false;
                currentColor = colorCantBuy;

                TransitionColors
                (
                    currentColor,
                    Color.white,
                    currentColor,
                    new Color(currentColor.r, currentColor.g, currentColor.b, 0.1f),
                    new Color(currentColor.r, currentColor.g, currentColor.b, 0.7f)
                );

                progress = (float)currentLevel / (float)skill.maxPoints;
                break;
        }

        progressImage.fillAmount = progress;
        branchPivot.localEulerAngles = new Vector3(0f, 0f, skill.branchRotation);
    }

    void TransitionColors(Color branch, Color icon, Color border, Color fill, Color progress)
    {
        if (colorRoutine != null)
            StopCoroutine(colorRoutine);

        colorRoutine = StartCoroutine(ColorTransitionRoutine(
            branch,
            icon,
            border,
            fill,
            progress
        ));
    }

    IEnumerator ColorTransitionRoutine(Color targetBranch, Color targetIcon, Color targetBorder, Color targetFill, Color targetProgress)
    {
        float t = 0f;

        Color startBranch = branachImage.color;
        Color startIcon = icon.color;
        Color startBorder = borderImage.color;
        Color startFill = fillImage.color;
        Color startProgress = progressImage.color;

        while (t < 1f)
        {
            t += Time.unscaledDeltaTime / colorTransitionTime;
            float eased = 1f - (1f - t) * (1f - t);

            branachImage.color = Color.Lerp(startBranch, targetBranch, eased);
            icon.color = Color.Lerp(startIcon, targetIcon, eased);
            borderImage.color = Color.Lerp(startBorder, targetBorder, eased);
            fillImage.color = Color.Lerp(startFill, targetFill, eased);
            progressImage.color = Color.Lerp(startProgress, targetProgress, eased);

            yield return null;
        }

        branachImage.color = targetBranch;
        icon.color = targetIcon;
        borderImage.color = targetBorder;
        fillImage.color = targetFill;
        progressImage.color = targetProgress;
    }


    public bool IsEnabled(int levelOfReq)
    {
        return levelOfReq != 0;
    }

    public bool IsUnlocked(int levelOfReq)
    {
        if (skill.requiredSkill != null)
        {
            if (levelOfReq < skill.requiredPoints) return false;
        }
        
        return true;
    }
    
    public bool CanBuy(int currentLevel)
    {
        int level = currentLevel;

        if (level >= skill.maxPoints)
            return false;

        int cost = skill.costs[level];
        if (stats.vertices < cost)
            return false;

        return true;
    }

    public bool IsBought(int currentLevel)
    {
        return (currentLevel == skill.maxPoints);
    }

    
}

