using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// �����, ������������ ����������� ����������.
    /// </summary>
    public class GameStatistics : Singleton<GameStatistics>
    {

        #region Properties and Components

        /// <summary>
        /// ����� ���������� �����.
        /// </summary>
        private int m_TotalScore;

        /// <summary>
        /// ������������ ���������� �����.
        /// </summary>
        private int m_BestScore;

        /// <summary>
        /// ���������� ������������ ����������.
        /// </summary>
        private int m_AsteroidsDestroyed;

        /// <summary>
        /// ������� ���������� ����������.
        /// </summary>
        private int m_BigAsteroidsDestroyed;

        /// <summary>
        /// ������� ���������� ����������.
        /// </summary>
        private int m_MediumAsteroidsDestroyed;

        /// <summary>
        /// ��������� ���������� ����������.
        /// </summary>
        private int m_SmallAsteroidsDestroyed;

        /// <summary>
        /// ����� ���������� �������.
        /// </summary>
        private int m_TotalKills;

        /// <summary>
        /// ������� ��������.
        /// </summary>
        private int m_LevelsWon;

        /// <summary>
        /// ������� ���������.
        /// </summary>
        private int m_LevelsLose;

        /// <summary>
        /// ���������� ������, ���������� � ����.
        /// </summary>
        private int m_GameTimeSeconds;

        /// <summary>
        /// ���������� �����, ���������� � ����.
        /// </summary>
        private int m_GameTimeMinuts;

        /// <summary>
        /// ���������� �����, ���������� � ����.
        /// </summary>
        private int m_GameTimeHours;

        /// <summary>
        /// ���������� ����, ���������� � ����.
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
        /// ��������� ������.
        /// </summary>
        private Timer m_LocalTimer;

        #endregion

        #endregion


        #region Unity Events

        private void Start()
        {
            // �������� ������������ �������.
            m_LocalTimer = new Timer(1.0f, true);

            // ���������� ���������� �� ������.
            LoadValues();
        }

        private void FixedUpdate()
        {
            // ���������� ���������� �������.
            m_LocalTimer.UpdateTimer();

            // ������� ������������ �������.
            if (m_LocalTimer.IsFinished) UpdateGameTime();
        }

        #endregion


        #region Private API

        /// <summary>
        /// �����, ����������� ����������� �����.
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
        /// �����, ������� ��� �������� ���������� �� ������.
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
        /// �������� ���� � ����������� ����������.
        /// </summary>
        /// <param name="score">���������� �����.</param>
        public void AddScore(int score)
        {
            m_TotalScore += score;
        }

        /// <summary>
        /// �������� �������� ����� �� ������� � ���� ��� ����� ������, ���� ������ �������� ����� - �������� ��� � ����������� ����������.
        /// </summary>
        /// <param name="score">�������� ����� �� �������.</param>
        public void CompareBestScore(int score)
        {
            if (score > m_BestScore)
            {
                m_BestScore = score;
            }       
        }

        /// <summary>
        /// �������� ������������ �������� � ����������� ����������.
        /// </summary>
        /// <param name="type">��� ���������.</param>
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
        /// �������� 1 �������� � ����������� ����������.
        /// </summary>
        public void AddKill()
        {
            m_TotalKills++;
        }

        /// <summary>
        /// ����� ���������� ���������� �������.
        /// </summary>
        public void AddCompletedLevel()
        {
            m_LevelsWon++;
        }

        /// <summary>
        /// ����� ���������� ����������� �������.
        /// </summary>
        public void AddLoseLevel()
        {
            m_LevelsLose++;
        }

        /// <summary>
        /// �����, ����������� ������� ����������.
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