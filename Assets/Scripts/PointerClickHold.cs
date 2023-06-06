using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace SpaceShooter
{
    /// <summary>
    ///  ласс, позвол€ющий обрабатывающий нажати€ с мобильных устройств.
    /// </summary>
    public class PointerClickHold : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {

        #region Properties and Components

        /// <summary>
        /// ѕеременна€, возвращающа€ true, если кнопка выстрела зажата.
        /// </summary>
        private bool m_Hold;

        /// <summary>
        /// —сылка на значение переменной зажати€ кнопки выстрела.
        /// </summary>
        public bool IsHold => m_Hold;

        #endregion


        #region Public API

        /// <summary>
        /// ћетод, срабатывающий при задеривании интерфейса выстрела.
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerDown(PointerEventData eventData)
        {
            m_Hold = true;
        }

        /// <summary>
        /// ћетод, срабатывающий при отпускании интерфейса выстрела.
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerUp(PointerEventData eventData)
        {
            m_Hold = false;
        }

        #endregion

    }
}