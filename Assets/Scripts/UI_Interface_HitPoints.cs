using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter
{
    /// <summary>
    /// �����, ���������� �� ����������� ������ � ����������.
    /// </summary>
    public class UI_Interface_HitPoints : Singleton<UI_Interface_HitPoints>
    {

        #region Properties and Components

        /// <summary>
        /// ������ �� ������ � �������.
        /// </summary>
        [SerializeField] private Image[] m_ImagesHearth;

        /// <summary>
        /// ���������� �������� ������.
        /// </summary>
        private int m_LastCurrentLives;

        /// <summary>
        /// ���������� �������� HP.
        /// </summary>
        private int m_LastCurrentHP;

        #endregion


        #region Unity Events

        private void FixedUpdate()
        {
            // �������� ��������� ������.
            UpdateHP();
        }

        #endregion


        #region Private API

        /// <summary>
        /// �����, ����������� ��������� ������.
        /// </summary>
        private void UpdateHP()
        {
            // �������� �� ������� ������.
            if (Player.Instance == null || Player.Instance.ActiveShip == null || m_ImagesHearth == null || m_ImagesHearth.Length != 3 || Player.NumberLives != 3) return;

            // ������ ��������� ������ �� �������� ������� ������, ����������� ���������� ������.
            SpaceShip activeShip = Player.Instance.ActiveShip;
            int shipMaxHP = activeShip.HitPoints;
            int shipCurrentHP = activeShip.CurrentHitPoints;
            int shipLives = Player.Instance.CurrentLives;

            // ��������� ��������������� �������� ������.
            float shipHitPointsNormalized = (float) shipCurrentHP / (float) shipMaxHP;

            // ����� �� ������, ���� ���-�� ������ �� �����������
            if (m_LastCurrentLives == shipCurrentHP && m_LastCurrentHP == shipCurrentHP) return;
            m_LastCurrentLives = shipLives;
            m_LastCurrentHP = shipCurrentHP;

            // ��������� ��������� � ����������� �� ���-�� ������.
            switch (shipLives)
            {
                case 3:
                    for (int i = 0; i < Player.NumberLives; i++)
                    {
                        if (i < 2) m_ImagesHearth[i].fillAmount = 1;
                        else m_ImagesHearth[i].fillAmount = shipHitPointsNormalized;
                    }
                    break;

                case 2:
                    for (int i = 0; i < Player.NumberLives; i++)
                    {
                        if (i == 0) m_ImagesHearth[i].fillAmount = 1;
                        if (i == 1) m_ImagesHearth[i].fillAmount = shipHitPointsNormalized;
                        if (i == 2) m_ImagesHearth[i].fillAmount = 0;
                    }
                    break;

                case 1:
                    for (int i = 0; i < Player.NumberLives; i++)
                    {
                        if (i > 0) m_ImagesHearth[i].fillAmount = 0;
                        else m_ImagesHearth[i].fillAmount = 1;
                    }
                    break;

                case 0:
                    for (int i = 0; i < Player.NumberLives; i++)
                    {
                        m_ImagesHearth[i].fillAmount = 0;
                    }
                    break;
            }
        }

        #endregion

    }
}