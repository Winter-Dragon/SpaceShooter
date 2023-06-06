using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// �����, ��������� ����������� �����.
    /// </summary>
    public class EntitySpawnerDebris : MonoBehaviour
    {

        #region Properties and Components

        /// <summary>
        /// ������� ������������ ������.
        /// </summary>
        [SerializeField] private GameObject[] m_DebrisPrefabs;

        /// <summary>
        /// ������� ������ ������������ ������.
        /// </summary>
        [SerializeField] private CircleArea m_Area;

        /// <summary>
        /// ���-�� ������, ������������ �� ���.
        /// </summary>
        [SerializeField] private int m_NumberDebris;

        /// <summary>
        /// ��������, ���������� ������.
        /// </summary>
        [SerializeField] private float m_RandomSpeed;

        #endregion


        #region Unity Events

        private void Start()
        {
            // ����� ������������ ���-�� ������.
            for (int i = 0; i < m_NumberDebris; i++)
            {
                SpawnDebris();
            }
        }

        #endregion


        #region Private API

        /// <summary>
        /// �����, ��������� ����������� �����
        /// </summary>
        private void SpawnDebris()
        {
            // ����� ���������� ������ �� �������.
            int index = Random.Range(0, m_DebrisPrefabs.Length);

            // ������� � �������� ��������� �������� � GameObject.
            GameObject debris = Instantiate(m_DebrisPrefabs[index].gameObject);

            // �������, �������������, ���� ����� - ��������.
            if (debris.GetComponent<Asteroid>() == true)
            {
                // ������ ������ �� ��������.
                Asteroid asteroid = debris.GetComponent<Asteroid>();

                // ������ ��������� ��������� ������.
                asteroid.SetAsteroidType(Random.Range(0, 3));
            }

            // ����������� ����� � ��������� ���� � ������� ������.
            debris.transform.position = m_Area.GetRandomInsideZone();

            // ����� � ������ ��������� Destructible � ����������� � ������� ������, ������ �����.
            debris.GetComponent<Destructible>().EventsOnDeath.AddListener(OnDebrisDead);

            // �������� ������ �� Rigidbody ������.
            Rigidbody2D debrisRigidbody = debris.GetComponent<Rigidbody2D>();

            // �������� �� null � �������/������������� ��������.
            if (debrisRigidbody != null && m_RandomSpeed > 0)
            {
                debrisRigidbody.velocity = UnityEngine.Random.insideUnitCircle * m_RandomSpeed;
            }
        }

        /// <summary>
        /// �����, ������������� ��� ����������� ������������ ������.
        /// </summary>
        private void OnDebrisDead()
        {
            SpawnDebris();
        }

        #endregion

    }
}