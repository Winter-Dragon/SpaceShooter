using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// Класс, обрабатывающий урон от столкновения с коллизией другого объекта, зависящий от скорости.
    /// </summary>
    public class CollisionDamageApplicator : MonoBehaviour
    {

        #region Properties and Components

        /// <summary>
        /// Тэг, при столкновении с которым к объекту не будет применяться урон.
        /// </summary>
        public static string IgnoreTag = "WorldBoundary";

        /// <summary>
        /// Модификатор урона, зависящий от скорости.
        /// </summary>
        [SerializeField] private float m_VelocityDamageModifier;

        /// <summary>
        /// Постоянное значение урона.
        /// </summary>
        [SerializeField] private float m_DamageConstant;

        /// <summary>
        /// Локально сохраняется текущий Destructible объекта.
        /// </summary>
        private Destructible сurrentObjectDestructible;

        #endregion


        #region Unity Events

        private void Start()
        {
            // Назначает ссылку Dest текущего объекта.
            сurrentObjectDestructible = transform.root.GetComponent<Destructible>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            // Не выполнять, если применён тэг WorldBoundary
            if (collision.transform.tag == IgnoreTag) return;

            // Если на текущем объекте есть Dest.
            if (сurrentObjectDestructible != null)
            {
                // Берём ссылку на Dest объекта столкновения.
                Destructible collisionDestructible = collision.transform.root.GetComponent<Destructible>();

                // Если объект тоже Dest.
                if (collisionDestructible != null)
                {
                    // Если объект не одной команды - нанести урон.
                    if (collisionDestructible.TeamID != сurrentObjectDestructible.TeamID)
                    {
                        // Урон = константа урона + ( модификатор скорости * длина вектора скорости )
                        сurrentObjectDestructible.ApplyDamage((int)m_DamageConstant + (int)(m_VelocityDamageModifier * collision.relativeVelocity.magnitude));
                    }
                }
                // Если объект не Dest.
                else
                {
                    // Урон = константа урона + ( модификатор скорости * длина вектора скорости )
                    сurrentObjectDestructible.ApplyDamage((int)m_DamageConstant + (int)(m_VelocityDamageModifier * collision.relativeVelocity.magnitude));
                }
            }
        }

        #endregion
    }
}