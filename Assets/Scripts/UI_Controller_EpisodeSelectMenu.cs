using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// Класс, контролирующий меню с выбором эпизодов.
    /// </summary>
    public class UI_Controller_EpisodeSelectMenu : MonoBehaviour
    {

        #region Properties and Components

        /// <summary>
        /// Ссылка на главное меню.
        /// </summary>
        [SerializeField] private GameObject m_MainMenu;

        #endregion


        #region Public API

        /// <summary>
        /// Метод, возвращающий главное меню.
        /// </summary>
        public void ClickButtonBack()
        {
            m_MainMenu.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }

        #endregion

    }
}