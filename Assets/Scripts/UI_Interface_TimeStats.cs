using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace SpaceShooter
{
    /// <summary>
    /// Класс, обновляющий текстовое поле Time в интерфейсе.
    /// </summary>
    public class UI_Interface_TimeStats : Singleton<UI_Interface_TimeStats>
    {

        #region Properties and Components

        /// <summary>
        /// Ссылка на текстовое поле времени.
        /// </summary>
        private TextMeshProUGUI m_TimeText;

        /// <summary>
        /// Ссылка на строку со временем.
        /// </summary>
        public string TimeText => m_TimeText.text;

        /// <summary>
        /// Сохранённое прошлое значение секунд.
        /// </summary>
        private int m_LastSeconds;

        #endregion


        #region Unity Events

        private void Start()
        {
            // Находим текстовое поле на объекте.
            m_TimeText = GetComponent<TextMeshProUGUI>();
        }

        private void FixedUpdate()
        {
            // Обновляем текстовое поле.
            UpdateScore();
        }

        #endregion


        #region Private API

        /// <summary>
        /// Метод, обновляющий интерфекс времени в игре.
        /// </summary>
        private void UpdateScore()
        {
            // Проверка на наличие LeveController на сцене.
            if (LevelController.Instance == null) return;

            // Записывается локальная переменная пройденных секунд уровня.
            int currentSeconds = (int) LevelController.Instance.LevelTime;

            // Выход из метода, если секунды не обновились.
            if (m_LastSeconds == currentSeconds) return;
            m_LastSeconds = currentSeconds;

            // Записывает нормализованное значение времени в строку.
            m_TimeText.text = GetNormalizedTime(currentSeconds);
        }

        /// <summary>
        /// Метод, возвращающий нормализованное значение времени из секунд в формате строки.
        /// </summary>
        /// <param name="seconds">Значение времени в секундах.</param>
        /// <returns>Возвращает нормализованное значеное времени в формате 00:00.</returns>
        private string GetNormalizedTime(float seconds)
        {
            string normalizedTime;

            // Записывается локальная переменная пройденных минут уровня.
            int currentMinuts = (int) (seconds / 60);

            // Если минута не прошла.
            if (currentMinuts == 0)
            {
                // Если секунды состоят из 1 цифры.
                if (seconds < 10)
                {
                    normalizedTime = "00 : 0" + seconds.ToString();
                }
                // Если более одной цифры.
                else
                {
                    normalizedTime = "00 : " + seconds.ToString();
                }
            }
            // Если минута прошла.
            else
            {
                // Преобразовать секунды.
                seconds = seconds - (currentMinuts * 60);

                // Если минуты состоят из 1 цифры.
                if (currentMinuts < 10)
                {
                    // Если секунды состоят из 1 цифры.
                    if (seconds < 10)
                    {
                        normalizedTime = "0" + currentMinuts.ToString() + " : 0" + seconds.ToString();
                    }
                    // Если более одной цифры в секундах.
                    else
                    {
                        normalizedTime = "0" + currentMinuts.ToString() + " : " + seconds.ToString();
                    }
                }
                // Если более 1 цифры в минутах.
                else
                {
                    // Если секунды состоят из 1 цифры.
                    if (seconds < 10)
                    {
                        normalizedTime = currentMinuts.ToString() + " : 0" + seconds.ToString();
                    }
                    // Если более одной цифры в секундах.
                    else
                    {
                        normalizedTime = currentMinuts.ToString() + " : " + seconds.ToString();
                    }
                }
            }

            // Возвращает нормализованное время.
            return normalizedTime;
        }

        #endregion

    }
}