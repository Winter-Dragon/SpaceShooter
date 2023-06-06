using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// Базовый класс всех интерактивных игровых объектов на сцене.
    /// </summary>
    public abstract class Entity : MonoBehaviour
    {

        #region Properties and Components

        /// <summary>
        /// Название объекта для пользователя.
        /// </summary>
        [SerializeField] private string m_Nickname;

        /// <summary>
        /// Ссылка на имя объекта.
        /// </summary>
        public string Nickname => m_Nickname;

        #endregion

    }
}