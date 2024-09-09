using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;


[CreateAssetMenu(fileName = "EnemyConfig", menuName = "Data/EnemyConfig")]
public class EnemyConfig : ScriptableObject
{
    public enum EnemyType
    {
        GreenEnemy,
        OrangeEnemy,
        SkeletonEnemy,
        BigEnemy
    }

    [SerializeField] private EnemyType m_enemyType;

    [Space(10f)]
    [SerializeField] private int m_maxHealth;
    [SerializeField] private int m_movementSpeed;
    [SerializeField] private int m_attackDamage;
    [SerializeField] private float m_scale;

    [SerializeField] private Sprite m_sprite;
    [SerializeField] private RuntimeAnimatorController m_runtimeAnimatorController;

    public RuntimeAnimatorController RuntimeAnimatorController => m_runtimeAnimatorController;
    public EnemyType Type => m_enemyType;
    public int MaxHealth => m_maxHealth;
    public int MovementSpeed => m_movementSpeed;
    public int AttackDamage => m_attackDamage;
    public float Scale => m_scale;
    public Sprite Sprite => m_sprite;

}

