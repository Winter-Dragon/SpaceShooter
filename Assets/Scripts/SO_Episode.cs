using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// Класс, отвечающий за хранение эпизода с уровнями.
    /// </summary>
    [CreateAssetMenu(fileName = "Episode", menuName = "ScriptableObjects/CreateNewEpisode")]
    public class SO_Episode : ScriptableObject
    {

        #region Properties and Components

        /// <summary>
        /// Название эпизода.
        /// </summary>
        [SerializeField] private string m_EpisodeName;

        /// <summary>
        /// Ссылка на имя эпизода.
        /// </summary>
        public string EpisodeName => m_EpisodeName;

        /// <summary>
        /// Массив со строками названий уровней.
        /// !!! Уровни должны быть загружены в билд.
        /// </summary>
        [SerializeField] private string[] m_Levels;

        /// <summary>
        /// Ссылка на массив с уровнями.
        /// </summary>
        public string[] Levels => m_Levels;

        /// <summary>
        /// Спрайт превью эпизода, отображается в гл меню.
        /// </summary>
        [SerializeField] private Sprite m_PreviewImage;

        /// <summary>
        /// Ссылка на спрайт превью эпизода.
        /// </summary>
        public Sprite PreviewImage => m_PreviewImage;

        #endregion

    }
}