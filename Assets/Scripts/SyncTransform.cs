using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// Скрипт, синхронизирующий Transform текущего объекта с выбранным объектом.
    /// </summary>
    public class SyncTransform : MonoBehaviour
    {

        #region Properties and Components

        /// <summary>
        /// Ссылка на объект, с которым нужно синхронизировать текущий Transform.
        /// </summary>
        [SerializeField] private Transform m_Target;

        #endregion


        #region Unity Events

        private void FixedUpdate()
        {
            // Задаёт текущее положение объекта позициям объекта слежения по x и y.
            transform.position = new Vector3(m_Target.position.x, m_Target.position.y, transform.position.z);
        }

        #endregion

    }
}