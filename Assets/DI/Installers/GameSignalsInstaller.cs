using UnityEngine;
using Zenject;
public class GameSignalsInstaller : Installer<GameSignalsInstaller>
{
    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);

        Container.DeclareSignal<EndGameSignal>();
        Container.DeclareSignal<KillEnemySignal>();
        Container.DeclareSignal<UpgradeAmmoSignal>();
        Container.DeclareSignal<PlayertHealthSignal>();
        Container.DeclareSignal<ExperienceSignal>();
        Container.DeclareSignal<UpgradeLevelDataSignal>();
        Container.DeclareSignal<UpgradeStatsSignal>();
    }
}
