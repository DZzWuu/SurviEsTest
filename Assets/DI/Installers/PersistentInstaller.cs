using UnityEngine;
using Zenject;

public class PersistentInstaller : MonoInstaller
{
    [Header("GameManager"), Space(10f)]
    [SerializeField] private GameObject m_gameManager;

    public override void InstallBindings()
    {
        Container.Bind<GameManager>().FromComponentInNewPrefab(m_gameManager).AsSingle();
    }
}