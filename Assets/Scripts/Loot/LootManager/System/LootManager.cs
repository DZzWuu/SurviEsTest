using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LootManager : MonoBehaviour
{
    [Inject]
    private readonly AmmoLoot.Factory m_ammoLootFactory;

    [Inject]
    private readonly HealthLoot.Factory m_healthLootFactory;

    [Inject]
    private readonly ExperienceGem.Factory m_experienceGemFactory;

    [Inject]
    private readonly SignalBus m_signalBus;

    private void Awake()
    {
        m_signalBus.Subscribe<KillEnemySignal>(OnEnemyKill);
    }

    public void OnEnemyKill(KillEnemySignal signal)
    {
        var gem = m_experienceGemFactory.Create();
        gem.transform.position = signal.Postion;

        int random = Random.Range(0, 10);

        if(random >= 9)
        {
            var ammo = m_ammoLootFactory.Create();
            ammo.transform.position = signal.Postion;
            return;
        }
        else if(random >= 6)
        {
            

            var health = m_healthLootFactory.Create();
            health.transform.position = signal.Postion;
            return;
        }
    }

}
