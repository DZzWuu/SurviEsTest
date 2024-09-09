using Scripts.Enemy;
using System;
using Random = UnityEngine.Random;
using UnityEngine;
using Zenject;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawner Settings"), Space(10f)]
    [SerializeField] private float m_spawnInterval = 2f;
    [SerializeField] private float m_spawnDistance = 5f; 

    public Camera mainCamera;
    private float m_timer;

    [SerializeField] private float m_decreaseInterval = 10f;
    private float m_decreaseTimer;

    [Inject]
    private readonly EnemyFacade.Factory m_enemyFactory;

    [Inject]
    private readonly SpawnerSettings m_settings;

    private void Start()
    {
        m_spawnInterval = m_settings.SpawnInterval;
        m_spawnDistance = m_settings.SpawnDistance;

        m_timer = m_spawnInterval;
        m_decreaseTimer = m_settings.SpawnRateTimer;
    }

    private void Update()
    {
        m_decreaseTimer -= Time.deltaTime;
        m_timer -= Time.deltaTime;

        if (m_timer <= 0f)
        {
            m_timer = m_spawnInterval;
            SpawnEnemy();
        }

        if (m_decreaseTimer <= 0f)
        {
            m_decreaseTimer = m_decreaseInterval;
            DecreaseSpawnInterval();
        }
    }

    private void DecreaseSpawnInterval()
    {
        m_spawnInterval *= m_settings.SpawnPersantageMulti;
        m_spawnInterval = Mathf.Max(m_spawnInterval, 0.1f);
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
                spawnPosition = new Vector3(cameraPosition.x - cameraWidth / 2 - m_spawnDistance, Random.Range(cameraPosition.y - cameraHeight / 2, cameraPosition.y + cameraHeight / 2), 0);
                break;
            case 1: 
                spawnPosition = new Vector3(cameraPosition.x + cameraWidth / 2 + m_spawnDistance, Random.Range(cameraPosition.y - cameraHeight / 2, cameraPosition.y + cameraHeight / 2), 0);
                break;
            case 2: 
                spawnPosition = new Vector3(Random.Range(cameraPosition.x - cameraWidth / 2, cameraPosition.x + cameraWidth / 2), cameraPosition.y - cameraHeight / 2 - m_spawnDistance, 0);
                break;
            case 3: 
                spawnPosition = new Vector3(Random.Range(cameraPosition.x - cameraWidth / 2, cameraPosition.x + cameraWidth / 2), cameraPosition.y + cameraHeight / 2 + m_spawnDistance, 0);
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
