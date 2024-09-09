using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class AmmoLoot : MonoBehaviour, IPoolable<IMemoryPool>
{
    [SerializeField] private int m_addAmmo = 5;
    [SerializeField] private float m_attractDistance = 8;

    [Inject]
    private IMemoryPool m_pool;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<IPickableAmmo>().AddAmmo(m_addAmmo);
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

    public class Factory : PlaceholderFactory<AmmoLoot>
    {
    }
}
