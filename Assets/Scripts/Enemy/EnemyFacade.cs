using System.Collections;
using System.Collections.Generic;
using System;
using Zenject;
using UnityEngine;


namespace Scripts.Enemy
{
    public class EnemyFacade : MonoBehaviour, IKill ,IPoolable<float, float, float, IMemoryPool>, IDisposable
    {
        [SerializeField] private EnemyConfig[] m_enemyConfig;
        [SerializeField] private Animator m_animator;

        [SerializeField] private EnemyStateHandler m_stateHandler;

        //[Inject]
        private Health m_health;
        private SignalBus m_signalBus;

        private IMemoryPool m_pool;


        //EnemyView _view;

        [Inject]
        public void Construct(SignalBus signalBus)
        {
            m_signalBus = signalBus;
            //m_stateHandler = stateHandler;
            //_view = view;
        }

        //[Inject]
        //public void Construct(EnemyView view)
        //{
        //    //m_signalBus = bus;
        //    _view = view;
        //}

        public void Dispose()
        {
            m_pool.Despawn(this);
        }

        public void OnDespawned()
        {
            m_pool = null;
        }

        public void OnSpawned(float speed, float attackDamage, float attackSpeed, IMemoryPool pool)
        {
            m_pool = pool;

            int random = UnityEngine.Random.Range(0, m_enemyConfig.Length);


            m_animator.runtimeAnimatorController = m_enemyConfig[random].RuntimeAnimatorController;
            //m_health.SetHealth(m_enemyConfig[random].MaxHealth);
            //m_stateHandler.SetEnemySpeed(m_enemyConfig[random].MovementSpeed);
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
