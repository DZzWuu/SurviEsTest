using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HealthBarSlot : MonoBehaviour
{
    [SerializeField] private Slider m_healthBar;
    
    public void Bind(float value , float maxHealth)
    {
        m_healthBar.maxValue = maxHealth;

        if(maxHealth == value)
        {
            m_healthBar.value = maxHealth;
        }

        m_healthBar.value = value;

        //var oldHealth = m_healthBar.value;

        //var newHealth = value;


        //if (value > oldHealth)
        //{
        //    DOVirtual.Float(oldHealth, newHealth, 0.25f, OnValueUpdate).OnComplete(() => m_healthBar.value = value);
        //}
    }

    private void OnValueUpdate(float value)
    {
        m_healthBar.value = value;
    }

}
