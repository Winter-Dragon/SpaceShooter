using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class Asteroid : Destructible
    {

        #region Properties and Components

        /// <summary>
        /// Ссылка на виды астероидов: большой, средний и малеький.
        /// </summary>
        public enum AsteroidType
        {
            Big,
            Medium,
            Small
        }

        /// <summary>
        /// Выбор размера астероида.
        /// </summary>
        [SerializeField] private AsteroidType m_AsteroidSize;

        // Префаб астероида.
        [SerializeField] private Asteroid m_AsteroidPrefab;

        /// <summary>
        /// Ссылка на размер текущего астероида.
        /// </summary>
        public AsteroidType AsteroidSize => m_AsteroidSize;

        #endregion


        #region Unity Events

        override protected void Start()
        {
            // Задаётся HP.
            base.Start();

            // Меняется размер.
            ChangeAsteroidSize();

            // Подписываемся на событие смерти астероида, вызываем метод OnAsteroidDeath.
            EventsOnDeath.AddListener(OnAsteroidDeath);

        }

        #endregion


        #region Private API

        /// <summary>
        /// Метод, срабатывающий при уничтожении астероида.
        /// </summary>
        private void OnAsteroidDeath()
        {
            // Если астероид маленький - выйти из метода.
            if (m_AsteroidSize == AsteroidType.Small) return;

            // Создать 2 астероида.
            for (int i = -1; i < 2; i += 2)
            {
                Asteroid asteroid = Instantiate(m_AsteroidPrefab);

                // Задаётся позиция и размер на 1 меньше чем был.
                asteroid.transform.position = transform.position;
                asteroid.SetAsteroidType((int)m_AsteroidSize + 1);
                
                // Записать ссылку на Rigidbody астероида.
                Rigidbody2D asteroidRigidbody = asteroid.GetComponent<Rigidbody2D>();

                // Задать скорость от -1 до +1 созданным астероидам.
                asteroidRigidbody.velocity = gameObject.GetComponent<Rigidbody2D>().velocity * i;
            }

        }

        #endregion


        #region PublicAPI

        /// <summary>
        /// Метод, задающий тип астероида.
        /// </summary>
        /// <param name="type">0 - Small, 1 - Medium, 2 - Big.</param>
        public void SetAsteroidType(int type)
        {
            // Защита от невозможного размера.
            if (type < 0 && type > 2) return;

            // Задаёт нужный тип.
            m_AsteroidSize = (AsteroidType) type;

            // Меняет размер и HP.
            ChangeAsteroidSize();
        }

        /// <summary>
        /// Метод, меняющий размер астероида.
        /// </summary>
        public void ChangeAsteroidSize()
        {
            switch (m_AsteroidSize)
            {
                case AsteroidType.Small:
                    transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                    ChangeCurrentHitPoints( (int) (CurrentHitPoints * 0.5f));
                    break;

                case AsteroidType.Medium:
                    transform.localScale = new Vector3(1f, 1f, 1f);
                    break;

                case AsteroidType.Big:
                    transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                    ChangeCurrentHitPoints((int)(CurrentHitPoints * 1.5f));
                    break;
            }
            
            // Если HP < 0, HP = 1
            if (CurrentHitPoints < 1) ChangeCurrentHitPoints(1);
        }

        #endregion

    }
}