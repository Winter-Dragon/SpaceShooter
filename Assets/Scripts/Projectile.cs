using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// �������� ����� ��������, ����������� ��������.
    /// </summary>
    public class Projectile : Entity
    {

        #region Properties and Components

        /// <summary>
        /// �������� �������.
        /// </summary>
        [SerializeField] private float m_Velocity;

        /// <summary>
        /// ������ �� �������� �������.
        /// </summary>
        public float Velocity => m_Velocity;

        /// <summary>
        /// ����� ����� �������.
        /// </summary>
        [SerializeField] private float m_LifeTime;

        /// <summary>
        /// ���� �������.
        /// </summary>
        [SerializeField] private int m_Damage;

        /// <summary>
        /// ������ �� ������ �������, ������������� ��� ���������.
        /// </summary>
        [SerializeField] private GameObject m_impactEffectPrefab;

        /// <summary>
        /// ���������� ������.
        /// </summary>
        private Timer m_Timer;

        /// <summary>
        /// ������, ������� ���������.
        /// </summary>
        private Destructible m_Parent;

        /// <summary>
        /// �������, ������� ����������.
        /// </summary>
        private Turret m_Turret;

        /// <summary>
        /// ���������� true, ���� ������ �������������.
        /// </summary>
        private bool m_Homing;

        /// <summary>
        /// ���� ���������������� ��������.
        /// </summary>
        private Transform m_Target;

        /// <summary>
        /// ����������, ������������ true, ���� ��������� �����.
        /// </summary>
        private bool isPlayer;

        #endregion


        #region Unity Events

        private void Start()
        {
            // �������� ������� ������� ����� �������.
            m_Timer = new Timer(m_LifeTime, false);

            //���� ������������ ������� - �����, ���������� ���.
            if (m_Parent == Player.Instance.ActiveShip) isPlayer = true;
        }

        private void FixedUpdate()
        {
            // ����������, �������� ��������, �� ������� ��������� ������ � ������ �����.
            float StepLengh = Time.fixedDeltaTime * m_Velocity;

            // ������� Raycast �� ����� ���������� ��������.
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, StepLengh);

            // ��������, �������������, ���� �� ���� ������.
            if (hit)
            {
                // �������� ������ �� ������
                Destructible destructible = hit.collider.transform.root.GetComponent<Destructible>();

                // �������� �� null, �� ���� � �� ���� �������.
                if (destructible != null && destructible != m_Parent && destructible.TeamID != m_Parent.TeamID)
                {
                    // ��������� �����.
                    destructible.ApplyDamage(m_Damage);

                    //���� ������������ ������� - �����.
                    if (isPlayer)
                    {
                        // ���� ������ ���������.
                        if (destructible.CurrentHitPoints <= 0)
                        {
                            // ��������� ����.
                            Player.Instance.AddScore(destructible.ScoreValue);
                            GameStatistics.Instance?.AddScore(destructible.ScoreValue);

                            // ������ ������ �� �������.
                            SpaceShip ship = destructible.GetComponent<SpaceShip>();

                            // ���� ��������� � ������� - �������� ��������.
                            if (ship != null)
                            {
                                Player.Instance.AddKill();
                                GameStatistics.Instance?.AddKill();
                            }

                            // ������ ������ �� ��������.
                            Asteroid asteroid = destructible.GetComponent<Asteroid>();

                            // ���� ��������� � �������� - ���������� � ����������� ����������.
                            if (asteroid != null)
                            {
                                GameStatistics.Instance?.AsteroidDestroyed(asteroid.AsteroidSize);
                            }
                        }
                    }
                }

                OnProjectileLifeEnd(hit.collider, hit.point);
            }

            // ���������� �������.
            m_Timer.UpdateTimer();

            // ���������� ������, ���� ������� ����� �����.
            if (m_Timer.IsFinished) Destroy(gameObject);

            // ������� ���������� �������� �������.
            Vector2 step = transform.up * StepLengh;

            // �������� ��� ������������� � ��� ����.
            switch (m_Homing)
            {
                case false:
                    // �������� ������� ������ � ������ �����������.
                    transform.position += new Vector3(step.x, step.y, 0);
                    break;

                case true:
                    // ���� ���� ���, ����� ����� ����.
                    if (m_Target == null)
                    {
                        FindTarget();
                        return;
                    }

                    // �������� ������ �����������.
                    Vector3 direction = m_Target.position - transform.position;

                    // �������� ������� �����.
                    transform.position += new Vector3(step.x, step.y, 0);

                    // ������������ ������� ����������� � ������� �������� ����.
                    transform.up = Vector3.Slerp(transform.up, direction, StepLengh * 0.25f );

                    break;
            }
        }

        #endregion


        #region Private API

        /// <summary>
        /// �������� ��� ������������ �������.
        /// </summary>
        /// <param name="collider">��������� ������� ������������.</param>
        /// <param name="position">������� ������� ������������.</param>
        private void OnProjectileLifeEnd(Collider2D collider, Vector2 position)
        {
            // �������� �� ������������ �������
            if (collider.transform.root.GetComponent<Destructible>() == m_Parent) return;

            // �������� ������� ��� ���������, ���� �� ����.
            if (m_Turret != null && m_impactEffectPrefab != null)
            {
                Destructible target = collider.transform.root.GetComponent<Destructible>();
                GameObject impactEffect = Instantiate(m_impactEffectPrefab);

                // ���� ������ ��� ��������� �������� �������� ����� - ����������� �.
                ExplosionDamageApplicator explosionDamageApplicator = impactEffect.GetComponent<ExplosionDamageApplicator>();
                explosionDamageApplicator?.TuneExplosion(m_Parent, target, transform.position, m_Damage);
            }

            // ���������� ������.
            Destroy(gameObject);
        }

        #endregion


        #region Public API

        /// <summary>
        /// �����, ����������� ������, ������� ���������.
        /// </summary>
        /// <param name="parent">Destructible �������, ������� ���������.</param>
        public void SetParentShooter(Destructible parent)
        {
            m_Parent = parent;
        }

        /// <summary>
        /// �����, ����������� �������, ������� ����������.
        /// </summary>
        /// <param name="turret">Turret ������������ �������.</param>
        public void SetShooterTurret(Turret turret)
        {
            m_Turret = turret;
        }

        /// <summary>
        /// �����, ������������� ������.
        /// </summary>
        public void FindTarget()
        {
            // ��������� �������������.
            m_Homing = true;

            // ��������� �� ��������� ����
            float nearestEnemyDistance = Mathf.Infinity;
            
            // ����������� ������ �� ������� Destructible �� �����.
            foreach (Destructible enemy in Destructible.AllDestructibles)
            {
                // �������� �� ����, ������ � ������������.
                if (enemy == null || enemy == m_Parent || enemy.IsIndestructibly == true
                    || enemy.TeamID == m_Parent.TeamID) continue;

                // ������� ���������� ������� ��������� �� ���� � �������� � � ��������� �����.
                float currdistance = Vector2.Distance(transform.position, enemy.transform.position);
                if (currdistance < nearestEnemyDistance)
                {
                    // ������ ��������� ���� � ��������� �� ��.
                    m_Target = enemy.transform;
                    nearestEnemyDistance = currdistance;
                }
            }

            // ���� ���� ���, �� ������������.
            if (m_Target == null) m_Homing = false;
        }

        #endregion

    }
}