using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// Класс смены оружия игроку.
    /// </summary>
    public class BonusNewWeapon : Bonus
    {

        #region Properties and Components

        /// <summary>
        /// Ссылка на новые характеристики туррели.
        /// </summary>
        [SerializeField] private SO_TurretProperties m_Properties;

        #endregion


        #region Protected API

        /// <summary>
        /// Метод, меняющий кораблю игрока оружие.
        /// </summary>
        /// <param name="ship">Корабль игрока.</param>
        protected override void OnPickedUp(SpaceShip ship)
        {
            ship.AssignWeapon(m_Properties);
        }

        #endregion

    }
}