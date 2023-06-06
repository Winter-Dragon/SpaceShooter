using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace SpaceShooter
{
    /// <summary>
    ///  ласс, контролирующий отображение меню с общеигровой статистикой.
    /// </summary>
    public class UI_Controller_GameStatistics : MonoBehaviour
    {

        #region Properties and Components

        /// <summary>
        /// —сылка на меню главного меню.
        /// </summary>
        [SerializeField] private GameObject m_MainMenu;

        /// <summary>
        /// —сылка на текстовое поле общего количества очков.
        /// </summary>
        [SerializeField] private TextMeshProUGUI m_TotalScoreValueText;

        /// <summary>
        /// —сылка на текстовое поле лучших очков.
        /// </summary>
        [SerializeField] private TextMeshProUGUI m_BestScoreValueText;

        /// <summary>
        /// —сылка на текстовое поле уничтоженных астероидов.
        /// </summary>
        [SerializeField] private TextMeshProUGUI m_AsteroidsDestroyedValueText;

        /// <summary>
        /// —сылка на текстовое поле уничтоженных больших астероидов.
        /// </summary>
        [SerializeField] private TextMeshProUGUI m_BigAsteroidsDestroyedValueText;

        /// <summary>
        /// —сылка на текстовое поле уничтоженных средних астероидов.
        /// </summary>
        [SerializeField] private TextMeshProUGUI m_MediumAsteroidsDestroyedValueText;

        /// <summary>
        /// —сылка на текстовое поле уничтоженных маленьких астероидов.
        /// </summary>
        [SerializeField] private TextMeshProUGUI m_SmallAsteroidsDestroyedValueText;

        /// <summary>
        /// —сылка на текстовое поле общего количества убийств.
        /// </summary>
        [SerializeField] private TextMeshProUGUI m_TotalKillsValueText;

        /// <summary>
        /// —сылка на текстовое поле количества выигранных уровней.
        /// </summary>
        [SerializeField] private TextMeshProUGUI m_LevelsVonValueText;

        /// <summary>
        /// —сылка на текстовое поле количества проигранных уровней.
        /// </summary>
        [SerializeField] private TextMeshProUGUI m_LevelsLoseValueText;

        /// <summary>
        /// —сылка на текстовое поле игрового времени.
        /// </summary>
        [SerializeField] private TextMeshProUGUI m_GameTimeValueText;

        /// <summary>
        /// Ћокальна€ ссылка на игровую статистику.
        /// </summary>
        private GameStatistics m_GameStatistics;

        /// <summary>
        /// Ћокальна€ переменна€ секунд.
        /// </summary>
        private int m_LocalTimeSeconds;

        #endregion


        #region Unity Events

        private void Start()
        {
            m_GameStatistics = GameStatistics.Instance;

            UpdateInfoState();
        }

        private void FixedUpdate()
        {
            UpdateTimeTextState();
        }

        #endregion


        #region Private API

        /// <summary>
        /// ћетод, обновл€ющий основные текстовые пол€.
        /// </summary>
        private void UpdateInfoState()
        {
            m_TotalScoreValueText.text = m_GameStatistics.TotalScore.ToString();
            m_BestScoreValueText.text = m_GameStatistics.BestScore.ToString();
            m_TotalKillsValueText.text = m_GameStatistics.TotalKills.ToString();
            m_AsteroidsDestroyedValueText.text = m_GameStatistics.AsteroidsDestroyed.ToString();
            m_BigAsteroidsDestroyedValueText.text = m_GameStatistics.BigAsteroidsDestroyed.ToString();
            m_MediumAsteroidsDestroyedValueText.text = m_GameStatistics.MediumAsteroidsDestroyed.ToString();
            m_SmallAsteroidsDestroyedValueText.text = m_GameStatistics.SmallAsteroidsDestroyed.ToString();
            m_LevelsVonValueText.text = m_GameStatistics.LevelsWon.ToString();
            m_LevelsLoseValueText.text = m_GameStatistics.LevelsLose.ToString();
        }

        /// <summary>
        /// ћетод, обновл€ющий текстовое поле времени.
        /// !! Ќе разворачивать :)
        /// </summary>
        private void UpdateTimeTextState()
        {
            int currentSeconds = m_GameStatistics.GameTimeSeconds;
            int currentMinuts = m_GameStatistics.GameTimeMinuts;
            int currentHours = m_GameStatistics.GameTimeHours;
            int currentDays = m_GameStatistics.GameTimeDays;

            if (m_LocalTimeSeconds == currentSeconds) return;
            m_LocalTimeSeconds = currentSeconds;

            if (currentSeconds < 10)
            {
                if (currentMinuts < 10)
                {
                    if(currentHours < 10)
                    {
                        if (currentDays > 1)
                        {
                            m_GameTimeValueText.text = currentDays.ToString() + " : " + "0" + currentHours.ToString() + " : " + "0" + currentMinuts.ToString() + " : " + "0" + currentSeconds.ToString();
                        }
                        else
                        {
                            m_GameTimeValueText.text = "0 : " + "0" + currentHours.ToString() + " : " + "0" + currentMinuts.ToString() + " : " + "0" + currentSeconds.ToString();
                        }
                    }
                    else
                    {
                        if (currentDays > 1)
                        {
                            m_GameTimeValueText.text = currentDays.ToString() + " : " + currentHours.ToString() + " : " + "0" + currentMinuts.ToString() + " : " + "0" + currentSeconds.ToString();
                        }
                        else
                        {
                            m_GameTimeValueText.text = "0 : " + currentHours.ToString() + " : " + "0" + currentMinuts.ToString() + " : " + "0" + currentSeconds.ToString();
                        }
                    }
                }
                else
                {
                    if (currentHours < 10)
                    {
                        if (currentDays > 1)
                        {
                            m_GameTimeValueText.text = currentDays.ToString() + " : " + "0" + currentHours.ToString() + " : " + currentMinuts.ToString() + " : " + "0" + currentSeconds.ToString();
                        }
                        else
                        {
                            m_GameTimeValueText.text = "0 : " + "0" + currentHours.ToString() + " : " + currentMinuts.ToString() + " : " + "0" + currentSeconds.ToString();
                        }
                    }
                    else
                    {
                        if (currentDays > 1)
                        {
                            m_GameTimeValueText.text = currentDays.ToString() + " : " + currentHours.ToString() + " : " + currentMinuts.ToString() + " : " + "0" + currentSeconds.ToString();
                        }
                        else
                        {
                            m_GameTimeValueText.text = "0 : " + currentHours.ToString() + " : " + currentMinuts.ToString() + " : " + "0" + currentSeconds.ToString();
                        }
                    }
                }
            }
            else
            {
                if (currentMinuts < 10)
                {
                    if (currentHours < 10)
                    {
                        if (currentDays > 1)
                        {
                            m_GameTimeValueText.text = currentDays.ToString() + " : " + "0" + currentHours.ToString() + " : " + "0" + currentMinuts.ToString() + " : " + currentSeconds.ToString();
                        }
                        else
                        {
                            m_GameTimeValueText.text = "0 : " + "0" + currentHours.ToString() + " : " + "0" + currentMinuts.ToString() + " : " + currentSeconds.ToString();
                        }
                    }
                    else
                    {
                        if (currentDays > 1)
                        {
                            m_GameTimeValueText.text = currentDays.ToString() + " : " + currentHours.ToString() + " : " + "0" + currentMinuts.ToString() + " : " + currentSeconds.ToString();
                        }
                        else
                        {
                            m_GameTimeValueText.text = "0 : " + currentHours.ToString() + " : " + "0" + currentMinuts.ToString() + " : " + currentSeconds.ToString();
                        }
                    }
                }
                else
                {
                    if (currentHours < 10)
                    {
                        if (currentDays > 1)
                        {
                            m_GameTimeValueText.text = currentDays.ToString() + " : " + "0" + currentHours.ToString() + " : " + currentMinuts.ToString() + " : " + currentSeconds.ToString();
                        }
                        else
                        {
                            m_GameTimeValueText.text = "0 : " + "0" + currentHours.ToString() + " : " + currentMinuts.ToString() + " : " + currentSeconds.ToString();
                        }
                    }
                    else
                    {
                        if (currentDays > 1)
                        {
                            m_GameTimeValueText.text = currentDays.ToString() + " : " + currentHours.ToString() + " : " + currentMinuts.ToString() + " : " + currentSeconds.ToString();
                        }
                        else
                        {
                            m_GameTimeValueText.text = "0 : " + currentHours.ToString() + " : " + currentMinuts.ToString() + " : " + currentSeconds.ToString();
                        }
                    }
                }
            }
        }

        #endregion


        #region Public API

        /// <summary>
        /// ƒействи€ при нажатии кнопки назад.
        /// </summary>
        public void ClickButtonBack()
        {
            m_MainMenu.SetActive(true);
            gameObject.SetActive(false);
        }

        #endregion

    }
}