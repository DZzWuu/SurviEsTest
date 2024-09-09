using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class HealthLoot : MonoBehaviour , IPoolable<IMemoryPool>
{
    [Inject]
    private IMemoryPool m_pool;

    [SerializeField] private int m_healthAmount = 10;
    [SerializeField] private float m_attractDistance = 8;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<IHeal>().Heal(m_healthAmount);
            m_pool.Despawn(this);
        }
    }

    private void Update()
    {
        if (Vector3.Distance(this.transform.position, PlayerMovement.PlayerPostion) > m_attractDistance)
            return;

        transform.position = Vector2.MoveTowards(transform.position, PlayerMovement.PlayerPostion, Time.deltaTime * 10);
    }

    public void OnDespawned()
    {
        m_pool = null;
    }

    public void OnSpawned(IMemoryPool pool)
    {
        m_pool = pool;
    }

    public class Factory : PlaceholderFactory<HealthLoot>
    {
    }
}
