using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoBarSlot : MonoBehaviour
{
    [SerializeField] private Text m_ammoCount;

    public void Bind(int ammoAmount)
    {
        m_ammoCount.text = ammoAmount.ToString();
    }
}
