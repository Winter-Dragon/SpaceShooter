using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// ����� ���������� ������ �������� �� ��������.
    /// </summary>
    public class CameraController : MonoBehaviour
    {

        #region Properties and Components

        /// <summary>
        /// ������ �� ������.
        /// </summary>
        [SerializeField] private Camera m_Camera;

        /// <summary>
        /// ������ �� ���� ������.
        /// </summary>
        [SerializeField] private Transform m_Target;

        /// <summary>
        /// �������� �������� ������������ (�������� �� ������).
        /// </summary>
        [SerializeField] private float m_InterpolationLinear;

        /// <summary>
        /// ������� �������� ������������ (�������� ��������).
        /// </summary>
        [SerializeField] private float m_InterpolationAngular;

        /// <summary>
        /// ��������� �������� �� ��� Z.
        /// </summary>
        [SerializeField] private float m_CameraZOffset;

        /// <summary>
        /// ��������� �������� �� ����������� ��������.
        /// </summary>
        [SerializeField] private float m_ForvardOffset;

        #endregion


        #region Unity Events

        private void FixedUpdate()
        {
            // �������� �� ������ � ���� ������.
            if (m_Camera == null || m_Target == null) return;

            // 1. ���������� ������� ��������� ������.
            Vector2 cameraPosition = m_Camera.transform.position;
            // 2. ���������� ������� ��������� ������ + �������� � ������� �������.
            Vector2 targetPosition = m_Target.position + m_Target.transform.up * m_ForvardOffset;
            // 3. ������� ����������������� �������� ������ ��������� ������.
            Vector2 newCameraPosotion = Vector2.Lerp(cameraPosition, targetPosition, m_InterpolationLinear * Time.deltaTime);

            // 4. ����� ������ ����� �������
            m_Camera.transform.position = new Vector3(newCameraPosotion.x, newCameraPosotion.y, m_Camera.transform.position.z + m_CameraZOffset);

            // 5. ���� ������ ������ �����������, ����� �� �������� �� �����������
            if (m_InterpolationAngular > 0)
            {
                m_Camera.transform.rotation = Quaternion.Slerp(m_Camera.transform.rotation, m_Target.rotation, m_InterpolationAngular * Time.deltaTime);
            }
        }

        #endregion


        #region Public API

        /// <summary>
        /// �����, �������� ������ ����� ���� ��������.
        /// </summary>
        /// <param name="newTarget">����� Transform ���� ��������.</param>
        public void SetTarget(Transform newTarget)
        {
            // ���������������� ���� �������� ������.
            m_Target = newTarget;
        }

        #endregion

    }
}