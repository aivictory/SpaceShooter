using UnityEngine;
using Common;

namespace SpaceShooter
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class SpaceShip : Destructible
    {
        [SerializeField] private Sprite m_PreviewImage;
        /// <summary>
        /// Weight for automatic installation for rigidbody.
        /// </summary>
        [Header("Space Ship")]
        [SerializeField] private float m_Mass;

        /// <summary>
        /// Forward pushing force
        /// </summary>
        [SerializeField] private float m_Thrust;

        [SerializeField] private float m_StartThrust;

        /// <summary>
        /// Rotating force.
        /// </summary>
        [SerializeField] private float m_Mobility;

        /// <summary>
        /// Max line speed.
        /// </summary>
        [SerializeField] private float m_MaxLinearVelocity;

        /// <summary>
        /// Max Rotating speed. In degrees\sec. 
        /// </summary>
        [SerializeField] private float m_MaxAngularVelocity;

        /// <summary>
        /// Link to Rigid.
        /// </summary>
        private Rigidbody2D m_Rigid;

        public float MaxLinearVelocity => m_MaxLinearVelocity;
        public float MaxAngularVelocity => m_MaxAngularVelocity;
        public Sprite PreviewImage => m_PreviewImage;

        #region Public API  

        /// <summary>
        /// Linear thrust control.
        /// </summary>
        public float ThrustControl { get; set; }

        /// <summary>
        /// Rotational thrust control.
        /// </summary>
        public float TorqueControl { get; set; }
        #endregion

        #region Unity Event
        protected override void Start()
        {
            base.Start();

            m_Rigid = GetComponent<Rigidbody2D>();
            m_Rigid.mass = m_Mass;

            m_Rigid.inertia = 1;

            InitOffensive();
        }

        private void FixedUpdate()
        {
            UpdateInvulnerability();
            UpdateBonusThrust();
            UpdateRigidBody();
            UpdateEnergyRegen();
        }
        #endregion

        /// <summary>
        /// Method of adding forces to a ship to move.
        /// </summary>
        private void UpdateRigidBody()
        {
            m_Rigid.AddForce(ThrustControl * m_Thrust * transform.up * Time.fixedDeltaTime, ForceMode2D.Force);

            m_Rigid.AddForce(-m_Rigid.velocity * (m_Thrust / m_MaxLinearVelocity) * Time.fixedDeltaTime, ForceMode2D.Force);

            m_Rigid.AddTorque(TorqueControl * m_Mobility * Time.fixedDeltaTime, ForceMode2D.Force);

            m_Rigid.AddTorque(-m_Rigid.angularVelocity * (m_Mobility / m_MaxAngularVelocity) * Time.fixedDeltaTime, ForceMode2D.Force);
        }

        [SerializeField] private Turret[] m_Turrets;

        public void Fire(TurretMode mode)
        {
            for (int i = 0; i < m_Turrets.Length; i++)
            {
                if (m_Turrets[i].Mode == mode)
                {
                    m_Turrets[i].Fire();
                }
            }
        }

        [SerializeField] private int m_MaxEnergy;
        [SerializeField] private int m_MaxAmmo;
        [SerializeField] private int m_EnergyPerSecond;
        [SerializeField] private int m_MaxThrust;
        [SerializeField] private float m_BonusThrustTime;
        [SerializeField] private float m_InvulnerabilityTime;

        private float m_PrimaryEnergy;
        private int m_SecondaryAmmo;
        private float m_TimerBonusThrust;
        private float m_TimerInvulnerability;

        public void AddEnergy(int e)
        {
            m_PrimaryEnergy = Mathf.Clamp(m_PrimaryEnergy + e, 0, m_MaxEnergy);
        }

        public void AddAmmo(int ammo)
        {
            m_SecondaryAmmo = Mathf.Clamp(m_SecondaryAmmo + ammo, 0, m_MaxAmmo);
        }

        public void Invulnerability(bool activate)
        {
            m_TimerInvulnerability = m_InvulnerabilityTime;

            var cda = GetComponent<CollisionDamageApplicator>();

            if (cda != null)
            {
                cda.isInvulnerable = activate;
            }
        }


        public void AddThrust(float speed)
        {
            m_TimerBonusThrust = m_BonusThrustTime;
            m_Thrust = Mathf.Clamp(m_Thrust + speed, 0, m_MaxThrust);
        }

        private void InitOffensive()
        {
            m_PrimaryEnergy = m_MaxEnergy;
            m_SecondaryAmmo = m_MaxAmmo;
        }

        private void UpdateEnergyRegen()
        {
            m_PrimaryEnergy += (float)m_EnergyPerSecond * Time.fixedDeltaTime;
            m_PrimaryEnergy = Mathf.Clamp(m_PrimaryEnergy, 0, m_MaxEnergy);
        }

        private void UpdateBonusThrust()
        {
            if (m_TimerBonusThrust > 0)
            {
                m_TimerBonusThrust -= Time.fixedDeltaTime;
                if (m_TimerBonusThrust <= 0)
                {
                    m_Thrust = m_StartThrust;
                }
            }
        }

        private void UpdateInvulnerability()
        {
            var cda = GetComponent<CollisionDamageApplicator>();

            if (m_TimerInvulnerability > 0)
            {
                m_TimerInvulnerability -= Time.fixedDeltaTime;
                if (m_TimerInvulnerability <= 0)
                {
                    cda.isInvulnerable = false;
                }
            }
        }

        public bool DrawAmmo(int count)
        {
            if (count == 0)
                return true;

            if (m_SecondaryAmmo >= count)
            {
                m_SecondaryAmmo -= count;
                return true;
            }
            return false;
        }

        public bool DrawEnergy(int count)
        {
            if (count == 0)
                return true;

            if (m_PrimaryEnergy >= count)
            {
                m_PrimaryEnergy -= count;
                return true;
            }
            return false;
        }

        public bool DrawThrust(int count)
        {
            if (count == 0)
                return true;

            if (m_Thrust >= count)
            {
                m_Thrust -= count;
                return true;
            }
            return false;
        }

        public void AssignWeapon(TurretProperties props)
        {
            for (int i = 0; i < m_Turrets.Length; i++)
            {
                m_Turrets[i].AssignLoadout(props);
            }
        }
    }
}
