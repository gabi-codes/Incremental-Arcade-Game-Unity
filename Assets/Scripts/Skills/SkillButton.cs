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
    [SerializeField] private Color colorLocked;
    [SerializeField] private Color colorCantBuy;
    [SerializeField] private Color colorCanBuy;
    [SerializeField] private Color colorBought;

    private Color currentColor;
    private float progress;

    private void Awake()
    {
        icon.sprite = skill.icon;
        root.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public void OnClick()
    {
        print("a");
        SkillTreeManager.Instance.Buy(skill);
    }

    public void UpdateVisuals(int currentLevel, int levelOfReq)
    {
        if (!IsEnabled(levelOfReq))
        {
            root.SetActive(false);
            return;
        }

        root.SetActive(true);

        if (IsBought(currentLevel))
        {
            currentColor = colorBought;
            progress = 1f;
            button.enabled = false;
        }
        else if (!IsUnlocked(levelOfReq))
        {
            currentColor = colorLocked;
            progress = 0f;
            button.enabled = false;
        }
        else
        {
            progress = (float)currentLevel / (float)skill.maxPoints;

            if (CanBuy(currentLevel))
            {
                currentColor = colorCanBuy;
                button.enabled = true;
            }
            else
            {
                currentColor = colorCantBuy;
                button.enabled = false;
            }    
        }

        progressImage.fillAmount = progress;

        borderImage.color = currentColor;
        branachImage.color = currentColor;
        fillImage.color = new Color(currentColor.r, currentColor.g, currentColor.b, 0.1f);
        progressImage.color = new Color(currentColor.r, currentColor.g, currentColor.b, 0.5f);

        branchPivot.localEulerAngles = new Vector3(0f, 0f, skill.branchRotation);

        print(progressImage.fillAmount);
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

