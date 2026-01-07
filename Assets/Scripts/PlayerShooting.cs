using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{

    [Header("Refs")]
    [SerializeField] private Transform playerSprite;
    [SerializeField] private BulletPlayer bulletPlayerPrefab;
    [SerializeField] private PlayerStats playerStats;

    [Header("Shooting")]
    [SerializeField] private float fireCooldown = 0.25f;
    [SerializeField] private float bulletSpawnOffset = 0.25f;

    private float lastTimeFired;

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            TryShoot();
        }
    }

    void TryShoot()
    {
        if (Time.time < lastTimeFired + fireCooldown) { return; }

        lastTimeFired = Time.time;
        Shoot();
    }

    void Shoot()
    {
        Vector2 shootDir = playerSprite.up; 

        Vector3 spawnPos = playerSprite.position + (Vector3)(shootDir * bulletSpawnOffset);
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, shootDir);

        BulletPlayer bullet = Instantiate(bulletPlayerPrefab, spawnPos, rotation);
        bullet.Init(shootDir);
    }
}
