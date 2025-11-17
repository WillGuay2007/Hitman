using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;


//Cette classe abstraite gère les citizens et les guards.

public abstract class BasePersonnage : MonoBehaviour, IPersonnage
{
    protected int _health = 100;
    public StateMachine _stateMachine;
    public NavMeshAgent _navMeshAgent;
    public Animator _animator;
    public GameObject _player;

    //Mes states pour optimiser
    public IdleState _idleState;
    public RoamState _roamState;
    public FleeState _fleeState;
    public DiedState _diedState;

    [SerializeField] private RoamingPointsCointainer _roamingPointsContainer;

    public virtual void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _player = GameObject.FindGameObjectWithTag("Player");

        _stateMachine = new StateMachine();

        _idleState = new IdleState(_stateMachine, this);
        _roamState = new RoamState(_stateMachine, this);
        _fleeState = new FleeState(_stateMachine, this);
        _diedState = new DiedState(_stateMachine, this);

        _stateMachine.ChangeState(_idleState);
    }

    public List<Transform> GetRoamingPoints()
    {
        return _roamingPointsContainer.RoamingPoints;
    }


    public virtual void Update()
    {
        _stateMachine.Update();
    }

    public virtual void onIdleEnter() { }
    public virtual void onIdleExit() { }
    public virtual void onIdleUpdate() { }
    public virtual void onDiedEnter() { }
    public virtual void onDiedExit() { }
    public virtual void onDiedUpdate() { }
    public virtual bool onRoamUpdate() { return true; }


    public virtual void onCriticalHealth() { }

    public virtual void TakeDamage(int damageAmount)
    {
        _health -= damageAmount;
        if (_health <= 0) Die();
        else if (_health <= 30) onCriticalHealth();
    }
    public virtual void Die()
    {
        _stateMachine.ChangeState(_diedState);
    }
}
