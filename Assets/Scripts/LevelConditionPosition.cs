using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace SpaceShooter
{
    /// <summary>
    /// �������, ��� ������� ������ ������ ������� ����������� �������.
    /// </summary>
    [RequireComponent(typeof(CircleCollider2D))]
    public class LevelConditionPosition : MonoBehaviour, ILevelCondition
    {

        #region Properties and Components

        /// <summary>
        /// ������ �����.
        /// </summary>
        [SerializeField] private float m_Radius;

        /// <summary>
        /// ��������� ����������, ������������, ��������� �� �������.
        /// </summary>
        private bool m_IsCompleted;

        /// <summary>
        /// ������ �� ���������� �������.
        /// </summary>
        bool ILevelCondition.isCompleted => m_IsCompleted;

        #endregion


        #region Unity Events

#if UNITY_EDITOR
        /// <summary>
        /// �����, ������������� ��� ��������� �����������.
        /// </summary>
        private void OnValidate()
        {
            GetComponent<CircleCollider2D>().radius = m_Radius;
        }

        private void OnDrawGizmos()
        {
            Handles.color = new Color(0, 0, 110, 0.05f);
            Handles.DrawSolidDisc(transform.position, transform.forward, m_Radius);
        }
#endif

        private void OnTriggerEnter2D(Collider2D collision)
        {
            // �������� �� ������� ������.
            SpaceShip ship = collision.transform.root.GetComponent<SpaceShip>();
            if (ship == null || ship != Player.Instance.ActiveShip) return;

            
            // ������� ���������.
            m_IsCompleted = true;
        }

        #endregion

    }
}