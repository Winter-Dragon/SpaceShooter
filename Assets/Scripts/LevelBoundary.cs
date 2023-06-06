using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// Класс, создающий границы сцены.
    /// </summary>
    public class LevelBoundary : Singleton<LevelBoundary>
    {

        #region Properties and Components

        /// <summary>
        /// Радиус границы уровня.
        /// </summary>
        [SerializeField] private float m_Radius;

        /// <summary>
        /// Ссылка на радиус границы уровня.
        /// </summary>
        public float Radius => m_Radius;

        /// <summary>
        /// Режимы ограничения уновня: лимит или телепорт.
        /// </summary>
        public enum Mode
        {
            Limit,
            Teleport
        }

        /// <summary>
        /// Выбирается режим ограничения уровня через инспектор.
        /// </summary>
        [SerializeField] private Mode m_LimitMode;

        /// <summary>
        /// Ссылка на режим ограничения уровня.
        /// </summary>
        public Mode LimitMode => m_LimitMode;

        #endregion


        #region Unity Events

        // В эдиторе рисует радиус ограничения уровня
#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            UnityEditor.Handles.color = Color.red;
            UnityEditor.Handles.DrawWireDisc(transform.position, transform.forward, m_Radius);
        }
#endif

#endregion

    }
}