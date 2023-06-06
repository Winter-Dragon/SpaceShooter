using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace SpaceShooter
{
    /// <summary>
    /// Условие, при котором объект должен достичь определённой позиции.
    /// </summary>
    [RequireComponent(typeof(CircleCollider2D))]
    public class LevelConditionPosition : MonoBehaviour, ILevelCondition
    {

        #region Properties and Components

        /// <summary>
        /// Радиус круга.
        /// </summary>
        [SerializeField] private float m_Radius;

        /// <summary>
        /// Локальная переменная, отображающая, завершено ли условие.
        /// </summary>
        private bool m_IsCompleted;

        /// <summary>
        /// Ссылка на выполнение условия.
        /// </summary>
        bool ILevelCondition.isCompleted => m_IsCompleted;

        #endregion


        #region Unity Events

#if UNITY_EDITOR
        /// <summary>
        /// Метод, срабатывающий при изменении компонентов.
        /// </summary>
        private void OnValidate()
        {
            GetComponent<CircleCollider2D>().radius = m_Radius;
        }

        private void OnDrawGizmos()
        {
            Handles.color = new Color(0, 0, 110, 0.05f);
            Handles.DrawSolidDisc(transform.position, transform.forward, m_Radius);
        }
#endif

        private void OnTriggerEnter2D(Collider2D collision)
        {
            // Проверка на корабль игрока.
            SpaceShip ship = collision.transform.root.GetComponent<SpaceShip>();
            if (ship == null || ship != Player.Instance.ActiveShip) return;

            
            // Условие выполнено.
            m_IsCompleted = true;
        }

        #endregion

    }
}