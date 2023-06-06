using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace SpaceShooter
{
    /// <summary>
    /// Класс, обновляющий интерфейс аммо.
    /// </summary>
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class UI_Interface_Ammo : MonoBehaviour
    {

        #region Properties and Components

        /// <summary>
        /// Ссылка на текущее текстовое поле с аммо.
        /// </summary>
        private TextMeshProUGUI m_AmmoText;

        /// <summary>
        /// Последнее сохранённое значение оружия.
        /// </summary>
        private int m_LastAmmo;

        #endregion


        #region Unity Events

        private void Start()
        {
            // Сохраняет стартовое значение боезапаса.
            if (Player.Instance.ActiveShip != null) m_LastAmmo = Player.Instance.ActiveShip.CurrentAmmo;

            // Записываем ссылку на текстовое поле.
            m_AmmoText = GetComponent<TextMeshProUGUI>();
        }

        private void FixedUpdate()
        {
            // Обновляет интерфейс аммо.
            UpdateAmmo();
        }

        #endregion


        #region Public API

        /// <summary>
        /// Метод, обновляющий отображение аммо в интерфейсе.
        /// </summary>
        private void UpdateAmmo()
        {
            // Сохранённая ссылка на активный корабль.
            SpaceShip activeShip = Player.Instance.ActiveShip;

            // Проверка на наличие корабля и обновлялось ли значение аммо.
            if (activeShip == null || activeShip.CurrentAmmo == m_LastAmmo) return;

            // Сохраняем значение аммо.
            m_LastAmmo = activeShip.CurrentAmmo;

            // Обновляем интерфейс.
            m_AmmoText.text = m_LastAmmo.ToString();
        }

        #endregion

    }
}