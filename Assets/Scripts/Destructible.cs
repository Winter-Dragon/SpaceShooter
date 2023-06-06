using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SpaceShooter
{
    /// <summary>
    /// ������������ ������ �� �����, ����� HP.
    /// </summary>
    public class Destructible : Entity
    {

        #region Properties and Components

        /// <summary>
        /// ������ ���������� �����������.
        /// </summary>
        [SerializeField] private bool m_Indestructible;

        /// <summary>
        /// ������, ������������, ���������� �� ������ �����������.
        /// </summary>
        public bool IsIndestructibly => m_Indestructible;

        /// <summary>
        /// ��������� �������� HP.
        /// </summary>
        [SerializeField] private int m_HitPoints;

        /// <summary>
        /// ������ �� ������������ �������� HP.
        /// </summary>
        public int HitPoints => m_HitPoints;

        /// <summary>
        /// ������� �������� HP.
        /// </summary>
        private int m_CurrentHitPoints;

        /// <summary>
        /// ������ �� ������� �������� HP.
        /// </summary>
        public int CurrentHitPoints => m_CurrentHitPoints;

        /// <summary>
        /// �������, ������������� ����� ������ �������.
        /// </summary>
        [SerializeField] private UnityEvent m_EventsOnDeath;

        /// <summary>
        /// ������ �� ������� ������ �������.
        /// </summary>
        public UnityEvent EventsOnDeath => m_EventsOnDeath;

        /// <summary>
        /// ���������� ������ ������������.
        /// </summary>
        private bool m_InvulnerabilityBonus;

        /// <summary>
        /// ������, ������������, ������� �� ����� ������������.
        /// </summary>
        public bool InvulnerabilityBonusIsActive => m_InvulnerabilityBonus;

        /// <summary>
        /// ��������� ������ ���� Destructible ���������.
        /// </summary>
        private static HashSet<Destructible> m_AllDestructibles;

        /// <summary>
        /// ������ ������ ��� ��������� ��� ���� ��������� Destructible.
        /// </summary>
        public static IReadOnlyCollection<Destructible> AllDestructibles => m_AllDestructibles;

        #region Team

        /// <summary>
        /// ��������� ����������� ������� - 0.
        /// </summary>
        public const int TEAM_ID_NEUTRAL = 0;

        /// <summary>
        /// ����� �������.
        /// </summary>
        [SerializeField] private int m_TeamID;

        /// <summary>
        /// ������ �� ����� �������.
        /// </summary>
        public int TeamID => m_TeamID;

        #endregion

        #region Score

        /// <summary>
        /// �������� �����, ���������� �� ����������� �������.
        /// </summary>
        [SerializeField] private int m_ScoreValue;

        /// <summary>
        /// ������ �� �������� �����, ���������� �� ����������� �������.
        /// </summary>
        public int ScoreValue => m_ScoreValue;

        #endregion

        #endregion


        #region Unity Events

        protected virtual void Start()
        {
            // �������� �������� HP.
            m_CurrentHitPoints = m_HitPoints;
        }

        /// <summary>
        /// �����, ������������ ��� ��������� �������.
        /// </summary>
        protected virtual void OnEnable()
        {
            // ���� ������ ��� - ������� ����� ������.
            if (m_AllDestructibles == null) m_AllDestructibles = new HashSet<Destructible>();

            // ��������� ������� ������ � ������.
            m_AllDestructibles.Add(this);
        }

        /// <summary>
        /// �������� ��� ����������� �������.
        /// </summary>
        protected virtual void OnDestroy()
        {
            // ������� ������� ������ �� ������.
            m_AllDestructibles.Remove(this);
        }

        #endregion

        #region Public API

        /// <summary>
        /// ���������� ����� � �������.
        /// </summary>
        /// <param name="damage">����, ��������� �������.</param>
        public void ApplyDamage(int damage)
        {
            // ��������, ����� �� ������ �������� ����.
            if (m_Indestructible) return;

            // �������� HP �������.
            m_CurrentHitPoints -= damage;

            // ���� HP <= 0, ��������� ������� ������.
            if (m_CurrentHitPoints <= 0)
            {
                OnDeath();
            }
        }

        /// <summary>
        /// �����, ����������� ������ ������������.
        /// </summary>
        /// <param name="inerability">True - ������������.</param>
        public void GetInvulnerability(bool inerability)
        {
            m_Indestructible = inerability;
            m_InvulnerabilityBonus = inerability;
        }

        /// <summary>
        /// �����, �������� ������� �������� HP �������.
        /// </summary>
        /// <param name="hp">����� �������� HP.</param>
        public void ChangeCurrentHitPoints(int hp)
        {
            // �������� �� 0.
            if (hp <= 0) return;

            // ������ �������� HP.
            m_CurrentHitPoints = hp;
        }

        #endregion

        #region Protected API

        /// <summary>
        /// ���������������� ������� ����������� �������, ����� HP < 0.
        /// </summary>
        protected virtual void OnDeath()
        {
            // �������� �� null � ����� ������� ������.
            m_EventsOnDeath?.Invoke();

            // ����������� �������
            Destroy(gameObject);
        }

        #endregion

    }
}
