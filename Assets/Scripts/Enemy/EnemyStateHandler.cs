using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Enemy;
using Zenject;

namespace Scripts.Enemy
{
    public interface IEnemyState
    {
        void EnterState();
        void Tick();
        void FixedUpdate();
        void ExitState();
    }

    public enum EnemyStates
    {
        Attack,
        None
    }

    public class EnemyStateHandler : ITickable, IInitializable
    {
        private float m_enemySpeed = 4;
        private IEnemyState m_currentStateHandler;
        private EnemyStates m_currentState = EnemyStates.None;
        public float EnemySpeed => m_enemySpeed;

        private List<IEnemyState> m_allStates = new List<IEnemyState>();

        private EnemyFacade m_enemyFacade;

        public enum EnemyStateType
        {
            Attack,
        }

        [Inject]
        private void Construct(EnemyFacade facade, AttackState attackState)
        {
            m_enemyFacade = facade;

            m_allStates = new List<IEnemyState>
            {
                attackState
            };
        }

        public void Init(EnemyStates state)
        {
            if (m_currentState == state)
                return;

            m_currentState = state;

            m_currentStateHandler = m_allStates[(int)state];
            m_currentStateHandler?.EnterState();
        }

        public void ToggleState()
        {
            m_currentStateHandler?.ExitState();
        }

        public void SetEnemySpeed(float speed)
        {
            m_enemySpeed = speed;
        }

        public void Tick()
        {
            m_currentStateHandler.Tick();
        }

        public void Initialize()
        {
            Init(EnemyStates.Attack);
        }
    }

    public class AttackState : IEnemyState
    {
        [Inject]
        private EnemyView m_enemyView;

        public void EnterState()
        {
        }

        public void ExitState()
        {
        }

        public void FixedUpdate()
        {
        }

        public void Tick()
        {
            m_enemyView.transform.position = Vector2.MoveTowards(m_enemyView.transform.position, PlayerMovement.PlayerPostion, Time.deltaTime * 2);//m_stateHandler.EnemySpeed);
        }
    }
}
