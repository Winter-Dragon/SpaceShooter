using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SpaceShooter
{
    /// <summary>
    /// Класс, отвечающий за отображение и запуск конкретного эпизода в меню.
    /// </summary>
    public class UI_Controller_EpisodeSelection : MonoBehaviour
    {

        #region Properties and Components

        /// <summary>
        /// Ссылка на SO эпизода.
        /// </summary>
        [SerializeField] private SO_Episode m_Episode;

        /// <summary>
        /// Ссылка на текстовую строку с именем эпизода.
        /// </summary>
        [SerializeField] private TextMeshProUGUI m_EpisodeNickName;

        /// <summary>
        /// Ссылка на картинку эпизода в меню.
        /// </summary>
        [SerializeField] private Image m_PreviewImage;

        #endregion


        #region Unity Events

        private void Start()
        {
            // Задаются имя и превью эпизода из СО.
            if (m_EpisodeNickName != null) m_EpisodeNickName.text = m_Episode.EpisodeName;
            if (m_PreviewImage != null) m_PreviewImage.sprite = m_Episode.PreviewImage;
        }

        #endregion


        #region Public API

        /// <summary>
        /// Метод, обрабатывающий запуск выбранного эпизода.
        /// </summary>
        public void OnStartEpisodeButtonClicked()
        {
            LevelSequenceController.Instance.StartEpisode(m_Episode);
        }

        #endregion

    }
}