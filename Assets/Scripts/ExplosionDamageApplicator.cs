using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// �����, ��������� ���� �������� Destructible �� �����.
    /// </summary>
    public class ExplosionDamageApplicator : MonoBehaviour
    {

        #region Properties and Components

        /// <summary>
        /// ������, ����������� �������� ������.
        /// </summary>
        private Destructible m_Parent;

        /// <summary>
        /// ������, � ������� ��� ����� ������.
        /// </summary>
        private Destructible m_Target;

        /// <summary>
        /// ��������� ���� �� �������.
        /// </summary>
        private int m_ExplosionDamage;

        /// <summary>
        /// ����������, ������������, �������� �� �����.
        /// </summary>
        private bool m_TuneCheck;

        /// <summary>
        /// ����������, ������������, ��� �������� ������ �����.
        /// </summary>
        private bool isPlayer;

        #endregion


        #region Unity Events

        private void OnTriggerEnter2D(Collider2D collision)
        {
            // ���� ����� �� �������� - ����� �� ������.
            if (!m_TuneCheck) return;

            // ��������� Destructible �������������� �������.
            Destructible destructible = collision.transform.root.GetComponent<Destructible>();

            // ���� � �������� ��� Destructible,
            // ���� �������� ������ � ���������� � ��� ���, ��� ���������,
            // ���� ������  � ���������� ����� �� �� �������, ��� � ������������,
            // ���� ������ ��� ������� ���� �� ������� - �� ���������� �����.
            if (destructible == null || destructible == m_Parent || destructible.TeamID == m_Parent.TeamID || destructible == m_Target) return;

            // ������� ���� �������.
            destructible.ApplyDamage(m_ExplosionDamage);

            //���� ������������ ������� - �����.
            if (isPlayer)
            {
                // ���� ������ ���������.
                if (destructible.CurrentHitPoints <= 0)
                {
                    // ��������� ����.
                    Player.Instance.AddScore(destructible.ScoreValue);
                    GameStatistics.Instance?.AddScore(destructible.ScoreValue);

                    // ������ ������ �� �������.
                    SpaceShip ship = destructible.GetComponent<SpaceShip>();

                    // ���� ��������� � ������� - �������� ��������.
                    if (ship != null)
                    {
                        Player.Instance.AddKill();
                        GameStatistics.Instance?.AddKill();
                    }

                    // ������ ������ �� ��������.
                    Asteroid asteroid = destructible.GetComponent<Asteroid>();

                    // ���� ��������� � �������� - ���������� � ����������� ����������.
                    if (asteroid != null)
                    {
                        GameStatistics.Instance?.AsteroidDestroyed(asteroid.AsteroidSize);
                    }
                }
            }
        }

        #endregion


        #region Public API

        /// <summary>
        /// ����� ��������� ������ �� �������.
        /// </summary>
        /// <param name="parent">Destructible ������������� �������.</param>
        /// <param name="target">Destructible �������, � �������� ����� ������.</param>
        /// <param name="position">������� �������� ������.</param>
        /// <param name="damage">���� �� ������.</param>
        public void TuneExplosion(Destructible parent, Destructible target, Vector3 position, int damage)
        {
            m_Parent = parent;
            m_Target = target;
            transform.position = position;
            m_ExplosionDamage = damage;
            m_TuneCheck = true;

            // ���� ��������� ����� - ������ �������.
            if (m_Parent == Player.Instance.ActiveShip) isPlayer = true;
        }

        #endregion

    }
}