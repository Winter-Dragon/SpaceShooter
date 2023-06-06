using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// �����, ���������� �� �������� ������� � ��������.
    /// </summary>
    [CreateAssetMenu(fileName = "Episode", menuName = "ScriptableObjects/CreateNewEpisode")]
    public class SO_Episode : ScriptableObject
    {

        #region Properties and Components

        /// <summary>
        /// �������� �������.
        /// </summary>
        [SerializeField] private string m_EpisodeName;

        /// <summary>
        /// ������ �� ��� �������.
        /// </summary>
        public string EpisodeName => m_EpisodeName;

        /// <summary>
        /// ������ �� �������� �������� �������.
        /// !!! ������ ������ ���� ��������� � ����.
        /// </summary>
        [SerializeField] private string[] m_Levels;

        /// <summary>
        /// ������ �� ������ � ��������.
        /// </summary>
        public string[] Levels => m_Levels;

        /// <summary>
        /// ������ ������ �������, ������������ � �� ����.
        /// </summary>
        [SerializeField] private Sprite m_PreviewImage;

        /// <summary>
        /// ������ �� ������ ������ �������.
        /// </summary>
        public Sprite PreviewImage => m_PreviewImage;

        #endregion

    }
}