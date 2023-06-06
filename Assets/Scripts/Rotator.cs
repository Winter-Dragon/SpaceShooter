using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// Класс, задающий вращение объекту по выбранному вектору.
    /// </summary>
    public class Rotator : MonoBehaviour
    {

        #region Properties and Components

        /// <summary>
        /// Вектор вращения объекта.
        /// </summary>
        [SerializeField] private Vector3 m_speed;

        /// <summary>
        /// Ссылка на объект вращения.
        /// </summary>
        private Transform m_Transform;

        #endregion


        #region Unity Events

        private void Start()
        {
            // Задаётся ссылка объекта вращения на текущую Transform объекта.
            m_Transform = GetComponent<Transform>();
        }

        private void FixedUpdate()
        {
            // Вращает объект по вектору вращения, назначенному в инспекторе.
            m_Transform.transform.Rotate(m_speed * Time.deltaTime);
        }

        #endregion

    }
}