using System.Collections;
using UnityEngine;

public class EnemyDeadState : AIState
{
    public EnemyDeadState(EnemyStateAgent agent) : base(agent) {}

    
    public override void OnEnter()
    {
        agent.mover.Speed = 0;
        agent.animator?.SetTrigger("Dead");
        agent.dead = true;
        agent.hitbox.enable = false;
        if (agent.boss != null)
        {
            agent.gameOver = true;
        }
        else
        {
            GameObject.Instantiate(agent.DropOnDeath, agent.transform.position, Quaternion.identity);
            GameObject.Destroy(agent.gameObject, 3.0f);
        }
    }

    public override void OnExit()
    {

    }

    public override void OnUpdate()
    {
        
    }
}
