using NUnit.Framework;
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
    [SerializeField] private RoamingPointsCointainer _roamingPointsContainer;

    public virtual void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _player = GameObject.FindGameObjectWithTag("Player");

        _stateMachine = new StateMachine();
        _stateMachine.ChangeState(new IdleState(_stateMachine, this));
    }

    public List<Transform> GetRoamingPoints()
    {
        return _roamingPointsContainer.RoamingPoints;
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
        _stateMachine.ChangeState(new RoamState(_stateMachine, this));
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
            _stateMachine.ChangeState(new IdleState(_stateMachine, this));
            return;
        }
        //Sinon il continue de faire ce qu'il faisait
    }
    public virtual void Die()
    {
        Destroy(gameObject);
    }
}
