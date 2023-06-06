using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter
{
    /// <summary>
    /// Класс, отвечающий за отображение поля энергии на сцене.
    /// </summary>
    public class UI_Interface_Energy : Singleton<UI_Interface_Energy>
    {

        #region Properties and Components

        /// <summary>
        /// Цвет шкалы энергии при её нехватке.
        /// </summary>
        [SerializeField] private Color m_LowEnergyColor;

        /// <summary>
        /// Ссылка на картинку со шкалой энергии.
        /// </summary>
        private Image m_ImageEnergyState;

        /// <summary>
        /// Стартовый цвет энергии.
        /// </summary>
        private Color m_ImageEnergyStartColor;

        #endregion


        #region Unity Events

        private void Start()
        {
            // Берём картинку с текущего объекта
            m_ImageEnergyState = GetComponent<Image>();

            // Сохраняем начальное значение цвета.
            m_ImageEnergyStartColor = m_ImageEnergyState.color;
        }

        private void FixedUpdate()
        {
            // Обновить интерфейс энергии.
            UpdateEnergy();
        }

        #endregion


        #region Private API

        /// <summary>
        /// Метод, обновляющий значение энергии в интерфейсе.
        /// </summary>
        private void UpdateEnergy()
        {
            // Проверка на наличие игрока и активного корабля.
            if (Player.Instance == null || Player.Instance.ActiveShip == null) return;

            // Сохраняется локальная ссылка на активный корабль игрока и его энергию.
            SpaceShip activeShip = Player.Instance.ActiveShip;
            float currentEnergy = activeShip.CurrentEnergy;
            float maxEnergy = (float) activeShip.MaxEnergy;

            // Находим нормализованное значение текущей энергии.
            float currentEnergyNormalized = currentEnergy / maxEnergy;

            // Заливаем картинку в зависимости от состояния энергии.
            m_ImageEnergyState.fillAmount = currentEnergyNormalized;

            // Если кол-во энергии 30%, меняется цвет шкалы энергии.
            if (currentEnergyNormalized < 0.3f)
            {
                m_ImageEnergyState.color = m_LowEnergyColor;
            }
            else
            {
                m_ImageEnergyState.color = m_ImageEnergyStartColor;
            }
        }

        #endregion

    }
}