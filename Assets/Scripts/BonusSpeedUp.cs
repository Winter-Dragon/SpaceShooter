using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class BonusSpeedUp : Bonus
    {

        #region Properties and Components

        /// <summary>
        /// �������� ���������.
        /// </summary>
        [SerializeField] private float m_Value;

        /// <summary>
        /// ����� ���������.
        /// </summary>
        [SerializeField] private float m_ActionTime;

        /// <summary>
        /// ������ �� �������� ���������.
        /// </summary>
        [SerializeField] private BonusSpeedUpAction m_PowerupPrefab;

        #endregion


        #region Protected API

        protected override void OnPickedUp(SpaceShip ship)
        {
            // �������� � ��������� ������.
            BonusSpeedUpAction bonus = Instantiate(m_PowerupPrefab);
            bonus.TuneBonus(ship, m_ActionTime, m_Value);
        }

        #endregion
    }
}