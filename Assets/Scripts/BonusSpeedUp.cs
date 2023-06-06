using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class BonusSpeedUp : Bonus
    {

        #region Properties and Components

        /// <summary>
        /// Значение ускорения.
        /// </summary>
        [SerializeField] private float m_Value;

        /// <summary>
        /// Время ускорения.
        /// </summary>
        [SerializeField] private float m_ActionTime;

        /// <summary>
        /// Префаб на действие ускорения.
        /// </summary>
        [SerializeField] private BonusSpeedUpAction m_PowerupPrefab;

        #endregion


        #region Protected API

        protected override void OnPickedUp(SpaceShip ship)
        {
            // Создание и настройка бонуса.
            BonusSpeedUpAction bonus = Instantiate(m_PowerupPrefab);
            bonus.TuneBonus(ship, m_ActionTime, m_Value);
        }

        #endregion
    }
}