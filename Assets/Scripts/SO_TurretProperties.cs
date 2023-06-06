using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// ������������� ����� ������: �������� � ���������������.
    /// </summary>
    public enum TurretMode
    {
        Primary,
        Secondary
    }

    /// <summary>
    /// �����, �������� �������������� ��������. �������� �� ���������, ������ ��������� �� �����.
    /// </summary>
    [CreateAssetMenu(fileName = "TurretProperties", menuName = "ScriptableObjects/CreateNewTurretProperties")]
    public sealed class SO_TurretProperties : ScriptableObject
    {

        #region Properties and Components

        /// <summary>
        /// ����� ���� ������.
        /// </summary>
        [SerializeField] private TurretMode m_Mode;

        /// <summary>
        /// ������ �� ������� ��� ������.
        /// </summary>
        public TurretMode Mode => m_Mode;

        /// <summary>
        /// ������ �� ������ Projectile.
        /// </summary>
        [SerializeField] private Projectile m_ProjectilePrefab;

        /// <summary>
        /// ������ �� ������� Projectile.
        /// </summary>
        public Projectile ProjectilePrefab => m_ProjectilePrefab;

        /// <summary>
        /// ���������������� �������.
        /// </summary>
        [SerializeField] private float m_RateOfFire;

        /// <summary>
        /// ������ �� ������� ���������������� �������.
        /// </summary>
        public float RateOfFire => m_RateOfFire;

        /// <summary>
        /// ������ ������� �� �������.
        /// </summary>
        [SerializeField] private int m_EnergyUsage;

        /// <summary>
        /// ������ �� ������� ������ ������� �� �������.
        /// </summary>
        public int EnergyUsage => m_EnergyUsage;

        /// <summary>
        /// ������ ���������.
        /// </summary>
        [SerializeField] private int m_AmmoUsage;

        /// <summary>
        /// ������ �� ������� ������ ���������.
        /// </summary>
        public int AmmoUsage => m_AmmoUsage;

        /// <summary>
        /// �������������.
        /// </summary>
        [SerializeField] private bool m_Homing;

        /// <summary>
        /// ������, ������������, ��������������� �� ������.
        /// </summary>
        public bool Homing => m_Homing;

        /// <summary>
        /// ������ �� ����� ��������.
        /// </summary>
        [SerializeField] private AudioClip m_LaunchSFX;

        /// <summary>
        /// ������ �� ������� ���� ��������.
        /// </summary>
        public AudioClip LaunchSFX => m_LaunchSFX;

        #endregion

    }
}