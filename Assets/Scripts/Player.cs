using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// Базовый класс игрока.
    /// </summary>
    public class Player : Singleton<Player>
    {

        #region Properties and Components

        /// <summary>
        /// Количество дополнительных жизней.
        /// </summary>
        public static int NumberLives = 3;

        /// <summary>
        /// Текущее количество жизней.
        /// </summary>
        private int m_CurrentLives;

        /// <summary>
        /// Ссылка на текущее значение жизней.
        /// </summary>
        public int CurrentLives => m_CurrentLives;

        /// <summary>
        /// Ссылка на коребль.
        /// </summary>
        [SerializeField] private SpaceShip m_SpaceShip;

        /// <summary>
        /// Ссылка на текущий корабль.
        /// </summary>
        public SpaceShip ActiveShip => m_SpaceShip;

        /// <summary>
        /// Ссылка на игровой объект частиц, создающихся после смерти игрока.
        /// </summary>
        [SerializeField] private GameObject m_ParticlesAfterDeathPrefab;

        /// <summary>
        /// Ссылка на контроллер камеры.
        /// </summary>
        [SerializeField] private CameraController m_CameraController;

        /// <summary>
        /// Ссылка на контроллер игрока.
        /// </summary>
        [SerializeField] private MovementController m_MovementController;

        /// <summary>
        /// Префаб дефолтного корабля.
        /// </summary>
        [SerializeField] private SpaceShip m_SpaceShipDefault;

        #region Score

        /// <summary>
        /// Переменная, хранящая кол-во очков.
        /// </summary>
        public int Score { get; private set; }

        /// <summary>
        /// Значение множителя очков.
        /// </summary>
        public static int ScoreMultiplier
        {
            get
            {
                if (UI_Interface_TimePanel.Instance == null) return 3;
                else return UI_Interface_TimePanel.ScoreMultiplier;
            }
        }

        /// <summary>
        /// Переменная, хранящая кол-во убийств.
        /// </summary>
        public int NumberKills { get; private set; }

        #endregion

        #endregion


        #region UnityEvents

        protected override void Awake()
        {
            // Awake от Singleton.
            base.Awake();

            // Уничтожить игровой объект корабля.
            if (m_SpaceShip != null) Destroy(m_SpaceShip.gameObject);
        }

        private void Start()
        {
            // Задаётся дефолтное значение корабля.
            if (LevelSequenceController.PlayerShip == null && m_SpaceShipDefault != null) LevelSequenceController.PlayerShip = m_SpaceShipDefault;

            // Задаётся начальное кол-во жизней.
            m_CurrentLives = NumberLives;

            // Возродить корабль.
            Respawn();
        }

        #endregion


        #region private API

        /// <summary>
        /// Метод, срабатывающий после уничтожения корабля.
        /// </summary>
        private void OnShipDeath()
        {
            // Создать и записать новые частицы после смерти корабля, задать им позицию смерти корабля.
            var m_ParticlesAfterDeath = Instantiate(m_ParticlesAfterDeathPrefab);
            m_ParticlesAfterDeath.transform.position = m_SpaceShip.transform.position;

            // Отнимает 1 жизнь.
            m_CurrentLives--;

            // Если жизни закончились - завершает уровень.
            if (m_CurrentLives <= 0) LevelSequenceController.Instance?.FinishCurrentLevel(false);
            // Если жизни есть - возродить корабль через 3 секунды.
            else Invoke("Respawn", 3);
        }

        /// <summary>
        /// Метод, возрождающий корабль.
        /// </summary>
        private void Respawn()
        {
            // Проверка на наличие SpaceShip.
            if (LevelSequenceController.PlayerShip == null) return;

            // Отписаться от взорвавшегося корабля.
            m_SpaceShip.EventsOnDeath.RemoveListener(OnShipDeath);

            // Создать и записать в переменную новый корабль.
            var newPlayerShip = Instantiate(LevelSequenceController.PlayerShip);

            // Перезаписать старый корабль на новый.
            m_SpaceShip = newPlayerShip.GetComponent<SpaceShip>();

            // Задать камере новую цель, задать управление новым кораблём.
            m_CameraController.SetTarget(m_SpaceShip.transform);
            m_MovementController.SetTargetShip(m_SpaceShip);

            // Подписаться на событие смерти нового корабля.
            m_SpaceShip.EventsOnDeath.AddListener(OnShipDeath);
        }

        #endregion


        #region Public API

        /// <summary>
        /// Добавить 1 убийство в счётчик убийств.
        /// </summary>
        public void AddKill()
        {
            NumberKills++;
        }

        /// <summary>
        /// Добавить очки в счётчик очков.
        /// </summary>
        /// <param name="scores">Кол-во очков.</param>
        public void AddScore(int scores)
        {
            Score += scores;
        }

        /// <summary>
        /// Обнулить переменные в данном классе.
        /// </summary>
        public void Restart()
        {
            // Обнулить статистику и переменные.
            NumberKills = 0;
            Score = 0;
            m_CurrentLives = NumberLives;

            // Возродить корабль.
            Respawn();
        }

        #endregion

    }
}