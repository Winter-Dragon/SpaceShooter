using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class AIPointPatrol : MonoBehaviour
    {

        #region Properties and Components

        /// <summary>
        /// Радиус зоны патрулирования.
        /// </summary>
        [SerializeField] private float m_Radius;

        /// <summary>
        /// Ссылка на радиус зоны патрулирования.
        /// </summary>
        public float Radius => m_Radius;

#if UNITY_EDITOR
        /// <summary>
        /// Цвет для выделения радиуса сферы, отображаемый в эдиторе.
        /// </summary>
        private static readonly Color GizmoColor = new Color(1, 0, 0, 0.3f);
#endif

        #endregion


        #region Unity Events

#if UNITY_EDITOR
        /// <summary>
        /// Метод, назначающий цвет и рисующий сферу для эдитора.
        /// </summary>
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = GizmoColor;
            Gizmos.DrawSphere(transform.position, m_Radius);
        }
#endif

        #endregion

    }
}