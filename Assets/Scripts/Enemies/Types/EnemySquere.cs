using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using static UnityEngine.GraphicsBuffer;

public class EnemySquereMovement : MonoBehaviour
{
    [SerializeField] private EnemyCore core;

    List<Vector2> path;
    int currentIndex = 0;

    private int pathVariant;

    public bool isActive = true;

    public void Init(int pathVar)
    {

        pathVariant = pathVar;
        BuildPath();
        transform.position = path[0];

    }

    void Update()
    {
        if (isActive) 
            MoveAlongPath();
    }

    void BuildPath()
    {

        switch (pathVariant)
        {
            case 0:
                path = new List<Vector2>
                {
                    new Vector2(-10f,  2f),
                    new Vector2(-6f,  2f),
                    new Vector2(-6f,  0f),
                    new Vector2(-4f,  0f),
                    new Vector2(-4f,  4f),
                    new Vector2(-2f,  4f),
                    new Vector2(0f,  4f),
                    new Vector2(0f,  0f),
                };
                break;
            
            case 1:
                path = new List<Vector2>
                {
                    new Vector2(-10f,  -2f),
                    new Vector2(-6f,  -2f),
                    new Vector2(-6f,  0f),
                    new Vector2(-4f,  0f),
                    new Vector2(-4f,  -4f),
                    new Vector2(-2f,  -4f),
                    new Vector2(0f,  -4f),
                    new Vector2(0f,  0f),
                };
                break;

            case 2:
                path = new List<Vector2>
                {
                    new Vector2(10f,  -2f),
                    new Vector2(6f,  -2f),
                    new Vector2(6f,  0f),
                    new Vector2(4f,  0f),
                    new Vector2(4f,  -4f),
                    new Vector2(2f,  -4f),
                    new Vector2(0f,  -4f),
                    new Vector2(0f,  0f),
                };
                break;

            case 3:
                path = new List<Vector2>
                {
                    new Vector2(10f,  2f),
                    new Vector2(6f,  2f),
                    new Vector2(6f,  0f),
                    new Vector2(4f,  0f),
                    new Vector2(4f,  4f),
                    new Vector2(2f,  4f),
                    new Vector2(0f,  4f),
                    new Vector2(0f,  0f),
                };
                break;
        }
    }

    void MoveAlongPath()
    {
        if (currentIndex >= path.Count)
            return;

        Vector2 target = path[currentIndex];

        transform.position = Vector2.MoveTowards(transform.position, target, core.speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, target) < 0.01f)
        {
            currentIndex++;
        }
    }

}
