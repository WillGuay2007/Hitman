using UnityEngine;
using UnityEngine.AI;

//Cette classe abstraite gère les citizens et les guards.

public abstract class BasePersonnage : MonoBehaviour, IPersonnage
{
    protected int _health = 100;
    public StateMachine _stateMachine;
    public NavMeshAgent _navMeshAgent;
    public Animator _animator;

    public virtual void Start()
    {
        _stateMachine = new StateMachine();
        _stateMachine.ChangeState(new IdleState(_stateMachine, this));
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }

    public virtual void Update()
    {
        _stateMachine.Update();
    }

    public virtual void Idle() {
        _stateMachine.ChangeState(new IdleState(_stateMachine, this));
    }
    public virtual void RoamAround()
    {

    }
    public virtual void TakeDamage(int damageAmount)
    {
        _health -= damageAmount;
        if (_health <= 0)
        {
            Die();
            return;
        }
        if (_health <= 30)
        {
            _stateMachine.ChangeState(new FleeState(_stateMachine, this));
            return;
        }
        //Sinon il continue de faire ce qu'il faisait
    }
    public virtual void Die()
    {
        Destroy(gameObject);
    }
}
