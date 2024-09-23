using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UIManager : MonoBehaviour
{
    [SerializeField] private HealthBarSlot m_healthBarSlot;
    [SerializeField] private LevelBarSlot m_levelBarSlot;
    [SerializeField] private AmmoBarSlot m_mmoBarSlot;
    [SerializeField] private UpgradeDisplaySlot m_upgradeDisplaySlot;

    [SerializeField] private Image m_endScreen;

    [Inject]
    private readonly SignalBus m_signalBus;

    private int m_killedEnemyCount;

    private void Awake()
    {
        m_signalBus.Subscribe<PlayertHealthSignal>(OnHealthChange);
        m_signalBus.Subscribe<KillEnemySignal>(OnKillEnemy);
        m_signalBus.Subscribe<UpgradeLevelDataSignal>(OnLevelChange);
        m_signalBus.Subscribe<EndGameSignal>(EndGame);
        m_signalBus.Subscribe<UpgradeAmmoSignal>(OnAmmoChange);
        m_signalBus.Subscribe<UpgradeStatsSignal>(OnUpgradeStats);

        m_killedEnemyCount = 0;
    }

    private void EndGame()
    {
        m_killedEnemyCount = 0; 
        m_levelBarSlot.BindKills(0);
        //m_endScreen.gameObject.SetActive(true);
    }

    private void OnUpgradeStats(UpgradeStatsSignal signal)
    {
        m_upgradeDisplaySlot.BindSlot(signal.UpdateIndex);
    }

    private void OnLevelChange(UpgradeLevelDataSignal upgradeLevelDataSignal)
    {
        m_levelBarSlot.BindExp(upgradeLevelDataSignal.Level,upgradeLevelDataSignal.Exp, upgradeLevelDataSignal.MinExp, upgradeLevelDataSignal.MaxExp);
    }

    private void OnHealthChange(PlayertHealthSignal playertHealthSignal)
    {
        m_healthBarSlot.Bind(playertHealthSignal.Health, playertHealthSignal.MaxHealth);
    }

    private void OnAmmoChange(UpgradeAmmoSignal upgradeAmmoSignal)
    {
        m_mmoBarSlot.Bind(upgradeAmmoSignal.AmmoAmount);
    }

    private void OnKillEnemy()
    {
        m_killedEnemyCount++;

        m_levelBarSlot.BindKills(m_killedEnemyCount);
    }

}
