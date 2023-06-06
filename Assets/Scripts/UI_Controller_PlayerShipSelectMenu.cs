using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter
{
    /// <summary>
    /// �����, ���������� �� ������ ���� ������ �������.
    /// </summary>
    public class UI_Controller_PlayerShipSelectMenu : Singleton<UI_Controller_PlayerShipSelectMenu>
    {

        #region Properties and Components

        /// <summary>
        /// ������ � �������� ������ �������.
        /// </summary>
        [SerializeField] private Button[] m_Buttons;

        /// <summary>
        /// ������ �� ������ � ��������.
        /// </summary>
        public Button[] Buttons => m_Buttons;

        /// <summary>
        /// ������ �� ������� ����.
        /// </summary>
        [SerializeField] private GameObject m_MainMenu;

        #endregion


        #region Public API

        /// <summary>
        /// �����, ������������ � ������� ����.
        /// </summary>
        public void ClickedButtonBack()
        {
            m_MainMenu.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }

        #endregion

    }
}