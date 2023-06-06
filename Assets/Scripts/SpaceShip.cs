using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// Основной класс космического корабля игрока.
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    public class SpaceShip : Destructible
    {

        #region Properties and Components

        [Header("Space Ship")]

        #region Ship Stats

        /// <summary>
        /// Масса для автоматической установки у rigidbody.
        /// </summary>
        [SerializeField] private float m_Mass;

        /// <summary>
        /// Линейное ускорение.
        /// </summary>
        [SerializeField] private float m_Thrust;

        /// <summary>
        /// Угловое ускорение.
        /// </summary>
        [SerializeField] private float m_Mobility;

        /// <summary>
        /// Максимальная линейная скорость.
        /// </summary>
        [SerializeField] private float m_MaxLinearVelocity;

        /// <summary>
        /// Максимальная вращательная скорость (в градусах/сек).
        /// </summary>
        [SerializeField] private float m_MaxAngularVelocity;

        /// <summary>
        /// Максимальное значение энергии.
        /// </summary>
        [SerializeField] private int m_MaxEnergy;

        /// <summary>
        /// Максимальное значение дополнительных боеприпасов.
        /// </summary>
        [SerializeField] private int m_MaxAmmo;

        /// <summary>
        /// Значение энергии, регенерирующееся в секунду.
        /// </summary>
        [SerializeField] private float m_EnergyRegenPerSecond;

        #region Links

        /// <summary>
        /// Ссылка на массу корабля.
        /// </summary>
        public float Mass => m_Mass;

        /// <summary>
        /// Ссылка на линейное значение ускорения корабля.
        /// </summary>
        public float Thrust => m_Thrust;

        /// <summary>
        /// Ссылка на угловое значение ускорения корабля.
        /// </summary>
        public float Mobility => m_Mobility;

        /// <summary>
        /// Ссылка на максимальную линейную скорость.
        /// </summary>
        public float MaxLinearVelocity => m_MaxLinearVelocity;

        /// <summary>
        /// Ссылка на максимальную вращательную скорость.
        /// </summary>
        public float MaxAngularVelocity => m_MaxAngularVelocity;

        /// <summary>
        /// Ссылка на максимальное значение энергии.
        /// </summary>
        public int MaxEnergy => m_MaxEnergy;

        /// <summary>
        /// Ссылка на максимальное кол-во боезапаса.
        /// </summary>
        public int MaxAmmo => m_MaxAmmo;

        /// <summary>
        /// Ссылка на значение энергии, регенерирующейся в секунду.
        /// </summary>
        public float EnergyRegenPerSecond => m_EnergyRegenPerSecond;

        #endregion

        #endregion

        #region Private Components

        /// <summary>
        /// Сохранённая ссылка на Rigidbody.
        /// </summary>
        private Rigidbody2D m_Rigidbody;

        /// <summary>
        /// Текущее значение энергии.
        /// </summary>
        private float m_PrimaryEnergy;

        /// <summary>
        /// Текущее кол-во дополнительного боеприпаса
        /// </summary>
        private int m_SecondaryAmmo;

        #endregion

        /// <summary>
        /// Спрайт корабля.
        /// </summary>
        [SerializeField] private Sprite m_ShipSprite;

        /// <summary>
        /// Ссылка на спрайт корабля.
        /// </summary>
        public Sprite ShipSprite => m_ShipSprite;

        /// <summary>
        /// Массив с оружием.
        /// </summary>
        [SerializeField] private Turret[] m_Turrets;

        /// <summary>
        /// Ссылка на текущие туррели игрока.
        /// </summary>
        public Turret[] CurrentTurrets => m_Turrets;

        /// <summary>
        /// Ссылка на текущую скорость корабля.
        /// </summary>
        public Vector2 Velocity => m_Rigidbody.velocity;

        /// <summary>
        /// Ссылка на текущее значение энергии.
        /// </summary>
        public float CurrentEnergy => m_PrimaryEnergy;

        /// <summary>
        /// Ссылка на текущее кол-во боезапаса.
        /// </summary>
        public int CurrentAmmo => m_SecondaryAmmo;

        #endregion


        #region Unity Events

        protected override void Start()
        {
            // Использовать свойства переопределённого метода.
            base.Start();

            // Проинициализировать Rigidbody и назначить ему массу и инерцию.
            m_Rigidbody = GetComponent<Rigidbody2D>();
            m_Rigidbody.mass = m_Mass;
            m_Rigidbody.inertia = 1;

            InitOffensive();
        }

        private void FixedUpdate()
        {
            UpdateRigidbody();

            UpdateEnergyRegen();
        }

        #endregion


        #region Private API

        /// <summary>
        /// Метод добавления сил кораблю для движения.
        /// </summary>
        private void UpdateRigidbody()
        {
            // Создание силы движения вперёд.
            m_Rigidbody.AddForce(ThrustControl * m_Thrust * transform.up * Time.fixedDeltaTime, ForceMode2D.Force);                                 // Контроль тяги * сила тяги * вектор вверх * фиксированное время кадра

            // Создание силы трения движения вперёд.
            m_Rigidbody.AddForce(-m_Rigidbody.velocity * ( m_Thrust / m_MaxLinearVelocity ) * Time.fixedDeltaTime, ForceMode2D.Force);              // Обратная скорость корабля * ( сила тяги / максимальная линейная скорость ) * фиксированное время кадра

            // Создание вращательной силы.
            m_Rigidbody.AddTorque(TorqueControl * m_Mobility * Time.fixedDeltaTime, ForceMode2D.Force);                                             // Контроль вращения * сила вращения * фиксированное время кадра

            // Создание силы трения при вращении.
            m_Rigidbody.AddTorque(-m_Rigidbody.angularVelocity * ( m_Mobility / m_MaxAngularVelocity ) * Time.fixedDeltaTime, ForceMode2D.Force);   // Обратная скорость углового вращения * ( сила вращения / максимальная угловая скорость) * фиксированное время кадра
        }

        /// <summary>
        /// Метод, инициализирующий начальное значение патронов и энергии.
        /// </summary>
        private void InitOffensive()
        {
            // Восполнение боезапаса на максимум.
            m_PrimaryEnergy = m_MaxEnergy;
            m_SecondaryAmmo = m_MaxAmmo;
        }

        /// <summary>
        /// Метод, регенерирующий энергию кораблю.
        /// </summary>
        private void UpdateEnergyRegen()
        {
            // Добавление энергии кораблю и перевод целого значения в дробное значение поменьше.
            m_PrimaryEnergy += (float) m_EnergyRegenPerSecond * Time.deltaTime;

            // Ограничить энергию до максимального значения
            m_PrimaryEnergy = Mathf.Clamp(m_PrimaryEnergy, 0, m_MaxEnergy);
        }

        #endregion


        #region Public API

        /// <summary>
        /// Управление линейной тягой. От -1.0 до +1.0 .
        /// </summary>
        public float ThrustControl { get; set; }

        /// <summary>
        /// Управление вращательной тягой. От -1.0 до +1.0 .
        /// </summary>
        public float TorqueControl { get; set; }

        /// <summary>
        /// Метод, вызывающий стрельбу у корабля.
        /// </summary>
        /// <param name="mode">Текущий режим стрельбы (основной или дополнительный).</param>
        public void Fire(TurretMode mode)
        {
            // Стреляет столько раз, сколько туррелей у корабля.
            for (int i = 0; i < m_Turrets.Length; i++)
            {
                // Если у туррели подходящий мод, стреляет из неё.
                if (m_Turrets[i].Mode == mode) m_Turrets[i].Fire();
            }
        }

        /// <summary>
        /// Медод, добавляющий энергию кораблю.
        /// </summary>
        /// <param name="energy">Значение энергии.</param>
        public void AddEnergy(int energy)
        {
            // Добавляем энергию и ограничиваем её значение от 0 до максимального значения.
            m_PrimaryEnergy = Mathf.Clamp(m_PrimaryEnergy + energy, 0, m_MaxEnergy);
        }

        /// <summary>
        /// Метод, добавляющий дополнительные снаряды игроку.
        /// </summary>
        /// <param name="ammo">Значение дополнительного боезапаса.</param>
        public void AddAmmo(int ammo)
        {
            // Добавляем боезапас и ограничиваем его значение от 0 до максимального значения.
            m_SecondaryAmmo = Mathf.Clamp(m_SecondaryAmmo + ammo, 0, m_MaxAmmo);
        }

        /// <summary>
        /// Метод, отнимающий патроны у игрока.
        /// </summary>
        /// <param name="count">Количество отнимаемых патронов.</param>
        public bool UsedAmmo(int count)
        {
            // Защита на 0, если отнять 0 патронов возвращает true.
            if (count == 0) return true;

            // Условие, выполняющееся если патронов хватает.
            if(m_SecondaryAmmo >= count)
            {
                // Отнять патроны.
                m_SecondaryAmmo -= count;

                // Возвращает true.
                return true;
            }

            // Возвращает false, если патронов не хватило.
            return false;
        }

        /// <summary>
        /// Метод, отнимающий энергию у игрока.
        /// </summary>
        /// <param name="count">Количество отнимаемой энергии.</param>
        public bool UsedEnergy(int count)
        {
            // Защита на 0, если отнять 0 энергии возвращает true.
            if (count == 0) return true;

            // Условие, выполняющееся если энергии хватает.
            if (m_PrimaryEnergy >= count)
            {
                // Отнять энергию.
                m_PrimaryEnergy -= count;

                // Возвращает true.
                return true;
            }

            // Возвращает false, если энергии не хватило.
            return false;
        }

        /// <summary>
        /// Метод, позволяющий менять свойства туррели (смена оружия).
        /// </summary>
        /// <param name="properties">Новые характеристики туррели.</param>
        public void AssignWeapon(SO_TurretProperties properties)
        {
            // Проверка на null.
            if (properties == null) return;

            // Меняет каждой туррели свойства.
            for(int i = 0; i < m_Turrets.Length; i++)
            {
                m_Turrets[i].AssignLoadout(properties);
            }
        }

        /// <summary>
        /// Метод, дающий кораблю линейную скорость.
        /// </summary>
        /// <param name="value">Значение скорости.</param>
        public void AddThrust(float value)
        {
            m_Thrust += value;
        }

        #endregion

    }
}
