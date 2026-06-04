using UnityEngine;
using CGL.Actor;

public class AttackBox : MonoBehaviour
{
    [SerializeField]
    EnemyStateAgent agent;
    [SerializeField]
    BossStateAgent boss;

    private Player player;

    private void OnTriggerEnter(Collider other)
    {
        if (agent != null)
        {
            if (other.gameObject.TryGetComponent<Player>(out player) && agent.attack == false && agent.dead == false)
            {
                agent.attack = true;
                player.Health.TakeDamage(agent.damage);
            }
        } else if (boss != null)
        {
            if (other.gameObject.TryGetComponent<Player>(out player) && agent.attack == false && agent.dead == false)
            {
                agent.attack = true;
                player.Health.TakeDamage(agent.damage);
            }
        } 
    }

    private void OnTriggerStay(Collider other)
    {
        if (agent != null)
        {
            if (other.gameObject.TryGetComponent<Player>(out player) && agent.attack == false && agent.dead == false)
            {
                agent.attack = true;
                player.Health.TakeDamage(agent.damage);
            }
        }
        else if (boss != null)
        {
            if (other.gameObject.TryGetComponent<Player>(out player) && agent.attack == false && agent.dead == false)
            {
                agent.attack = true;
                player.Health.TakeDamage(agent.damage);
            }
        }
    }
}
