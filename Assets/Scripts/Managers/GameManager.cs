using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerStats stats;

    [SerializeField] private GameObject darken;
    [SerializeField] private GameObject startButton;
    [SerializeField] private Planet planet;
    [SerializeField] private GameObject player;
    [SerializeField] private EnemySpawnManager spawnManager;

    void Start()
    {
        stats.damage = 0;
        stats.shotSpeed = 0;
        stats.speed = 0;
        stats.maxHp = 0;
        stats.vertices = 200;

        darken.SetActive(false);
        player.SetActive(false);

        SkillTreeManager.Instance.Activate();
    }

    public void StartSession()
    {
        planet.Restart();
        SkillTreeManager.Instance.Disactivate();
        spawnManager.StartSpawning();

        startButton.SetActive(false);
        player.SetActive(true);

    }

    public void EndSession()
    {
        darken.SetActive(true);
        spawnManager.StopSpawning();

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            EnemyRoot enemyRoot = enemy.GetComponent<EnemyRoot>();
            if (enemyRoot != null) { enemyRoot.Stop(); }
        }

        GameObject[] currencies = GameObject.FindGameObjectsWithTag("Currency");
        foreach (GameObject currency in currencies)
        {
            Vert vert = currency.GetComponent<Vert>();
            if (vert != null) { vert.isActive = false; }
        }

        player.SetActive(false);
    }

    public void EnableSkillTree()
    {
        darken.SetActive(false);
        startButton.SetActive(true);

        SkillTreeManager.Instance.Activate();

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }

        GameObject[] currencies = GameObject.FindGameObjectsWithTag("Currency");
        foreach (GameObject currency in currencies)
        {
            Destroy(currency);
        }
    }
}
