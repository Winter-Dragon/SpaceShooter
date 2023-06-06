using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// �����, �������������� ���� �� ������������ � ��������� ������� �������, ��������� �� ��������.
    /// </summary>
    public class CollisionDamageApplicator : MonoBehaviour
    {

        #region Properties and Components

        /// <summary>
        /// ���, ��� ������������ � ������� � ������� �� ����� ����������� ����.
        /// </summary>
        public static string IgnoreTag = "WorldBoundary";

        /// <summary>
        /// ����������� �����, ��������� �� ��������.
        /// </summary>
        [SerializeField] private float m_VelocityDamageModifier;

        /// <summary>
        /// ���������� �������� �����.
        /// </summary>
        [SerializeField] private float m_DamageConstant;

        /// <summary>
        /// �������� ����������� ������� Destructible �������.
        /// </summary>
        private Destructible �urrentObjectDestructible;

        #endregion


        #region Unity Events

        private void Start()
        {
            // ��������� ������ Dest �������� �������.
            �urrentObjectDestructible = transform.root.GetComponent<Destructible>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            // �� ���������, ���� ������� ��� WorldBoundary
            if (collision.transform.tag == IgnoreTag) return;

            // ���� �� ������� ������� ���� Dest.
            if (�urrentObjectDestructible != null)
            {
                // ���� ������ �� Dest ������� ������������.
                Destructible collisionDestructible = collision.transform.root.GetComponent<Destructible>();

                // ���� ������ ���� Dest.
                if (collisionDestructible != null)
                {
                    // ���� ������ �� ����� ������� - ������� ����.
                    if (collisionDestructible.TeamID != �urrentObjectDestructible.TeamID)
                    {
                        // ���� = ��������� ����� + ( ����������� �������� * ����� ������� �������� )
                        �urrentObjectDestructible.ApplyDamage((int)m_DamageConstant + (int)(m_VelocityDamageModifier * collision.relativeVelocity.magnitude));
                    }
                }
                // ���� ������ �� Dest.
                else
                {
                    // ���� = ��������� ����� + ( ����������� �������� * ����� ������� �������� )
                    �urrentObjectDestructible.ApplyDamage((int)m_DamageConstant + (int)(m_VelocityDamageModifier * collision.relativeVelocity.magnitude));
                }
            }
        }

        #endregion
    }
}