using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SpaceShooter
{
    /// <summary>
    /// �����, �������������� �������� ����� ��������.
    /// </summary>
    public class LevelSequenceController : Singleton<LevelSequenceController>
    {

        #region Properties and Components

        /// <summary>
        /// ��������� ������, �������� � ���� ��� ����� �������� ����.
        /// </summary>
        public static string MainMenuSceneNickName = "main_menu";

        /// <summary>
        /// ����� Episode �������� �������.
        /// </summary>
        public SO_Episode CurrentEpisode { get; private set; }

        /// <summary>
        /// �������� �������� ������.
        /// </summary>
        public int CurrentLevel { get; private set; }

        /// <summary>
        /// ����������, ������������, �������� �� �������.
        /// </summary>
        public bool LastLevelResult { get; private set; }

        /// <summary>
        /// ������ �� ������� ������ ������� ������.
        /// </summary>
        public static SpaceShip PlayerShip { get; set; }

        #endregion


        #region Public API

        /// <summary>
        /// ����� ������� �������.
        /// </summary>
        /// <param name="episode">Episode, ������� ���������� ���������.</param>
        public void StartEpisode(SO_Episode episode)
        {
            // ����� �������� �������� ������� � ������.
            CurrentEpisode = episode;
            CurrentLevel = 0;

            // ��������� �����.
            SceneManager.LoadScene(episode.Levels[CurrentLevel]);
        }

        /// <summary>
        /// ����� ����������� �������� ������.
        /// </summary>
        public void RestartLevel()
        {
            // �������� ���������� � ������ Player.
            if (Player.Instance != null) Player.Instance.Restart();

            // ������������� �����.
            SceneManager.LoadScene(CurrentEpisode.Levels[CurrentLevel]);
        }

        /// <summary>
        /// �����, ������������� ��� ���������� ������.
        /// </summary>
        /// <param name="success">true ���� ������� ��������, false ���� �� ��������.</param>
        public void FinishCurrentLevel(bool success)
        {
            // ����������� ���������� ������.
            LastLevelResult = success;

            // ������������ ���������� ������.
            UI_Controller_ResultPanel.Instance.ShowResults(success);
        }

        /// <summary>
        /// ����� ������� ���������� ������.
        /// </summary>
        public void AdvanceLevel()
        {
            // �������� ���������� � ������ Player.
            if (Player.Instance != null) Player.Instance.Restart();

            // ������� �������++.
            CurrentLevel++;

            // ���� ������ � ������� ��������� - ���������� � ������� ����.
            if (CurrentEpisode.Levels.Length <= CurrentLevel) SceneManager.LoadScene(MainMenuSceneNickName);
            //���� �� ��������� - ��������� ��������� �������.
            else SceneManager.LoadScene(CurrentEpisode.Levels[CurrentLevel]);
        }

        #endregion

    }
}