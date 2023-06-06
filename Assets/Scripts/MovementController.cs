using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter
{
    /// <summary>
    /// Основной класс управления коспическим кораблём через пользовательский ввод.
    /// </summary>
    public class MovementController : MonoBehaviour
    {

        #region Properties and Compontnts

        /// <summary>
        /// Список управления: клавиатура, мобильные устройства.
        /// </summary>
        public enum ControlMode
        {
            Keyboard,
            Mobile
        }

        /// <summary>
        /// Выбор управления: с клавиатуры, или стиком.
        /// </summary>
        [SerializeField] private ControlMode m_ControlMode;

        /// <summary>
        /// Ссылка на мобильный джойстик.
        /// </summary>
        [SerializeField] private VirtualJoystick m_MobileJoystick;

        /// <summary>
        /// Ссылка на кнопку выстрела основным оружием.
        /// </summary>
        [SerializeField] private PointerClickHold m_MobileFirePrimary;

        /// <summary>
        /// Ссылка на кнопку выстрела вспомогательным оружием.
        /// </summary>
        [SerializeField] private PointerClickHold m_MobileFireSecondary;

        /// <summary>
        /// Ссылка на кнопку паузы.
        /// </summary>
        [SerializeField] private Button m_PausedButton;

        /// <summary>
        /// Ссылка на текущий корабль.
        /// </summary>
        private SpaceShip m_TargetShip;

        #endregion


        #region Unity Events

        private void Start()
        {
#if UNITY_EDITOR
            // В эдиторе, в зависимости от выбранного в инспекторе режима, убирает/добавляет джойстик и кнопки стрельбы.
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
            // В сборке автоматически включает отображение джойстика и кнопок стрельбы, если с мобильного устройства.
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
            // Проверка, есть ли объект управления.
            if (m_TargetShip == null) return;

            // В зависимости от выбранного управления запускает нужный метод управления.
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
        /// Метод, управляющий кораблём через клавиатуру и мышь.
        /// </summary>
        private void ControlKeyboard()
        {
            // Создание переменных линейной и угловой тяги.
            float thrust = 0f;
            float torque = 0f;

            // При нажатии кнопок управления, задаёт необходимую линейную и угловую тягу.
            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) thrust = 1.0f;
            if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) thrust = -1.0f;
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) torque = 1.0f;
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) torque = -1.0f;

            // При нажатии кнопок стрельбы, запускает стрельбу основным и вспомогательным оружием.
            if (Input.GetKey(KeyCode.Mouse0)) m_TargetShip.Fire(TurretMode.Primary);
            if (Input.GetKey(KeyCode.Mouse1)) m_TargetShip.Fire(TurretMode.Secondary);

            // При нажатии Esc пауза.
            if (Input.GetKey(KeyCode.Escape)) UI_Controller_PauseMenu.Instance.PauseActive();

            // Задать кораблю линейную и угловую тягу.
            m_TargetShip.ThrustControl = thrust;
            m_TargetShip.TorqueControl = torque;
        }

        /// <summary>
        /// Метод управления кораблём через джойстик с мобильных устройств.
        /// </summary>
        private void ControlMobile()
        {
            // 1. Создать вектор, равный вектору положения джойстика.
            Vector3 direction = m_MobileJoystick.Value;

            // 2. Узнать, направлен ли вектор джойстика движения вперёд в ту же сторону, что и у корабля.
            var dot = Vector2.Dot(direction, m_TargetShip.transform.up);
            // 2. Узнать, направлен ли вектор джойстика поворота в ту же сторону, что и у корабля.
            var dot2 = Vector2.Dot(direction, m_TargetShip.transform.right);

            // 3. Двигать корабль вперёд, если джойстик направлен в ту же сторону, что и корабль.
            m_TargetShip.ThrustControl = Mathf.Max(0, dot);
            // 4. Поворачивать корабль, если вектор джойстика поворота не направлен в ту же сторону, что и у корабля.
            m_TargetShip.TorqueControl = -dot2;

            // Если кнопка стрельбы нажата - запускает стельбу из соответствующего оружия.
            if (m_MobileFirePrimary.IsHold == true) m_TargetShip.Fire(TurretMode.Primary);
            if (m_MobileFireSecondary.IsHold == true) m_TargetShip.Fire(TurretMode.Secondary);
        }

        #endregion


        #region Public API

        /// <summary>
        /// Метод, задающий новый объект управления.
        /// </summary>
        /// <param name="newSpaceShip">Новый объект управления (SpaceShip).</param>
        public void SetTargetShip(SpaceShip newSpaceShip)
        {
            // Обновляет объект управления.
            m_TargetShip = newSpaceShip;
        }

        #endregion

    }
}