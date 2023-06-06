using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// �������� ����� �������, ����������� �� ��������.
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    public class Turret : MonoBehaviour
    {

        #region Properties and Compinents

        /// <summary>
        /// ����� ���� ������.
        /// </summary>
        [SerializeField] private TurretMode m_Mode;

        /// <summary>
        /// ���������� ������� ��� ������.
        /// </summary>
        public TurretMode Mode => m_Mode;

        /// <summary>
        /// ������ �� �������������� �������.
        /// </summary>
        [SerializeField] private SO_TurretProperties m_TurretProperties;

        /// <summary>
        /// ������ �� ������� �������������� �������.
        /// </summary>
        public SO_TurretProperties CurrentTurretProperties => m_TurretProperties;

        /// <summary>
        /// ������ �� ���������� ��������.
        /// </summary>
        private float m_RefiteTimer;

        /// <summary>
        /// ����� �� ������� ��������. Canfire = true, ���� ������ <= 0.
        /// </summary>
        public bool CanFire => m_RefiteTimer <= 0;

        /// <summary>
        /// ������ �� �������, ������� ��������.
        /// </summary>
        [SerializeField] private SpaceShip m_Ship;

        /// <summary>
        /// ������ �� Audio Source � �������.
        /// </summary>
        private AudioSource m_AudioSource;

        /// <summary>
        /// ����� ������.
        /// </summary>
        private float m_AudioTimer;

        #endregion


        #region Unity Events

        private void Start()
        {
            // ������ ���������� ���������� � �������� ������ ���� �� Turret Properties.
            m_AudioSource = GetComponent<AudioSource>();
            m_AudioSource.clip = m_TurretProperties.LaunchSFX;
        }

        private void FixedUpdate()
        {
            // ���� ������ > 0, �������� ����� �����.
            if (m_RefiteTimer > 0) m_RefiteTimer -= Time.fixedDeltaTime;
            m_AudioTimer += Time.fixedDeltaTime;
        }

        #endregion


        #region Public API

        /// <summary>
        /// �����, ����������� ������� ��������.
        /// </summary>
        public void Fire()
        {
            // �������� �� null � ������ ��������.
            if (m_TurretProperties == null || m_RefiteTimer > 0) return;

            // ��������, ������� �� ������� � ��������
            if (m_Ship.UsedEnergy(m_TurretProperties.EnergyUsage) == false || m_Ship.UsedAmmo(m_TurretProperties.AmmoUsage) == false) return;

            // �������� Projectile, ���������� ������� � �����������.
            Projectile projectile = Instantiate(m_TurretProperties.ProjectilePrefab).GetComponent<Projectile>();
            projectile.transform.position = transform.position;
            projectile.transform.up = transform.up;

            // ��������� ������������ ������� � �������.
            projectile.SetParentShooter(m_Ship);
            projectile.SetShooterTurret(GetComponent<Turret>());

            // ���� ������������� - ����� ����.
            if (m_TurretProperties.Homing == true) projectile.FindTarget();

            // ��������� ���� ��������.
            if (m_AudioSource.clip != null && m_AudioSource.clip.length < m_AudioTimer)
            {
                m_AudioSource.Play();
                m_AudioTimer = 0;
            }

            // ��������� ������� �������� ��������������� �������.
            m_RefiteTimer = m_TurretProperties.RateOfFire;
        }

        /// <summary>
        /// �����, ���������� �������������� �������.
        /// </summary>
        /// <param name="properties">����� ��������������.</param>
        public void AssignLoadout(SO_TurretProperties properties)
        {
            // �������� �� ������������ ���� �������.
            if (m_Mode != properties.Mode) return;

            // ��������� ������� ��������.
            m_RefiteTimer = 0;

            // ������ ����� �������������� �������.
            m_TurretProperties = properties;
        }

        #endregion

    }
}