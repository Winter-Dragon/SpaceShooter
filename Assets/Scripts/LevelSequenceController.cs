using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SpaceShooter
{
    /// <summary>
    /// Класс, контролирующий переходы между уровнями.
    /// </summary>
    public class LevelSequenceController : Singleton<LevelSequenceController>
    {

        #region Properties and Components

        /// <summary>
        /// Статичная строка, хранящая в себе имя сцены главного меню.
        /// </summary>
        public static string MainMenuSceneNickName = "main_menu";

        /// <summary>
        /// Класс Episode текущего эпизода.
        /// </summary>
        public SO_Episode CurrentEpisode { get; private set; }

        /// <summary>
        /// Значение текущего уровня.
        /// </summary>
        public int CurrentLevel { get; private set; }

        /// <summary>
        /// Переменная, отображающая, завершён ли уровень.
        /// </summary>
        public bool LastLevelResult { get; private set; }

        /// <summary>
        /// Ссылка на текущий префаб корабля игрока.
        /// </summary>
        public static SpaceShip PlayerShip { get; set; }

        #endregion


        #region Public API

        /// <summary>
        /// Метод запуска эпизода.
        /// </summary>
        /// <param name="episode">Episode, который необходимо запустить.</param>
        public void StartEpisode(SO_Episode episode)
        {
            // Задаём значение текущего эпизода и уровня.
            CurrentEpisode = episode;
            CurrentLevel = 0;

            // Загружаем сцену.
            SceneManager.LoadScene(episode.Levels[CurrentLevel]);
        }

        /// <summary>
        /// Метод перезапуска текущего уровня.
        /// </summary>
        public void RestartLevel()
        {
            // Обнуляем переменные в классе Player.
            if (Player.Instance != null) Player.Instance.Restart();

            // Перезагружаем сцену.
            SceneManager.LoadScene(CurrentEpisode.Levels[CurrentLevel]);
        }

        /// <summary>
        /// Метод, срабатывающий при завершении уровня.
        /// </summary>
        /// <param name="success">true если уровень завершён, false если не завершён.</param>
        public void FinishCurrentLevel(bool success)
        {
            // Сохраняются результаты уровня.
            LastLevelResult = success;

            // Отображается статистика уровня.
            UI_Controller_ResultPanel.Instance.ShowResults(success);
        }

        /// <summary>
        /// Метод запуска следующего уровня.
        /// </summary>
        public void AdvanceLevel()
        {
            // Обнуляем переменные в классе Player.
            if (Player.Instance != null) Player.Instance.Restart();

            // Текущий уровень++.
            CurrentLevel++;

            // Если уровни в эпизоде завершены - возвращает в главное меню.
            if (CurrentEpisode.Levels.Length <= CurrentLevel) SceneManager.LoadScene(MainMenuSceneNickName);
            //Если не завершены - запускает следующий уровень.
            else SceneManager.LoadScene(CurrentEpisode.Levels[CurrentLevel]);
        }

        #endregion

    }
}