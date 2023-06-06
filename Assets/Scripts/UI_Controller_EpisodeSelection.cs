using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SpaceShooter
{
    /// <summary>
    /// �����, ���������� �� ����������� � ������ ����������� ������� � ����.
    /// </summary>
    public class UI_Controller_EpisodeSelection : MonoBehaviour
    {

        #region Properties and Components

        /// <summary>
        /// ������ �� SO �������.
        /// </summary>
        [SerializeField] private SO_Episode m_Episode;

        /// <summary>
        /// ������ �� ��������� ������ � ������ �������.
        /// </summary>
        [SerializeField] private TextMeshProUGUI m_EpisodeNickName;

        /// <summary>
        /// ������ �� �������� ������� � ����.
        /// </summary>
        [SerializeField] private Image m_PreviewImage;

        #endregion


        #region Unity Events

        private void Start()
        {
            // �������� ��� � ������ ������� �� ��.
            if (m_EpisodeNickName != null) m_EpisodeNickName.text = m_Episode.EpisodeName;
            if (m_PreviewImage != null) m_PreviewImage.sprite = m_Episode.PreviewImage;
        }

        #endregion


        #region Public API

        /// <summary>
        /// �����, �������������� ������ ���������� �������.
        /// </summary>
        public void OnStartEpisodeButtonClicked()
        {
            LevelSequenceController.Instance.StartEpisode(m_Episode);
        }

        #endregion

    }
}