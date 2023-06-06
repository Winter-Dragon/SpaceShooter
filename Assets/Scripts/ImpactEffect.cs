using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// ����� �������� ��������� ��������.
    /// </summary>
    public class ImpactEffect : MonoBehaviour
    {

        #region Properties and Components

        /// <summary>
        /// ����� ����� ��������.
        /// </summary>
        [SerializeField] private float m_LifeTime;

        /// <summary>
        /// ���������� ������.
        /// </summary>
        private Timer m_Timer;

        #endregion


        #region Unity Events

        private void Start()
        {
            m_Timer = new Timer(m_LifeTime, false);
        }

        protected virtual void FixedUpdate()
        {
            // ���������� ������, ���� ������ ����������.
            if (m_Timer.IsFinished) Destroy(gameObject);

            // ��������� ������.
            m_Timer.UpdateTimer();
        }

        #endregion

    }
}