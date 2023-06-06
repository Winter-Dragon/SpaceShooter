using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// Класс, записывающий общеигровую статистику.
    /// </summary>
    public class GameStatistics : Singleton<GameStatistics>
    {

        #region Properties and Components

        /// <summary>
        /// Общее количество очков.
        /// </summary>
        private int m_TotalScore;

        /// <summary>
        /// Максимальное количество очков.
        /// </summary>
        private int m_BestScore;

        /// <summary>
        /// Количество уничтоженных астероидов.
        /// </summary>
        private int m_AsteroidsDestroyed;

        /// <summary>
        /// Больших астероидов уничтожено.
        /// </summary>
        private int m_BigAsteroidsDestroyed;

        /// <summary>
        /// Средних астероидов уничтожено.
        /// </summary>
        private int m_MediumAsteroidsDestroyed;

        /// <summary>
        /// Маленьких астероидов уничтожено.
        /// </summary>
        private int m_SmallAsteroidsDestroyed;

        /// <summary>
        /// Общее количество убийств.
        /// </summary>
        private int m_TotalKills;

        /// <summary>
        /// Уровней выиграно.
        /// </summary>
        private int m_LevelsWon;

        /// <summary>
        /// Уровней проиграно.
        /// </summary>
        private int m_LevelsLose;

        /// <summary>
        /// Количество секунд, проведённых в игре.
        /// </summary>
        private int m_GameTimeSeconds;

        /// <summary>
        /// Количество минут, проведённых в игре.
        /// </summary>
        private int m_GameTimeMinuts;

        /// <summary>
        /// Количество часов, проведённых в игре.
        /// </summary>
        private int m_GameTimeHours;

        /// <summary>
        /// Количество дней, проведённых в игре.
        /// </summary>
        private int m_GameTimeDays;

        #region Links

        public int TotalScore => m_TotalScore;
        public int BestScore => m_BestScore;
        public int AsteroidsDestroyed => m_AsteroidsDestroyed;
        public int BigAsteroidsDestroyed => m_BigAsteroidsDestroyed;
        public int MediumAsteroidsDestroyed => m_MediumAsteroidsDestroyed;
        public int SmallAsteroidsDestroyed => m_SmallAsteroidsDestroyed;
        public int TotalKills => m_TotalKills;
        public int LevelsWon => m_LevelsWon;
        public int LevelsLose => m_LevelsLose;
        public int GameTimeSeconds => m_GameTimeSeconds;
        public int GameTimeMinuts => m_GameTimeMinuts;
        public int GameTimeHours => m_GameTimeHours;
        public int GameTimeDays => m_GameTimeDays;

        #endregion

        #region Local

        /// <summary>
        /// Локальный таймер.
        /// </summary>
        private Timer m_LocalTimer;

        #endregion

        #endregion


        #region Unity Events

        private void Start()
        {
            // Создание посекундного таймера.
            m_LocalTimer = new Timer(1.0f, true);

            // Обновление переменных из памяти.
            LoadValues();
        }

        private void FixedUpdate()
        {
            // Обновление секундного таймера.
            m_LocalTimer.UpdateTimer();

            // Подсчёт общеигрового времени.
            if (m_LocalTimer.IsFinished) UpdateGameTime();
        }

        #endregion


        #region Private API

        /// <summary>
        /// Метод, обновляющий общеигровое время.
        /// </summary>
        private void UpdateGameTime()
        {
            m_GameTimeSeconds++;
            if (m_GameTimeSeconds >= 60)
            {
                m_GameTimeSeconds -= 60;
                m_GameTimeMinuts++;

                if (m_GameTimeMinuts >= 60)
                {
                    m_GameTimeMinuts -= 60;
                    m_GameTimeHours++;

                    if (m_GameTimeHours >= 24)
                    {
                        m_GameTimeHours -= 24;

                        m_GameTimeDays++;
                    }
                }
            }
        }

        /// <summary>
        /// Метод, берущий все значения статистики из памяти.
        /// </summary>
        private void LoadValues()
        {
            m_TotalScore = PlayerPrefs.GetInt("TotalScore", 0);
            m_BestScore = PlayerPrefs.GetInt("BestScore", 0);
            m_AsteroidsDestroyed = PlayerPrefs.GetInt("AsteroidsDestroyed", 0);
            m_BigAsteroidsDestroyed = PlayerPrefs.GetInt("BigAsteroidsDestroyed", 0);
            m_MediumAsteroidsDestroyed = PlayerPrefs.GetInt("MediumAsteroidsDestroyed", 0);
            m_SmallAsteroidsDestroyed = PlayerPrefs.GetInt("SmallAsteroidsDestroyed", 0);
            m_TotalKills = PlayerPrefs.GetInt("TotalKills", 0);
            m_LevelsWon = PlayerPrefs.GetInt("LevelsWon", 0);
            m_LevelsLose = PlayerPrefs.GetInt("LevelsLose", 0);
            m_GameTimeSeconds = PlayerPrefs.GetInt("GameTimeSeconds", 0);
            m_GameTimeMinuts = PlayerPrefs.GetInt("GameTimeMinuts", 0);
            m_GameTimeHours = PlayerPrefs.GetInt("GameTimeHours", 0);
            m_GameTimeDays = PlayerPrefs.GetInt("GameTimeDays", 0);
        }

        #endregion


        #region Public API

        /// <summary>
        /// Добавить очки в общеигровую статистику.
        /// </summary>
        /// <param name="score">Количество очков.</param>
        public void AddScore(int score)
        {
            m_TotalScore += score;
        }

        /// <summary>
        /// Сравнить значение очков за уровень и если оно будет больше, чеем лучшее значение очков - заменить его в общеигровой статистике.
        /// </summary>
        /// <param name="score">Значение очков за уровень.</param>
        public void CompareBestScore(int score)
        {
            if (score > m_BestScore)
            {
                m_BestScore = score;
            }       
        }

        /// <summary>
        /// Добавить уничтоженный астероид в общеигровую статистику.
        /// </summary>
        /// <param name="type">Тип астероида.</param>
        public void AsteroidDestroyed(Asteroid.AsteroidType type)
        {
            switch (type)
            {
                case Asteroid.AsteroidType.Big:
                    m_BigAsteroidsDestroyed++;
                    break;

                case Asteroid.AsteroidType.Medium:
                    m_MediumAsteroidsDestroyed++;
                    break;

                case Asteroid.AsteroidType.Small:
                    m_SmallAsteroidsDestroyed++;
                    break;
            }

            m_AsteroidsDestroyed++;
        }

        /// <summary>
        /// Добавить 1 убийство в общеигровую статистику.
        /// </summary>
        public void AddKill()
        {
            m_TotalKills++;
        }

        /// <summary>
        /// Общее количество выигранных уровней.
        /// </summary>
        public void AddCompletedLevel()
        {
            m_LevelsWon++;
        }

        /// <summary>
        /// Общее количество проигранных уровней.
        /// </summary>
        public void AddLoseLevel()
        {
            m_LevelsLose++;
        }

        /// <summary>
        /// Метод, сохраняющий игровую статистику.
        /// </summary>
        public void SaveGameStatistics()
        {
            PlayerPrefs.SetInt("TotalScore", m_TotalScore);
            PlayerPrefs.SetInt("BestScore", m_BestScore);
            PlayerPrefs.SetInt("BigAsteroidsDestroyed", m_BigAsteroidsDestroyed);
            PlayerPrefs.SetInt("MediumAsteroidsDestroyed", m_MediumAsteroidsDestroyed);
            PlayerPrefs.SetInt("SmallAsteroidsDestroyed", m_SmallAsteroidsDestroyed);
            PlayerPrefs.SetInt("AsteroidsDestroyed", m_AsteroidsDestroyed);
            PlayerPrefs.SetInt("LevelsLose", m_LevelsLose);
            PlayerPrefs.SetInt("LevelsWon", m_LevelsWon);
            PlayerPrefs.SetInt("TotalKills", m_TotalKills);
            PlayerPrefs.SetInt("GameTimeSeconds", m_GameTimeSeconds);
            PlayerPrefs.SetInt("GameTimeMinuts", m_GameTimeMinuts);
            PlayerPrefs.SetInt("GameTimeHours", m_GameTimeHours);
            PlayerPrefs.SetInt("GameTimeDays", m_GameTimeDays);
        }

        #endregion

    }
}