using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter
{
    public class KillText : MonoBehaviour
    {
        private const string ScoreTextPrefix = "Score: ";

        [SerializeField] private Text m_Text;

        private int lastKills;

        private void Update()
        {
            int numKills = Player.Instance.NumKills;

            if (numKills != lastKills)
            {
                m_Text.text = ScoreTextPrefix + numKills.ToString();
                lastKills = numKills;
            }
        }
    }
}

