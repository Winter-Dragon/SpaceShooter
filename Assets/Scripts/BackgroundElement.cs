using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// Основной класс Background элемента. Создаёт паралакс эффект и задаёт скейлы текстуре.
    /// </summary>
    [RequireComponent(typeof(MeshRenderer))]
    public class BackgroundElement : MonoBehaviour
    {

        #region Properties and Components

        /// <summary>
        /// Сила параллакс эффекта.
        /// </summary>
        [Range(0.1f, 4.0f)]
        [SerializeField] private float m_ParallaxPower;

        /// <summary>
        /// Скейлы текстуры.
        /// </summary>
        [SerializeField] private float m_TextureScale;

        /// <summary>
        /// Ссылка на материал текстуры.
        /// </summary>
        private Material m_QuadMaterial;

        /// <summary>
        /// Начальная точка смещения.
        /// </summary>
        private Vector2 m_InitialOffset;

        #endregion


        #region UnityEvents

        private void Start()
        {
            // Получение ссылки на материал
            m_QuadMaterial = GetComponent<MeshRenderer>().material;

            // Задаётся случайное смещение в пределах окружности.
            m_InitialOffset = UnityEngine.Random.insideUnitCircle;

            // Материалу задаётся скейл из значения, записанного в Inspector.
            m_QuadMaterial.mainTextureScale = Vector2.one * m_TextureScale;
        }

        private void FixedUpdate()
        {
            // Создаётся вектор, задаётся начальное смещение.
            Vector2 offset = m_InitialOffset;

            // На векторы накладываются сила параллакс эффекта.
            offset.x += transform.position.x / transform.localScale.x / m_ParallaxPower;
            offset.y += transform.position.y / transform.localScale.y / m_ParallaxPower;

            // Смещение материала приравнивается к созданному вектору.
            m_QuadMaterial.mainTextureOffset = offset;
        }

        #endregion

    }
}