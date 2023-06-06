using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class GravityWell : MonoBehaviour
    {

        #region Properties and Components

        /// <summary>
        /// Сила притяжения.
        /// </summary>
        [SerializeField] private float m_Force;

        /// <summary>
        /// Радиус притяжения.
        /// </summary>
        [SerializeField] private float m_Radius;

        #endregion


        #region Unity Events

        private void OnTriggerStay2D(Collider2D collision)
        {
            // Проверка, есть ли у объекта Rigidbody.
            if (collision.attachedRigidbody == null) return;

            // Получить вектор направления от столкнувшегося объекта до себя (от 2 к 1).
            Vector2 direction = transform.position - collision.transform.position;

            // Получаем длину вектора = расстояние до столкнувшегося объекта.
            float distance = direction.magnitude;

            // Если расстояние до объекта < радиуса притяжения.
            if (distance < m_Radius)
            {
                // Создать вектор силы притяжения (Чем ближе объект, тем сильнее притяжение) = нормализованную дистанцию * силу притяжения * ( радиус притяжения / длина вектора до объекта ).
                Vector2 force = direction.normalized * m_Force * (m_Radius / distance);

                // Добавляем вектор силы столкнувшемуся объекту.
                collision.attachedRigidbody.AddForce(force, ForceMode2D.Force);
            }
        }

#if UNITY_EDITOR
        /// <summary>
        /// Метод, автоматически задающий радиус коллайдеру от радиуса притяжения.
        /// </summary>
        private void OnValidate()
        {
            GetComponent<CircleCollider2D>().radius = m_Radius;
        }
#endif

#endregion

    }
}