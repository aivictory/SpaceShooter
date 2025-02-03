using UnityEngine;
using SpaceShooter;

namespace Common
{


    public class ShipInputController : MonoBehaviour
    {
        public enum ControlMode
        {
            Keyboard,
            Mobile
        }

        [SerializeField] private ControlMode m_ControlMode;

        public void Construct (VirtualGamepad virtualGamepad)
        {
            m_VirtualGamepad = virtualGamepad;
        }

        private SpaceShip m_TargetShip;
        public void SetTargetShip(SpaceShip ship) => m_TargetShip = ship;
        private VirtualGamepad m_VirtualGamepad;

        private void Start()
        {
            if (m_ControlMode == ControlMode.Keyboard)
            {
                m_VirtualGamepad.VirtualJoystick.gameObject.SetActive(false);

                m_VirtualGamepad.MobileFirePrimary.gameObject.SetActive(false);
                m_VirtualGamepad.MobileFireSecondary.gameObject.SetActive(false);
            }

            else
            {
                m_VirtualGamepad.VirtualJoystick.gameObject.SetActive(true);

                m_VirtualGamepad.MobileFirePrimary.gameObject.SetActive(true);
                m_VirtualGamepad.MobileFireSecondary.gameObject.SetActive(true);
            }
        }

        private void Update()
        {

            if (m_TargetShip == null) return;

            if (m_ControlMode == ControlMode.Keyboard)
                ControllKeyboard();

            if (m_ControlMode == ControlMode.Mobile)
                ControllMobile();
        }

        private void ControllMobile()
        {
            Vector3 dir = m_VirtualGamepad.VirtualJoystick.Value;

            var dot = Vector2.Dot(dir, m_TargetShip.transform.up);
            var dot2 = Vector2.Dot(dir, m_TargetShip.transform.right);

            if (m_VirtualGamepad.MobileFirePrimary.IsHold == true)
            {
                m_TargetShip.Fire(TurretMode.Primary);
            }

            if (m_VirtualGamepad.MobileFireSecondary.IsHold == true)
            {
                m_TargetShip.Fire(TurretMode.Secondary);
            }

            m_TargetShip.ThrustControl = Mathf.Max(0, dot);
            m_TargetShip.TorqueControl = -dot2;
        }

        private void ControllKeyboard()
        {
            float thrust = 0;
            float torque = 0;

            if (Input.GetKey(KeyCode.UpArrow))
                thrust = 1.0f;

            if (Input.GetKey(KeyCode.DownArrow))
                thrust = -1.0f;

            if (Input.GetKey(KeyCode.LeftArrow))
                torque = 1.0f;

            if (Input.GetKey(KeyCode.RightArrow))
                torque = -1.0f;

            if (Input.GetKey(KeyCode.Space))
            {
                m_TargetShip.Fire(TurretMode.Primary);
            }

            if (Input.GetKey(KeyCode.X))
            {
                m_TargetShip.Fire(TurretMode.Secondary);
            }

            m_TargetShip.ThrustControl = thrust;
            m_TargetShip.TorqueControl = torque;
        }
    }
}