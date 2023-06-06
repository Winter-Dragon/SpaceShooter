using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// Класс, спавнящий космический мусор.
    /// </summary>
    public class EntitySpawnerDebris : MonoBehaviour
    {

        #region Properties and Components

        /// <summary>
        /// Префабы космического мусора.
        /// </summary>
        [SerializeField] private GameObject[] m_DebrisPrefabs;

        /// <summary>
        /// Область спавна космического мусора.
        /// </summary>
        [SerializeField] private CircleArea m_Area;

        /// <summary>
        /// Кол-во мусора, спавнящегося за раз.
        /// </summary>
        [SerializeField] private int m_NumberDebris;

        /// <summary>
        /// Скорость, задающаяся мусору.
        /// </summary>
        [SerializeField] private float m_RandomSpeed;

        #endregion


        #region Unity Events

        private void Start()
        {
            // Спавн необходимого кол-ва мусора.
            for (int i = 0; i < m_NumberDebris; i++)
            {
                SpawnDebris();
            }
        }

        #endregion


        #region Private API

        /// <summary>
        /// Метод, спавнящий космический мусор
        /// </summary>
        private void SpawnDebris()
        {
            // Выбор случайного мусора из массива.
            int index = Random.Range(0, m_DebrisPrefabs.Length);

            // Создать и записать созданную сущность в GameObject.
            GameObject debris = Instantiate(m_DebrisPrefabs[index].gameObject);

            // Условие, выполняющиеся, если мусор - астероид.
            if (debris.GetComponent<Asteroid>() == true)
            {
                // Создаём ссылку на астероид.
                Asteroid asteroid = debris.GetComponent<Asteroid>();

                // Задать астероиду случайный размер.
                asteroid.SetAsteroidType(Random.Range(0, 3));
            }

            // Переместить мусор в случайную зону в области спавна.
            debris.transform.position = m_Area.GetRandomInsideZone();

            // Найти у мусора коспонент Destructible и привязаться к событию смерти, вызвав метод.
            debris.GetComponent<Destructible>().EventsOnDeath.AddListener(OnDebrisDead);

            // Записать ссылку на Rigidbody мусора.
            Rigidbody2D debrisRigidbody = debris.GetComponent<Rigidbody2D>();

            // Проверка на null и нулевую/отрицательную скорость.
            if (debrisRigidbody != null && m_RandomSpeed > 0)
            {
                debrisRigidbody.velocity = UnityEngine.Random.insideUnitCircle * m_RandomSpeed;
            }
        }

        /// <summary>
        /// Метод, срабатывающий при уничтожении космического мусора.
        /// </summary>
        private void OnDebrisDead()
        {
            SpawnDebris();
        }

        #endregion

    }
}