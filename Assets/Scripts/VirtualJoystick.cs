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
        /// ������ �� ��� ���������.
        /// </summary>
        [SerializeField] private Image m_JoyBack;

        /// <summary>
        /// ������ �� �������� ����� ���������.
        /// </summary>
        [SerializeField] private Image m_Joystick;

        /// <summary>
        /// ������ �� ������� �������� ���������.
        /// </summary>
        public Vector3 Value { get; private set; }

        #endregion


        #region Public API

        /// <summary>
        /// ����� ����������� ���������.
        /// </summary>
        /// <param name="eventData"></param>
        public void OnDrag(PointerEventData eventData)
        {
            // 1. �������� ������� �������.
            Vector2 position = Vector2.zero;

            // 2. ������� ������� ��������� ���������� ���������.
            RectTransformUtility.ScreenPointToLocalPointInRectangle(m_JoyBack.rectTransform, eventData.position, eventData.pressEventCamera, out position);

            // 3. ������������� �������� �������.
            position.x /= m_JoyBack.rectTransform.sizeDelta.x;
            position.y /= m_JoyBack.rectTransform.sizeDelta.y;

            // 4. �������� �������� 0,0 � �����.
            position.x = position.x * 2 - 1;
            position.y = position.y * 2 - 1;

            // 5. ����� �������� ������� Value.
            Value = new Vector3(position.x, position.y, 0);

            // 6. ����������� ������: ���� ����� ������� > 1 => ������������� ������
            if (Value.magnitude > 1) Value = Value.normalized;

            // 7. ������� ���������� �������� ���������
            float offsetX = m_JoyBack.rectTransform.sizeDelta.x / 2 - m_Joystick.rectTransform.sizeDelta.x / 2;
            float offsetY = m_JoyBack.rectTransform.sizeDelta.y / 2 - m_Joystick.rectTransform.sizeDelta.y / 2;

            // 8. ������ �������� ���������
            m_Joystick.rectTransform.anchoredPosition = new Vector2(Value.x * offsetX, Value.y * offsetY);
        }

        /// <summary>
        /// ����� ����� �� ��������.
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerDown(PointerEventData eventData)
        {
            // ������� ����� �����������.
            OnDrag(eventData);
        }

        /// <summary>
        /// ����� ���������� ���������.
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerUp(PointerEventData eventData)
        {
            // �������� ������ � ������ ��������� ��������� ���������.
            Value = Vector3.zero;
            m_Joystick.rectTransform.anchoredPosition = Vector3.zero;
        }

        #endregion

    }
}