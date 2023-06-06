using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// �����, ��������� ������� �����.
    /// </summary>
    public class LevelBoundary : Singleton<LevelBoundary>
    {

        #region Properties and Components

        /// <summary>
        /// ������ ������� ������.
        /// </summary>
        [SerializeField] private float m_Radius;

        /// <summary>
        /// ������ �� ������ ������� ������.
        /// </summary>
        public float Radius => m_Radius;

        /// <summary>
        /// ������ ����������� ������: ����� ��� ��������.
        /// </summary>
        public enum Mode
        {
            Limit,
            Teleport
        }

        /// <summary>
        /// ���������� ����� ����������� ������ ����� ���������.
        /// </summary>
        [SerializeField] private Mode m_LimitMode;

        /// <summary>
        /// ������ �� ����� ����������� ������.
        /// </summary>
        public Mode LimitMode => m_LimitMode;

        #endregion


        #region Unity Events

        // � ������� ������ ������ ����������� ������
#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            UnityEditor.Handles.color = Color.red;
            UnityEditor.Handles.DrawWireDisc(transform.position, transform.forward, m_Radius);
        }
#endif

#endregion

    }
}