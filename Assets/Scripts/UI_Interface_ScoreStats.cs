using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SpaceShooter
{
    /// <summary>
    /// �����, ����������� ��������� ���� Score.
    /// </summary>
    public class UI_Interface_ScoreStats : Singleton<UI_Interface_ScoreStats>
    {

        #region Properties and Components

        /// <summary>
        /// ������ �� ����� TMP.
        /// </summary>
        private TextMeshProUGUI m_ScoreText;

        /// <summary>
        /// ����������, �������� � ���� ��������� ���-�� ����� ������.
        /// </summary>
        private int m_LastScore;

        #endregion


        #region Unity Events

        private void Start()
        {
            // ����� ������ �� TMP � �������� �������.
            m_ScoreText = GetComponent<TextMeshProUGUI>();
        }

        private void FixedUpdate()
        {
            // �������� ���-�� �����.
            UpdateScore();
        }

        #endregion


        #region Private API

        /// <summary>
        /// �����, ����������� ������� �������� ����� � �������� ����������� � ����������.
        /// </summary>
        private void UpdateScore()
        {
            // �������� �� ������� ������ �� �����.
            if (Player.Instance == null) return;

            // ������������ ��������� ���������� ���-�� ����� � ������.
            int currentScore = Player.Instance.Score;

            // ���� ���-�� ����� ����������, ��������� ��������� � ������.
            if (m_LastScore != currentScore)
            {
                m_LastScore = currentScore;

                m_ScoreText.text = m_LastScore.ToString();
            }
        }

        #endregion

    }
}