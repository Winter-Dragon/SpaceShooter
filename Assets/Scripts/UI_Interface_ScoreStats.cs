using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SpaceShooter
{
    /// <summary>
    /// Класс, обновляющий текстовое поле Score.
    /// </summary>
    public class UI_Interface_ScoreStats : Singleton<UI_Interface_ScoreStats>
    {

        #region Properties and Components

        /// <summary>
        /// Ссылка на класс TMP.
        /// </summary>
        private TextMeshProUGUI m_ScoreText;

        /// <summary>
        /// Переменная, хранящая в себе последнее кол-во очков игрока.
        /// </summary>
        private int m_LastScore;

        #endregion


        #region Unity Events

        private void Start()
        {
            // Задаём ссылку на TMP с текущего объекта.
            m_ScoreText = GetComponent<TextMeshProUGUI>();
        }

        private void FixedUpdate()
        {
            // Обновить кол-во очков.
            UpdateScore();
        }

        #endregion


        #region Private API

        /// <summary>
        /// Метод, обновляющий текущее значение очков и меняющий отображение в интерфейсе.
        /// </summary>
        private void UpdateScore()
        {
            // Проверка на наличие игрока на сцене.
            if (Player.Instance == null) return;

            // Записывается локальная переменная кол-ва очков у игрока.
            int currentScore = Player.Instance.Score;

            // Если кол-во очков обновилось, обновляет интерфейс с очками.
            if (m_LastScore != currentScore)
            {
                m_LastScore = currentScore;

                m_ScoreText.text = m_LastScore.ToString();
            }
        }

        #endregion

    }
}