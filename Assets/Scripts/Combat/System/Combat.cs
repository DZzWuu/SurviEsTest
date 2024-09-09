using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Cysharp.Threading.Tasks;
using System;
using Zenject;
using System.Threading;

public class Combat : MonoBehaviour, IPickableAmmo
{
    [Header("Combat Settings"), Space(10f)]
    [SerializeField] private float m_detectionDistance = 5; 
    [SerializeField] private float m_detectIntervalTime = 1;
    [SerializeField] private float m_bulletSpeed = 5;

    [SerializeField] private Collider2D m_currentTargetCollider;

    [SerializeField] private BulletType m_bulletType;
    [SerializeField] private int m_enemyCount;
    [SerializeField] private LayerMask m_layerMask;

    [SerializeField] private int m_bulletAmount = 20;

    private Collider2D[] m_detectedCollider;

    [Inject]
    private readonly Bullet.Factory m_bulletFactory;

    [Inject]
    private readonly SignalBus m_signalBus;

    [Inject]
    private readonly Settings m_settings;

    public float IntervalTime => m_detectIntervalTime;


    private CancellationTokenSource m_cancellationTokenSource;

    private void Awake()
    {
        m_bulletAmount = m_settings.AmmoCount;
        m_detectedCollider = new Collider2D[1];
        m_cancellationTokenSource = new CancellationTokenSource();

        m_signalBus.Fire(new UpgradeAmmoSignal(m_bulletAmount));
        SetIntervalTime(m_settings.ShootRate);
    }

    private void OnDisable()
    {
        m_cancellationTokenSource?.Cancel();
        m_cancellationTokenSource?.Dispose();
    }

    private void Start()
    {
        AutoDetect(m_cancellationTokenSource.Token).Forget();
    }

    public void SetIntervalTime(float intervalTime)
    {
        m_detectIntervalTime = intervalTime;
    }

    private async UniTaskVoid AutoDetect(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(m_detectIntervalTime));
            //Debug.Log("Detect colsest enemy...");
            DetectEnemies();
        }
    }

    public void AddAmmo(int amount)
    {
        m_bulletAmount += amount;
        m_signalBus.Fire(new UpgradeAmmoSignal(m_bulletAmount));
    }

    private void DetectEnemies()
    {
        if (m_bulletAmount <= 0)
            return;

        m_enemyCount = Physics2D.OverlapCapsuleNonAlloc(transform.position, new Vector2(m_settings.ShootRange, m_settings.ShootRange), CapsuleDirection2D.Vertical, 0, m_detectedCollider, m_layerMask);

        if (m_enemyCount > 0)
        {
            m_currentTargetCollider = GetClosestTarget(m_detectedCollider);
            Shot(m_currentTargetCollider.transform);
        }
    }

    private void Shot(Transform target)
    {
        var bullet = m_bulletFactory.Create(m_bulletSpeed, target, m_bulletType);
        bullet.transform.position = transform.position;

        m_bulletAmount--;
        m_signalBus.Fire(new UpgradeAmmoSignal(m_bulletAmount));

    }

    private Collider2D GetClosestTarget(Collider2D[] colliders)
    {
        var closestColliders = colliders.OrderBy((collider) => Vector2.Distance(collider.gameObject.transform.position, this.transform.position)).ToArray();
        return closestColliders[0];
    }

    [Serializable]
    public class Settings
    {
        public float ShootRate;
        public float ShootRange;
        public int AmmoCount;
    }
}
