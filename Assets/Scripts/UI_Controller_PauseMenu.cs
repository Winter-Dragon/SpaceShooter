using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SpaceShooter
{
    /// <summary>
    /// �����, ���������� �� ������ ���� �����.
    /// </summary>
    public class UI_Controller_PauseMenu : Singleton<UI_Controller_PauseMenu>
    {

        #region Unity Events

        private void Start()
        {
            // ��� ������ ������ ������.
            gameObject.SetActive(false);
        }

        #endregion


        #region Public API

        /// <summary>
        /// ����� ��������� �����.
        /// </summary>
        public void PauseActive()
        {
            // ������� ���� ����� � ���������� �����.
            Time.timeScale = 0;
            gameObject.SetActive(true);
        }

        /// <summary>
        /// ����� ������� �� ������ ������������� ����.
        /// </summary>
        public void ClickedButtonResume()
        {
            // ������ ���� ����� � ����������� �����.
            Time.timeScale = 1;
            gameObject.SetActive(false);
        }

        /// <summary>
        /// ����� ������� �� ������ ����������� ������.
        /// </summary>
        public void ClickedButtonRestart()
        {
            // ������ ���� �����, ����������� ����� � ������������� �����.
            Time.timeScale = 1;
            gameObject.SetActive(false);
            LevelSequenceController.Instance.RestartLevel();
        }

        /// <summary>
        /// ����� ������� �� ������ �������� ����.
        /// </summary>
        public void ClickedButtonMainMenu()
        {
            // ����������� �����, ������ ���� ����� � ��������� ����� �������� ����.
            Time.timeScale = 1;
            gameObject.SetActive(false);
            SceneManager.LoadScene(LevelSequenceController.MainMenuSceneNickName);
        }

        #endregion
    }
}