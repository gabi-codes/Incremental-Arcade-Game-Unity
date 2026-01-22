using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCircle : EnemyMovement
{
    [SerializeField] private EnemyCore core;

    private int pathVariant;

    private Vector2 startPos;
    private Vector2 center;

    private float radius;
    private float angle;
    private float dir;

    private float angularSpeed = 20f;

    public override void Init(int pathVar)
    {

        pathVariant = pathVar;
        BuildPath();
        transform.position = startPos;

        Vector2 offset = startPos - center;
        radius = offset.magnitude;
        angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;

    }

    void BuildPath()
    {
        switch (pathVariant)
        {
            case 0:
                startPos = new Vector2(-3.5f, 6f);
                center = new Vector2(-3.5f, 1.75f);
                dir = 1;
                break;

            case 1:
                startPos = new Vector2(3.5f, -6f);
                center = new Vector2(3.5f, -1.75f);
                dir = 1;
                break;

            case 2:
                startPos = new Vector2(-3.5f, -6f);
                center = new Vector2(-3.5f, -1.75f);
                dir = -1;
                break;

            case 3:
                startPos = new Vector2(3.5f, 6f);
                center = new Vector2(3.5f, 1.75f);
                dir = -1;
                break;

        }
    }

    void Update()
    {
        if (isActive)
            MoveAlongPath();
    }

    void MoveAlongPath()
    {
        angularSpeed = 20;
        angle += angularSpeed * Time.deltaTime * dir;

        Vector2 offset = new Vector2(
            Mathf.Cos(angle * Mathf.Deg2Rad),
            Mathf.Sin(angle * Mathf.Deg2Rad)
        ) * radius;

        transform.position = center + offset;
    }
}
