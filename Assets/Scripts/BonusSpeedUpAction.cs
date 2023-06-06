using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class BonusSpeedUpAction : MonoBehaviour
    {

        #region Properties and Components

        /// <summary>
        /// ������ �� �������.
        /// </summary>
        private SpaceShip m_SpaceShip;

        /// <summary>
        /// ���������� ������.
        /// </summary>
        private Timer m_Timer;

        /// <summary>
        /// ���� ������.
        /// </summary>
        private float m_Value;

        private bool m_BonusIsActive;

        #endregion


        #region Unity Events

        private void FixedUpdate()
        {
            // �������� �� null.
            if (m_SpaceShip == null || m_Timer == null) return;

            // �������, �������������, ��� ����� ����� ���� ���.
            if (m_BonusIsActive == false)
            {
                m_SpaceShip.AddThrust(m_Value);
                m_BonusIsActive = true;
            }

            // ����������� ������
            m_Timer.UpdateTimer();

            // ��� ��������� ������ ������� ��������� � ������������.
            if (m_Timer.IsFinished)
            {
                m_SpaceShip.AddThrust(-m_Value);
                Destroy(gameObject);
            }
        }

        #endregion


        #region Public API

        /// <summary>
        /// �����, ������������� �����.
        /// </summary>
        /// <param name="ship">�������, ���������� �����.</param>
        /// <param name="time">����� �������� ������.</param>
        /// <param name="value">���� ���������.</param>
        public void TuneBonus(SpaceShip ship, float time, float value)
        {
            // ������� ������� � ������������ ������. ������������ ���� ������.
            m_SpaceShip = ship;
            m_Timer = new Timer(time, false);
            m_Value = value;
        }

        #endregion

    }
}
