using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace SpaceShooter
{
    /// <summary>
    /// �����, �������� ������� ���������� ������ ��������.
    /// </summary>
    public class CircleArea : MonoBehaviour
    {

        #region Properties and Components

        /// <summary>
        /// ������ ����������.
        /// </summary>
        [SerializeField] private float m_Radius;

        /// <summary>
        /// ������ �� ������ ����������.
        /// </summary>
        public float Radius => m_Radius;

        #endregion


        #region Unity Events

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            // ���������� ���������� � �������� ��������.
            Handles.color = Color.green;
            Handles.DrawWireDisc(transform.position, transform.forward, m_Radius);
        }
#endif

        #endregion


        #region Public API

        /// <summary>
        /// �����, ������������ ��������� ���������� ����� � ����������.
        /// </summary>
        /// <returns></returns>
        public Vector2 GetRandomInsideZone()
        {
            return (Vector2)transform.position + (Vector2)UnityEngine.Random.insideUnitSphere * m_Radius;
        }

        #endregion

    }
}