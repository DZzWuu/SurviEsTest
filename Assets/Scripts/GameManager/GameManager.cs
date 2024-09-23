using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Cysharp.Threading.Tasks;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

/// <summary>
/// Important!!! WIP ScoreManager etc. soon to avoid SRP, GameManger only manage scene loading
///  </summary>

public class GameManager : MonoBehaviour , IGameManager
{
    public enum GameState
    {
        Intro,
        Gameplay,
    }

    [Inject]
    private readonly SignalBus m_signalBus;

    [Inject]
    private readonly Settings m_settings;

    private LevelConfig m_levelConfig;
    [SerializeField] private AssetReference m_gameplayScene;

    private GameState m_state;

    private LevelConfig.LevelData m_currentLevel;
    private int m_currentExp;

    private SceneInstance m_previousScene;
    private bool m_clearProviousScene;

    private void Awake()
    {
        //m_signalBus.Subscribe<ExperienceSignal>(OnExpChange);
        m_signalBus.Subscribe<EndGameSignal>(EndGame);
    }

    private void Start()
    {
        //m_currentLevel = m_settings.LevelConfig.GetLevelData(0);
        UpdateGameState(GameState.Intro);
    }

    private void EndGame()
    {
        UpdateGameState(GameState.Gameplay);
    }

    public void UpdateGameState(GameState state)
    {
        m_state = state;

        switch (state)
        {
            case GameState.Intro:
                LoadScene(); //refactor
                break;

            case GameState.Gameplay:
                LoadScene();
                break;
        }
    }

    private void LoadScene()
    {
        if (m_clearProviousScene)
        {
            Addressables.UnloadSceneAsync(m_previousScene).Completed += (asyncHandle) =>
            {
                m_clearProviousScene = false;
                m_previousScene = new SceneInstance();
            };

        }

        Addressables.LoadSceneAsync(m_gameplayScene, LoadSceneMode.Single).Completed += (asyncHandle) =>
        {
            m_clearProviousScene = true;
            m_previousScene = asyncHandle.Result;
        };
    }

    //private void OnExpChange(ExperienceSignal signal)
    //{
    //    m_currentExp += signal.Exp;
    //    //Debug.Log("Add EXP" + signal.Exp);
    //    CheckLevelExp();
    //}

    //private void CheckLevelExp()
    //{
    //    int minExp = 0;

    //    if(m_currentExp > m_currentLevel.Exp)
    //    {
    //        int level = m_currentLevel.Level;
    //        level++;

    //        minExp = m_currentLevel.Exp;

    //        m_currentLevel = m_settings.LevelConfig.GetLevelData(level);

    //    }

    //    m_signalBus.Fire(new UpgradeLevelDataSignal() { Level = m_currentLevel.Level, Exp = m_currentExp, MinExp = minExp, MaxExp = m_currentLevel.Exp});
    //}

    [Serializable]
    public class Settings
    {
        public LevelConfig LevelConfig;
    }
}
