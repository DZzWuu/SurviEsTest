using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animancer;
using System;
using Zenject;

public class PlayerMovement : MonoBehaviour
{
    private float m_playerSpeed;

    private string m_horizontalAxis = "Horizontal";
    private string m_verticalAxis = "Vertical";

    public AnimancerComponent m_animacer;
    private DirectionalAnimationSet m_currentAnimationSet;

    [SerializeField] private DirectionalAnimationSet _Idle;
    [SerializeField] private DirectionalAnimationSet _Walk;

    private float m_inputHorizontal;
    private float m_inputVertical;

    private bool m_mirrorAxis;

    private Rigidbody2D m_rigidbody;
    
    private Vector2 m_moveDirection;

    public static Vector3 PlayerPostion = Vector3.zero;

    [Inject]
    private readonly Settings m_settings;

    public float Speed => m_playerSpeed;

    public void SetMovementSpeed(float speed)
    {
        m_playerSpeed = speed;
    }

    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();

        m_currentAnimationSet = _Idle;

        SetMovementSpeed(m_settings.MovementSpeed);
    }

    private void Update()
    {
        PlayerPostion = this.transform.position;
        HanldePlayerInput();
        
    }

    private void FixedUpdate()
    {
        CalculatePlayerMovement();
    }

    private void LateUpdate()
    {
        PlayerAnimation();
    }

    private void CalculatePlayerMovement()
    {
        m_rigidbody.velocity = new Vector2(m_moveDirection.x * m_playerSpeed, m_moveDirection.y * m_playerSpeed);
        
    }

    private void PlayerAnimation()
    {
        if (m_moveDirection.magnitude > 0)
        {
            if (m_inputHorizontal > 0f)
            {
                m_mirrorAxis = true;
                m_animacer.Play(_Walk.GetClip(DirectionalAnimationSet.Direction.Right));
            }
            else
            {
                m_mirrorAxis = false;
                m_animacer.Play(_Walk.GetClip(DirectionalAnimationSet.Direction.Left));
            }

        }
        else
        {
            if (m_mirrorAxis == true)
            {
                m_animacer.Play(_Idle.GetClip(DirectionalAnimationSet.Direction.Right));
            }
            else
            {
                m_animacer.Play(_Idle.GetClip(DirectionalAnimationSet.Direction.Left));
            }
        }
    }

    private void HanldePlayerInput()
    {
        m_inputHorizontal = SimpleInput.GetAxis(m_horizontalAxis);
        m_inputVertical = SimpleInput.GetAxis(m_verticalAxis);

        m_moveDirection = new Vector2(m_inputHorizontal, m_inputVertical).normalized;
    }

    [Serializable]
    public class Settings
    {
        public float MovementSpeed;
    }
}
