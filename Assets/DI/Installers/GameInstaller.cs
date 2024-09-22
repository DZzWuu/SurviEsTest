using UnityEngine;
using Zenject;
using Scripts.Enemy;
using System;

public class GameInstaller : MonoInstaller
{

    [Inject]
    private Settings _settings = null;

    public override void InstallBindings()
    {
        //InstallGameManager();
        InstallEnemyFactory();
        InstallBulletFactory();
        InstallExperienceGemsFactory();
        InstallLootFactory();
        InstallSignals();
    }

    private void InstallGameManager()
    {
        //Container.BindInterfacesTo<GameManager>().AsSingle();
    }

    private void InstallEnemyFactory()
    {
        Container.BindFactory<float, float, float, EnemyFacade, EnemyFacade.Factory>()
            .FromPoolableMemoryPool<float, float, float, EnemyFacade, EnemyFacadePool>(poolBinder => poolBinder
            .WithInitialSize(10)
            .FromSubContainerResolve()
            .ByNewPrefabInstaller<EnemyInstaller>(_settings.EnemyPrefab)
            .UnderTransformGroup("Enemies"));
    }

    private void InstallBulletFactory()
    {
        Container.BindFactory<float, Transform, BulletType, Bullet, Bullet.Factory>()
            .FromPoolableMemoryPool<float, Transform,BulletType, Bullet,BulletPool>(poolBinder => poolBinder
            .WithInitialSize(10)
            .FromComponentInNewPrefab(_settings.BulletPrefab)
            .UnderTransformGroup("Bullets"));
    }

    private void InstallExperienceGemsFactory()
    {
        Container.BindFactory<ExperienceGem, ExperienceGem.Factory>()
            .FromPoolableMemoryPool<ExperienceGem, ExperienceGemPool>(poolBinder => poolBinder
            .WithInitialSize(50)
            .FromComponentInNewPrefab(_settings.ExperienceGemPrefab)
            .UnderTransformGroup("ExperienceGems"));
    }

    private void InstallLootFactory()
    {
        Container.BindFactory<AmmoLoot, AmmoLoot.Factory>()
            .FromPoolableMemoryPool<AmmoLoot, AmmoLootPool>(poolBinder => poolBinder
            .WithInitialSize(5)
            .FromComponentInNewPrefab(_settings.AmmoBoxPrefab)
            .UnderTransformGroup("Loot"));

        Container.BindFactory<HealthLoot, HealthLoot.Factory>()
            .FromPoolableMemoryPool<HealthLoot, HealthLootPool>(poolBinder => poolBinder
            .WithInitialSize(5)
            .FromComponentInNewPrefab(_settings.HealthPotionPrefab)
            .UnderTransformGroup("Loot"));
    }

    private void InstallSignals()
    {
        GameSignalsInstaller.Install(Container);

        //SignalBusInstaller.Install(Container);

        //Container.DeclareSignal<EndGameSignal>();
        //Container.DeclareSignal<KillEnemySignal>();
        //Container.DeclareSignal<UpgradeAmmoSignal>();
        //Container.DeclareSignal<PlayertHealthSignal>();
        //Container.DeclareSignal<ExperienceSignal>();
        //Container.DeclareSignal<UpgradeLevelDataSignal>();
        //Container.DeclareSignal<UpgradeStatsSignal>();
    }

    [Serializable]
    public class Settings
    {
        public GameObject EnemyPrefab;
        public GameObject BulletPrefab;
        public GameObject ExperienceGemPrefab;
        public GameObject AmmoBoxPrefab;
        public GameObject HealthPotionPrefab;
    }

    class EnemyFacadePool : MonoPoolableMemoryPool<float, float, float, IMemoryPool, EnemyFacade>
    {
    }

    class OrangeEnemyFacadePool : MonoPoolableMemoryPool<float, float, float, IMemoryPool, EnemyFacade>
    {
    }

    class BulletPool : MonoPoolableMemoryPool<float, Transform,BulletType, IMemoryPool, Bullet>
    {
    }

    class ExperienceGemPool : MonoPoolableMemoryPool<IMemoryPool, ExperienceGem>
    {
    }

    class HealthLootPool : MonoPoolableMemoryPool<IMemoryPool, HealthLoot>
    {
    }

    class AmmoLootPool : MonoPoolableMemoryPool<IMemoryPool, AmmoLoot>
    {
    }
}