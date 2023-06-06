using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace SpaceShooter
{
    /// <summary>
    /// �����, ����������� ��������� ���� Time � ����������.
    /// </summary>
    public class UI_Interface_TimeStats : Singleton<UI_Interface_TimeStats>
    {

        #region Properties and Components

        /// <summary>
        /// ������ �� ��������� ���� �������.
        /// </summary>
        private TextMeshProUGUI m_TimeText;

        /// <summary>
        /// ������ �� ������ �� ��������.
        /// </summary>
        public string TimeText => m_TimeText.text;

        /// <summary>
        /// ���������� ������� �������� ������.
        /// </summary>
        private int m_LastSeconds;

        #endregion


        #region Unity Events

        private void Start()
        {
            // ������� ��������� ���� �� �������.
            m_TimeText = GetComponent<TextMeshProUGUI>();
        }

        private void FixedUpdate()
        {
            // ��������� ��������� ����.
            UpdateScore();
        }

        #endregion


        #region Private API

        /// <summary>
        /// �����, ����������� ��������� ������� � ����.
        /// </summary>
        private void UpdateScore()
        {
            // �������� �� ������� LeveController �� �����.
            if (LevelController.Instance == null) return;

            // ������������ ��������� ���������� ���������� ������ ������.
            int currentSeconds = (int) LevelController.Instance.LevelTime;

            // ����� �� ������, ���� ������� �� ����������.
            if (m_LastSeconds == currentSeconds) return;
            m_LastSeconds = currentSeconds;

            // ���������� ��������������� �������� ������� � ������.
            m_TimeText.text = GetNormalizedTime(currentSeconds);
        }

        /// <summary>
        /// �����, ������������ ��������������� �������� ������� �� ������ � ������� ������.
        /// </summary>
        /// <param name="seconds">�������� ������� � ��������.</param>
        /// <returns>���������� ��������������� �������� ������� � ������� 00:00.</returns>
        private string GetNormalizedTime(float seconds)
        {
            string normalizedTime;

            // ������������ ��������� ���������� ���������� ����� ������.
            int currentMinuts = (int) (seconds / 60);

            // ���� ������ �� ������.
            if (currentMinuts == 0)
            {
                // ���� ������� ������� �� 1 �����.
                if (seconds < 10)
                {
                    normalizedTime = "00 : 0" + seconds.ToString();
                }
                // ���� ����� ����� �����.
                else
                {
                    normalizedTime = "00 : " + seconds.ToString();
                }
            }
            // ���� ������ ������.
            else
            {
                // ������������� �������.
                seconds = seconds - (currentMinuts * 60);

                // ���� ������ ������� �� 1 �����.
                if (currentMinuts < 10)
                {
                    // ���� ������� ������� �� 1 �����.
                    if (seconds < 10)
                    {
                        normalizedTime = "0" + currentMinuts.ToString() + " : 0" + seconds.ToString();
                    }
                    // ���� ����� ����� ����� � ��������.
                    else
                    {
                        normalizedTime = "0" + currentMinuts.ToString() + " : " + seconds.ToString();
                    }
                }
                // ���� ����� 1 ����� � �������.
                else
                {
                    // ���� ������� ������� �� 1 �����.
                    if (seconds < 10)
                    {
                        normalizedTime = currentMinuts.ToString() + " : 0" + seconds.ToString();
                    }
                    // ���� ����� ����� ����� � ��������.
                    else
                    {
                        normalizedTime = currentMinuts.ToString() + " : " + seconds.ToString();
                    }
                }
            }

            // ���������� ��������������� �����.
            return normalizedTime;
        }

        #endregion

    }
}