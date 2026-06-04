using CGL.Actor;
using CGL.Navigation;
using CGL.UI;
using System.Collections;
using UnityEngine;

public class EnemyStateAgent : MonoBehaviour
{
    [SerializeField]
    public Animator animator;

    [SerializeField]
    float speed;

    [SerializeField]
    public GameObject DropOnDeath;

    [SerializeField]
    public NavMeshMover mover;

    [SerializeField]
    public NavMeshWaypointFollower follower;

    [SerializeField]
    public int damage;

    [SerializeField]
    public Health health;

    [SerializeField]
    public SliderUI bar;

    public PushDownStateMachine stateMachine {  get; private set; } = new PushDownStateMachine();
    public bool attack = false;
    public float timer;
    public bool dead = false;
    public HitBox hitbox;
    public float MaxSpeed;
    public bool gameOver = false;
    public BossStateAgent boss;

    private void Awake()
    {
        MaxSpeed = speed;

        stateMachine.AddState(new EnemyChaseState(this));
        stateMachine.AddState(new EnemyAttackState(this));
        stateMachine.AddState(new EnemyDeadState(this));
    }

    private void Start()
    {
        stateMachine.SetState<EnemyChaseState>();
        TryGetComponent<BossStateAgent>(out boss);
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        stateMachine.Update();
        if (gameOver)
        {
            GameObject health = GameObject.FindWithTag("Health_Bar");
            health.GetComponent<Canvas>().enabled = false;
            StartCoroutine(WinDelay(boss));
        }

        if (stateMachine.stack.Count == 0)
        {
            stateMachine.SetState<EnemyChaseState>();
        }

        UpdateHealth();
    }

    public void OnDeath()
    {
        stateMachine.SetState<EnemyDeadState>();
    }

    public void UpdateHealth()
    {
        if (bar != null)
        {
            bar.OnValueChanged(health.CurrentHealth / health.MaxHealth);
        }
    }

    public IEnumerator WinDelay(BossStateAgent boss)
    {
        yield return new WaitForSeconds(3.0f);
        boss.win.OnWinChanged();
        GameObject.Instantiate(DropOnDeath, transform.position, Quaternion.identity);
        GameObject.Destroy(gameObject);
    }
}
