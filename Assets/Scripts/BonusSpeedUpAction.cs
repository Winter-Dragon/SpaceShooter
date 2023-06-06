using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class BonusSpeedUpAction : MonoBehaviour
    {

        #region Properties and Components

        /// <summary>
        /// Ссылка на корабль.
        /// </summary>
        private SpaceShip m_SpaceShip;

        /// <summary>
        /// Внутренный таймер.
        /// </summary>
        private Timer m_Timer;

        /// <summary>
        /// Сила бонуса.
        /// </summary>
        private float m_Value;

        private bool m_BonusIsActive;

        #endregion


        #region Unity Events

        private void FixedUpdate()
        {
            // Проверка на null.
            if (m_SpaceShip == null || m_Timer == null) return;

            // Условие, гарантирующее, что бонус даётся один раз.
            if (m_BonusIsActive == false)
            {
                m_SpaceShip.AddThrust(m_Value);
                m_BonusIsActive = true;
            }

            // Обновляется таймер
            m_Timer.UpdateTimer();

            // При окончании бонуса снимает ускорение и уничтожается.
            if (m_Timer.IsFinished)
            {
                m_SpaceShip.AddThrust(-m_Value);
                Destroy(gameObject);
            }
        }

        #endregion


        #region Public API

        /// <summary>
        /// Метод, настраивающий бонус.
        /// </summary>
        /// <param name="ship">Корабль, получающий бонус.</param>
        /// <param name="time">Время действия бонуса.</param>
        /// <param name="value">Сила ускорения.</param>
        public void TuneBonus(SpaceShip ship, float time, float value)
        {
            // Задаётся корабль и активируется таймер. Записывается сила бонуса.
            m_SpaceShip = ship;
            m_Timer = new Timer(time, false);
            m_Value = value;
        }

        #endregion

    }
}
