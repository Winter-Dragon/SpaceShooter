using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// Ограничитель позиции. Работает в связке со скриптом LevelBoundary, если такой имеется на сцене.
    /// Ставится на объект, который необходимо ограничить.
    /// </summary>
    public class LevelBoundaryLimiter : MonoBehaviour
    {

        #region Unity Events

        private void FixedUpdate()
        {
            // Проверка, есть ли на сцене граница уровня.
            if (LevelBoundary.Instance == null) return;

            // Создаются локальные переменные текущей границы и её радиуса.
            var levelBoundary = LevelBoundary.Instance;
            var radius = levelBoundary.Radius;

            // Действия, когда объект заходит за границу.
            if (transform.position.magnitude > radius)
            {
                switch (levelBoundary.LimitMode)
                {
                    // Ограничение: объект остаётся на текущем месте.
                    case LevelBoundary.Mode.Limit:

                        transform.position = transform.position.normalized * radius;
                        break;

                    // Телепортация: объект телепортируется на противоположную границу.
                    case LevelBoundary.Mode.Teleport:

                        transform.position = -transform.position.normalized * radius;
                        break;
                }
            }
        }

        #endregion

    }
}