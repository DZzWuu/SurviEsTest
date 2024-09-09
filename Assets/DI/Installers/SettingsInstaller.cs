using System;
using UnityEngine;
using Zenject;
using Scripts.Enemy;

[CreateAssetMenu(fileName = "SettingsInstaller", menuName = "Installers/SettingsInstaller")]
public class SettingsInstaller : ScriptableObjectInstaller<SettingsInstaller>
{
    [SerializeField] private PlayerSettings m_playerSettings;

    [Space(10f)]
    [SerializeField] private LevelSettings m_levelConfig;

    [Space(10f)]
    [SerializeField] private EnemySpawnerSettings m_enemySpawnerConfig;

    [Space(10f)]
    [SerializeField] private EnemyConfig[] m_enemyConfigs;

    [Space(10f)]
    [SerializeField] private GameInstaller.Settings m_gamePrefabs;

    public PlayerSettings Player => m_playerSettings;
 

    public override void InstallBindings()
    {
        Container.BindInstance(m_gamePrefabs).IfNotBound();
        Container.BindInstance(m_playerSettings.MovementSettings).IfNotBound();
        Container.BindInstance(m_playerSettings.HealthSettings).IfNotBound();
        Container.BindInstance(m_playerSettings.CombatSettings).IfNotBound();
        Container.BindInstance(m_enemySpawnerConfig.SpawnerSettings).IfNotBound();

        Container.BindInstance(m_levelConfig.LevelConfig).IfNotBound();
    }

    [Serializable]
    public class PlayerSettings
    {
        public PlayerMovement.Settings MovementSettings;
        public Health.Settings HealthSettings;
        public Combat.Settings CombatSettings;
    }

    [Serializable]
    public class EnemySpawnerSettings
    {
        public EnemySpawner.SpawnerSettings SpawnerSettings;
    }

    [Serializable]
    public class LevelSettings
    {
        public GameManager.Settings LevelConfig;
    }

}