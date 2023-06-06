using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace SpaceShooter
{
    /// <summary>
    /// �����, ����������� �������������� ������� � ��������� ���������.
    /// </summary>
    public class PointerClickHold : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {

        #region Properties and Components

        /// <summary>
        /// ����������, ������������ true, ���� ������ �������� ������.
        /// </summary>
        private bool m_Hold;

        /// <summary>
        /// ������ �� �������� ���������� ������� ������ ��������.
        /// </summary>
        public bool IsHold => m_Hold;

        #endregion


        #region Public API

        /// <summary>
        /// �����, ������������� ��� ����������� ���������� ��������.
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerDown(PointerEventData eventData)
        {
            m_Hold = true;
        }

        /// <summary>
        /// �����, ������������� ��� ���������� ���������� ��������.
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerUp(PointerEventData eventData)
        {
            m_Hold = false;
        }

        #endregion

    }
}