using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace SpaceShooter
{
    /// <summary>
    /// Класс, задающий область окружности вокруг элемента.
    /// </summary>
    public class CircleArea : MonoBehaviour
    {

        #region Properties and Components

        /// <summary>
        /// Радиус окружности.
        /// </summary>
        [SerializeField] private float m_Radius;

        /// <summary>
        /// Ссылка на радиус окружности.
        /// </summary>
        public float Radius => m_Radius;

        #endregion


        #region Unity Events

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            // Нарисовать окружность с заданным радиусом.
            Handles.color = Color.green;
            Handles.DrawWireDisc(transform.position, transform.forward, m_Radius);
        }
#endif

        #endregion


        #region Public API

        /// <summary>
        /// Метод, возвращающий случайные координаты точки в окружности.
        /// </summary>
        /// <returns></returns>
        public Vector2 GetRandomInsideZone()
        {
            return (Vector2)transform.position + (Vector2)UnityEngine.Random.insideUnitSphere * m_Radius;
        }

        #endregion

    }
}