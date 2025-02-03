using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter
{
    public class ScoreText : MonoBehaviour
    {


        [SerializeField] private Text m_Text;

        private float lastScoreText;

        private void Update()
        {
            int score = Player.Instance.Score;

            if (score != lastScoreText)
            {
                m_Text.text = "Score: " + score.ToString();
                lastScoreText = score;
            }
        }
    }
}

