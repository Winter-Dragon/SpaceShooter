using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// Инициализация видов оружия: основное и вспомогательное.
    /// </summary>
    public enum TurretMode
    {
        Primary,
        Secondary
    }

    /// <summary>
    /// Класс, задающий характеристике туррелям. Работает из редактора, нельзя поставить на сцену.
    /// </summary>
    [CreateAssetMenu(fileName = "TurretProperties", menuName = "ScriptableObjects/CreateNewTurretProperties")]
    public sealed class SO_TurretProperties : ScriptableObject
    {

        #region Properties and Components

        /// <summary>
        /// Выбор вида оружия.
        /// </summary>
        [SerializeField] private TurretMode m_Mode;

        /// <summary>
        /// Ссылка на текущий вид оружия.
        /// </summary>
        public TurretMode Mode => m_Mode;

        /// <summary>
        /// Ссылка на префаб Projectile.
        /// </summary>
        [SerializeField] private Projectile m_ProjectilePrefab;

        /// <summary>
        /// Ссылка на текущий Projectile.
        /// </summary>
        public Projectile ProjectilePrefab => m_ProjectilePrefab;

        /// <summary>
        /// Скорострельность туррели.
        /// </summary>
        [SerializeField] private float m_RateOfFire;

        /// <summary>
        /// Ссылка на текущую скорострельность туррели.
        /// </summary>
        public float RateOfFire => m_RateOfFire;

        /// <summary>
        /// Расход энергии за выстрел.
        /// </summary>
        [SerializeField] private int m_EnergyUsage;

        /// <summary>
        /// Ссылка на текущий расход энергии за выстрел.
        /// </summary>
        public int EnergyUsage => m_EnergyUsage;

        /// <summary>
        /// Расход боезапаса.
        /// </summary>
        [SerializeField] private int m_AmmoUsage;

        /// <summary>
        /// Ссылка на текущий расход боезапаса.
        /// </summary>
        public int AmmoUsage => m_AmmoUsage;

        /// <summary>
        /// Самонаведение.
        /// </summary>
        [SerializeField] private bool m_Homing;

        /// <summary>
        /// Ссылка, показывающая, самонаводящееся ли оружие.
        /// </summary>
        public bool Homing => m_Homing;

        /// <summary>
        /// Ссылка на аудио выстрела.
        /// </summary>
        [SerializeField] private AudioClip m_LaunchSFX;

        /// <summary>
        /// Ссылка на текущий звук выстрела.
        /// </summary>
        public AudioClip LaunchSFX => m_LaunchSFX;

        #endregion

    }
}