using System.Collections;
using System.Collections.Generic;
using System;
using Zenject;
using UnityEngine;


namespace Scripts.Enemy
{
    public class EnemyFacade : MonoBehaviour, IDisposable, IKill ,IPoolable<float, float, float, IMemoryPool>
    {
        [SerializeField] private EnemyConfig[] m_enemyConfig;
        [SerializeField] private Animator m_animator;

        [SerializeField] private Health m_health;
        [SerializeField] private EnemyStateHandler m_stateHandler;

        [Inject]
        private readonly SignalBus m_signalBus;

        private IMemoryPool m_pool;

        public void Dispose()
        {
            //m_pool.Despawn(this);
        }

        public void OnDespawned()
        {
            //m_pool = null;
        }

        public void OnSpawned(float speed, float attackDamage, float attackSpeed, IMemoryPool pool)
        {
            m_pool = pool;

            int random = UnityEngine.Random.Range(0, m_enemyConfig.Length);
            

            m_animator.runtimeAnimatorController = m_enemyConfig[random].RuntimeAnimatorController;
            m_health.SetHealth(m_enemyConfig[random].MaxHealth);
            m_stateHandler.SetEnemySpeed(m_enemyConfig[random].MovementSpeed);
        }

        public void Kill()
        {
            m_signalBus.Fire(new KillEnemySignal() { Postion = transform.position });
            m_pool.Despawn(this);
        }

        public class Factory : PlaceholderFactory<float, float, float, EnemyFacade>
        {
        }
    }
}
