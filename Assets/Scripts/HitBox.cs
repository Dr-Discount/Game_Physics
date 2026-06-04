using CGL.Actor;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class HitBox : MonoBehaviour
{
    [SerializeField]
    Health health;

    private Bullet bullet;
    public bool enable = true;

    public void OnHit(int damage)
    {
        health.TakeDamage(damage);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (enable)
        {
            bullet = null;

            other.TryGetComponent<Bullet>(out bullet);
            if (bullet != null)
            {
                OnHit(bullet.Damage);
                Destroy(bullet.gameObject);
            }
        }
    }
}
