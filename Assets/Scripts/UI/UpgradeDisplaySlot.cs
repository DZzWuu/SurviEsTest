using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UpgradeDisplaySlot : MonoBehaviour
{
    [SerializeField] private Image m_upgradeIconDisplay;
    [SerializeField] private Text m_upgradeText;
    [SerializeField] private Transform m_upgradeObject;

    [Space(10f)]
    [SerializeField] private Sprite[] m_spirtes;
    [SerializeField] private string[] m_text;

    private Tween m_aniationTween;

    private void Awake()
    {
       m_upgradeObject.transform.DOScale(Vector3.zero, 0);
    }

    public void BindSlot(int index)
    {
        m_upgradeIconDisplay.sprite = m_spirtes[index];
        m_upgradeText.text = m_text[index];

        m_upgradeObject.DOScale(Vector3.one, .25f).SetEase(Ease.InOutBack).OnComplete(QuitAniamtion);
    }

    private void QuitAniamtion()
    {
        m_upgradeObject.DOScale(Vector3.zero, .25f).SetDelay(1f).SetEase(Ease.InOutBack);
    }
}
