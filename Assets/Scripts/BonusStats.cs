using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// ����� ���������� ������ ������� � ��������.
    /// </summary>
    public class BonusStats : Bonus
    {

        #region Properties And Components

        /// <summary>
        /// ������ �������� ������� ���������� ����.
        /// </summary>
        public enum EffectType
        {
            AddEnergy,
            AddAmmo
        }

        /// <summary>
        /// ����� ���� ������� �� ����������.
        /// </summary>
        [SerializeField] private EffectType m_EffectType;

        /// <summary>
        /// �������� ������.
        /// </summary>
        [SerializeField] private float m_Value;

        #endregion


        #region Protected API

        /// <summary>
        /// �����, ����������� ������ ������� � �������.
        /// </summary>
        /// <param name="ship">������� ������.</param>
        protected override void OnPickedUp(SpaceShip ship)
        {
            switch (m_EffectType)
            {
                // �������� ������� ���������� �������.
                case EffectType.AddEnergy:
                    ship.AddEnergy((int) m_Value);
                    break;

                // �������� ������� ���������� ��������.
                case EffectType.AddAmmo:
                    ship.AddAmmo((int)m_Value);
                    break;
            }
        }

        #endregion

    }
}