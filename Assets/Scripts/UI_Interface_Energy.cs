using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter
{
    /// <summary>
    /// �����, ���������� �� ����������� ���� ������� �� �����.
    /// </summary>
    public class UI_Interface_Energy : Singleton<UI_Interface_Energy>
    {

        #region Properties and Components

        /// <summary>
        /// ���� ����� ������� ��� � ��������.
        /// </summary>
        [SerializeField] private Color m_LowEnergyColor;

        /// <summary>
        /// ������ �� �������� �� ������ �������.
        /// </summary>
        private Image m_ImageEnergyState;

        /// <summary>
        /// ��������� ���� �������.
        /// </summary>
        private Color m_ImageEnergyStartColor;

        #endregion


        #region Unity Events

        private void Start()
        {
            // ���� �������� � �������� �������
            m_ImageEnergyState = GetComponent<Image>();

            // ��������� ��������� �������� �����.
            m_ImageEnergyStartColor = m_ImageEnergyState.color;
        }

        private void FixedUpdate()
        {
            // �������� ��������� �������.
            UpdateEnergy();
        }

        #endregion


        #region Private API

        /// <summary>
        /// �����, ����������� �������� ������� � ����������.
        /// </summary>
        private void UpdateEnergy()
        {
            // �������� �� ������� ������ � ��������� �������.
            if (Player.Instance == null || Player.Instance.ActiveShip == null) return;

            // ����������� ��������� ������ �� �������� ������� ������ � ��� �������.
            SpaceShip activeShip = Player.Instance.ActiveShip;
            float currentEnergy = activeShip.CurrentEnergy;
            float maxEnergy = (float) activeShip.MaxEnergy;

            // ������� ��������������� �������� ������� �������.
            float currentEnergyNormalized = currentEnergy / maxEnergy;

            // �������� �������� � ����������� �� ��������� �������.
            m_ImageEnergyState.fillAmount = currentEnergyNormalized;

            // ���� ���-�� ������� 30%, �������� ���� ����� �������.
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