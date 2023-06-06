using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter
{
    /// <summary>
    /// �������� ����� ���������� ����������� ������� ����� ���������������� ����.
    /// </summary>
    public class MovementController : MonoBehaviour
    {

        #region Properties and Compontnts

        /// <summary>
        /// ������ ����������: ����������, ��������� ����������.
        /// </summary>
        public enum ControlMode
        {
            Keyboard,
            Mobile
        }

        /// <summary>
        /// ����� ����������: � ����������, ��� ������.
        /// </summary>
        [SerializeField] private ControlMode m_ControlMode;

        /// <summary>
        /// ������ �� ��������� ��������.
        /// </summary>
        [SerializeField] private VirtualJoystick m_MobileJoystick;

        /// <summary>
        /// ������ �� ������ �������� �������� �������.
        /// </summary>
        [SerializeField] private PointerClickHold m_MobileFirePrimary;

        /// <summary>
        /// ������ �� ������ �������� ��������������� �������.
        /// </summary>
        [SerializeField] private PointerClickHold m_MobileFireSecondary;

        /// <summary>
        /// ������ �� ������ �����.
        /// </summary>
        [SerializeField] private Button m_PausedButton;

        /// <summary>
        /// ������ �� ������� �������.
        /// </summary>
        private SpaceShip m_TargetShip;

        #endregion


        #region Unity Events

        private void Start()
        {
#if UNITY_EDITOR
            // � �������, � ����������� �� ���������� � ���������� ������, �������/��������� �������� � ������ ��������.
            switch (m_ControlMode)
            {
                case ControlMode.Keyboard:
                    m_MobileJoystick.gameObject.SetActive(false);

                    m_MobileFirePrimary.gameObject.SetActive(false);
                    m_MobileFireSecondary.gameObject.SetActive(false);
                    m_PausedButton.gameObject.SetActive(false);
                    break;

                case ControlMode.Mobile:
                    m_MobileJoystick.gameObject.SetActive(true);

                    m_MobileFirePrimary.gameObject.SetActive(true);
                    m_MobileFireSecondary.gameObject.SetActive(true);
                    m_PausedButton.gameObject.SetActive(true);
                    break;
            }
            // � ������ ������������� �������� ����������� ��������� � ������ ��������, ���� � ���������� ����������.
#else
            if (Application.isMobilePlatform)
            {
                m_ControlMode = ControlMode.Mobile;
                m_MobileJoystick.gameObject.SetActive(true);

                m_MobileFirePrimary.gameObject.SetActive(true);
                m_MobileFireSecondary.gameObject.SetActive(true);
                m_PausedButton.gameObject.SetActive(true);
            }
            else
            {
                m_ControlMode = ControlMode.Keyboard;
                m_MobileJoystick.gameObject.SetActive(false);

                m_MobileFirePrimary.gameObject.SetActive(false);
                m_MobileFireSecondary.gameObject.SetActive(false);
                m_PausedButton.gameObject.SetActive(false);
            }
#endif
        }

        private void FixedUpdate()
        {
            // ��������, ���� �� ������ ����������.
            if (m_TargetShip == null) return;

            // � ����������� �� ���������� ���������� ��������� ������ ����� ����������.
            switch (m_ControlMode)
            {
                case ControlMode.Keyboard:
                    ControlKeyboard();
                    break;

                case ControlMode.Mobile:
                    ControlMobile();
                    break;
            }
        }

        #endregion


        #region Private API

        /// <summary>
        /// �����, ����������� ������� ����� ���������� � ����.
        /// </summary>
        private void ControlKeyboard()
        {
            // �������� ���������� �������� � ������� ����.
            float thrust = 0f;
            float torque = 0f;

            // ��� ������� ������ ����������, ����� ����������� �������� � ������� ����.
            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) thrust = 1.0f;
            if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) thrust = -1.0f;
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) torque = 1.0f;
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) torque = -1.0f;

            // ��� ������� ������ ��������, ��������� �������� �������� � ��������������� �������.
            if (Input.GetKey(KeyCode.Mouse0)) m_TargetShip.Fire(TurretMode.Primary);
            if (Input.GetKey(KeyCode.Mouse1)) m_TargetShip.Fire(TurretMode.Secondary);

            // ��� ������� Esc �����.
            if (Input.GetKey(KeyCode.Escape)) UI_Controller_PauseMenu.Instance.PauseActive();

            // ������ ������� �������� � ������� ����.
            m_TargetShip.ThrustControl = thrust;
            m_TargetShip.TorqueControl = torque;
        }

        /// <summary>
        /// ����� ���������� ������� ����� �������� � ��������� ���������.
        /// </summary>
        private void ControlMobile()
        {
            // 1. ������� ������, ������ ������� ��������� ���������.
            Vector3 direction = m_MobileJoystick.Value;

            // 2. ������, ��������� �� ������ ��������� �������� ����� � �� �� �������, ��� � � �������.
            var dot = Vector2.Dot(direction, m_TargetShip.transform.up);
            // 2. ������, ��������� �� ������ ��������� �������� � �� �� �������, ��� � � �������.
            var dot2 = Vector2.Dot(direction, m_TargetShip.transform.right);

            // 3. ������� ������� �����, ���� �������� ��������� � �� �� �������, ��� � �������.
            m_TargetShip.ThrustControl = Mathf.Max(0, dot);
            // 4. ������������ �������, ���� ������ ��������� �������� �� ��������� � �� �� �������, ��� � � �������.
            m_TargetShip.TorqueControl = -dot2;

            // ���� ������ �������� ������ - ��������� ������� �� ���������������� ������.
            if (m_MobileFirePrimary.IsHold == true) m_TargetShip.Fire(TurretMode.Primary);
            if (m_MobileFireSecondary.IsHold == true) m_TargetShip.Fire(TurretMode.Secondary);
        }

        #endregion


        #region Public API

        /// <summary>
        /// �����, �������� ����� ������ ����������.
        /// </summary>
        /// <param name="newSpaceShip">����� ������ ���������� (SpaceShip).</param>
        public void SetTargetShip(SpaceShip newSpaceShip)
        {
            // ��������� ������ ����������.
            m_TargetShip = newSpaceShip;
        }

        #endregion

    }
}