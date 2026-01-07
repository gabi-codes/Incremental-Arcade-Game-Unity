using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTreeManager : MonoBehaviour
{
    public static SkillTreeManager Instance;

    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private int vertices;

    [SerializeField] private List<SkillDefinition> allSkills;

    private Dictionary<string, int> skillLevels = new();

    void Awake()
    {
        Instance = this;
        playerStats.damage = 0;
        playerStats.shotSpeed = 0;
        playerStats.speed = 0;
    }

    public bool CanBuy(SkillDefinition skill)
    {
        int level = skillLevels[skill.id];

        if (level >= skill.maxPoints)
            return false;

        int cost = skill.costs[level];
        if (vertices < cost)
            return false;

        if (skill.requiredSkill != null)
        {
            int reqLevel = skillLevels[skill.requiredSkill.id];
            if (reqLevel < skill.requiredPoints)
                return false;
        }

        return true;
    }

    public void Buy(SkillDefinition skill)
    {
        if (!CanBuy(skill))
            return;

        int level = skillLevels[skill.id];
        int cost = skill.costs[level];

        vertices -= cost;
        skillLevels[skill.id]++;

        ApplySkill(skill);
    }

    void ApplySkill(SkillDefinition skill)
    {
        switch (skill.skillType)
        {
            case SkillType.Damage:
                playerStats.damage += skill.valuePerPoint;
                break;

            case SkillType.Speed:
                playerStats.speed += skill.valuePerPoint;
                break;

            case SkillType.ShotSpeed:
                playerStats.shotSpeed += skill.valuePerPoint;
                break;
        }
    }


}
