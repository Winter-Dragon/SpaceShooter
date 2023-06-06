using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// ����� �������� ���� ����.
    /// </summary>
    public class UI_Controller_MainMenu : Singleton<UI_Controller_MainMenu>
    {

        #region Properties and Components

        /// <summary>
        /// ������ �� ������ ������ ��������.
        /// </summary>
        [SerializeField] private GameObject m_EpisodeSelections;

        /// <summary>
        /// ������ �� ������ ������ �������.
        /// </summary>
        [SerializeField] private GameObject m_ShipSelection;

        /// <summary>
        /// ������ �� ������ ����������.
        /// </summary>
        [SerializeField] private GameObject m_Statistics;

        #endregion


        #region Public API

        /// <summary>
        /// ����� ������� �� ������ ������� ����.
        /// </summary>
        public void ClickButtonPlay()
        {
            m_EpisodeSelections.gameObject.SetActive(true);

            gameObject.SetActive(false);
        }

        /// <summary>
        /// ����� ������� �� ������ ������ �������.
        /// </summary>
        public void ClickButtonChooseShip()
        {
            m_ShipSelection.gameObject.SetActive(true);

            gameObject.SetActive(false);
        }

        /// <summary>
        /// ����� ������� �� ������ ����������.
        /// </summary>
        public void ClickButtonStatistics()
        {
            m_Statistics.gameObject.SetActive(true);

            gameObject.SetActive(false);
        }

        /// <summary>
        /// ����� ������� �� ������ ������.
        /// </summary>
        public void ClickButtonExit()
        {
            GameStatistics.Instance.SaveGameStatistics();

            Application.Quit();
        }

        #endregion

    }
}