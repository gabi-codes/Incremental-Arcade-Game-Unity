using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRoot : MonoBehaviour
{
    [SerializeField] private EnemyMovement movement;
    [SerializeField] private EnemyCore core;

    public void Init(int pathVariant, int tier)
    {
        movement.Init(pathVariant);
        core.Init(tier);
        
    }

    public void Stop()
    {
        movement.isActive = false;
    }
}
