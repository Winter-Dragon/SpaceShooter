using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter
{
    /// <summary>
    /// Класс, отвечающий за работу меню выбора корабля.
    /// </summary>
    public class UI_Controller_PlayerShipSelectMenu : Singleton<UI_Controller_PlayerShipSelectMenu>
    {

        #region Properties and Components

        /// <summary>
        /// Массив с кнопками выбора корабля.
        /// </summary>
        [SerializeField] private Button[] m_Buttons;

        /// <summary>
        /// Ссылка на массив с кнопками.
        /// </summary>
        public Button[] Buttons => m_Buttons;

        /// <summary>
        /// Ссылка на главное меню.
        /// </summary>
        [SerializeField] private GameObject m_MainMenu;

        #endregion


        #region Public API

        /// <summary>
        /// Метод, возвращающий в главное меню.
        /// </summary>
        public void ClickedButtonBack()
        {
            m_MainMenu.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }

        #endregion

    }
}