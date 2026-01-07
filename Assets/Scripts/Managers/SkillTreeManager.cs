using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class SkillTreeManager : MonoBehaviour
{
    public static SkillTreeManager Instance;

    [SerializeField] private PlayerStats playerStats;

    [SerializeField] private List<SkillButton> allSkills;

    private Dictionary<SkillDefinition, int> skillLevels = new();

    void Awake()
    {
        Instance = this;

        playerStats.damage = 0;
        playerStats.shotSpeed = 0;
        playerStats.speed = 0;
        playerStats.vertices = 200;

        foreach (var skillButton in allSkills)
        {
            skillLevels.Add(skillButton.skill, 0);
        }

        foreach (var skillButton in allSkills)
        {
            int levelOfReq = (skillButton.skill.requiredSkill == null) ? -1 : skillLevels[skillButton.skill.requiredSkill];
            skillButton.UpdateVisuals(skillLevels[skillButton.skill], levelOfReq);
        }
    }

    public void Buy(SkillDefinition skill)
    {
        int level = skillLevels[skill];
        int cost = skill.costs[level];

        playerStats.vertices -= cost;
        skillLevels[skill]++;

        ApplySkill(skill);

        foreach (var skillButton in allSkills)
        {
            int levelOfReq = (skillButton.skill.requiredSkill == null) ? -1 : skillLevels[skillButton.skill.requiredSkill];
            skillButton.UpdateVisuals(skillLevels[skillButton.skill], levelOfReq);
        }
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
