using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Enemy;

namespace Scripts.Enemy
{
    public interface IEnemyState
    {
        void EnterState(EnemyStateHandler stateHandler);
        void Tick();
        void FixedUpdate();
        void ExitState();
    }

    public class EnemyStateHandler : MonoBehaviour
    {
        private float m_enemySpeed = 4;
        private IEnemyState m_currentState;

        public float EnemySpeed => m_enemySpeed;

        private List<IEnemyState> m_allStates = new List<IEnemyState>();

        public enum EnemyStateType
        {
            Attack,
        }

        private void OnEnable()
        {
            AttackState attack = new AttackState();
            m_allStates.Add(attack);
            Init(attack);
        }

        public void Init(IEnemyState state)
        {
            if (m_currentState == state)
                return;

            m_currentState = state;
            state?.EnterState(this);
        }

        public void ToggleState()
        {
            m_currentState?.ExitState();
        }

        private void Update()
        {
            m_currentState?.Tick();
        }

        public void SetEnemySpeed(float speed)
        {
            m_enemySpeed = speed;
        }
    }

    public class AttackState : IEnemyState
    {

        private EnemyStateHandler m_stateHandler;

        public void EnterState(EnemyStateHandler stateHandler)
        {
            m_stateHandler = stateHandler;
        }

        public void ExitState()
        {
            
        }

        public void FixedUpdate()
        {
            
        }

        public void Tick()
        {
            m_stateHandler.transform.position = Vector2.MoveTowards(m_stateHandler.transform.position, PlayerMovement.PlayerPostion, Time.deltaTime * m_stateHandler.EnemySpeed);
        }
    }
}
