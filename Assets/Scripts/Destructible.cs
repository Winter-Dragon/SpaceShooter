using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SpaceShooter
{
    /// <summary>
    /// Уничтожаемый объект на сцене, имеет HP.
    /// </summary>
    public class Destructible : Entity
    {

        #region Properties and Components

        /// <summary>
        /// Объект игнорирует повреждения.
        /// </summary>
        [SerializeField] private bool m_Indestructible;

        /// <summary>
        /// Ссылка, показывающая, игнорирует ли объект повреждения.
        /// </summary>
        public bool IsIndestructibly => m_Indestructible;

        /// <summary>
        /// Стартовое значение HP.
        /// </summary>
        [SerializeField] private int m_HitPoints;

        /// <summary>
        /// Ссылка на максимальное значение HP.
        /// </summary>
        public int HitPoints => m_HitPoints;

        /// <summary>
        /// Текущее значение HP.
        /// </summary>
        private int m_CurrentHitPoints;

        /// <summary>
        /// Ссылка на текущее значение HP.
        /// </summary>
        public int CurrentHitPoints => m_CurrentHitPoints;

        /// <summary>
        /// События, выполняющиеся после смерти объекта.
        /// </summary>
        [SerializeField] private UnityEvent m_EventsOnDeath;

        /// <summary>
        /// Ссылка на событие смерти объекта.
        /// </summary>
        public UnityEvent EventsOnDeath => m_EventsOnDeath;

        /// <summary>
        /// Переменная бонуса неуязвимости.
        /// </summary>
        private bool m_InvulnerabilityBonus;

        /// <summary>
        /// Ссылка, отображающая, активен ли бонус неяузвимости.
        /// </summary>
        public bool InvulnerabilityBonusIsActive => m_InvulnerabilityBonus;

        /// <summary>
        /// Статичный список всех Destructible элементов.
        /// </summary>
        private static HashSet<Destructible> m_AllDestructibles;

        /// <summary>
        /// Ссылка только для прочтения для всех элементов Destructible.
        /// </summary>
        public static IReadOnlyCollection<Destructible> AllDestructibles => m_AllDestructibles;

        #region Team

        /// <summary>
        /// Константа нейтральной команды - 0.
        /// </summary>
        public const int TEAM_ID_NEUTRAL = 0;

        /// <summary>
        /// Номер команды.
        /// </summary>
        [SerializeField] private int m_TeamID;

        /// <summary>
        /// Ссылка на номер команды.
        /// </summary>
        public int TeamID => m_TeamID;

        #endregion

        #region Score

        /// <summary>
        /// Значение очков, получаемых за уничтожение объекта.
        /// </summary>
        [SerializeField] private int m_ScoreValue;

        /// <summary>
        /// Ссылка на значение очков, получаемых за уничтожение объекта.
        /// </summary>
        public int ScoreValue => m_ScoreValue;

        #endregion

        #endregion


        #region Unity Events

        protected virtual void Start()
        {
            // Обнуляет значение HP.
            m_CurrentHitPoints = m_HitPoints;
        }

        /// <summary>
        /// Метод, вызывающийся при появлении объекта.
        /// </summary>
        protected virtual void OnEnable()
        {
            // Если списка нет - создать новый список.
            if (m_AllDestructibles == null) m_AllDestructibles = new HashSet<Destructible>();

            // Добавляет текущий объект в список.
            m_AllDestructibles.Add(this);
        }

        /// <summary>
        /// Действия при уничтожении объекта.
        /// </summary>
        protected virtual void OnDestroy()
        {
            // Удалить текущий объект из списка.
            m_AllDestructibles.Remove(this);
        }

        #endregion

        #region Public API

        /// <summary>
        /// Применение урона к объекту.
        /// </summary>
        /// <param name="damage">Урон, наносимый объекту.</param>
        public void ApplyDamage(int damage)
        {
            // Проверка, может ли объект получать урон.
            if (m_Indestructible) return;

            // Отнимает HP объекта.
            m_CurrentHitPoints -= damage;

            // Если HP <= 0, запускает событие смерти.
            if (m_CurrentHitPoints <= 0)
            {
                OnDeath();
            }
        }

        /// <summary>
        /// Метод, позволяющий задать неуязвимость.
        /// </summary>
        /// <param name="inerability">True - неуязвимость.</param>
        public void GetInvulnerability(bool inerability)
        {
            m_Indestructible = inerability;
            m_InvulnerabilityBonus = inerability;
        }

        /// <summary>
        /// Метод, меняющий текущее значение HP объекта.
        /// </summary>
        /// <param name="hp">Новое значение HP.</param>
        public void ChangeCurrentHitPoints(int hp)
        {
            // Проверка на 0.
            if (hp <= 0) return;

            // Меняет значение HP.
            m_CurrentHitPoints = hp;
        }

        #endregion

        #region Protected API

        /// <summary>
        /// Переопределяемое событие уничножение объекта, когда HP < 0.
        /// </summary>
        protected virtual void OnDeath()
        {
            // Проверка на null и вызов события смерти.
            m_EventsOnDeath?.Invoke();

            // Уничтожение объекта
            Destroy(gameObject);
        }

        #endregion

    }
}
