using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class Timer
    {

        #region Properties and Components

        /// <summary>
        /// ����� �������.
        /// </summary>
        private float m_CurrentTime;
        
        /// <summary>
        /// ������������� �������.
        /// </summary>
        private bool m_Loop;

        /// <summary>
        /// ������������ ����� �������.
        /// </summary>
        private float m_CashTime;

        /// <summary>
        /// ���������� �������� �������.
        /// </summary>
        private int m_AmountIterations;

        /// <summary>
        /// ��������� ��������, ������������, ������� ��� ������ ����������.
        /// </summary>
        public int AmountIterations => m_AmountIterations;

        /// <summary>
        /// ����������, �����������, ����������� �� ��������.
        /// </summary>
        private bool m_CheckIteration;

        /// <summary>
        /// ��������, ������������ true ���� ������ ����������.
        /// </summary>
        public bool IsFinished => m_CurrentTime <= 0;

        /// <summary>
        /// ����������� ������ Timer, ����������� ������ ����� ��������.
        /// </summary>
        /// <param name="startTime">����� �������.</param>
        /// <param name="loop">���������� �������.</param>
        public Timer(float startTime, bool loop)
        {
            Start(startTime, loop);
        }

        #endregion


        #region Public API

        /// <summary>
        /// �����, ��������� ������.
        /// </summary>
        /// <param name="startTime">����� �������.</param>
        /// <param name="loop">���������� �������.</param>
        public void Start(float startTime, bool loop)
        {
            m_CurrentTime = startTime;
            m_CashTime = startTime;
            m_Loop = loop;
        }

        /// <summary>
        /// ����� ���������� �������.
        /// </summary>
        /// <param name="deltaTime">���������� �����.</param>
        public void UpdateTimer()
        {
            // ��� ������������ �������.
            if (m_Loop)
            {
                m_CurrentTime -= Time.fixedDeltaTime;

                // ���� ������ ����������.
                if (m_CurrentTime <= 0)
                {
                    // �������, ������������� ������ �� ����� ������ ��������
                    if (m_CheckIteration)
                    {
                        // ������������ ����� ������� ������� � ������������� �������.
                        m_CurrentTime = m_CashTime;
                        m_CheckIteration = false;
                        return;
                    }

                    // ���� ���� ��������.
                    m_AmountIterations++;
                    m_CheckIteration = true;
                }
            }
            // ��� �������������� �������.
            else
            {
                // ��������, �� ���������� �� ������
                if (m_CurrentTime <= 0 && m_CheckIteration == false)
                {
                    m_AmountIterations++;
                    m_CheckIteration = true;
                    return;
                }

                // ��� �������.
                m_CurrentTime -= Time.fixedDeltaTime;
            }
        }

        /// <summary>
        /// �����, ��������������� ������.
        /// </summary>
        public void RestartTimer()
        {
            m_CurrentTime = m_CashTime;
            m_CheckIteration = false;
        }

        #endregion

    }

}