using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyMovement : MonoBehaviour
{
    public bool isActive = true;

    public abstract void Init(int pathVariant);

}
