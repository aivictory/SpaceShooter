using UnityEngine;
using Common;

namespace SpaceShooter
{
    public class DebrisFragment : MonoBehaviour
    {
        [SerializeField] private Destructible[] m_DebrisFragmentPrefab;
        [SerializeField] private float m_RandomSpeed;
        [SerializeField] private int m_NumFragmentDebris;

        public void SpawnFragmentDebris()
        {
            for (int i = 0; i < m_NumFragmentDebris; i++)
            {
                int index = Random.Range(0, m_DebrisFragmentPrefab.Length);

                GameObject debrisFragment = Instantiate(m_DebrisFragmentPrefab[index].gameObject, transform.position, Quaternion.identity);

                Rigidbody2D rb = debrisFragment.GetComponent<Rigidbody2D>();

                if (rb != null && m_RandomSpeed > 0)
                {
                    rb.velocity = (Vector2)UnityEngine.Random.insideUnitSphere * m_RandomSpeed;
                }
            }
        }
    }
}