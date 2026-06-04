using UnityEngine;

public class EnemyAttackState : AIState
{
    public EnemyAttackState(EnemyStateAgent agent) : base(agent) {}

    public override void OnEnter()
    {
        agent.timer = 1.5f;
        agent.animator?.SetTrigger("Attack");
        agent.mover.Speed = 0;
    }

    public override void OnExit()
    {
        
    }

    public override void OnUpdate()
    {
        if (agent.timer < 0)
        {
            agent.stateMachine.PopState();
            agent.attack = false;
        }
    }
}
