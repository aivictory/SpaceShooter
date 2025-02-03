using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter
{
    public class ShipSelectionButton : MonoBehaviour
    {
        private const string HPTextPrefix = "HP: ";
        private const string SpeedTextPrefix = "Speed: ";
        private const string AgilityTextPrefix = "Agility: ";

        [SerializeField] private MainMenu m_MainMenu;
        [SerializeField] private SpaceShip m_Prefab;

        [SerializeField] private Text m_ShipName;
        [SerializeField] private Text m_HitPoints;
        [SerializeField] private Text m_Speed;
        [SerializeField] private Text m_Agility;
        [SerializeField] private Image m_Preview;

       
        private void Start()
        {
            if (m_Prefab == null) return;

            m_ShipName.text = m_Prefab.Nickname;
            m_HitPoints.text = HPTextPrefix + m_Prefab.MaxHitPoints.ToString();
            m_Speed.text = SpeedTextPrefix + m_Prefab.MaxLinearVelocity.ToString();
            m_Agility.text = AgilityTextPrefix + m_Prefab.MaxAngularVelocity.ToString();
            m_Preview.sprite = m_Prefab.PreviewImage;

        }

        public void SelectShip()
        {
            Player.SelectedSpaceShip = m_Prefab;
            m_MainMenu.ShowMainPanel();
        }
    }
}

