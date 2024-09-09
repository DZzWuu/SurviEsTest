using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelBarSlot : MonoBehaviour
{
    [SerializeField] private Text m_killedEnemyCount;
    [SerializeField] private Text m_levelCount;
    [SerializeField] private Slider m_expBar;

    private void Awake()
    {
        m_expBar.value = 0;
    }

    public void BindExp(int amount, int exp, int minExp, int maxExp)
    {
        m_expBar.minValue = minExp;
        m_expBar.maxValue = maxExp;
        m_expBar.value = exp;

        m_levelCount.text = "Lv ." + amount.ToString();
    }

    public void BindKills(int amount)
    {
        m_killedEnemyCount.text = amount.ToString();
    }

}
