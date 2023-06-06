using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace SpaceShooter
{
    /// <summary>
    /// Класс, описывающий поведение ИИ.
    /// </summary>
    [RequireComponent(typeof(SpaceShip))]
    public class AIController : MonoBehaviour
    {

        #region Properties and Components

        /// <summary>
        /// Список поведений ИИ.
        /// </summary>
        public enum AIBehaviour
        {
            Null,
            Patrol
        }

        /// <summary>
        /// Выбор поведения через Inspector.
        /// </summary>
        [SerializeField] private AIBehaviour m_AIBehaviour;

        /// <summary>
        /// Ссылка на зону патрулирования.
        /// </summary>
        [SerializeField] private List<AIPointPatrol> m_PatrolPoints;

        /// <summary>
        /// Переменная, отображающая текущую позицию точки патруля.
        /// </summary>
        private int m_CurrentPositionIndex = 0;

        /// <summary>
        /// Скорость перемещения.
        /// </summary>
        [Range(0.0f, 1.0f)]
        [SerializeField] private float m_NavigationLinear;

        /// <summary>
        /// Скорость вращения.
        /// </summary>
        [Range(0.0f, 1.0f)]
        [SerializeField] private float m_NavigationAngular;

        /// <summary>
        /// Скорость изменения позиции.
        /// </summary>
        [SerializeField] private float m_RandomSelectMovePointTime;

        /// <summary>
        /// Скорость изменения цели.
        /// </summary>
        [SerializeField] private float m_FindNewTargetTime;

        /// <summary>
        /// Радиус поиска цели.
        /// </summary>
        [SerializeField] private float m_FindTargetRadius;

        /// <summary>
        /// Скорость стрельбы.
        /// </summary>
        [SerializeField] private float m_ShootDelay;

        /// <summary>
        /// Длина рейкаста уклонения корабля от столкновений.
        /// </summary>
        [SerializeField] private float m_EvadeRayLenght;

        /// <summary>
        /// Ссылка на текущий SpaceShip.
        /// </summary>
        private SpaceShip m_SpaceShip;

        /// <summary>
        /// Вектор целевой позиции движения корабля.
        /// </summary>
        private Vector3 m_MovePosition;

        /// <summary>
        /// Destructible цели корабля.
        /// </summary>
        private Destructible m_SelectedTarget;

        /// <summary>
        /// Константа максимального угла поворота, при которой вращение становится максимальным.
        /// </summary>
        private const float MAX_ANGULAR_ANGLE = 45.0f;

        #region Timers

        /// <summary>
        /// Таймер смены позиции.
        /// </summary>
        private Timer m_RandomizeDirectionTimer;

        /// <summary>
        /// Таймер стрельбы.
        /// </summary>
        private Timer m_FireTimer;

        /// <summary>
        /// Таймер смены цели.
        /// </summary>
        private Timer m_FindNewTargetTimer;

        #endregion

        #endregion


        #region Unity Events

        private void Start()
        {
            // Берём ссылку на SpaceShip с объекта.
            m_SpaceShip = GetComponent<SpaceShip>();

            // Инициализируем таймеры.
            InitTimers();
        }

        private void FixedUpdate()
        {
            // Обновить все таймеры.
            UpdateTimers();

            // Обновить ИИ.
            UpdateAI();
        }

#if UNITY_EDITOR
        /// <summary>
        /// Гизмос, рисующий точку передвижения ИИ.
        /// </summary>
        private void OnDrawGizmosSelected()
        {
            // Точка передвижения ИИ и вектор движения.
            Handles.color = new Color(255, 0, 0, 1);
            Handles.DrawSolidDisc(m_MovePosition, transform.forward, 0.3f);
            Handles.DrawLine(transform.position, m_MovePosition);

            // Радиус поиска цели.
            Handles.color = new Color(255, 140, 0, 0.01f);
            Handles.DrawSolidDisc(transform.position, transform.forward, m_FindTargetRadius);
        }
#endif

#endregion


        #region Private API

        #region Timers

        /// <summary>
        /// Метод, инициализирующий все таймеры.
        /// </summary>
        private void InitTimers()
        {
            m_RandomizeDirectionTimer = new Timer(m_RandomSelectMovePointTime, true);
            m_FireTimer = new Timer(m_ShootDelay, false);
            m_FindNewTargetTimer = new Timer(m_FindNewTargetTime, true);
        }

        /// <summary>
        /// Метод, обновляющий все таймеры.
        /// </summary>
        private void UpdateTimers()
        {
            m_RandomizeDirectionTimer.UpdateTimer();
            m_FireTimer.UpdateTimer();
            m_FindNewTargetTimer.UpdateTimer();
        }

        #endregion

        /// <summary>
        /// Метод, обновляющий действия ИИ.
        /// </summary>
        private void UpdateAI()
        {
            // Обновляем действоя при патрулировании.
            UpdateBehaviourPatrul();
        }

        /// <summary>
        /// Метод, обновляющий действия при патрулировании.
        /// </summary>
        private void UpdateBehaviourPatrul()
        {
            ActionFindNewMovePosition();
            ActionEvadeCollision();
            ActionControlShip();
            ActionFindNewAttackTarget();
            ActionFire();
        }

        /// <summary>
        /// Метод, ищущий следующую цель для передвижения корабля.
        /// </summary>
        private void ActionFindNewMovePosition()
        {
            // Действия ИИ.
            switch (m_AIBehaviour)
            {
                // Дейвствие Null.
                case AIBehaviour.Null:

                    // Цель движения - точка упреждения противника.
                    m_MovePosition = MakeLead();

                    break;

                // Действие Patrul
                case AIBehaviour.Patrol:

                    // Если цель есть - цель движения становится позицией цели.
                    if (m_SelectedTarget != null)
                    {
                        // Цель движения - точка упреждения противника.
                        m_MovePosition = MakeLead();
                    }
                    // Если цели нет.
                    else
                    {
                        // Если есть зона патрулирования.
                        if (m_PatrolPoints != null)
                        {
                            // Переменная, отображающая, находится ли корабль в зоне патруля.
                            bool isInsidePatrolZone = (m_PatrolPoints[m_CurrentPositionIndex].transform.position - transform.position).sqrMagnitude < m_PatrolPoints[m_CurrentPositionIndex].Radius * m_PatrolPoints[m_CurrentPositionIndex].Radius;

                            // Если зона патрулирования одна.
                            if (m_PatrolPoints.Count == 1)
                            {
                                // Если корабль в зоне патруля.
                                if (isInsidePatrolZone)
                                {
                                    // Если таймер смены позиции завершён
                                    if (m_RandomizeDirectionTimer.IsFinished)
                                    {
                                        // Создаётся точка случайного перемещения в зоне патруля.
                                        Vector2 newPoint = UnityEngine.Random.onUnitSphere * m_PatrolPoints[m_CurrentPositionIndex].Radius + m_PatrolPoints[m_CurrentPositionIndex].transform.position;

                                        // Задаётся новая позиция движения к точке.
                                        m_MovePosition = newPoint;
                                    }
                                }
                                // Если корабль вне зоны патруля.
                                else
                                {
                                    // Позиция движения = зона патруля.
                                    m_MovePosition = m_PatrolPoints[0].transform.position;
                                }
                            }
                            // Если зон патрулирования несколько.
                            else
                            {
                                // Если позиция ИИ настигла зону патруля.
                                if (isInsidePatrolZone)
                                {
                                    // Индекс позиции ++.
                                    m_CurrentPositionIndex++;

                                    // Если индекс больше, чем кол-во точек патрулирования - он сбрасывается в 0.
                                    if (m_CurrentPositionIndex >= m_PatrolPoints.Count) m_CurrentPositionIndex = 0;
                                }

                                // Задаётся вектор следующей позиции.
                                Vector2 newPoint = m_PatrolPoints[m_CurrentPositionIndex].transform.position;

                                // Задаётся новая позиция движения к точке.
                                m_MovePosition = newPoint;
                            }
                        }
                        // Если нет зоны патрулирования.
                        else
                        {
                            // Если таймер смены позиции завершён
                            if (m_RandomizeDirectionTimer.IsFinished)
                            {
                                // Создаётся точка случайного перемещения в зоне LevelBoundery.
                                Vector2 newPoint = UnityEngine.Random.onUnitSphere * LevelBoundary.Instance.Radius;

                                // Задаётся новая позиция движения к точке.
                                m_MovePosition = newPoint;
                            }
                        }
                    }

                    break;
            }
        }

        /// <summary>
        /// Метод, с помощью которого кораблю избегает столкновений.
        /// </summary>
        private void ActionEvadeCollision()
        {
            // Если рейкаст пересекается с объектом
            if (Physics2D.Raycast(transform.position, transform.up, m_EvadeRayLenght))
            {
                // Точка движения = текущая точка + немного направо.
                m_MovePosition = transform.position + transform.right * 100.0f;
            }
        }

        /// <summary>
        /// Метод, контролирующий движение ИИ.
        /// </summary>
        private void ActionControlShip()
        {
            // Движение вперёд.
            m_SpaceShip.ThrustControl = m_NavigationLinear;

            // Поворот в нужную сторону.
            m_SpaceShip.TorqueControl = ComputeAliginTorqueNormilized(m_MovePosition, m_SpaceShip.transform) * m_NavigationAngular;
        }

        /// <summary>
        /// Статичный метод, возвращающий нормализованный угол поворота ИИ.
        /// </summary>
        /// <param name="targetPosition">Целевая позиция.</param>
        /// <param name="ship">Текущая Transform SpaceShip'a.</param>
        /// <returns></returns>
        private static float ComputeAliginTorqueNormilized(Vector3 targetPosition, Transform ship)
        {
            // Перевод целевой позиции в локальные координаты, создаётся вектор с этими координатами.
            Vector2 localTargetPosition = ship.InverseTransformPoint(targetPosition);

            // Вычисляется угол от локального вектора цели и вектора вперёд. 3 значение - вокруг какой оси необходимо крутиться, чтобы вычислить угол.
            float angle = Vector3.SignedAngle(localTargetPosition, Vector3.up, Vector3.forward);

            // Ограничивает угол до MaxAngularAngle и переводит в нормализованное значение. В итоге, если угол становится больше, чем MaxAngularAngle - Вращательная сила максимальная.
            angle = Mathf.Clamp(angle, -MAX_ANGULAR_ANGLE, MAX_ANGULAR_ANGLE) / MAX_ANGULAR_ANGLE;

            // Меняем знак и возвращаем угол.
            return -angle;
        }

        /// <summary>
        /// Метод, назначающий новую цель для атаки.
        /// </summary>
        private void ActionFindNewAttackTarget()
        {
            // Если таймер поиска новой цели готов.
            if (m_FindNewTargetTimer.IsFinished)
            {
                // Выбирается ближайшая цель.
                m_SelectedTarget = FindNearestDestructibleTarget();

                // Перезапустить таймер.
                m_FindNewTargetTimer.RestartTimer();
            }
        }

        /// <summary>
        /// Метод, стреляющий в цель для атаки
        /// </summary>
        private void ActionFire()
        {
            // Если цель для стрельбы есть.
            if (m_SelectedTarget != null)
            {
                // Если таймер стрельбы готов.
                if (m_FireTimer.IsFinished)
                {
                    // Стрельба из основного орудия.
                    m_SpaceShip.Fire(TurretMode.Primary);

                    // Перезапустить таймер.
                    m_FireTimer.RestartTimer();
                }
            }
        }

        /// <summary>
        /// Метод, ищущий ближайший Destructible.
        /// </summary>
        /// <returns>Destructible ближайшей цели.</returns>
        private Destructible FindNearestDestructibleTarget()
        {
            // Дистанция до ближайшей цели.
            float nearestEnemyDistance = Mathf.Infinity;

            // Потенциальная цель.
            Destructible potentialTarget = null;

            // Пройти циклом по всем Destructible на сцене.
            foreach(Destructible destructible in Destructible.AllDestructibles)
            {
                // Исключить себя, нейтральные объекты, свою команду, ноль.
                if (destructible.GetComponent<SpaceShip>() == m_SpaceShip || 
                    destructible.TeamID == Destructible.TEAM_ID_NEUTRAL || 
                    destructible.TeamID == m_SpaceShip.TeamID ||
                    destructible == null) continue;

                // Создать переменную текущей дистанции до цели и сравнить её с ближайшей целью.
                float distance = Vector2.Distance(m_SpaceShip.transform.position, destructible.transform.position);
                if (distance < nearestEnemyDistance && distance <= m_FindTargetRadius)
                {
                    // Задать ближайшую цель и дистанцию до неё.
                    potentialTarget = destructible;
                    nearestEnemyDistance = distance;
                }
            }

            // Возвратить потенциальную цель.
            return potentialTarget;
        }

        /// <summary>
        /// Метод, задающий упреждение для движения объекта.
        /// </summary>
        private Vector2 MakeLead()
        {
            // Проверка, есть ли цель для атаки.
            if (m_SelectedTarget == null) return m_MovePosition;

            // Задаётся текущая цель (SpaceShip), идёт проверка на null.
            SpaceShip currentTarget = m_SelectedTarget.GetComponent<SpaceShip>();
            if (currentTarget == null) return m_MovePosition;

                // 1. Текущее положение ИИ.
            // Задаётся вектор текущей позиции.
            Vector2 currentPosition = transform.position;

                // 2. Скорость снаряда ИИ.
            // Создаётся переменная скорости снаряда.
            float currentProjectileSpeed = 0.0f;
            // Создаётся массив с текущими туррелями.
            Turret[] currentTurrets = m_SpaceShip.CurrentTurrets;
            // Находится туррель с основным оружием.
            for (int i = 0; i < currentTurrets.Length; i++)
            {
                if (currentTurrets[i].Mode == TurretMode.Primary)
                {
                    // Задаётся текущая скорость снаряда.
                    currentProjectileSpeed = currentTurrets[i].CurrentTurretProperties.ProjectilePrefab.Velocity;
                    break;
                }
            }

                // 3. Вектор позиции цели.
            Vector2 currentMoveTargetPosition = currentTarget.transform.position;

                // 4. Скорость движения цели
            Vector2 currentTargetSpeed = currentTarget.Velocity;

            // Возвращает посчитанную точку упреждения.
            return CalculateLeadTarget(currentMoveTargetPosition, currentTargetSpeed, currentProjectileSpeed, currentPosition);
        }

        /// <summary>
        /// Метод, вычисляющий точку упреждения.
        /// </summary>
        /// <param name="targetPosition">Позиция цели.</param>
        /// <param name="targetVelocity">Скорость цели.</param>
        /// <param name="bulletSpeed">Скорость Projectile.</param>
        /// <param name="shooterPosition">Позиция стрелка.</param>
        /// <returns></returns>
        private Vector2 CalculateLeadTarget(Vector2 targetPosition, Vector2 targetVelocity, float bulletSpeed, Vector2 shooterPosition)
        {
            // Находится вектор от стрелка до цели.
            Vector2 relativePosition = targetPosition - shooterPosition;

            // Находится длина вектора.
            float distance = relativePosition.magnitude;

            // Считается предполагаемое время до столкновения снаряда с целью.
            float timeToIntercept = distance / bulletSpeed;

            // Находится вектор с координатами точки упреждения.
            Vector2 leadTarget = targetPosition + (targetVelocity * timeToIntercept);

            return leadTarget;
        }

        #endregion


        #region Public API

        /// <summary>
        /// Метод, задающий ИИ зону патрулирования.
        /// </summary>
        /// <param name="point">Зона патрулирования.</param>
        public void SetPatrolBehaviour(AIPointPatrol point)
        {
            m_AIBehaviour = AIBehaviour.Patrol;
            m_PatrolPoints[m_CurrentPositionIndex] = point;
        }

        #endregion

    }
}