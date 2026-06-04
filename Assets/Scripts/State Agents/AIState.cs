using UnityEngine;

public abstract class AIState : MonoBehaviour
{
    protected EnemyStateAgent agent;
    protected BossStateAgent boss;

    public AIState(EnemyStateAgent agent)
    {
        this.agent = agent;
    }
    public AIState(BossStateAgent boss)
    {
        this.boss = boss;
    }

    public string Name => GetType().Name;

    public abstract void OnEnter();
    public abstract void OnUpdate();
    public abstract void OnExit();
}