using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// Класс контроллёра камеры слежения за объектом.
    /// </summary>
    public class CameraController : MonoBehaviour
    {

        #region Properties and Components

        /// <summary>
        /// Ссылка на камеру.
        /// </summary>
        [SerializeField] private Camera m_Camera;

        /// <summary>
        /// Ссылка на цель камеры.
        /// </summary>
        [SerializeField] private Transform m_Target;

        /// <summary>
        /// Линейная скорость интерполяции (скорость по прямой).
        /// </summary>
        [SerializeField] private float m_InterpolationLinear;

        /// <summary>
        /// Угловая скорость интерполяции (скорость вращения).
        /// </summary>
        [SerializeField] private float m_InterpolationAngular;

        /// <summary>
        /// Начальное смещение по оси Z.
        /// </summary>
        [SerializeField] private float m_CameraZOffset;

        /// <summary>
        /// Начальное смещение по направлению движения.
        /// </summary>
        [SerializeField] private float m_ForvardOffset;

        #endregion


        #region Unity Events

        private void FixedUpdate()
        {
            // Проверка на камеру и цель камеры.
            if (m_Camera == null || m_Target == null) return;

            // 1. Записываем текущее положение камеры.
            Vector2 cameraPosition = m_Camera.transform.position;
            // 2. Записываем целевое положение камеры + смещение в сторону вектора.
            Vector2 targetPosition = m_Target.position + m_Target.transform.up * m_ForvardOffset;
            // 3. Находим интерполированное значение нового положения камеры.
            Vector2 newCameraPosotion = Vector2.Lerp(cameraPosition, targetPosition, m_InterpolationLinear * Time.deltaTime);

            // 4. Задаём камере новую позицию
            m_Camera.transform.position = new Vector3(newCameraPosotion.x, newCameraPosotion.y, m_Camera.transform.position.z + m_CameraZOffset);

            // 5. Если камера должна повернуться, задаём ей вращение по кватерниону
            if (m_InterpolationAngular > 0)
            {
                m_Camera.transform.rotation = Quaternion.Slerp(m_Camera.transform.rotation, m_Target.rotation, m_InterpolationAngular * Time.deltaTime);
            }
        }

        #endregion


        #region Public API

        /// <summary>
        /// Метод, задающий камере новую цель слежения.
        /// </summary>
        /// <param name="newTarget">Новая Transform цели слежения.</param>
        public void SetTarget(Transform newTarget)
        {
            // Перезаписывается цель слежения камеры.
            m_Target = newTarget;
        }

        #endregion

    }
}