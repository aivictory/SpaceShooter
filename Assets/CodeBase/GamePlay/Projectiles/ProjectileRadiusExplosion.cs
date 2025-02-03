using UnityEngine;
using Common;

namespace SpaceShooter
{
    public class ProjectileRadiusExplosion : ProjectileBase
    {
        [SerializeField] private CircleArea m_Radius;
        [SerializeField] private int m_damage;

        protected override void OnProjectileLifeEnd(Collider2D col, Vector2 pos)
        {
            var colliders = Physics2D.OverlapCircleAll(transform.position, m_Radius.Radius);

            foreach (var coli in colliders)
            {
                var dest = coli.transform.root.GetComponent<Destructible>();

                if (dest != null && dest != m_Parent)
                    dest.ApplyDamage(m_damage);
            }

            base.OnProjectileLifeEnd(col, pos);
        }
    }
}