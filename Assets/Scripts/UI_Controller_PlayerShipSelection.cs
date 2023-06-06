using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace SpaceShooter
{
    /// <summary>
    /// �����, ���������� �� ����������� �������� ������� � ���� ������ �������.
    /// </summary>
    public class UI_Controller_PlayerShipSelection : MonoBehaviour
    {

        #region Properties and Components

        /// <summary>
        /// ������ �������.
        /// </summary>
        [SerializeField] private SpaceShip m_Prefab;

        /// <summary>
        /// ������ �� ������ �������.
        /// </summary>
        [SerializeField] public SpaceShip Prefab => m_Prefab;

        /// <summary>
        /// ������ �� Image � �������� �������.
        /// </summary>
        [SerializeField] private Image m_PreviewImage;

        /// <summary>
        /// ������ �� ������ � ������ �������.
        /// </summary>
        [SerializeField] private TextMeshProUGUI m_Shipname;

        /// <summary>
        /// ������ �� ������ � HP �������.
        /// </summary>
        [SerializeField] private TextMeshProUGUI m_Hitpoints;

        /// <summary>
        /// ������ �� ������ �� ��������� �������.
        /// </summary>
        [SerializeField] private TextMeshProUGUI m_Speed;

        /// <summary>
        /// ������ �� ������ � �������������� �������.
        /// </summary>
        [SerializeField] private TextMeshProUGUI m_Agility;

        /// <summary>
        /// ������ �� ������ � ������� ���������� �������.
        /// </summary>
        [SerializeField] private TextMeshProUGUI m_Thrust;

        /// <summary>
        /// ������ �� ������ � ������� ���������� �������.
        /// </summary>
        [SerializeField] private TextMeshProUGUI m_Mobility;

        /// <summary>
        /// ������ �� ������ � �������� �������.
        /// </summary>
        [SerializeField] private TextMeshProUGUI m_Energy;

        /// <summary>
        /// ������ �� ������ � ������ �������.
        /// </summary>
        [SerializeField] private TextMeshProUGUI m_Mass;

        /// <summary>
        /// ������ �� ������ � ������� �������.
        /// </summary>
        [SerializeField] private TextMeshProUGUI m_EnergyRegenPerSecond;

        /// <summary>
        /// ������ �� ������ � �������� ���� ��������.
        /// </summary>
        private Button[] m_Buttons;

        #endregion


        #region Unity Events

        private void Start()
        {
            // �������� �� ������� �������.
            if (m_Prefab == null) return;

            // ������� �������� ��������� �������.
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

            // ���������� ������� ��������.
            m_Buttons = UI_Controller_PlayerShipSelectMenu.Instance.Buttons;
        }

        #endregion


        #region Public API

        /// <summary>
        /// �����, ������������ ������ ��� �������� ���������� ����� "select".
        /// </summary>
        public void ReturnPreviousValueButtons()
        {
            // �������� �� ������� ������� � ��������.
            if (m_Buttons == null || m_Buttons.Length == 0) return;

            // ���� �� ���� �������, ���������� ������������ ��������.
            for (int i = 0; i < m_Buttons.Length; i++)
            {
                m_Buttons[i].GetComponentInChildren<TextMeshProUGUI>().text = "select";
            }
        }

        /// <summary>
        /// ����� ������� �� ������ ������ �������.
        /// </summary>
        public void OnSelectShip()
        {
            // �������� ������� ������� �� ���������.
            LevelSequenceController.PlayerShip = m_Prefab;
        }

        #endregion

    }
}