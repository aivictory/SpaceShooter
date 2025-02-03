using UnityEngine;

namespace SpaceShooter
{
    public class ProjectileExplosion : MonoBehaviour
    {
        public GameObject ExplosionPrefab;

        public void OnExplode()
        {
            Destroy(ExplosionPrefab);
        }
    }
}