using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SpaceShooter
{
    /// <summary>
    /// Класс, отвечающий за работу меню паузы.
    /// </summary>
    public class UI_Controller_PauseMenu : Singleton<UI_Controller_PauseMenu>
    {

        #region Unity Events

        private void Start()
        {
            // При старте скрыть объект.
            gameObject.SetActive(false);
        }

        #endregion


        #region Public API

        /// <summary>
        /// Метод включения паузы.
        /// </summary>
        public void PauseActive()
        {
            // Открыть меню паузы и остановить время.
            Time.timeScale = 0;
            gameObject.SetActive(true);
        }

        /// <summary>
        /// Метод нажатия на кнопку возобновления игры.
        /// </summary>
        public void ClickedButtonResume()
        {
            // Скрыть меню паузы и возобновить время.
            Time.timeScale = 1;
            gameObject.SetActive(false);
        }

        /// <summary>
        /// Метод нажатия на кнопку перезапуска уровня.
        /// </summary>
        public void ClickedButtonRestart()
        {
            // Скрыть меню паузы, возобновить время и перезапустить сцену.
            Time.timeScale = 1;
            gameObject.SetActive(false);
            LevelSequenceController.Instance.RestartLevel();
        }

        /// <summary>
        /// Метод нажатия на кнопку главного меню.
        /// </summary>
        public void ClickedButtonMainMenu()
        {
            // Возобновить время, скрыть меню паузы и запустить сцену главного меню.
            Time.timeScale = 1;
            gameObject.SetActive(false);
            SceneManager.LoadScene(LevelSequenceController.MainMenuSceneNickName);
        }

        #endregion
    }
}