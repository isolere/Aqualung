using UnityEngine;
using UnityEngine.AI;
using Platformer.Mechanics;

/**
 * Controlador general per la IA
 */
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Animator))]
public class AIController : MonoBehaviour
{
    public enum AIState
    {
        Idle,
        Attacking,
        Following,
        Dead
    }

    [SerializeField] private float turnSpeed = 5f;

    [Header("Sensors")] [SerializeField] private Collider2D reachCollider;
    [SerializeField] private Collider2D detectionCollider;

    private bool _alerted = false;

    private AIState _state;

    private Animator _animator;

    private GameObject _target;

    /*private static readonly int _animIDSpeed = Animator.StringToHash("Speed");
    private static readonly int _animIDDead = Animator.StringToHash("Dead");*/

    private Health _health;
    private Rigidbody2D _rigidbody;
    public float moveSpeed = 2f;

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _health = GetComponent<Health>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        reachCollider.GetComponent<TestTrigger>().OnTargetEnter += StartAttack;
        reachCollider.GetComponent<TestTrigger>().OnTargetExit += StartFollowing;

        detectionCollider.GetComponent<TestTrigger>().OnTargetEnter += Alert;

        _health.OnEnemyDeath += ProcessDeath;
    }

    private void ProcessDeath()
    {
        _state = AIState.Dead;
       // _animator.SetTrigger(_animIDDead);
        Disable();
        Destroy(gameObject, 3f);

        GameState.Instance.DecreaseAlert();
    }

    private void Disable()
    {
        Collider2D characterCollider = GetComponent<Collider2D>();
        characterCollider.enabled = false;
        reachCollider.enabled = false;
        detectionCollider.enabled = false;
    }

    private void Update()
    {
        if (_state == AIState.Dead)
        {
            return;
        }

        UpdateMovement();
        UpdateAI();
    }

    private void UpdateMovement()
    {
        //_animator.SetFloat(_animIDSpeed, speed);
    }
    
    private void UpdateAI()
    {
        switch (_state)
        {
            case AIState.Idle:
                break;

            case AIState.Attacking:
                Debug.Log("Atacant");
                FaceTarget();
                break;

            case AIState.Following:
                Vector2 direction = (_target.transform.position - transform.position).normalized;
                _rigidbody.velocity = direction * moveSpeed;
                break;
        }
    }

    private void FaceTarget()
    {
        Vector2 direction = (_target.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector2(direction.x, 0f));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }

    void StartAttack(GameObject target)
    {
        _target = target;
        _state = AIState.Attacking;
    }

    void StartFollowing(GameObject target)
    {
        _target = target;
        _state = AIState.Following;
    }

    void Alert(GameObject target)
    {
        StartFollowing(target);

        if (!_alerted)
        {
            _alerted = true;
            GameState.Instance.IncreaseAlert();
        }
    }
}