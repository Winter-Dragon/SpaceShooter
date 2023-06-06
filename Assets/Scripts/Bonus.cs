using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// Основной класс бонусов.
    /// </summary>
    [RequireComponent(typeof(BoxCollider2D))]
    public abstract class Bonus : MonoBehaviour
    {

        #region Unity Events

        private void OnTriggerEnter2D(Collider2D collision)
        {
            // Если столкнулся SpaceShip - записывает его в переменную ship.
            SpaceShip ship = collision.transform.root.GetComponent<SpaceShip>();

            // Проверка на null и игрока.
            if (ship == null || Player.Instance.ActiveShip == false) return;

            OnPickedUp(ship);

            // Уничтожить бонус.
            Destroy(gameObject);
        }

        #endregion


        #region Protected API

        /// <summary>
        /// Метод, срабатывающий когда корабль подбирает бонус.
        /// </summary>
        /// <param name="ship">Корабль, подобравший бонус.</param>
        protected abstract void OnPickedUp(SpaceShip ship);

        #endregion

    }
}