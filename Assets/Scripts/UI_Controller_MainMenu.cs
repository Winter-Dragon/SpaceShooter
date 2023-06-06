using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// Класс главного меню игры.
    /// </summary>
    public class UI_Controller_MainMenu : Singleton<UI_Controller_MainMenu>
    {

        #region Properties and Components

        /// <summary>
        /// Ссылка на панель выбора эпизодов.
        /// </summary>
        [SerializeField] private GameObject m_EpisodeSelections;

        /// <summary>
        /// Ссылка на панель выбора корабля.
        /// </summary>
        [SerializeField] private GameObject m_ShipSelection;

        /// <summary>
        /// Ссылка на панель статистики.
        /// </summary>
        [SerializeField] private GameObject m_Statistics;

        #endregion


        #region Public API

        /// <summary>
        /// Метод нажатия на кнопку запуска игры.
        /// </summary>
        public void ClickButtonPlay()
        {
            m_EpisodeSelections.gameObject.SetActive(true);

            gameObject.SetActive(false);
        }

        /// <summary>
        /// Метод нажатия на кнопку выбора корабля.
        /// </summary>
        public void ClickButtonChooseShip()
        {
            m_ShipSelection.gameObject.SetActive(true);

            gameObject.SetActive(false);
        }

        /// <summary>
        /// Метод нажатия на кнопку статистики.
        /// </summary>
        public void ClickButtonStatistics()
        {
            m_Statistics.gameObject.SetActive(true);

            gameObject.SetActive(false);
        }

        /// <summary>
        /// Метод нажатия на кнопку выхода.
        /// </summary>
        public void ClickButtonExit()
        {
            GameStatistics.Instance.SaveGameStatistics();

            Application.Quit();
        }

        #endregion

    }
}