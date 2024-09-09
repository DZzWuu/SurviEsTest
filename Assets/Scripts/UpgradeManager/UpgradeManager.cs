using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] private PlayerMovement m_playerMovement;
    [SerializeField] private Combat m_combat;
    [SerializeField] private Health m_health;

    private int m_currentLevel = 0;

    private bool m_isPoisonedBullet;

    [Inject]
    private readonly SignalBus m_signalBus;

    private void Awake()
    {
        m_isPoisonedBullet = false;
        m_signalBus.Subscribe<UpgradeLevelDataSignal>(OnLevelChange);
    }

    private void OnLevelChange(UpgradeLevelDataSignal signal)
    {
        if(m_currentLevel != signal.Level)
        {
            int randomNumber = Random.Range(0, 3);

            switch (randomNumber)
            {
                case 0:
                    UpgradeMovementSpeed();
                    break;
                case 1:
                    UpgradeComabat();
                    break;
                case 2:
                    UpgradeHealth();
                    break;
                case 3:
                    UpgradePoisonedBullet();
                    break;
            }
        }

        m_currentLevel = signal.Level;
    }

    private void UpgradeMovementSpeed()
    {
        float newSpeed = m_playerMovement.Speed + 0.5f;
        m_playerMovement.SetMovementSpeed(newSpeed);

        m_signalBus.Fire(new UpgradeStatsSignal() { UpdateIndex = 0 });
    }

    private void UpgradeComabat()
    {
        float newShootRate = m_combat.IntervalTime - 0.05f;
        m_combat.SetIntervalTime(newShootRate);

        m_signalBus.Fire(new UpgradeStatsSignal() { UpdateIndex = 1 });
    }

    private void UpgradeHealth()
    {
        int maxHealth = m_health.MaxHealth + 50;
        m_health.SetHealth(maxHealth);

        m_signalBus.Fire(new UpgradeStatsSignal() { UpdateIndex = 2 });
    }

    private void UpgradePoisonedBullet()
    {
        m_signalBus.Fire(new UpgradeStatsSignal() { UpdateIndex = 3 });
    }

}
