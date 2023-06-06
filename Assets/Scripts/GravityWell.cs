using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class GravityWell : MonoBehaviour
    {

        #region Properties and Components

        /// <summary>
        /// ���� ����������.
        /// </summary>
        [SerializeField] private float m_Force;

        /// <summary>
        /// ������ ����������.
        /// </summary>
        [SerializeField] private float m_Radius;

        #endregion


        #region Unity Events

        private void OnTriggerStay2D(Collider2D collision)
        {
            // ��������, ���� �� � ������� Rigidbody.
            if (collision.attachedRigidbody == null) return;

            // �������� ������ ����������� �� �������������� ������� �� ���� (�� 2 � 1).
            Vector2 direction = transform.position - collision.transform.position;

            // �������� ����� ������� = ���������� �� �������������� �������.
            float distance = direction.magnitude;

            // ���� ���������� �� ������� < ������� ����������.
            if (distance < m_Radius)
            {
                // ������� ������ ���� ���������� (��� ����� ������, ��� ������� ����������) = ��������������� ��������� * ���� ���������� * ( ������ ���������� / ����� ������� �� ������� ).
                Vector2 force = direction.normalized * m_Force * (m_Radius / distance);

                // ��������� ������ ���� �������������� �������.
                collision.attachedRigidbody.AddForce(force, ForceMode2D.Force);
            }
        }

#if UNITY_EDITOR
        /// <summary>
        /// �����, ������������� �������� ������ ���������� �� ������� ����������.
        /// </summary>
        private void OnValidate()
        {
            GetComponent<CircleCollider2D>().radius = m_Radius;
        }
#endif

#endregion

    }
}