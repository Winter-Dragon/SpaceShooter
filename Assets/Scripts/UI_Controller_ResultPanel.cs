using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

namespace SpaceShooter
{
    /// <summary>
    /// Класс интерфейса панели результатов.
    /// </summary>
    public class UI_Controller_ResultPanel : Singleton<UI_Controller_ResultPanel>
    {

        #region Properties and Components

        /// <summary>
        /// Текстовое поле результата уровня.
        /// </summary>
        [SerializeField] private TextMeshProUGUI m_Result;

        /// <summary>
        /// Текстовое поле очков.
        /// </summary>
        [SerializeField] private TextMeshProUGUI m_Score;

        /// <summary>
        /// Текстовое поле убийств.
        /// </summary>
        [SerializeField] private TextMeshProUGUI m_Kills;

        /// <summary>
        /// Текстовое поле времени уровня.
        /// </summary>
        [SerializeField] private TextMeshProUGUI m_Time;

        /// <summary>
        /// Текстовое поле кнопки.
        /// </summary>
        [SerializeField] private TextMeshProUGUI m_ButtonText;

        /// <summary>
        /// Локальная переменная, завершён ли уровень.
        /// </summary>
        private bool m_Success;

        #endregion


        #region Unity Events

        private void Start()
        {
            // При старте скрыть окно.
            gameObject.SetActive(false);
        }

        #endregion


        #region Public API

        /// <summary>
        /// Метод отображения результатов уровня.
        /// </summary>
        /// <param name="success">true если уровень пройден, false не пройден.</param>
        public void ShowResults(bool success)
        {
            // Проверка на игрока.
            if (Player.Instance == null) return;

            // Открыть окно.
            gameObject.SetActive(true);

            // Записать результаты прохождения уровня.
            m_Success = success;
            int score = Player.Instance.Score;
            int kills = Player.Instance.NumberKills;
            string time = UI_Interface_TimeStats.Instance.TimeText;

            // Сравнить значение очков с максимальным и добавить в статистику.
            GameStatistics.Instance.CompareBestScore(score * Player.ScoreMultiplier);

            // Добавить выигранный/проигранный уровень в статистику.
            if (success) GameStatistics.Instance.AddCompletedLevel();
            else GameStatistics.Instance.AddLoseLevel();

            // Записать текстовые поля в зависимости от результатов уровня.
            m_Result.text = success ? "you're win" : "you're lose";
            m_Score.text = (score * Player.ScoreMultiplier).ToString();
            m_Kills.text = kills.ToString();
            m_Time.text = time;
            m_ButtonText.text = success ? "Next" : "Restart";

            // Остановить время.
            Time.timeScale = 0;
        }

        /// <summary>
        /// Метод нажатия на кнопку next/restart.
        /// </summary>
        public void ClickButtonNext()
        {
            // Закрыть окно.
            gameObject.SetActive(false);

            // Возобновить время.
            Time.timeScale = 1;

            // Если победа - запустить следующий уровень, если поражение - перезапустить текущий уровень.
            if (m_Success) LevelSequenceController.Instance.AdvanceLevel();
            else LevelSequenceController.Instance.RestartLevel();
        }

        /// <summary>
        /// Метод нажатия на кнопку выхода в главное меню.
        /// </summary>
        public void ClickButtonMainMenu()
        {
            // Возобновить время, скрыть меню результатов и запустить сцену главного меню.
            Time.timeScale = 1;
            gameObject.SetActive(false);
            SceneManager.LoadScene(LevelSequenceController.MainMenuSceneNickName);
        }

        #endregion

    }
}