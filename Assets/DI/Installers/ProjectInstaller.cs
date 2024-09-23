using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    [Header("GameManager"), Space(10f)]
    [SerializeField] private GameObject m_gameManager;
    [SerializeField] private GameObject m_gameCanvas;

    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<GameManager>().FromComponentInNewPrefab(m_gameManager).AsSingle().NonLazy();
        Container.Bind<UIManager>().FromComponentInNewPrefab(m_gameCanvas).AsSingle().NonLazy();

        InstallSignals();
    }

    private void InstallSignals()
    {
        GameSignalsInstaller.Install(Container);
    }
}