using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// Класс добавления игроку энергии и патронов.
    /// </summary>
    public class BonusStats : Bonus
    {

        #region Properties And Components

        /// <summary>
        /// Список эффектов бонусов добавления стат.
        /// </summary>
        public enum EffectType
        {
            AddEnergy,
            AddAmmo
        }

        /// <summary>
        /// Выбор типа эффекта из инспектора.
        /// </summary>
        [SerializeField] private EffectType m_EffectType;

        /// <summary>
        /// Ценность бонуса.
        /// </summary>
        [SerializeField] private float m_Value;

        #endregion


        #region Protected API

        /// <summary>
        /// Метод, добавляющий игроку энергию и патроны.
        /// </summary>
        /// <param name="ship">Корабль игрока.</param>
        protected override void OnPickedUp(SpaceShip ship)
        {
            switch (m_EffectType)
            {
                // Действие эффекта добавления энергии.
                case EffectType.AddEnergy:
                    ship.AddEnergy((int) m_Value);
                    break;

                // Действие эффекта добавления патронов.
                case EffectType.AddAmmo:
                    ship.AddAmmo((int)m_Value);
                    break;
            }
        }

        #endregion

    }
}