using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// �������� ����� ������������ ������� ������.
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    public class SpaceShip : Destructible
    {

        #region Properties and Components

        [Header("Space Ship")]

        #region Ship Stats

        /// <summary>
        /// ����� ��� �������������� ��������� � rigidbody.
        /// </summary>
        [SerializeField] private float m_Mass;

        /// <summary>
        /// �������� ���������.
        /// </summary>
        [SerializeField] private float m_Thrust;

        /// <summary>
        /// ������� ���������.
        /// </summary>
        [SerializeField] private float m_Mobility;

        /// <summary>
        /// ������������ �������� ��������.
        /// </summary>
        [SerializeField] private float m_MaxLinearVelocity;

        /// <summary>
        /// ������������ ������������ �������� (� ��������/���).
        /// </summary>
        [SerializeField] private float m_MaxAngularVelocity;

        /// <summary>
        /// ������������ �������� �������.
        /// </summary>
        [SerializeField] private int m_MaxEnergy;

        /// <summary>
        /// ������������ �������� �������������� �����������.
        /// </summary>
        [SerializeField] private int m_MaxAmmo;

        /// <summary>
        /// �������� �������, ���������������� � �������.
        /// </summary>
        [SerializeField] private float m_EnergyRegenPerSecond;

        #region Links

        /// <summary>
        /// ������ �� ����� �������.
        /// </summary>
        public float Mass => m_Mass;

        /// <summary>
        /// ������ �� �������� �������� ��������� �������.
        /// </summary>
        public float Thrust => m_Thrust;

        /// <summary>
        /// ������ �� ������� �������� ��������� �������.
        /// </summary>
        public float Mobility => m_Mobility;

        /// <summary>
        /// ������ �� ������������ �������� ��������.
        /// </summary>
        public float MaxLinearVelocity => m_MaxLinearVelocity;

        /// <summary>
        /// ������ �� ������������ ������������ ��������.
        /// </summary>
        public float MaxAngularVelocity => m_MaxAngularVelocity;

        /// <summary>
        /// ������ �� ������������ �������� �������.
        /// </summary>
        public int MaxEnergy => m_MaxEnergy;

        /// <summary>
        /// ������ �� ������������ ���-�� ���������.
        /// </summary>
        public int MaxAmmo => m_MaxAmmo;

        /// <summary>
        /// ������ �� �������� �������, ���������������� � �������.
        /// </summary>
        public float EnergyRegenPerSecond => m_EnergyRegenPerSecond;

        #endregion

        #endregion

        #region Private Components

        /// <summary>
        /// ���������� ������ �� Rigidbody.
        /// </summary>
        private Rigidbody2D m_Rigidbody;

        /// <summary>
        /// ������� �������� �������.
        /// </summary>
        private float m_PrimaryEnergy;

        /// <summary>
        /// ������� ���-�� ��������������� ����������
        /// </summary>
        private int m_SecondaryAmmo;

        #endregion

        /// <summary>
        /// ������ �������.
        /// </summary>
        [SerializeField] private Sprite m_ShipSprite;

        /// <summary>
        /// ������ �� ������ �������.
        /// </summary>
        public Sprite ShipSprite => m_ShipSprite;

        /// <summary>
        /// ������ � �������.
        /// </summary>
        [SerializeField] private Turret[] m_Turrets;

        /// <summary>
        /// ������ �� ������� ������� ������.
        /// </summary>
        public Turret[] CurrentTurrets => m_Turrets;

        /// <summary>
        /// ������ �� ������� �������� �������.
        /// </summary>
        public Vector2 Velocity => m_Rigidbody.velocity;

        /// <summary>
        /// ������ �� ������� �������� �������.
        /// </summary>
        public float CurrentEnergy => m_PrimaryEnergy;

        /// <summary>
        /// ������ �� ������� ���-�� ���������.
        /// </summary>
        public int CurrentAmmo => m_SecondaryAmmo;

        #endregion


        #region Unity Events

        protected override void Start()
        {
            // ������������ �������� ���������������� ������.
            base.Start();

            // ������������������� Rigidbody � ��������� ��� ����� � �������.
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
        /// ����� ���������� ��� ������� ��� ��������.
        /// </summary>
        private void UpdateRigidbody()
        {
            // �������� ���� �������� �����.
            m_Rigidbody.AddForce(ThrustControl * m_Thrust * transform.up * Time.fixedDeltaTime, ForceMode2D.Force);                                 // �������� ���� * ���� ���� * ������ ����� * ������������� ����� �����

            // �������� ���� ������ �������� �����.
            m_Rigidbody.AddForce(-m_Rigidbody.velocity * ( m_Thrust / m_MaxLinearVelocity ) * Time.fixedDeltaTime, ForceMode2D.Force);              // �������� �������� ������� * ( ���� ���� / ������������ �������� �������� ) * ������������� ����� �����

            // �������� ������������ ����.
            m_Rigidbody.AddTorque(TorqueControl * m_Mobility * Time.fixedDeltaTime, ForceMode2D.Force);                                             // �������� �������� * ���� �������� * ������������� ����� �����

            // �������� ���� ������ ��� ��������.
            m_Rigidbody.AddTorque(-m_Rigidbody.angularVelocity * ( m_Mobility / m_MaxAngularVelocity ) * Time.fixedDeltaTime, ForceMode2D.Force);   // �������� �������� �������� �������� * ( ���� �������� / ������������ ������� ��������) * ������������� ����� �����
        }

        /// <summary>
        /// �����, ���������������� ��������� �������� �������� � �������.
        /// </summary>
        private void InitOffensive()
        {
            // ����������� ��������� �� ��������.
            m_PrimaryEnergy = m_MaxEnergy;
            m_SecondaryAmmo = m_MaxAmmo;
        }

        /// <summary>
        /// �����, �������������� ������� �������.
        /// </summary>
        private void UpdateEnergyRegen()
        {
            // ���������� ������� ������� � ������� ������ �������� � ������� �������� ��������.
            m_PrimaryEnergy += (float) m_EnergyRegenPerSecond * Time.deltaTime;

            // ���������� ������� �� ������������� ��������
            m_PrimaryEnergy = Mathf.Clamp(m_PrimaryEnergy, 0, m_MaxEnergy);
        }

        #endregion


        #region Public API

        /// <summary>
        /// ���������� �������� �����. �� -1.0 �� +1.0 .
        /// </summary>
        public float ThrustControl { get; set; }

        /// <summary>
        /// ���������� ������������ �����. �� -1.0 �� +1.0 .
        /// </summary>
        public float TorqueControl { get; set; }

        /// <summary>
        /// �����, ���������� �������� � �������.
        /// </summary>
        /// <param name="mode">������� ����� �������� (�������� ��� ��������������).</param>
        public void Fire(TurretMode mode)
        {
            // �������� ������� ���, ������� �������� � �������.
            for (int i = 0; i < m_Turrets.Length; i++)
            {
                // ���� � ������� ���������� ���, �������� �� ��.
                if (m_Turrets[i].Mode == mode) m_Turrets[i].Fire();
            }
        }

        /// <summary>
        /// �����, ����������� ������� �������.
        /// </summary>
        /// <param name="energy">�������� �������.</param>
        public void AddEnergy(int energy)
        {
            // ��������� ������� � ������������ � �������� �� 0 �� ������������� ��������.
            m_PrimaryEnergy = Mathf.Clamp(m_PrimaryEnergy + energy, 0, m_MaxEnergy);
        }

        /// <summary>
        /// �����, ����������� �������������� ������� ������.
        /// </summary>
        /// <param name="ammo">�������� ��������������� ���������.</param>
        public void AddAmmo(int ammo)
        {
            // ��������� �������� � ������������ ��� �������� �� 0 �� ������������� ��������.
            m_SecondaryAmmo = Mathf.Clamp(m_SecondaryAmmo + ammo, 0, m_MaxAmmo);
        }

        /// <summary>
        /// �����, ���������� ������� � ������.
        /// </summary>
        /// <param name="count">���������� ���������� ��������.</param>
        public bool UsedAmmo(int count)
        {
            // ������ �� 0, ���� ������ 0 �������� ���������� true.
            if (count == 0) return true;

            // �������, ������������� ���� �������� �������.
            if(m_SecondaryAmmo >= count)
            {
                // ������ �������.
                m_SecondaryAmmo -= count;

                // ���������� true.
                return true;
            }

            // ���������� false, ���� �������� �� �������.
            return false;
        }

        /// <summary>
        /// �����, ���������� ������� � ������.
        /// </summary>
        /// <param name="count">���������� ���������� �������.</param>
        public bool UsedEnergy(int count)
        {
            // ������ �� 0, ���� ������ 0 ������� ���������� true.
            if (count == 0) return true;

            // �������, ������������� ���� ������� �������.
            if (m_PrimaryEnergy >= count)
            {
                // ������ �������.
                m_PrimaryEnergy -= count;

                // ���������� true.
                return true;
            }

            // ���������� false, ���� ������� �� �������.
            return false;
        }

        /// <summary>
        /// �����, ����������� ������ �������� ������� (����� ������).
        /// </summary>
        /// <param name="properties">����� �������������� �������.</param>
        public void AssignWeapon(SO_TurretProperties properties)
        {
            // �������� �� null.
            if (properties == null) return;

            // ������ ������ ������� ��������.
            for(int i = 0; i < m_Turrets.Length; i++)
            {
                m_Turrets[i].AssignLoadout(properties);
            }
        }

        /// <summary>
        /// �����, ������ ������� �������� ��������.
        /// </summary>
        /// <param name="value">�������� ��������.</param>
        public void AddThrust(float value)
        {
            m_Thrust += value;
        }

        #endregion

    }
}
