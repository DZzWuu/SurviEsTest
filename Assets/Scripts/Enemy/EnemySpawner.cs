using Scripts.Enemy;
using System;
using Random = UnityEngine.Random;
using UnityEngine;
using Zenject;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawner Settings"), Space(10f)]
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private float spawnDistance = 5f; 

    public Camera mainCamera;
    private float timer;

    [SerializeField] private float decreaseInterval = 10f;
    private float spawnTimer;
    private float decreaseTimer;

    [Inject]
    private readonly EnemyFacade.Factory m_enemyFactory;

    [Inject]
    private readonly SpawnerSettings m_settings;

    private void Start()
    {
        spawnInterval = m_settings.SpawnInterval;
        spawnDistance = m_settings.SpawnDistance;

        timer = spawnInterval;
        decreaseTimer = m_settings.SpawnRateTimer;
    }

    private void Update()
    {
        decreaseTimer -= Time.deltaTime;
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            timer = spawnInterval;
            SpawnEnemy();
        }

        if (decreaseTimer <= 0f)
        {
            decreaseTimer = decreaseInterval;
            DecreaseSpawnInterval();
        }
    }

    private void DecreaseSpawnInterval()
    {
        spawnInterval *= m_settings.SpawnPersantageMulti;
        spawnInterval = Mathf.Max(spawnInterval, 0.1f);
    }

    private void SpawnEnemy()
    {
        Vector3 cameraPosition = mainCamera.transform.position;
        float cameraHeight = 2f * mainCamera.orthographicSize;
        float cameraWidth = cameraHeight * mainCamera.aspect;

        int side = Random.Range(0, 4);
        Vector3 spawnPosition = Vector3.zero;

        switch (side)
        {
            case 0: 
                spawnPosition = new Vector3(cameraPosition.x - cameraWidth / 2 - spawnDistance, Random.Range(cameraPosition.y - cameraHeight / 2, cameraPosition.y + cameraHeight / 2), 0);
                break;
            case 1: 
                spawnPosition = new Vector3(cameraPosition.x + cameraWidth / 2 + spawnDistance, Random.Range(cameraPosition.y - cameraHeight / 2, cameraPosition.y + cameraHeight / 2), 0);
                break;
            case 2: 
                spawnPosition = new Vector3(Random.Range(cameraPosition.x - cameraWidth / 2, cameraPosition.x + cameraWidth / 2), cameraPosition.y - cameraHeight / 2 - spawnDistance, 0);
                break;
            case 3: 
                spawnPosition = new Vector3(Random.Range(cameraPosition.x - cameraWidth / 2, cameraPosition.x + cameraWidth / 2), cameraPosition.y + cameraHeight / 2 + spawnDistance, 0);
                break;
        }

        var enemy = m_enemyFactory.Create(3,3,3);
        enemy.transform.position = spawnPosition;
    }

    [Serializable]
    public class SpawnerSettings
    {
        public float SpawnInterval;
        public float SpawnDistance;
        public float SpawnRateTimer;
        public float SpawnPersantageMulti;
    }
}
