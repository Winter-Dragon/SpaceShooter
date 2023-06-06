using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// Класс создания временных эффектов.
    /// </summary>
    public class ImpactEffect : MonoBehaviour
    {

        #region Properties and Components

        /// <summary>
        /// Время жизни эффектов.
        /// </summary>
        [SerializeField] private float m_LifeTime;

        /// <summary>
        /// Внутренний таймер.
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
            // Уничтожить объект, если таймер завершился.
            if (m_Timer.IsFinished) Destroy(gameObject);

            // Обновляем таймер.
            m_Timer.UpdateTimer();
        }

        #endregion

    }
}