using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// �����, ��������� ������� ��������.
    /// </summary>
    public class EntitySpawner : MonoBehaviour
    {

        #region Properties and Components

        /// <summary>
        /// ������ ������: ��� ������ � ������������.
        /// </summary>
        public enum SpawnMode
        {
            Start,
            Loop
        }

        /// <summary>
        /// ������ �������� ���������, ������� ����� ����������.
        /// </summary>
        [SerializeField] private Entity[] m_EntityPrefabs;

        /// <summary>
        /// ���� ������ ���������.
        /// </summary>
        [SerializeField] private CircleArea m_Area;

        /// <summary>
        /// ����� ������ ������.
        /// </summary>
        [SerializeField] private SpawnMode m_SpawnMode;

        /// <summary>
        /// ���-�� ��������, ������� ��������� �� ���.
        /// </summary>
        [SerializeField] private int m_NumberSpawns;

        /// <summary>
        /// ��� ����� ����������� ������ ������.
        /// </summary>
        [SerializeField] private float m_RespawnTime;

        /// <summary>
        /// ���������� ������.
        /// </summary>
        private Timer m_Timer;

        #endregion


        #region UnityEvents

        private void Start()
        {
            // ������� ������� � ������� ����������, ���� ����� ������ "��� ������".
            if (m_SpawnMode == SpawnMode.Start)
            {
                SpawnEntities();

                enabled = false;
            }

            // �������� ������ �� ������� ������.
            m_Timer = new Timer(m_RespawnTime, true);
        }

        private void FixedUpdate()
        {
            // ��������� ������.
            m_Timer.UpdateTimer();

            // ���� ����� ������ ��� �� �������, �������� ���������� ������ � ����� �� ������.
            if (m_Timer.IsFinished)
            {
                SpawnEntities();
            }
        }

        #endregion


        #region Private API

        /// <summary>
        /// �����, ��������� �������� � ��������� �������.
        /// </summary>
        private void SpawnEntities()
        {
            
            for(int i = 0; i < m_NumberSpawns; i++)
            {
                // ����� ��������� �������� �� �������.
                int index = Random.Range(0, m_EntityPrefabs.Length);

                // ������� � �������� ��������� �������� � GameObject.
                GameObject entity = Instantiate(m_EntityPrefabs[index].gameObject);

                // ����������� �������� � ��������� ���� � ������� ������.
                entity.transform.position = m_Area.GetRandomInsideZone();
            }
        }

        #endregion

    }
}