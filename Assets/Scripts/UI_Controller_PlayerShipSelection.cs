using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace SpaceShooter
{
    /// <summary>
    /// Класс, отвечающий за отображение карточки корабля в меню выбора корабля.
    /// </summary>
    public class UI_Controller_PlayerShipSelection : MonoBehaviour
    {

        #region Properties and Components

        /// <summary>
        /// Префаб корабля.
        /// </summary>
        [SerializeField] private SpaceShip m_Prefab;

        /// <summary>
        /// Ссылка на префаб корабля.
        /// </summary>
        [SerializeField] public SpaceShip Prefab => m_Prefab;

        /// <summary>
        /// Ссылка на Image у картинки корабля.
        /// </summary>
        [SerializeField] private Image m_PreviewImage;

        /// <summary>
        /// Ссылка на строку с именем корабля.
        /// </summary>
        [SerializeField] private TextMeshProUGUI m_Shipname;

        /// <summary>
        /// Ссылка на строку с HP корабля.
        /// </summary>
        [SerializeField] private TextMeshProUGUI m_Hitpoints;

        /// <summary>
        /// Ссылка на строку со скоростью корабля.
        /// </summary>
        [SerializeField] private TextMeshProUGUI m_Speed;

        /// <summary>
        /// Ссылка на строку с управляемостью корабля.
        /// </summary>
        [SerializeField] private TextMeshProUGUI m_Agility;

        /// <summary>
        /// Ссылка на строку с угловым ускорением корабля.
        /// </summary>
        [SerializeField] private TextMeshProUGUI m_Thrust;

        /// <summary>
        /// Ссылка на строку с угловым ускорением корабля.
        /// </summary>
        [SerializeField] private TextMeshProUGUI m_Mobility;

        /// <summary>
        /// Ссылка на строку с энергией корабля.
        /// </summary>
        [SerializeField] private TextMeshProUGUI m_Energy;

        /// <summary>
        /// Ссылка на строку с массой корабля.
        /// </summary>
        [SerializeField] private TextMeshProUGUI m_Mass;

        /// <summary>
        /// Ссылка на строку с регеном энергии.
        /// </summary>
        [SerializeField] private TextMeshProUGUI m_EnergyRegenPerSecond;

        /// <summary>
        /// Ссылка на массив с кнопками всех карточек.
        /// </summary>
        private Button[] m_Buttons;

        #endregion


        #region Unity Events

        private void Start()
        {
            // Проверка на наличие префаба.
            if (m_Prefab == null) return;

            // Задаётся значение текстовым строкам.
            m_PreviewImage.sprite = m_Prefab.ShipSprite;
            m_Shipname.text = m_Prefab.Nickname;
            m_Hitpoints.text = m_Prefab.HitPoints.ToString();
            m_Speed.text = m_Prefab.MaxLinearVelocity.ToString();
            m_Agility.text = m_Prefab.MaxAngularVelocity.ToString();
            m_Thrust.text = m_Prefab.Thrust.ToString();
            m_Mobility.text = m_Prefab.Mobility.ToString();
            m_Energy.text = m_Prefab.MaxEnergy.ToString();
            m_Mass.text = m_Prefab.Mass.ToString();
            m_EnergyRegenPerSecond.text = m_Prefab.EnergyRegenPerSecond.ToString();

            // Наполнение массива кнопками.
            m_Buttons = UI_Controller_PlayerShipSelectMenu.Instance.Buttons;
        }

        #endregion


        #region Public API

        /// <summary>
        /// Метод, возвращающий тексту под кнопками предыдущий текст "select".
        /// </summary>
        public void ReturnPreviousValueButtons()
        {
            // Проверка на наличие массива с кнопками.
            if (m_Buttons == null || m_Buttons.Length == 0) return;

            // Цикл по всем кнопкам, присвоение стандартного значения.
            for (int i = 0; i < m_Buttons.Length; i++)
            {
                m_Buttons[i].GetComponentInChildren<TextMeshProUGUI>().text = "select";
            }
        }

        /// <summary>
        /// Метод нажатия на кнопку выбора корабля.
        /// </summary>
        public void OnSelectShip()
        {
            // Заменяет игровой корабль на выбранный.
            LevelSequenceController.PlayerShip = m_Prefab;
        }

        #endregion

    }
}