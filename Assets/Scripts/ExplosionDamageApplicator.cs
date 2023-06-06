using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// Класс, наносящий урон объектам Destructible от взыва.
    /// </summary>
    public class ExplosionDamageApplicator : MonoBehaviour
    {

        #region Properties and Components

        /// <summary>
        /// Объект, выпустивший взрывной снаряд.
        /// </summary>
        private Destructible m_Parent;

        /// <summary>
        /// Объект, в который уже попал снаряд.
        /// </summary>
        private Destructible m_Target;

        /// <summary>
        /// Начальный урон от снаряда.
        /// </summary>
        private int m_ExplosionDamage;

        /// <summary>
        /// Переменная, отображающая, настроен ли взрыв.
        /// </summary>
        private bool m_TuneCheck;

        /// <summary>
        /// Переменная, отображающая, что выпустил снаряд игрок.
        /// </summary>
        private bool isPlayer;

        #endregion


        #region Unity Events

        private void OnTriggerEnter2D(Collider2D collision)
        {
            // Если взрыв не настроен - выйти из метода.
            if (!m_TuneCheck) return;

            // Назначаем Destructible снолкнувшегося объекта.
            Destructible destructible = collision.transform.root.GetComponent<Destructible>();

            // Если у родителя нет Destructible,
            // Если выпустил снаряд и снолкнулся с ним тот, кто выстрелил,
            // Если объект  в коллайдере имеет ту же команду, что и выстреливший,
            // Если объект уже получил урон от снаряда - не продолжать метод.
            if (destructible == null || destructible == m_Parent || destructible.TeamID == m_Parent.TeamID || destructible == m_Target) return;

            // Нанести урон объекту.
            destructible.ApplyDamage(m_ExplosionDamage);

            //Если выстреливший корабль - игрок.
            if (isPlayer)
            {
                // Если объект уничтожен.
                if (destructible.CurrentHitPoints <= 0)
                {
                    // Начислить очки.
                    Player.Instance.AddScore(destructible.ScoreValue);
                    GameStatistics.Instance?.AddScore(destructible.ScoreValue);

                    // Берётся ссылка на корабль.
                    SpaceShip ship = destructible.GetComponent<SpaceShip>();

                    // Если попадание в корабль - добавить убийство.
                    if (ship != null)
                    {
                        Player.Instance.AddKill();
                        GameStatistics.Instance?.AddKill();
                    }

                    // Берётся ссылка на астероид.
                    Asteroid asteroid = destructible.GetComponent<Asteroid>();

                    // Если попадание в астероид - записывает в общеигровую статистику.
                    if (asteroid != null)
                    {
                        GameStatistics.Instance?.AsteroidDestroyed(asteroid.AsteroidSize);
                    }
                }
            }
        }

        #endregion


        #region Public API

        /// <summary>
        /// Метод настройки взрыва от снаряда.
        /// </summary>
        /// <param name="parent">Destructible выстрелившего объекта.</param>
        /// <param name="target">Destructible объекта, в которого попал снаряд.</param>
        /// <param name="position">Позиция создания взрыва.</param>
        /// <param name="damage">Урон от взрыва.</param>
        public void TuneExplosion(Destructible parent, Destructible target, Vector3 position, int damage)
        {
            m_Parent = parent;
            m_Target = target;
            transform.position = position;
            m_ExplosionDamage = damage;
            m_TuneCheck = true;

            // Если выстрелил игрок - делает пометку.
            if (m_Parent == Player.Instance.ActiveShip) isPlayer = true;
        }

        #endregion

    }
}