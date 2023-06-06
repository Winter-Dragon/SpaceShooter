using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// �����, �������������� ���� � ������� ��������.
    /// </summary>
    public class UI_Controller_EpisodeSelectMenu : MonoBehaviour
    {

        #region Properties and Components

        /// <summary>
        /// ������ �� ������� ����.
        /// </summary>
        [SerializeField] private GameObject m_MainMenu;

        #endregion


        #region Public API

        /// <summary>
        /// �����, ������������ ������� ����.
        /// </summary>
        public void ClickButtonBack()
        {
            m_MainMenu.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }

        #endregion

    }
}