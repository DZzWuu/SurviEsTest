using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Camera m_mainCamera;
    [SerializeField] private Vector3 m_cameraOffset;
    [SerializeField] private Transform m_playerPostion;

    private void LateUpdate()
    {
        m_mainCamera.transform.position = m_playerPostion.position + m_cameraOffset;
    }
}
