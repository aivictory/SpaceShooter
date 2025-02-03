using UnityEngine;
using Common;

namespace SpaceShooter
{
    public class EntitySpawnerDebris : MonoBehaviour
    {
        [SerializeField] private Destructible[] m_DebrisPrefabs;
        [SerializeField] private DebrisFragment m_DebrisFragment;
        [SerializeField] private int m_NumDebris;
        [SerializeField] private CircleArea m_Area;
        [SerializeField] private float m_RandomSpeed;

        private void Start()
        {
            for (int i = 0; i < m_NumDebris; i++)
            {
                SpawnDebris();
            }
        }

        private void SpawnDebris()
        {
            int index = Random.Range(0, m_DebrisPrefabs.Length);

            GameObject debris = Instantiate(m_DebrisPrefabs[index].gameObject);

            debris.transform.position = m_Area.GetRandomInsideZone();
            debris.GetComponent<Destructible>().EventOnDeath.AddListener(OnDebrisDead);

            DebrisFragment df = debris.GetComponent<DebrisFragment>();
            df.GetComponent<Destructible>().EventOnDeath.AddListener(df.SpawnFragmentDebris);

            Rigidbody2D rb = debris.GetComponent<Rigidbody2D>();

            if (rb != null && m_RandomSpeed > 0)
            {
                rb.velocity = (Vector2)UnityEngine.Random.insideUnitSphere * m_RandomSpeed;
            }
        }

        private void OnDebrisDead()
        {
            if (m_DebrisFragment != null)
            {
                m_DebrisFragment.SpawnFragmentDebris();
            }

            SpawnDebris();
        }
    }
}