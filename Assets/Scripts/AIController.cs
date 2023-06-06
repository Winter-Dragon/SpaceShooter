using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace SpaceShooter
{
    /// <summary>
    /// �����, ����������� ��������� ��.
    /// </summary>
    [RequireComponent(typeof(SpaceShip))]
    public class AIController : MonoBehaviour
    {

        #region Properties and Components

        /// <summary>
        /// ������ ��������� ��.
        /// </summary>
        public enum AIBehaviour
        {
            Null,
            Patrol
        }

        /// <summary>
        /// ����� ��������� ����� Inspector.
        /// </summary>
        [SerializeField] private AIBehaviour m_AIBehaviour;

        /// <summary>
        /// ������ �� ���� ��������������.
        /// </summary>
        [SerializeField] private List<AIPointPatrol> m_PatrolPoints;

        /// <summary>
        /// ����������, ������������ ������� ������� ����� �������.
        /// </summary>
        private int m_CurrentPositionIndex = 0;

        /// <summary>
        /// �������� �����������.
        /// </summary>
        [Range(0.0f, 1.0f)]
        [SerializeField] private float m_NavigationLinear;

        /// <summary>
        /// �������� ��������.
        /// </summary>
        [Range(0.0f, 1.0f)]
        [SerializeField] private float m_NavigationAngular;

        /// <summary>
        /// �������� ��������� �������.
        /// </summary>
        [SerializeField] private float m_RandomSelectMovePointTime;

        /// <summary>
        /// �������� ��������� ����.
        /// </summary>
        [SerializeField] private float m_FindNewTargetTime;

        /// <summary>
        /// ������ ������ ����.
        /// </summary>
        [SerializeField] private float m_FindTargetRadius;

        /// <summary>
        /// �������� ��������.
        /// </summary>
        [SerializeField] private float m_ShootDelay;

        /// <summary>
        /// ����� �������� ��������� ������� �� ������������.
        /// </summary>
        [SerializeField] private float m_EvadeRayLenght;

        /// <summary>
        /// ������ �� ������� SpaceShip.
        /// </summary>
        private SpaceShip m_SpaceShip;

        /// <summary>
        /// ������ ������� ������� �������� �������.
        /// </summary>
        private Vector3 m_MovePosition;

        /// <summary>
        /// Destructible ���� �������.
        /// </summary>
        private Destructible m_SelectedTarget;

        /// <summary>
        /// ��������� ������������� ���� ��������, ��� ������� �������� ���������� ������������.
        /// </summary>
        private const float MAX_ANGULAR_ANGLE = 45.0f;

        #region Timers

        /// <summary>
        /// ������ ����� �������.
        /// </summary>
        private Timer m_RandomizeDirectionTimer;

        /// <summary>
        /// ������ ��������.
        /// </summary>
        private Timer m_FireTimer;

        /// <summary>
        /// ������ ����� ����.
        /// </summary>
        private Timer m_FindNewTargetTimer;

        #endregion

        #endregion


        #region Unity Events

        private void Start()
        {
            // ���� ������ �� SpaceShip � �������.
            m_SpaceShip = GetComponent<SpaceShip>();

            // �������������� �������.
            InitTimers();
        }

        private void FixedUpdate()
        {
            // �������� ��� �������.
            UpdateTimers();

            // �������� ��.
            UpdateAI();
        }

#if UNITY_EDITOR
        /// <summary>
        /// ������, �������� ����� ������������ ��.
        /// </summary>
        private void OnDrawGizmosSelected()
        {
            // ����� ������������ �� � ������ ��������.
            Handles.color = new Color(255, 0, 0, 1);
            Handles.DrawSolidDisc(m_MovePosition, transform.forward, 0.3f);
            Handles.DrawLine(transform.position, m_MovePosition);

            // ������ ������ ����.
            Handles.color = new Color(255, 140, 0, 0.01f);
            Handles.DrawSolidDisc(transform.position, transform.forward, m_FindTargetRadius);
        }
#endif

#endregion


        #region Private API

        #region Timers

        /// <summary>
        /// �����, ���������������� ��� �������.
        /// </summary>
        private void InitTimers()
        {
            m_RandomizeDirectionTimer = new Timer(m_RandomSelectMovePointTime, true);
            m_FireTimer = new Timer(m_ShootDelay, false);
            m_FindNewTargetTimer = new Timer(m_FindNewTargetTime, true);
        }

        /// <summary>
        /// �����, ����������� ��� �������.
        /// </summary>
        private void UpdateTimers()
        {
            m_RandomizeDirectionTimer.UpdateTimer();
            m_FireTimer.UpdateTimer();
            m_FindNewTargetTimer.UpdateTimer();
        }

        #endregion

        /// <summary>
        /// �����, ����������� �������� ��.
        /// </summary>
        private void UpdateAI()
        {
            // ��������� �������� ��� ��������������.
            UpdateBehaviourPatrul();
        }

        /// <summary>
        /// �����, ����������� �������� ��� ��������������.
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
        /// �����, ������ ��������� ���� ��� ������������ �������.
        /// </summary>
        private void ActionFindNewMovePosition()
        {
            // �������� ��.
            switch (m_AIBehaviour)
            {
                // ��������� Null.
                case AIBehaviour.Null:

                    // ���� �������� - ����� ���������� ����������.
                    m_MovePosition = MakeLead();

                    break;

                // �������� Patrul
                case AIBehaviour.Patrol:

                    // ���� ���� ���� - ���� �������� ���������� �������� ����.
                    if (m_SelectedTarget != null)
                    {
                        // ���� �������� - ����� ���������� ����������.
                        m_MovePosition = MakeLead();
                    }
                    // ���� ���� ���.
                    else
                    {
                        // ���� ���� ���� ��������������.
                        if (m_PatrolPoints != null)
                        {
                            // ����������, ������������, ��������� �� ������� � ���� �������.
                            bool isInsidePatrolZone = (m_PatrolPoints[m_CurrentPositionIndex].transform.position - transform.position).sqrMagnitude < m_PatrolPoints[m_CurrentPositionIndex].Radius * m_PatrolPoints[m_CurrentPositionIndex].Radius;

                            // ���� ���� �������������� ����.
                            if (m_PatrolPoints.Count == 1)
                            {
                                // ���� ������� � ���� �������.
                                if (isInsidePatrolZone)
                                {
                                    // ���� ������ ����� ������� ��������
                                    if (m_RandomizeDirectionTimer.IsFinished)
                                    {
                                        // �������� ����� ���������� ����������� � ���� �������.
                                        Vector2 newPoint = UnityEngine.Random.onUnitSphere * m_PatrolPoints[m_CurrentPositionIndex].Radius + m_PatrolPoints[m_CurrentPositionIndex].transform.position;

                                        // ������� ����� ������� �������� � �����.
                                        m_MovePosition = newPoint;
                                    }
                                }
                                // ���� ������� ��� ���� �������.
                                else
                                {
                                    // ������� �������� = ���� �������.
                                    m_MovePosition = m_PatrolPoints[0].transform.position;
                                }
                            }
                            // ���� ��� �������������� ���������.
                            else
                            {
                                // ���� ������� �� �������� ���� �������.
                                if (isInsidePatrolZone)
                                {
                                    // ������ ������� ++.
                                    m_CurrentPositionIndex++;

                                    // ���� ������ ������, ��� ���-�� ����� �������������� - �� ������������ � 0.
                                    if (m_CurrentPositionIndex >= m_PatrolPoints.Count) m_CurrentPositionIndex = 0;
                                }

                                // ������� ������ ��������� �������.
                                Vector2 newPoint = m_PatrolPoints[m_CurrentPositionIndex].transform.position;

                                // ������� ����� ������� �������� � �����.
                                m_MovePosition = newPoint;
                            }
                        }
                        // ���� ��� ���� ��������������.
                        else
                        {
                            // ���� ������ ����� ������� ��������
                            if (m_RandomizeDirectionTimer.IsFinished)
                            {
                                // �������� ����� ���������� ����������� � ���� LevelBoundery.
                                Vector2 newPoint = UnityEngine.Random.onUnitSphere * LevelBoundary.Instance.Radius;

                                // ������� ����� ������� �������� � �����.
                                m_MovePosition = newPoint;
                            }
                        }
                    }

                    break;
            }
        }

        /// <summary>
        /// �����, � ������� �������� ������� �������� ������������.
        /// </summary>
        private void ActionEvadeCollision()
        {
            // ���� ������� ������������ � ��������
            if (Physics2D.Raycast(transform.position, transform.up, m_EvadeRayLenght))
            {
                // ����� �������� = ������� ����� + ������� �������.
                m_MovePosition = transform.position + transform.right * 100.0f;
            }
        }

        /// <summary>
        /// �����, �������������� �������� ��.
        /// </summary>
        private void ActionControlShip()
        {
            // �������� �����.
            m_SpaceShip.ThrustControl = m_NavigationLinear;

            // ������� � ������ �������.
            m_SpaceShip.TorqueControl = ComputeAliginTorqueNormilized(m_MovePosition, m_SpaceShip.transform) * m_NavigationAngular;
        }

        /// <summary>
        /// ��������� �����, ������������ ��������������� ���� �������� ��.
        /// </summary>
        /// <param name="targetPosition">������� �������.</param>
        /// <param name="ship">������� Transform SpaceShip'a.</param>
        /// <returns></returns>
        private static float ComputeAliginTorqueNormilized(Vector3 targetPosition, Transform ship)
        {
            // ������� ������� ������� � ��������� ����������, �������� ������ � ����� ������������.
            Vector2 localTargetPosition = ship.InverseTransformPoint(targetPosition);

            // ����������� ���� �� ���������� ������� ���� � ������� �����. 3 �������� - ������ ����� ��� ���������� ���������, ����� ��������� ����.
            float angle = Vector3.SignedAngle(localTargetPosition, Vector3.up, Vector3.forward);

            // ������������ ���� �� MaxAngularAngle � ��������� � ��������������� ��������. � �����, ���� ���� ���������� ������, ��� MaxAngularAngle - ������������ ���� ������������.
            angle = Mathf.Clamp(angle, -MAX_ANGULAR_ANGLE, MAX_ANGULAR_ANGLE) / MAX_ANGULAR_ANGLE;

            // ������ ���� � ���������� ����.
            return -angle;
        }

        /// <summary>
        /// �����, ����������� ����� ���� ��� �����.
        /// </summary>
        private void ActionFindNewAttackTarget()
        {
            // ���� ������ ������ ����� ���� �����.
            if (m_FindNewTargetTimer.IsFinished)
            {
                // ���������� ��������� ����.
                m_SelectedTarget = FindNearestDestructibleTarget();

                // ������������� ������.
                m_FindNewTargetTimer.RestartTimer();
            }
        }

        /// <summary>
        /// �����, ���������� � ���� ��� �����
        /// </summary>
        private void ActionFire()
        {
            // ���� ���� ��� �������� ����.
            if (m_SelectedTarget != null)
            {
                // ���� ������ �������� �����.
                if (m_FireTimer.IsFinished)
                {
                    // �������� �� ��������� ������.
                    m_SpaceShip.Fire(TurretMode.Primary);

                    // ������������� ������.
                    m_FireTimer.RestartTimer();
                }
            }
        }

        /// <summary>
        /// �����, ������ ��������� Destructible.
        /// </summary>
        /// <returns>Destructible ��������� ����.</returns>
        private Destructible FindNearestDestructibleTarget()
        {
            // ��������� �� ��������� ����.
            float nearestEnemyDistance = Mathf.Infinity;

            // ������������� ����.
            Destructible potentialTarget = null;

            // ������ ������ �� ���� Destructible �� �����.
            foreach(Destructible destructible in Destructible.AllDestructibles)
            {
                // ��������� ����, ����������� �������, ���� �������, ����.
                if (destructible.GetComponent<SpaceShip>() == m_SpaceShip || 
                    destructible.TeamID == Destructible.TEAM_ID_NEUTRAL || 
                    destructible.TeamID == m_SpaceShip.TeamID ||
                    destructible == null) continue;

                // ������� ���������� ������� ��������� �� ���� � �������� � � ��������� �����.
                float distance = Vector2.Distance(m_SpaceShip.transform.position, destructible.transform.position);
                if (distance < nearestEnemyDistance && distance <= m_FindTargetRadius)
                {
                    // ������ ��������� ���� � ��������� �� ��.
                    potentialTarget = destructible;
                    nearestEnemyDistance = distance;
                }
            }

            // ���������� ������������� ����.
            return potentialTarget;
        }

        /// <summary>
        /// �����, �������� ���������� ��� �������� �������.
        /// </summary>
        private Vector2 MakeLead()
        {
            // ��������, ���� �� ���� ��� �����.
            if (m_SelectedTarget == null) return m_MovePosition;

            // ������� ������� ���� (SpaceShip), ��� �������� �� null.
            SpaceShip currentTarget = m_SelectedTarget.GetComponent<SpaceShip>();
            if (currentTarget == null) return m_MovePosition;

                // 1. ������� ��������� ��.
            // ������� ������ ������� �������.
            Vector2 currentPosition = transform.position;

                // 2. �������� ������� ��.
            // �������� ���������� �������� �������.
            float currentProjectileSpeed = 0.0f;
            // �������� ������ � �������� ���������.
            Turret[] currentTurrets = m_SpaceShip.CurrentTurrets;
            // ��������� ������� � �������� �������.
            for (int i = 0; i < currentTurrets.Length; i++)
            {
                if (currentTurrets[i].Mode == TurretMode.Primary)
                {
                    // ������� ������� �������� �������.
                    currentProjectileSpeed = currentTurrets[i].CurrentTurretProperties.ProjectilePrefab.Velocity;
                    break;
                }
            }

                // 3. ������ ������� ����.
            Vector2 currentMoveTargetPosition = currentTarget.transform.position;

                // 4. �������� �������� ����
            Vector2 currentTargetSpeed = currentTarget.Velocity;

            // ���������� ����������� ����� ����������.
            return CalculateLeadTarget(currentMoveTargetPosition, currentTargetSpeed, currentProjectileSpeed, currentPosition);
        }

        /// <summary>
        /// �����, ����������� ����� ����������.
        /// </summary>
        /// <param name="targetPosition">������� ����.</param>
        /// <param name="targetVelocity">�������� ����.</param>
        /// <param name="bulletSpeed">�������� Projectile.</param>
        /// <param name="shooterPosition">������� �������.</param>
        /// <returns></returns>
        private Vector2 CalculateLeadTarget(Vector2 targetPosition, Vector2 targetVelocity, float bulletSpeed, Vector2 shooterPosition)
        {
            // ��������� ������ �� ������� �� ����.
            Vector2 relativePosition = targetPosition - shooterPosition;

            // ��������� ����� �������.
            float distance = relativePosition.magnitude;

            // ��������� �������������� ����� �� ������������ ������� � �����.
            float timeToIntercept = distance / bulletSpeed;

            // ��������� ������ � ������������ ����� ����������.
            Vector2 leadTarget = targetPosition + (targetVelocity * timeToIntercept);

            return leadTarget;
        }

        #endregion


        #region Public API

        /// <summary>
        /// �����, �������� �� ���� ��������������.
        /// </summary>
        /// <param name="point">���� ��������������.</param>
        public void SetPatrolBehaviour(AIPointPatrol point)
        {
            m_AIBehaviour = AIBehaviour.Patrol;
            m_PatrolPoints[m_CurrentPositionIndex] = point;
        }

        #endregion

    }
}