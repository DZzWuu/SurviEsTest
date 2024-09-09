using Scripts.Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    [SerializeField] private int m_attackDamage = 10;
    [SerializeField] private float m_interval = .5f;

    private float m_timer;

    private void OnTriggerStay2D(Collider2D collision)
    {
        m_timer += Time.deltaTime;

        if (m_timer >= m_interval)
        {
            m_timer = 0;

            if (collision.CompareTag("Player"))
            {
                //Debug.Log("Damage");
                collision?.GetComponent<IDamageable>().Damage(m_attackDamage);
            }
        }
    }
}
