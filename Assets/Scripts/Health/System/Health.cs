using Scripts.Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Zenject;

public class Health : MonoBehaviour, IDamageable, IHeal
{
    [Inject]
    private readonly SignalBus m_singalBus;

    [Inject]
    private readonly Settings m_settings;

    public enum HealthType
    {
        Enemy,
        Player
    }
    
    [SerializeField] private HealthType m_healthType;
    [SerializeField] private int m_maxHealth = 100;

    private int m_health;

    public int MaxHealth => m_maxHealth;

    private void Awake()
    {
        if (m_healthType == HealthType.Player)
        {
            m_maxHealth = m_settings.PlayerMaxHealth;
        }
    }

    private void Start()
    {
        ResetHealth();
    }

    private void OnDisable()
    {
        ResetHealth();
    }

    public void SetHealth(int amount)
    {
        m_maxHealth = amount;
        m_health = amount;

        m_singalBus.Fire(new PlayertHealthSignal() { Health = m_health, MaxHealth = m_maxHealth });
    }

    public void Damage(int amount)
    {
        if (m_health <= 0)
        {
            if (m_healthType == HealthType.Player)
            {
                m_singalBus.Fire(new PlayertHealthSignal() { Health = m_health , MaxHealth = m_maxHealth });
            }

            Kill();
            return;
        }

        m_health -= amount;

        if (m_healthType == HealthType.Player)
        {
            m_singalBus.Fire(new PlayertHealthSignal() { Health = m_health, MaxHealth = m_maxHealth });
        }
    }

    public void Heal(int amount)
    {
        if (m_health >= m_maxHealth)
            return;

        m_health += amount;

        if (m_healthType == HealthType.Player)
        {
            m_singalBus.Fire(new PlayertHealthSignal() { Health = m_health, MaxHealth = m_maxHealth });
        }
    }

    public void ResetHealth()
    {
        m_health = m_maxHealth;

        if (m_healthType == HealthType.Player)
        {
            m_health = m_settings.PlayerMaxHealth;
            m_singalBus.Fire(new PlayertHealthSignal() { Health = m_health, MaxHealth = m_maxHealth });
        }
    }

    public void Kill()
    {
        transform.GetComponent<IKill>().Kill();
    }

    [Serializable]
    public class Settings
    {
        public int PlayerMaxHealth;
    }
}
