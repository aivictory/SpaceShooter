using UnityEngine;

namespace SpaceShooter
{
    public class ExplosionController : MonoBehaviour
    {
        [SerializeField] private GameObject explosionPrefab;
        [SerializeField] private SpaceShip m_ShipExplode;

        private GameObject explosion;

        private void Start()
        {
            m_ShipExplode.EventOnDeath.AddListener(OnExplode);
        }

        private void OnExplode()
        {
            explosion = Instantiate(explosionPrefab, transform.position, transform.rotation);
            Destroy(explosion, 1);
        }
    }
}
