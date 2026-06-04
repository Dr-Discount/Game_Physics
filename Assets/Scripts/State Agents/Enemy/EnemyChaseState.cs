using UnityEngine;

public class EnemyChaseState : AIState
{
    public EnemyChaseState(EnemyStateAgent agent) : base(agent) {}

    public override void OnEnter()
    {
        agent.mover.Speed = agent.MaxSpeed;
    }

    public override void OnExit()
    {
        
    }

    public override void OnUpdate()
    {
        if (agent.attack)
        {
            agent.stateMachine.PushState<EnemyAttackState>();
        }
    }
}
