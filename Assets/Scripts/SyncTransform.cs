using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// ������, ���������������� Transform �������� ������� � ��������� ��������.
    /// </summary>
    public class SyncTransform : MonoBehaviour
    {

        #region Properties and Components

        /// <summary>
        /// ������ �� ������, � ������� ����� ���������������� ������� Transform.
        /// </summary>
        [SerializeField] private Transform m_Target;

        #endregion


        #region Unity Events

        private void FixedUpdate()
        {
            // ����� ������� ��������� ������� �������� ������� �������� �� x � y.
            transform.position = new Vector3(m_Target.position.x, m_Target.position.y, transform.position.z);
        }

        #endregion

    }
}