using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SpaceShooter
{
    /// <summary>
    /// Интерфейс, который необходимо добавить для создания условия прохождения уровня.
    /// </summary>
    public interface ILevelCondition
    {
        /// <summary>
        /// Возвращает true, если условие выполнено.
        /// </summary>
        bool isCompleted { get; }
    }

    /// <summary>
    /// Класс, проверяющий, все ли условия для завершения уровня выполнены.
    /// </summary>
    public class LevelController : Singleton<LevelController>
    {

        #region Properties and Components

        /// <summary>
        /// Время прохождения уровня.
        /// </summary>
        [SerializeField] private int m_ReferenceTime;

        /// <summary>
        /// Ссылка на время прохождения уровня.
        /// </summary>
        public int ReferenceTime => m_ReferenceTime;

        /// <summary>
        /// Событие, срабатывающее когда уровень завершён.
        /// </summary>
        [SerializeField] private UnityEvent m_EventLevelCompleted;

        /// <summary>
        /// Массив с условиями для завершения уровня.
        /// </summary>
        private ILevelCondition[] m_Conditions;

        /// <summary>
        /// Переменная, возвращающая true если уровень пройден.
        /// </summary>
        private bool m_IsLevelCompleted;

        /// <summary>
        /// Текущее пройденное время.
        /// </summary>
        private float m_LevelTime;

        /// <summary>
        /// Ссылка на текущее пройденное время.
        /// </summary>
        public float LevelTime => m_LevelTime;

        #endregion


        #region Unity Events

        private void Start()
        {
            // Заполняет массив с условиями дочерними объектами с интерфейсами ILevelCondition.
            m_Conditions = GetComponentsInChildren<ILevelCondition>();
        }

        private void FixedUpdate()
        {
            // Если уровень не завершён - прибавляет время.
            if (!m_IsLevelCompleted)
            {
                m_LevelTime += Time.fixedDeltaTime;
            }

            // Проверяет условия прохождения уровня.
            CheckLevelConditions();
        }

        #endregion


        #region Private API

        /// <summary>
        /// Метод, проверяющий, все ли условия для завершения уровня выполнены.
        /// </summary>
        private void CheckLevelConditions()
        {
            // Проверка на наличие массива с условиями и его заполненость.
            if (m_Conditions == null || m_Conditions.Length == 0) return;

            // Переменная, отображающая сколько условий выполнено.
            int numCompleted = 0;

            // Прохождение по всем условиям, считает кол-во выполненных условий.
            foreach (var condition in m_Conditions)
            {
                if (condition.isCompleted) numCompleted++;
            }

            // Если все условия выполнены.
            if (numCompleted == m_Conditions.Length)
            {
                // Уровен завершён.
                m_IsLevelCompleted = true;

                // Вызов событий по завершению уровня, проверка на null.
                m_EventLevelCompleted?.Invoke();

                // Вызов метода завершения уровня.
                LevelSequenceController.Instance?.FinishCurrentLevel(true);
            }
        }

        #endregion

    }
}