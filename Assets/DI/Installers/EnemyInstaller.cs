using UnityEngine;
using Zenject;
using Scripts.Enemy;

public class EnemyInstaller : Installer<EnemyInstaller>
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<EnemyStateHandler>().AsSingle();

        Container.Bind<AttackState>().AsSingle();
    }
}