using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

namespace SpaceShooter
{
    /// <summary>
    /// ����� ���������� ������ �����������.
    /// </summary>
    public class UI_Controller_ResultPanel : Singleton<UI_Controller_ResultPanel>
    {

        #region Properties and Components

        /// <summary>
        /// ��������� ���� ���������� ������.
        /// </summary>
        [SerializeField] private TextMeshProUGUI m_Result;

        /// <summary>
        /// ��������� ���� �����.
        /// </summary>
        [SerializeField] private TextMeshProUGUI m_Score;

        /// <summary>
        /// ��������� ���� �������.
        /// </summary>
        [SerializeField] private TextMeshProUGUI m_Kills;

        /// <summary>
        /// ��������� ���� ������� ������.
        /// </summary>
        [SerializeField] private TextMeshProUGUI m_Time;

        /// <summary>
        /// ��������� ���� ������.
        /// </summary>
        [SerializeField] private TextMeshProUGUI m_ButtonText;

        /// <summary>
        /// ��������� ����������, �������� �� �������.
        /// </summary>
        private bool m_Success;

        #endregion


        #region Unity Events

        private void Start()
        {
            // ��� ������ ������ ����.
            gameObject.SetActive(false);
        }

        #endregion


        #region Public API

        /// <summary>
        /// ����� ����������� ����������� ������.
        /// </summary>
        /// <param name="success">true ���� ������� �������, false �� �������.</param>
        public void ShowResults(bool success)
        {
            // �������� �� ������.
            if (Player.Instance == null) return;

            // ������� ����.
            gameObject.SetActive(true);

            // �������� ���������� ����������� ������.
            m_Success = success;
            int score = Player.Instance.Score;
            int kills = Player.Instance.NumberKills;
            string time = UI_Interface_TimeStats.Instance.TimeText;

            // �������� �������� ����� � ������������ � �������� � ����������.
            GameStatistics.Instance.CompareBestScore(score * Player.ScoreMultiplier);

            // �������� ����������/����������� ������� � ����������.
            if (success) GameStatistics.Instance.AddCompletedLevel();
            else GameStatistics.Instance.AddLoseLevel();

            // �������� ��������� ���� � ����������� �� ����������� ������.
            m_Result.text = success ? "you're win" : "you're lose";
            m_Score.text = (score * Player.ScoreMultiplier).ToString();
            m_Kills.text = kills.ToString();
            m_Time.text = time;
            m_ButtonText.text = success ? "Next" : "Restart";

            // ���������� �����.
            Time.timeScale = 0;
        }

        /// <summary>
        /// ����� ������� �� ������ next/restart.
        /// </summary>
        public void ClickButtonNext()
        {
            // ������� ����.
            gameObject.SetActive(false);

            // ����������� �����.
            Time.timeScale = 1;

            // ���� ������ - ��������� ��������� �������, ���� ��������� - ������������� ������� �������.
            if (m_Success) LevelSequenceController.Instance.AdvanceLevel();
            else LevelSequenceController.Instance.RestartLevel();
        }

        /// <summary>
        /// ����� ������� �� ������ ������ � ������� ����.
        /// </summary>
        public void ClickButtonMainMenu()
        {
            // ����������� �����, ������ ���� ����������� � ��������� ����� �������� ����.
            Time.timeScale = 1;
            gameObject.SetActive(false);
            SceneManager.LoadScene(LevelSequenceController.MainMenuSceneNickName);
        }

        #endregion

    }
}