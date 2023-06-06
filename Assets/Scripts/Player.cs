using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// ������� ����� ������.
    /// </summary>
    public class Player : Singleton<Player>
    {

        #region Properties and Components

        /// <summary>
        /// ���������� �������������� ������.
        /// </summary>
        public static int NumberLives = 3;

        /// <summary>
        /// ������� ���������� ������.
        /// </summary>
        private int m_CurrentLives;

        /// <summary>
        /// ������ �� ������� �������� ������.
        /// </summary>
        public int CurrentLives => m_CurrentLives;

        /// <summary>
        /// ������ �� �������.
        /// </summary>
        [SerializeField] private SpaceShip m_SpaceShip;

        /// <summary>
        /// ������ �� ������� �������.
        /// </summary>
        public SpaceShip ActiveShip => m_SpaceShip;

        /// <summary>
        /// ������ �� ������� ������ ������, ����������� ����� ������ ������.
        /// </summary>
        [SerializeField] private GameObject m_ParticlesAfterDeathPrefab;

        /// <summary>
        /// ������ �� ���������� ������.
        /// </summary>
        [SerializeField] private CameraController m_CameraController;

        /// <summary>
        /// ������ �� ���������� ������.
        /// </summary>
        [SerializeField] private MovementController m_MovementController;

        /// <summary>
        /// ������ ���������� �������.
        /// </summary>
        [SerializeField] private SpaceShip m_SpaceShipDefault;

        #region Score

        /// <summary>
        /// ����������, �������� ���-�� �����.
        /// </summary>
        public int Score { get; private set; }

        /// <summary>
        /// �������� ��������� �����.
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
        /// ����������, �������� ���-�� �������.
        /// </summary>
        public int NumberKills { get; private set; }

        #endregion

        #endregion


        #region UnityEvents

        protected override void Awake()
        {
            // Awake �� Singleton.
            base.Awake();

            // ���������� ������� ������ �������.
            if (m_SpaceShip != null) Destroy(m_SpaceShip.gameObject);
        }

        private void Start()
        {
            // ������� ��������� �������� �������.
            if (LevelSequenceController.PlayerShip == null && m_SpaceShipDefault != null) LevelSequenceController.PlayerShip = m_SpaceShipDefault;

            // ������� ��������� ���-�� ������.
            m_CurrentLives = NumberLives;

            // ��������� �������.
            Respawn();
        }

        #endregion


        #region private API

        /// <summary>
        /// �����, ������������� ����� ����������� �������.
        /// </summary>
        private void OnShipDeath()
        {
            // ������� � �������� ����� ������� ����� ������ �������, ������ �� ������� ������ �������.
            var m_ParticlesAfterDeath = Instantiate(m_ParticlesAfterDeathPrefab);
            m_ParticlesAfterDeath.transform.position = m_SpaceShip.transform.position;

            // �������� 1 �����.
            m_CurrentLives--;

            // ���� ����� ����������� - ��������� �������.
            if (m_CurrentLives <= 0) LevelSequenceController.Instance?.FinishCurrentLevel(false);
            // ���� ����� ���� - ��������� ������� ����� 3 �������.
            else Invoke("Respawn", 3);
        }

        /// <summary>
        /// �����, ������������ �������.
        /// </summary>
        private void Respawn()
        {
            // �������� �� ������� SpaceShip.
            if (LevelSequenceController.PlayerShip == null) return;

            // ���������� �� ������������� �������.
            m_SpaceShip.EventsOnDeath.RemoveListener(OnShipDeath);

            // ������� � �������� � ���������� ����� �������.
            var newPlayerShip = Instantiate(LevelSequenceController.PlayerShip);

            // ������������ ������ ������� �� �����.
            m_SpaceShip = newPlayerShip.GetComponent<SpaceShip>();

            // ������ ������ ����� ����, ������ ���������� ����� �������.
            m_CameraController.SetTarget(m_SpaceShip.transform);
            m_MovementController.SetTargetShip(m_SpaceShip);

            // ����������� �� ������� ������ ������ �������.
            m_SpaceShip.EventsOnDeath.AddListener(OnShipDeath);
        }

        #endregion


        #region Public API

        /// <summary>
        /// �������� 1 �������� � ������� �������.
        /// </summary>
        public void AddKill()
        {
            NumberKills++;
        }

        /// <summary>
        /// �������� ���� � ������� �����.
        /// </summary>
        /// <param name="scores">���-�� �����.</param>
        public void AddScore(int scores)
        {
            Score += scores;
        }

        /// <summary>
        /// �������� ���������� � ������ ������.
        /// </summary>
        public void Restart()
        {
            // �������� ���������� � ����������.
            NumberKills = 0;
            Score = 0;
            m_CurrentLives = NumberLives;

            // ��������� �������.
            Respawn();
        }

        #endregion

    }
}