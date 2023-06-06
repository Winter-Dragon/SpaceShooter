using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace SpaceShooter
{
    public class VirtualJoystick : Singleton<VirtualJoystick>, IDragHandler, IPointerUpHandler, IPointerDownHandler
    {

        #region Properties and Components

        /// <summary>
        /// Ссылка на фон джойстика.
        /// </summary>
        [SerializeField] private Image m_JoyBack;

        /// <summary>
        /// Ссылка на активную часть джойстика.
        /// </summary>
        [SerializeField] private Image m_Joystick;

        /// <summary>
        /// Ссылка на текущее значение джойстика.
        /// </summary>
        public Vector3 Value { get; private set; }

        #endregion


        #region Public API

        /// <summary>
        /// Метод перемещения джойстика.
        /// </summary>
        /// <param name="eventData"></param>
        public void OnDrag(PointerEventData eventData)
        {
            // 1. Создание пустого вектора.
            Vector2 position = Vector2.zero;

            // 2. Придать вектору локальные координаты джойстика.
            RectTransformUtility.ScreenPointToLocalPointInRectangle(m_JoyBack.rectTransform, eventData.position, eventData.pressEventCamera, out position);

            // 3. Нормализовать значение вектора.
            position.x /= m_JoyBack.rectTransform.sizeDelta.x;
            position.y /= m_JoyBack.rectTransform.sizeDelta.y;

            // 4. Сместить значение 0,0 в центр.
            position.x = position.x * 2 - 1;
            position.y = position.y * 2 - 1;

            // 5. Задаём значения вектору Value.
            Value = new Vector3(position.x, position.y, 0);

            // 6. Нормализуем вектор: если длина вектора > 1 => Нормализовать вектор
            if (Value.magnitude > 1) Value = Value.normalized;

            // 7. Создать переменные смещения джойстика
            float offsetX = m_JoyBack.rectTransform.sizeDelta.x / 2 - m_Joystick.rectTransform.sizeDelta.x / 2;
            float offsetY = m_JoyBack.rectTransform.sizeDelta.y / 2 - m_Joystick.rectTransform.sizeDelta.y / 2;

            // 8. Задать смещение джойстику
            m_Joystick.rectTransform.anchoredPosition = new Vector2(Value.x * offsetX, Value.y * offsetY);
        }

        /// <summary>
        /// Метод клика на джойстик.
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerDown(PointerEventData eventData)
        {
            // Вызвать метод перемещения.
            OnDrag(eventData);
        }

        /// <summary>
        /// Метод отпускания джойстика.
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerUp(PointerEventData eventData)
        {
            // Обнулить вектор и задать начальное положение джойстика.
            Value = Vector3.zero;
            m_Joystick.rectTransform.anchoredPosition = Vector3.zero;
        }

        #endregion

    }
}