using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// ����� � �������� ��� ����������� ������ (���-�� ��������� �����).
    /// </summary>
    public class LevelConditionScore : MonoBehaviour, ILevelCondition
    {

        #region Properties and Components

        /// <summary>
        /// ����������� ���-�� ����� ��� ���������� ������.
        /// </summary>
        [SerializeField] private int m_Score;

        /// <summary>
        /// ���������� true, ���� ������� ����������.
        /// </summary>
        private bool m_IsReached;

        /// <summary>
        /// ���������� ��������� ����������, ������������ ��������� �� ������� � ILevelCondition.
        /// </summary>
        bool ILevelCondition.isCompleted
        {
            get
            {
                // �������� �� ������.
                if (Player.Instance != null && Player.Instance != null)
                {
                    // ���� ���-�� ����� ���������� ��� ���������� �������, ������� true.
                    if (Player.Instance.Score >= m_Score)
                    {
                        m_IsReached = true;
                    }
                }

                return m_IsReached;
            }
        }

        #endregion

    }
}