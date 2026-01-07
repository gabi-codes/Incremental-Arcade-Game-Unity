using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("references")]
    [SerializeField] private Transform playerSprite;
    [SerializeField] private PlayerStats playerStats;

    [Header("orbit")]
    [SerializeField] private float radius = 1.0f;
    [SerializeField] private float rotationSpeed = 180f;
    [SerializeField] private float aimFollowSpeed = 720f;
    [SerializeField] private float maxAimOffset = 90f;

    private float currentAngle;
    private float currentAimOffset;

    void Start()
    {
        Vector2 dir = playerSprite.localPosition.normalized;
        currentAngle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
    }

    
    void Update()
    {
        float targetAngle = GetMouseAngle();
        currentAngle = Mathf.MoveTowardsAngle(currentAngle, targetAngle, CalculateRotationSpeed() * Time.deltaTime);
        
        float angleDelta = Mathf.DeltaAngle(currentAngle, targetAngle);
        float targetAimOffset = Mathf.Clamp(angleDelta, -maxAimOffset, maxAimOffset);

        currentAimOffset = Mathf.MoveTowards(currentAimOffset, targetAimOffset, aimFollowSpeed * Time.deltaTime);

        UpdateSpriteTransform();
    }

    float GetMouseAngle()
    {
        Vector3 mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = -Camera.main.transform.position.z;

        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);

        Vector2 direction = (Vector2)(mouseWorldPos - transform.position);

        return Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    }

    void UpdateSpriteTransform()
    {
        float rad = currentAngle * Mathf.Deg2Rad;
        Vector2 localPos = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)) * radius;

        playerSprite.localPosition = localPos;

        playerSprite.localRotation = Quaternion.Euler(0f, 0f, currentAngle + currentAimOffset - 90f);
    }

    float CalculateRotationSpeed()
    {
        return rotationSpeed + playerStats.speed * 180;
    }
}
