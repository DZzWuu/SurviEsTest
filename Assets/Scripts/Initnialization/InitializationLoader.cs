using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Cysharp.Threading.Tasks;
using System;

namespace Scripts.Initialization
{
    /// <summary>
    /// Class responsible for initializing the game and loading the gameplay scene.
    ///  </summary>
    public class InitializationLoader : MonoBehaviour
    {
        [Header("Load Settings"), Space(10f), Tooltip("AssetReference to gameplay scene")]
        [SerializeField] private AssetReference m_gameplayScene;


        
        private void Awake()
        {
            Application.targetFrameRate = 60;
            Addressables.InitializeAsync();
        }

        private async void Start()
        {
            //Debug.Log("Initialization...");

            await Init();
        }

        private async UniTask Init()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(1f));
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

    }
}


