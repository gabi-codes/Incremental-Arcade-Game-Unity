using UnityEngine;

[CreateAssetMenu(menuName = "Game/Skill")]
public class SkillDefinition : ScriptableObject
{
    public string id;

    [Header("Limits")]
    public int maxPoints;

    [Header("Costs")]
    public int[] costs;
    public CostType costType;

    [Header("Unlock requirements")]
    public SkillDefinition requiredSkill;
    public int requiredPoints;

    [Header("Effect")]
    public SkillType skillType;
    public int valuePerPoint;
}

public enum SkillType
{
    Damage,
    Speed,
    ShotSpeed
}

public enum CostType
{
    Vertices,
    Edges,
    Polygons
}