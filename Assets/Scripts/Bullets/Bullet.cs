using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Enemy;
using Zenject;

public enum BulletType
{
    Enemy,
    Player
}

public class Bullet : MonoBehaviour, IPoolable<float,Transform ,BulletType, IMemoryPool>
{
    private float m_bulletSpeed;
    private BulletType m_bulletType;
    public Transform _bulletHead;
    public Transform m_bulletTarget;
    [SerializeField] private float m_bulletLifeTime;
    [SerializeField] private int m_bulletDamage = 10;

    private IMemoryPool m_pool;

    private bool m_isStarted= false;
    private bool m_isDespawned = false;

    private void OnDisable()
    {
        m_isDespawned = false;
    }

    public void OnDespawned()
    {
        m_isStarted = false;
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            //Debug.Log("Bullet hit!");
            var damageable = collision.GetComponent<IDamageable>();
            damageable.Damage(m_bulletDamage);
            if (!m_isDespawned) 
            {
                m_isDespawned = true;
                m_pool.Despawn(this);
            }
        }
    }

    private void Update()
    {

        if (m_isStarted == false)
            return;

        transform.position = Vector2.MoveTowards(transform.position, m_bulletTarget.position, Time.deltaTime * m_bulletSpeed);
    }

    public void OnSpawned(float bulletSpeed, Transform bulletTarget, BulletType type, IMemoryPool pool)
    {
        m_isDespawned = false;
        m_bulletSpeed = bulletSpeed;
        m_bulletType = type;

        m_bulletTarget = bulletTarget;

        m_pool = pool;
        m_isStarted = true;
    }

    public class Factory : PlaceholderFactory<float, Transform, BulletType, Bullet>
    {
    }
}
