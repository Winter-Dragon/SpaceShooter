using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace SpaceShooter
{
    /// <summary>
    /// �����, ����������� ��������� ����.
    /// </summary>
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class UI_Interface_Ammo : MonoBehaviour
    {

        #region Properties and Components

        /// <summary>
        /// ������ �� ������� ��������� ���� � ����.
        /// </summary>
        private TextMeshProUGUI m_AmmoText;

        /// <summary>
        /// ��������� ���������� �������� ������.
        /// </summary>
        private int m_LastAmmo;

        #endregion


        #region Unity Events

        private void Start()
        {
            // ��������� ��������� �������� ���������.
            if (Player.Instance.ActiveShip != null) m_LastAmmo = Player.Instance.ActiveShip.CurrentAmmo;

            // ���������� ������ �� ��������� ����.
            m_AmmoText = GetComponent<TextMeshProUGUI>();
        }

        private void FixedUpdate()
        {
            // ��������� ��������� ����.
            UpdateAmmo();
        }

        #endregion


        #region Public API

        /// <summary>
        /// �����, ����������� ����������� ���� � ����������.
        /// </summary>
        private void UpdateAmmo()
        {
            // ���������� ������ �� �������� �������.
            SpaceShip activeShip = Player.Instance.ActiveShip;

            // �������� �� ������� ������� � ����������� �� �������� ����.
            if (activeShip == null || activeShip.CurrentAmmo == m_LastAmmo) return;

            // ��������� �������� ����.
            m_LastAmmo = activeShip.CurrentAmmo;

            // ��������� ���������.
            m_AmmoText.text = m_LastAmmo.ToString();
        }

        #endregion

    }
}