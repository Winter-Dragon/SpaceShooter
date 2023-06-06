using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// Основной класс туррели, позволяющий ей стрелять.
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    public class Turret : MonoBehaviour
    {

        #region Properties and Compinents

        /// <summary>
        /// Выбор мода оружия.
        /// </summary>
        [SerializeField] private TurretMode m_Mode;

        /// <summary>
        /// Возвращает текущий мод оружия.
        /// </summary>
        public TurretMode Mode => m_Mode;

        /// <summary>
        /// Ссылка на характеристики туррели.
        /// </summary>
        [SerializeField] private SO_TurretProperties m_TurretProperties;

        /// <summary>
        /// Ссылка на текущие характеристики туррели.
        /// </summary>
        public SO_TurretProperties CurrentTurretProperties => m_TurretProperties;

        /// <summary>
        /// Таймер до следующего выстрела.
        /// </summary>
        private float m_RefiteTimer;

        /// <summary>
        /// Может ли туррель стрелять. Canfire = true, если таймер <= 0.
        /// </summary>
        public bool CanFire => m_RefiteTimer <= 0;

        /// <summary>
        /// Ссылка на корабль, который стреляет.
        /// </summary>
        [SerializeField] private SpaceShip m_Ship;

        /// <summary>
        /// Ссылка на Audio Source у туррели.
        /// </summary>
        private AudioSource m_AudioSource;

        /// <summary>
        /// Аудио таймер.
        /// </summary>
        private float m_AudioTimer;

        #endregion


        #region Unity Events

        private void Start()
        {
            // Задать переменную аудиосурса и записать нужный клип из Turret Properties.
            m_AudioSource = GetComponent<AudioSource>();
            m_AudioSource.clip = m_TurretProperties.LaunchSFX;
        }

        private void FixedUpdate()
        {
            // Если таймер > 0, отнимает время кадра.
            if (m_RefiteTimer > 0) m_RefiteTimer -= Time.fixedDeltaTime;
            m_AudioTimer += Time.fixedDeltaTime;
        }

        #endregion


        #region Public API

        /// <summary>
        /// Метод, позволяющий туррели стрелять.
        /// </summary>
        public void Fire()
        {
            // Проверка на null и таймер стрельбы.
            if (m_TurretProperties == null || m_RefiteTimer > 0) return;

            // Проверка, хватает ли энергии и патронов
            if (m_Ship.UsedEnergy(m_TurretProperties.EnergyUsage) == false || m_Ship.UsedAmmo(m_TurretProperties.AmmoUsage) == false) return;

            // Создание Projectile, добавление позиции и направления.
            Projectile projectile = Instantiate(m_TurretProperties.ProjectilePrefab).GetComponent<Projectile>();
            projectile.transform.position = transform.position;
            projectile.transform.up = transform.up;

            // Назначить выстреливший корабль и туррель.
            projectile.SetParentShooter(m_Ship);
            projectile.SetShooterTurret(GetComponent<Turret>());

            // Если самонаведение - найти цель.
            if (m_TurretProperties.Homing == true) projectile.FindTarget();

            // Проиграть звук выстрела.
            if (m_AudioSource.clip != null && m_AudioSource.clip.length < m_AudioTimer)
            {
                m_AudioSource.Play();
                m_AudioTimer = 0;
            }

            // Обнуление таймера согласно характеристикам туррели.
            m_RefiteTimer = m_TurretProperties.RateOfFire;
        }

        /// <summary>
        /// Метод, заменяющий характеристики туррели.
        /// </summary>
        /// <param name="properties">Новые характеристики.</param>
        public void AssignLoadout(SO_TurretProperties properties)
        {
            // Проверка на соответствие мода туррели.
            if (m_Mode != properties.Mode) return;

            // Обнуление таймера стрельбы.
            m_RefiteTimer = 0;

            // Задать новые характеристики туррели.
            m_TurretProperties = properties;
        }

        #endregion

    }
}