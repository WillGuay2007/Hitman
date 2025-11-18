using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;


//Cette classe abstraite gère les citizens et les guards.

public abstract class BasePersonnage : MonoBehaviour, IPersonnage
{
    protected int _health = 100;
    protected float _maxHealth;
    public StateMachine _stateMachine;
    public NavMeshAgent _navMeshAgent;
    public Animator _animator;
    public GameObject _player;

    //Mes states pour optimiser
    public IdleState _idleState;
    public RoamState _roamState;
    public FleeState _fleeState;
    public DiedState _diedState;

    public PlayerControls _playerControls;
    [SerializeField] private RoamingPointsCointainer _roamingPointsContainer;
    public bool CanRegognizePlayer = false;

    public virtual void Start()
    {

        _maxHealth = _health;

        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _player = GameObject.FindGameObjectWithTag("Player");

        _stateMachine = new StateMachine();
        _playerControls = _player.GetComponent<PlayerControls>();

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
    public virtual void onRoamEnter() { }
    public virtual void onRoamUpdate() { }
    public virtual void onFleeUpdate() { }
    public virtual void onFleeEnter() { }
    public virtual void onCriticalHealth() { }

    public virtual void TakeDamage(int damageAmount)
    {

        _health -= damageAmount;
        if (_health <= 0) Die();
        else if (_health <= 30) onCriticalHealth();

        GameObject Mesh = transform.GetChild(0).gameObject;
        Color MeshColor = Mesh.GetComponent<SkinnedMeshRenderer>().material.color;
        //Modifications sur la couleur ici.
        Mesh.GetComponent<SkinnedMeshRenderer>().material.color = MeshColor;
    }
    public virtual void Die()
    {
        _stateMachine.ChangeState(_diedState);
    }

    public void DestroyNPC()
    {
        Destroy(gameObject);
    }

    public virtual Transform GetRandomPoint()
    {
        List<Transform> Points = GetRoamingPoints();
        return Points[Random.Range(0, Points.Count)];
    }

    public virtual Transform GetRandomPointOutOfPlayerRadius(float Radius)
    {
        List<Transform> Points = GetRoamingPoints();
        Transform ChosenPoint = GetRandomPoint();

        while (Vector3.Distance(ChosenPoint.position, _player.transform.position) <= Radius)
        {
            ChosenPoint = GetRandomPoint();
        }
        return ChosenPoint;
    }

    public virtual Transform GetFurthestPointFromPlayer()
    {
        List<Transform> Points = GetRoamingPoints();
        Vector3 PlayerPosition = _player.transform.position;
        Transform FurthestPoint = null;
        float FurthestDistance = 0f;

        foreach (Transform t in Points)
        {
            float Distance = Vector3.Distance(t.position, PlayerPosition);
            if (Distance > FurthestDistance)
            {
                FurthestDistance = Distance;
                FurthestPoint = t;
            }
        }
        return FurthestPoint;
    }

}
