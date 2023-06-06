using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// Основной класс снарядов, выпускаемых туррелью.
    /// </summary>
    public class Projectile : Entity
    {

        #region Properties and Components

        /// <summary>
        /// Скорость снаряда.
        /// </summary>
        [SerializeField] private float m_Velocity;

        /// <summary>
        /// Ссылка на скорость снаряда.
        /// </summary>
        public float Velocity => m_Velocity;

        /// <summary>
        /// Время жизни снаряда.
        /// </summary>
        [SerializeField] private float m_LifeTime;

        /// <summary>
        /// Урон снаряда.
        /// </summary>
        [SerializeField] private int m_Damage;

        /// <summary>
        /// Ссылка на префаб объекта, появляющегося при попадании.
        /// </summary>
        [SerializeField] private GameObject m_impactEffectPrefab;

        /// <summary>
        /// Внутренний таймер.
        /// </summary>
        private Timer m_Timer;

        /// <summary>
        /// Объект, который выстрелил.
        /// </summary>
        private Destructible m_Parent;

        /// <summary>
        /// Туррель, которая выстрелила.
        /// </summary>
        private Turret m_Turret;

        /// <summary>
        /// Возвращает true, если оружие самонаводится.
        /// </summary>
        private bool m_Homing;

        /// <summary>
        /// Цель самонаводящегося выстрела.
        /// </summary>
        private Transform m_Target;

        /// <summary>
        /// Переменная, отображающая true, если выстрелил игрок.
        /// </summary>
        private bool isPlayer;

        #endregion


        #region Unity Events

        private void Start()
        {
            // Создание таймера времени жизни снаряда.
            m_Timer = new Timer(m_LifeTime, false);

            //Если выстреливший корабль - игрок, запоминает это.
            if (m_Parent == Player.Instance.ActiveShip) isPlayer = true;
        }

        private void FixedUpdate()
        {
            // Переменная, хранящая смещение, на которое смещается снаряд в каждом кадре.
            float StepLengh = Time.fixedDeltaTime * m_Velocity;

            // Создать Raycast на длину следующего смещения.
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, StepLengh);

            // Действия, выполняющиеся, если на пути объект.
            if (hit)
            {
                // Получаем ссылку на объект
                Destructible destructible = hit.collider.transform.root.GetComponent<Destructible>();

                // Проверка на null, на себя и на свою команду.
                if (destructible != null && destructible != m_Parent && destructible.TeamID != m_Parent.TeamID)
                {
                    // Обработка урона.
                    destructible.ApplyDamage(m_Damage);

                    //Если выстреливший корабль - игрок.
                    if (isPlayer)
                    {
                        // Если объект уничтожен.
                        if (destructible.CurrentHitPoints <= 0)
                        {
                            // Начислить очки.
                            Player.Instance.AddScore(destructible.ScoreValue);
                            GameStatistics.Instance?.AddScore(destructible.ScoreValue);

                            // Берётся ссылка на корабль.
                            SpaceShip ship = destructible.GetComponent<SpaceShip>();

                            // Если попадание в корабль - добавить убийство.
                            if (ship != null)
                            {
                                Player.Instance.AddKill();
                                GameStatistics.Instance?.AddKill();
                            }

                            // Берётся ссылка на астероид.
                            Asteroid asteroid = destructible.GetComponent<Asteroid>();

                            // Если попадание в астероид - записывает в общеигровую статистику.
                            if (asteroid != null)
                            {
                                GameStatistics.Instance?.AsteroidDestroyed(asteroid.AsteroidSize);
                            }
                        }
                    }
                }

                OnProjectileLifeEnd(hit.collider, hit.point);
            }

            // Обновление таймера.
            m_Timer.UpdateTimer();

            // Уничтожить объект, если истекло время жизни.
            if (m_Timer.IsFinished) Destroy(gameObject);

            // Создать переменную смещения снаряда.
            Vector2 step = transform.up * StepLengh;

            // Действия при самонаведении и без него.
            switch (m_Homing)
            {
                case false:
                    // Смещение снаряда шагами в нужном направлении.
                    transform.position += new Vector3(step.x, step.y, 0);
                    break;

                case true:
                    // Если цели нет, найти новую цель.
                    if (m_Target == null)
                    {
                        FindTarget();
                        return;
                    }

                    // Создаётся вектор направления.
                    Vector3 direction = m_Target.position - transform.position;

                    // Смещение снаряда вперёд.
                    transform.position += new Vector3(step.x, step.y, 0);

                    // Интерполяция вектора направления в сторону движения цели.
                    transform.up = Vector3.Slerp(transform.up, direction, StepLengh * 0.25f );

                    break;
            }
        }

        #endregion


        #region Private API

        /// <summary>
        /// Действия при столкновении снаряда.
        /// </summary>
        /// <param name="collider">Коллайдер объекта столкновения.</param>
        /// <param name="position">Позиция объекта столкновения.</param>
        private void OnProjectileLifeEnd(Collider2D collider, Vector2 position)
        {
            // Проверка на родительский корабль
            if (collider.transform.root.GetComponent<Destructible>() == m_Parent) return;

            // Создание эффекта при попадании, если он есть.
            if (m_Turret != null && m_impactEffectPrefab != null)
            {
                Destructible target = collider.transform.root.GetComponent<Destructible>();
                GameObject impactEffect = Instantiate(m_impactEffectPrefab);

                // Если эффект при попадании содержит взрывную волну - настраивает её.
                ExplosionDamageApplicator explosionDamageApplicator = impactEffect.GetComponent<ExplosionDamageApplicator>();
                explosionDamageApplicator?.TuneExplosion(m_Parent, target, transform.position, m_Damage);
            }

            // Уничтожить снаряд.
            Destroy(gameObject);
        }

        #endregion


        #region Public API

        /// <summary>
        /// Метод, назначающий объект, который выстрелил.
        /// </summary>
        /// <param name="parent">Destructible объекта, который выстрелил.</param>
        public void SetParentShooter(Destructible parent)
        {
            m_Parent = parent;
        }

        /// <summary>
        /// Метод, назначающий туррель, которая выстрелила.
        /// </summary>
        /// <param name="turret">Turret выстрелившей туррели.</param>
        public void SetShooterTurret(Turret turret)
        {
            m_Turret = turret;
        }

        /// <summary>
        /// Метод, самонаводящий снаряд.
        /// </summary>
        public void FindTarget()
        {
            // Указывает самонаведение.
            m_Homing = true;

            // Дистанция до ближайшей цели
            float nearestEnemyDistance = Mathf.Infinity;
            
            // Прохождение циклом по каждому Destructible на сцене.
            foreach (Destructible enemy in Destructible.AllDestructibles)
            {
                // Проверка на ноль, игрока и неуязвимость.
                if (enemy == null || enemy == m_Parent || enemy.IsIndestructibly == true
                    || enemy.TeamID == m_Parent.TeamID) continue;

                // Создать переменную текущей дистанции до цели и сравнить её с ближайшей целью.
                float currdistance = Vector2.Distance(transform.position, enemy.transform.position);
                if (currdistance < nearestEnemyDistance)
                {
                    // Задать ближайшую цель и дистанцию до неё.
                    m_Target = enemy.transform;
                    nearestEnemyDistance = currdistance;
                }
            }

            // Если цели нет, не самонаводить.
            if (m_Target == null) m_Homing = false;
        }

        #endregion

    }
}