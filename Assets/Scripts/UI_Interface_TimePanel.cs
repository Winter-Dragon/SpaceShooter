using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SpaceShooter
{
    /// <summary>
    /// Класс, отвечающий за отображение панели со временем в интерфейсе. Хранит множитель очков за уровень.
    /// </summary>
    public class UI_Interface_TimePanel : Singleton<UI_Interface_TimePanel>
    {

        #region Properties and Components

        /// <summary>
        /// Ссылка на активную часть панели времени X3.
        /// </summary>
        [SerializeField] private Image m_TimePanelScoreX3;

        /// <summary>
        /// Ссылка на активную часть панели времени X2.
        /// </summary>
        [SerializeField] private Image m_TimePanelScoreX2;

        /// <summary>
        /// Ссылка на строку множителя очков.
        /// </summary>
        [SerializeField] private TextMeshProUGUI m_ScoreMultiplierText;

        /// <summary>
        /// Время уровня, за которое даются бонусные очки.
        /// </summary>
        private float m_levelTIme;

        /// <summary>
        /// Локальный таймер.
        /// </summary>
        private float m_LocalTimer;

        /// <summary>
        /// Переменная, отображающая, закончился ли таймер.
        /// </summary>
        private bool m_TimeIsOver;

        /// <summary>
        /// Значение множителя очков.
        /// </summary>
        public static int ScoreMultiplier;

        #endregion


        #region Unity Events

        private void Start()
        {
            // Задаётся время уровня.
            m_levelTIme = LevelController.Instance.ReferenceTime;
            // Полностью закрашивается строка со временем.
            m_TimePanelScoreX2.fillAmount = 1;
            m_TimePanelScoreX3.fillAmount = 1;
            // Обнуление таймера.
            m_LocalTimer = m_levelTIme;
            // Множитель очков х3.
            ScoreMultiplier = 3;
        }

        private void FixedUpdate()
        {
            if (!m_TimeIsOver)
            {
                UpdateTimePanel();
                UpdateScoreMultiplierPanel();
            }
        }

        #endregion


        #region Private API

        /// <summary>
        /// Метод, обновляющий строку со временем.
        /// </summary>
        private void UpdateTimePanel()
        {
            m_LocalTimer -= Time.fixedDeltaTime;
            
            float fillPanelX3 = (m_LocalTimer - (m_levelTIme / 2)) / (m_levelTIme / 2);

            // Действия когда не прошла половина уровня.
            if (fillPanelX3 >= 0)
            {
                m_TimePanelScoreX3.fillAmount = fillPanelX3;
            }
            // Когда прошла половина уровня
            else
            {
                m_TimePanelScoreX3.fillAmount = 0;
                ScoreMultiplier = 2;

                float fillPanelX2 = m_LocalTimer / (m_levelTIme / 2);

                if (fillPanelX2 >= 0)
                {
                    m_TimePanelScoreX2.fillAmount = fillPanelX2;
                }
                // Когда время закончилось
                else
                {
                    m_TimePanelScoreX2.fillAmount = 0;

                    m_TimeIsOver = true;
                    ScoreMultiplier = 1;
                }
            }
        }

        /// <summary>
        /// Метод, меняющий отображения текстовой переменной множителя очков.
        /// </summary>
        private void UpdateScoreMultiplierPanel()
        {
            switch (ScoreMultiplier)
            {
                case 3:
                    m_ScoreMultiplierText.text = "x3";
                    break;

                case 2:
                    m_ScoreMultiplierText.text = "x2";
                    break;

                case 1:
                    m_ScoreMultiplierText.gameObject.SetActive(false);
                    break;
            }
        }

        #endregion

    }
}