using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// Класс, спавнящий игровые сущности.
    /// </summary>
    public class EntitySpawner : MonoBehaviour
    {

        #region Properties and Components

        /// <summary>
        /// Режимы спавна: при старте и периодически.
        /// </summary>
        public enum SpawnMode
        {
            Start,
            Loop
        }

        /// <summary>
        /// Массив префабов сущностей, которые могут спавниться.
        /// </summary>
        [SerializeField] private Entity[] m_EntityPrefabs;

        /// <summary>
        /// Зона спавна сущностей.
        /// </summary>
        [SerializeField] private CircleArea m_Area;

        /// <summary>
        /// Выбор режима спавна.
        /// </summary>
        [SerializeField] private SpawnMode m_SpawnMode;

        /// <summary>
        /// Кол-во объектов, которые спавнятся за раз.
        /// </summary>
        [SerializeField] private int m_NumberSpawns;

        /// <summary>
        /// Как часто обновляется таймер спавна.
        /// </summary>
        [SerializeField] private float m_RespawnTime;

        /// <summary>
        /// Внутренний таймер.
        /// </summary>
        private Timer m_Timer;

        #endregion


        #region UnityEvents

        private void Start()
        {
            // Спавнит объекты и убирает активность, если режим спавна "при старте".
            if (m_SpawnMode == SpawnMode.Start)
            {
                SpawnEntities();

                enabled = false;
            }

            // Обнуляет таймер по таймеру спавна.
            m_Timer = new Timer(m_RespawnTime, true);
        }

        private void FixedUpdate()
        {
            // Обновляет таймер.
            m_Timer.UpdateTimer();

            // Если время спавна ещё не настало, обновить внутренний таймер и выйти из метода.
            if (m_Timer.IsFinished)
            {
                SpawnEntities();
            }
        }

        #endregion


        #region Private API

        /// <summary>
        /// Метод, спавняжий сущность в случайной позиции.
        /// </summary>
        private void SpawnEntities()
        {
            
            for(int i = 0; i < m_NumberSpawns; i++)
            {
                // Выбор случайной сущности из массива.
                int index = Random.Range(0, m_EntityPrefabs.Length);

                // Создать и записать созданную сущность в GameObject.
                GameObject entity = Instantiate(m_EntityPrefabs[index].gameObject);

                // Переместить сущность в случайную зону в области спавна.
                entity.transform.position = m_Area.GetRandomInsideZone();
            }
        }

        #endregion

    }
}