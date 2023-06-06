using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class Timer
    {

        #region Properties and Components

        /// <summary>
        /// Время таймера.
        /// </summary>
        private float m_CurrentTime;
        
        /// <summary>
        /// Зацикленность таймера.
        /// </summary>
        private bool m_Loop;

        /// <summary>
        /// Кэшированное время таймера.
        /// </summary>
        private float m_CashTime;

        /// <summary>
        /// Количество итераций таймера.
        /// </summary>
        private int m_AmountIterations;

        /// <summary>
        /// Публичное свойство, отображающее, сколько раз таймер завершился.
        /// </summary>
        public int AmountIterations => m_AmountIterations;

        /// <summary>
        /// Переменная, фиксирующая, завершилась ли итерация.
        /// </summary>
        private bool m_CheckIteration;

        /// <summary>
        /// Свойство, возвращающее true если таймер завершился.
        /// </summary>
        public bool IsFinished => m_CurrentTime <= 0;

        /// <summary>
        /// Конструктор класса Timer, запускающий таймер после создания.
        /// </summary>
        /// <param name="startTime">Время таймера.</param>
        /// <param name="loop">Повторение таймера.</param>
        public Timer(float startTime, bool loop)
        {
            Start(startTime, loop);
        }

        #endregion


        #region Public API

        /// <summary>
        /// Метод, создающий таймер.
        /// </summary>
        /// <param name="startTime">Время таймера.</param>
        /// <param name="loop">Повторение таймера.</param>
        public void Start(float startTime, bool loop)
        {
            m_CurrentTime = startTime;
            m_CashTime = startTime;
            m_Loop = loop;
        }

        /// <summary>
        /// Метод обновления таймера.
        /// </summary>
        /// <param name="deltaTime">Пройденное время.</param>
        public void UpdateTimer()
        {
            // Ход циклического таймера.
            if (m_Loop)
            {
                m_CurrentTime -= Time.fixedDeltaTime;

                // Если таймер завершился.
                if (m_CurrentTime <= 0)
                {
                    // Условие, срабатывающее только во время второй итерации
                    if (m_CheckIteration)
                    {
                        // Приравниваем время идущего таймера к кэшированному времени.
                        m_CurrentTime = m_CashTime;
                        m_CheckIteration = false;
                        return;
                    }

                    // Ведёт счёт итераций.
                    m_AmountIterations++;
                    m_CheckIteration = true;
                }
            }
            // Ход нециклического таймера.
            else
            {
                // Проверка, не завершился ли таймер
                if (m_CurrentTime <= 0 && m_CheckIteration == false)
                {
                    m_AmountIterations++;
                    m_CheckIteration = true;
                    return;
                }

                // Ход таймера.
                m_CurrentTime -= Time.fixedDeltaTime;
            }
        }

        /// <summary>
        /// Метод, перезапускающий таймер.
        /// </summary>
        public void RestartTimer()
        {
            m_CurrentTime = m_CashTime;
            m_CheckIteration = false;
        }

        #endregion

    }

}