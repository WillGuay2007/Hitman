using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using System.Collections;


//Cette classe abstraite gère les citizens et les guards.

public abstract class BasePersonnage : MonoBehaviour, IPersonnage
{
    public float _health = 100;
    public float _maxHealth;
    public StateMachine _stateMachine;
    public NavMeshAgent _navMeshAgent;
    public Animator _animator;
    public GameObject _player;
    public bool IsDead = false;
    public NPC_Infos _npcs_Infos;

    //Mes states pour optimiser
    public IdleState _idleState;
    public RoamState _roamState;
    public FleeState _fleeState;
    public DiedState _diedState;
    public AlertState _alertState;
    public AttackState _attackState;
    public EatFruitState _eatFruitState;
    public GoingForAlarmState _goingForAlarmState;
    public LookAroundState _lookAroundState;

    [SerializeField] private string _currentStateName; //Puisque tu demandais d'afficher la current state dans l'inspecteur.

    public AudioPlayer _audioPlayer;

    public bool _canUpdate = true;

    public PlayerControls _playerControls;
    [SerializeField] private RoamingPointsCointainer _roamingPointsContainer;
    public bool CanRegognizePlayer = false;

    [SerializeField] private Transform _fruitsContainer;
    public List<Transform> _fruits;
    public List<Transform> _alarms;
    [SerializeField] private Transform _alarmContainer;



    public GameObject Mesh;
    public Color MeshColor;

    public virtual void Start()
    {
        _maxHealth = _health;
        _canUpdate = true;

        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _player = GameObject.FindGameObjectWithTag("Player");

        _npcs_Infos = FindAnyObjectByType<NPC_Infos>();

        _fruits = new List<Transform>();

        foreach (Transform fruit in _fruitsContainer)
        {
            _fruits.Add(fruit);
        }
        foreach (Transform alarm in _alarmContainer)
        {
            _alarms.Add(alarm);
        }

        _stateMachine = new StateMachine();
        _playerControls = _player.GetComponent<PlayerControls>();

        _audioPlayer = FindAnyObjectByType<AudioPlayer>();

        _idleState = new IdleState(_stateMachine, this);
        _roamState = new RoamState(_stateMachine, this);
        _fleeState = new FleeState(_stateMachine, this);
        _diedState = new DiedState(_stateMachine, this);
        _eatFruitState = new EatFruitState(_stateMachine, this);
        _alertState = new AlertState(_stateMachine, this);
        _attackState = new AttackState(_stateMachine, this);
        _goingForAlarmState = new GoingForAlarmState(_stateMachine, this);
        _lookAroundState = new LookAroundState(_stateMachine, this);

        Mesh = transform.GetChild(0).gameObject;
        MeshColor = Mesh.GetComponent<SkinnedMeshRenderer>().material.color;

        _stateMachine.ChangeState(_idleState);
    }

    public List<Transform> GetRoamingPoints()
    {
        return _roamingPointsContainer.RoamingPoints;
    }


    public virtual void Update()
    {
        _currentStateName = _stateMachine._currentState.GetType().Name;
        if (!_canUpdate) return;

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
    public virtual void onTakeDamage() { }

    public virtual void TakeDamage(int damageAmount)
    {
        if (IsDead) return;
        StartCoroutine(TakeDamageEffect());
        _health -= damageAmount;
        if (_health > 0) onTakeDamage();
        if (_health <= 0) { IsDead = true; Die(); return; }
        else if (_health <= 30) onCriticalHealth();
    }
    public virtual void Die()
    {
        Mesh.GetComponent<SkinnedMeshRenderer>().material.color = Color.gray;
        MeshColor = Color.gray;
        _stateMachine.ChangeState(_diedState);
    }

    public void DestroyComponents()
    {
        Destroy(_navMeshAgent);
        GetComponent<CapsuleCollider>().isTrigger = true; //Comme ca ils peuvent toujours savoir si ils ont vu un mort.
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

    public Transform GetClosestAlarm()
    {
        Transform closestAlarm = _alarms[0];
        float bestDistance = Mathf.Infinity;
        foreach (Transform alarm in _alarms)
        {
            if (Vector3.Distance(transform.position, alarm.position) < bestDistance)
            {
                bestDistance = Vector3.Distance(transform.position, alarm.position);
                closestAlarm = alarm;
            }
        }
        return closestAlarm;
    }

    public Vector3 getRandomPolarCoordinate(float radius)
    {
        float randomAngle = Random.Range(0, 360f);
        return new Vector3(Mathf.Cos(randomAngle) * radius, Mathf.Sin(randomAngle) * radius);
    } //Je suis conscient que le point peut sortir du nav mesh surface. j'ai fait de mon mieux pour pas que ca arrive en gardant le radius assez bas quand je l'utilise.

    IEnumerator TakeDamageEffect()
    {
        Mesh.GetComponent<SkinnedMeshRenderer>().material.color = Color.yellow;
        yield return new WaitForSeconds(0.2f);
        Mesh.GetComponent<SkinnedMeshRenderer>().material.color = MeshColor;
    }

}
