using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class Asteroid : Destructible
    {

        #region Properties and Components

        /// <summary>
        /// ������ �� ���� ����������: �������, ������� � ��������.
        /// </summary>
        public enum AsteroidType
        {
            Big,
            Medium,
            Small
        }

        /// <summary>
        /// ����� ������� ���������.
        /// </summary>
        [SerializeField] private AsteroidType m_AsteroidSize;

        // ������ ���������.
        [SerializeField] private Asteroid m_AsteroidPrefab;

        /// <summary>
        /// ������ �� ������ �������� ���������.
        /// </summary>
        public AsteroidType AsteroidSize => m_AsteroidSize;

        #endregion


        #region Unity Events

        override protected void Start()
        {
            // ������� HP.
            base.Start();

            // �������� ������.
            ChangeAsteroidSize();

            // ������������� �� ������� ������ ���������, �������� ����� OnAsteroidDeath.
            EventsOnDeath.AddListener(OnAsteroidDeath);

        }

        #endregion


        #region Private API

        /// <summary>
        /// �����, ������������� ��� ����������� ���������.
        /// </summary>
        private void OnAsteroidDeath()
        {
            // ���� �������� ��������� - ����� �� ������.
            if (m_AsteroidSize == AsteroidType.Small) return;

            // ������� 2 ���������.
            for (int i = -1; i < 2; i += 2)
            {
                Asteroid asteroid = Instantiate(m_AsteroidPrefab);

                // ������� ������� � ������ �� 1 ������ ��� ���.
                asteroid.transform.position = transform.position;
                asteroid.SetAsteroidType((int)m_AsteroidSize + 1);
                
                // �������� ������ �� Rigidbody ���������.
                Rigidbody2D asteroidRigidbody = asteroid.GetComponent<Rigidbody2D>();

                // ������ �������� �� -1 �� +1 ��������� ����������.
                asteroidRigidbody.velocity = gameObject.GetComponent<Rigidbody2D>().velocity * i;
            }

        }

        #endregion


        #region PublicAPI

        /// <summary>
        /// �����, �������� ��� ���������.
        /// </summary>
        /// <param name="type">0 - Small, 1 - Medium, 2 - Big.</param>
        public void SetAsteroidType(int type)
        {
            // ������ �� ������������ �������.
            if (type < 0 && type > 2) return;

            // ����� ������ ���.
            m_AsteroidSize = (AsteroidType) type;

            // ������ ������ � HP.
            ChangeAsteroidSize();
        }

        /// <summary>
        /// �����, �������� ������ ���������.
        /// </summary>
        public void ChangeAsteroidSize()
        {
            switch (m_AsteroidSize)
            {
                case AsteroidType.Small:
                    transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                    ChangeCurrentHitPoints( (int) (CurrentHitPoints * 0.5f));
                    break;

                case AsteroidType.Medium:
                    transform.localScale = new Vector3(1f, 1f, 1f);
                    break;

                case AsteroidType.Big:
                    transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                    ChangeCurrentHitPoints((int)(CurrentHitPoints * 1.5f));
                    break;
            }
            
            // ���� HP < 0, HP = 1
            if (CurrentHitPoints < 1) ChangeCurrentHitPoints(1);
        }

        #endregion

    }
}