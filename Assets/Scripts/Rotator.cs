using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// �����, �������� �������� ������� �� ���������� �������.
    /// </summary>
    public class Rotator : MonoBehaviour
    {

        #region Properties and Components

        /// <summary>
        /// ������ �������� �������.
        /// </summary>
        [SerializeField] private Vector3 m_speed;

        /// <summary>
        /// ������ �� ������ ��������.
        /// </summary>
        private Transform m_Transform;

        #endregion


        #region Unity Events

        private void Start()
        {
            // ������� ������ ������� �������� �� ������� Transform �������.
            m_Transform = GetComponent<Transform>();
        }

        private void FixedUpdate()
        {
            // ������� ������ �� ������� ��������, ������������ � ����������.
            m_Transform.transform.Rotate(m_speed * Time.deltaTime);
        }

        #endregion

    }
}