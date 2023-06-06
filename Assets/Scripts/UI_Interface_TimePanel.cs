using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SpaceShooter
{
    /// <summary>
    /// �����, ���������� �� ����������� ������ �� �������� � ����������. ������ ��������� ����� �� �������.
    /// </summary>
    public class UI_Interface_TimePanel : Singleton<UI_Interface_TimePanel>
    {

        #region Properties and Components

        /// <summary>
        /// ������ �� �������� ����� ������ ������� X3.
        /// </summary>
        [SerializeField] private Image m_TimePanelScoreX3;

        /// <summary>
        /// ������ �� �������� ����� ������ ������� X2.
        /// </summary>
        [SerializeField] private Image m_TimePanelScoreX2;

        /// <summary>
        /// ������ �� ������ ��������� �����.
        /// </summary>
        [SerializeField] private TextMeshProUGUI m_ScoreMultiplierText;

        /// <summary>
        /// ����� ������, �� ������� ������ �������� ����.
        /// </summary>
        private float m_levelTIme;

        /// <summary>
        /// ��������� ������.
        /// </summary>
        private float m_LocalTimer;

        /// <summary>
        /// ����������, ������������, ���������� �� ������.
        /// </summary>
        private bool m_TimeIsOver;

        /// <summary>
        /// �������� ��������� �����.
        /// </summary>
        public static int ScoreMultiplier;

        #endregion


        #region Unity Events

        private void Start()
        {
            // ������� ����� ������.
            m_levelTIme = LevelController.Instance.ReferenceTime;
            // ��������� ������������� ������ �� ��������.
            m_TimePanelScoreX2.fillAmount = 1;
            m_TimePanelScoreX3.fillAmount = 1;
            // ��������� �������.
            m_LocalTimer = m_levelTIme;
            // ��������� ����� �3.
            ScoreMultiplier = 3;
        }

        private void FixedUpdate()
        {
            if (!m_TimeIsOver)
            {
                UpdateTimePanel();
                UpdateScoreMultiplierPanel();
            }
        }

        #endregion


        #region Private API

        /// <summary>
        /// �����, ����������� ������ �� ��������.
        /// </summary>
        private void UpdateTimePanel()
        {
            m_LocalTimer -= Time.fixedDeltaTime;
            
            float fillPanelX3 = (m_LocalTimer - (m_levelTIme / 2)) / (m_levelTIme / 2);

            // �������� ����� �� ������ �������� ������.
            if (fillPanelX3 >= 0)
            {
                m_TimePanelScoreX3.fillAmount = fillPanelX3;
            }
            // ����� ������ �������� ������
            else
            {
                m_TimePanelScoreX3.fillAmount = 0;
                ScoreMultiplier = 2;

                float fillPanelX2 = m_LocalTimer / (m_levelTIme / 2);

                if (fillPanelX2 >= 0)
                {
                    m_TimePanelScoreX2.fillAmount = fillPanelX2;
                }
                // ����� ����� �����������
                else
                {
                    m_TimePanelScoreX2.fillAmount = 0;

                    m_TimeIsOver = true;
                    ScoreMultiplier = 1;
                }
            }
        }

        /// <summary>
        /// �����, �������� ����������� ��������� ���������� ��������� �����.
        /// </summary>
        private void UpdateScoreMultiplierPanel()
        {
            switch (ScoreMultiplier)
            {
                case 3:
                    m_ScoreMultiplierText.text = "x3";
                    break;

                case 2:
                    m_ScoreMultiplierText.text = "x2";
                    break;

                case 1:
                    m_ScoreMultiplierText.gameObject.SetActive(false);
                    break;
            }
        }

        #endregion

    }
}