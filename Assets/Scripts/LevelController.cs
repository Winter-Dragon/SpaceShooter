using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SpaceShooter
{
    /// <summary>
    /// ���������, ������� ���������� �������� ��� �������� ������� ����������� ������.
    /// </summary>
    public interface ILevelCondition
    {
        /// <summary>
        /// ���������� true, ���� ������� ���������.
        /// </summary>
        bool isCompleted { get; }
    }

    /// <summary>
    /// �����, �����������, ��� �� ������� ��� ���������� ������ ���������.
    /// </summary>
    public class LevelController : Singleton<LevelController>
    {

        #region Properties and Components

        /// <summary>
        /// ����� ����������� ������.
        /// </summary>
        [SerializeField] private int m_ReferenceTime;

        /// <summary>
        /// ������ �� ����� ����������� ������.
        /// </summary>
        public int ReferenceTime => m_ReferenceTime;

        /// <summary>
        /// �������, ������������� ����� ������� ��������.
        /// </summary>
        [SerializeField] private UnityEvent m_EventLevelCompleted;

        /// <summary>
        /// ������ � ��������� ��� ���������� ������.
        /// </summary>
        private ILevelCondition[] m_Conditions;

        /// <summary>
        /// ����������, ������������ true ���� ������� �������.
        /// </summary>
        private bool m_IsLevelCompleted;

        /// <summary>
        /// ������� ���������� �����.
        /// </summary>
        private float m_LevelTime;

        /// <summary>
        /// ������ �� ������� ���������� �����.
        /// </summary>
        public float LevelTime => m_LevelTime;

        #endregion


        #region Unity Events

        private void Start()
        {
            // ��������� ������ � ��������� ��������� ��������� � ������������ ILevelCondition.
            m_Conditions = GetComponentsInChildren<ILevelCondition>();
        }

        private void FixedUpdate()
        {
            // ���� ������� �� �������� - ���������� �����.
            if (!m_IsLevelCompleted)
            {
                m_LevelTime += Time.fixedDeltaTime;
            }

            // ��������� ������� ����������� ������.
            CheckLevelConditions();
        }

        #endregion


        #region Private API

        /// <summary>
        /// �����, �����������, ��� �� ������� ��� ���������� ������ ���������.
        /// </summary>
        private void CheckLevelConditions()
        {
            // �������� �� ������� ������� � ��������� � ��� ������������.
            if (m_Conditions == null || m_Conditions.Length == 0) return;

            // ����������, ������������ ������� ������� ���������.
            int numCompleted = 0;

            // ����������� �� ���� ��������, ������� ���-�� ����������� �������.
            foreach (var condition in m_Conditions)
            {
                if (condition.isCompleted) numCompleted++;
            }

            // ���� ��� ������� ���������.
            if (numCompleted == m_Conditions.Length)
            {
                // ������ ��������.
                m_IsLevelCompleted = true;

                // ����� ������� �� ���������� ������, �������� �� null.
                m_EventLevelCompleted?.Invoke();

                // ����� ������ ���������� ������.
                LevelSequenceController.Instance?.FinishCurrentLevel(true);
            }
        }

        #endregion

    }
}