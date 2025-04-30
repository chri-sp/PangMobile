using System.Collections;
using UnityEngine;

public class CharacterDeath : MonoBehaviour
{
    [Header("Death Motion Settings")] 
    [SerializeField] private float _jumpUpForce = 5f;
    [SerializeField] private float _backwardForce = 2f;
    [SerializeField] private float _bounceMultiplier = 0.6f;

    [Header("Death Collider Settings")] 
    [SerializeField] private float _sphereCastRadius = 0.5f;
    [SerializeField] private Vector3 _sphereCastOriginOffset = new Vector3(0f, 0.5f, 0f);

    private Vector3 _velocity;
    private const float _gravity = -9.8f;
    private LayerMask _obstacleLayer;

    private Animator _Animator;
    private bool _deathAnimationStarted = false;
    private bool _collisionDetectionEnabled = false;
    private bool _hasBounced = false;

    void Awake()
    {
        _Animator = GetComponent<Animator>();
        _obstacleLayer = LayerMask.GetMask("Obstacle");
    }

    void OnEnable()
    {
        GameManager.OnGameOver += Death;
    }

    void OnDisable()
    {
        GameManager.OnGameOver -= Death;
    }

    void Death()
    {
        StartCoroutine(DeathAnimation());
    }

    IEnumerator DeathAnimation()
    {
        yield return new WaitForSecondsRealtime(0.3f);
        _Animator.Play("DEATH");
        _deathAnimationStarted = true;

        // Initial impulse upward and backward
        float direction = transform.localScale.x > 0 ? -1f : 1f;
        _velocity = new Vector3(_backwardForce * direction, _jumpUpForce);

        // Ensure the character doesn't immediately collide after death
        yield return new WaitForSecondsRealtime(0.5f);
        _collisionDetectionEnabled = true;
    }

    void Update()
    {
        if (!_deathAnimationStarted) return;
        SimulateMotion();

        if (_collisionDetectionEnabled && !_hasBounced)
        {
            CheckBounce();
        }
    }

    // Physics handled manually with unscaled deltaTime because the game is paused on death
    void SimulateMotion()
    {
        _velocity.y += _gravity * Time.unscaledDeltaTime;
        transform.position += _velocity * Time.unscaledDeltaTime;
    }

    void CheckBounce()
    {
        if (Physics.SphereCast(transform.position + _sphereCastOriginOffset, _sphereCastRadius, _velocity.normalized,
                out RaycastHit hit, 0.2f, _obstacleLayer))
        {
            _hasBounced = true;
            Vector3 bounceDirection = Vector3.Reflect(_velocity, hit.normal).normalized;
            _velocity = bounceDirection * _velocity.magnitude * _bounceMultiplier;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position + _sphereCastOriginOffset, _sphereCastRadius);
    }
}