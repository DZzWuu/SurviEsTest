using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Cysharp.Threading.Tasks;

public class GameManager : MonoBehaviour
{
    [Inject]
    private readonly SignalBus m_signalBus;

    [Inject]
    private readonly Settings m_settings;

    private LevelConfig m_levelConfig;
    [SerializeField] private AssetReference m_gameplayScene;

    private LevelConfig.LevelData m_currentLevel;

    private int m_currentExp;

    private void Awake()
    {
        m_signalBus.Subscribe<ExperienceSignal>(OnExpChange);
        m_signalBus.Subscribe<EndGameSignal>(EndGame);
    }

    private void Start()
    {
        m_currentLevel = m_settings.LevelConfig.GetLevelData(0);
    }

    public void StartGame()
    {

    }

    public void EndGame()
    {
        ResetGameplay().Forget();
    }

    private async UniTask ResetGameplay()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(3f));
        await LoadGameplayScene();
    }

    private async UniTask LoadGameplayScene()
    {
        try
        {
            AsyncOperationHandle handle = m_gameplayScene.LoadSceneAsync(UnityEngine.SceneManagement.LoadSceneMode.Single, true);
            await handle.ToUniTask();
        }
        catch (Exception ex)
        {
            Debug.LogError($"Unable to load gameplay scene: {ex.Message}");
        }
    }

    private void OnExpChange(ExperienceSignal signal)
    {
        m_currentExp += signal.Exp;
        //Debug.Log("Add EXP" + signal.Exp);
        CheckLevelExp();
    }

    private void CheckLevelExp()
    {
        int minExp = 0;

        if(m_currentExp > m_currentLevel.Exp)
        {
            int level = m_currentLevel.Level;
            level++;

            minExp = m_currentLevel.Exp;

            m_currentLevel = m_settings.LevelConfig.GetLevelData(level);

        }

        m_signalBus.Fire(new UpgradeLevelDataSignal() { Level = m_currentLevel.Level, Exp = m_currentExp, MinExp = minExp, MaxExp = m_currentLevel.Exp});
    }

    [Serializable]
    public class Settings
    {
        public LevelConfig LevelConfig;
    }
}
