using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    ///  ласс с условием дл€ прохождени€ уровн€ (кол-во набранных очков).
    /// </summary>
    public class LevelConditionScore : MonoBehaviour, ILevelCondition
    {

        #region Properties and Components

        /// <summary>
        /// Ќеобходимое кол-во очков дл€ завершени€ уровн€.
        /// </summary>
        [SerializeField] private int m_Score;

        /// <summary>
        /// ¬озвращает true, если условие достигнуто.
        /// </summary>
        private bool m_IsReached;

        /// <summary>
        /// ¬озвращает локальную переменную, отображающее выполнено ли условие в ILevelCondition.
        /// </summary>
        bool ILevelCondition.isCompleted
        {
            get
            {
                // ѕроверка на игрока.
                if (Player.Instance != null && Player.Instance != null)
                {
                    // ≈сли кол-ва очков достаточно дл€ завершени€ услови€, условие true.
                    if (Player.Instance.Score >= m_Score)
                    {
                        m_IsReached = true;
                    }
                }

                return m_IsReached;
            }
        }

        #endregion

    }
}