using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Camera m_mainCamera;
    public Vector3 m_cameraOffset;
    public Transform m_playerPostion;
    public AnimationClip m_animCurve;

    private Vector3 m_targetPostion;

    private void Awake()
    {
        m_mainCamera = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        m_mainCamera.transform.position = m_playerPostion.position + m_cameraOffset;
    }
}
