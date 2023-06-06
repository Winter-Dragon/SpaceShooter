using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// ����� ����� ������ ������.
    /// </summary>
    public class BonusNewWeapon : Bonus
    {

        #region Properties and Components

        /// <summary>
        /// ������ �� ����� �������������� �������.
        /// </summary>
        [SerializeField] private SO_TurretProperties m_Properties;

        #endregion


        #region Protected API

        /// <summary>
        /// �����, �������� ������� ������ ������.
        /// </summary>
        /// <param name="ship">������� ������.</param>
        protected override void OnPickedUp(SpaceShip ship)
        {
            ship.AssignWeapon(m_Properties);
        }

        #endregion

    }
}