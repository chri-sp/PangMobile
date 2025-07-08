using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    
    private InputSystem_Actions _inputActions;
    private InputAction _moveAction;
    
    [SerializeField] private float m_Speed = 5f;
    private Rigidbody m_Rb;
    private float m_Horizontal;
    
    private Animator m_Animator;
    private static int Animator_Walk = Animator.StringToHash("WALK");
    private bool m_InRightDirection = true;
    
    void Awake()
    {
        m_Rb = GetComponent<Rigidbody>();
        m_Animator = GetComponent<Animator>();
        
        _inputActions = new InputSystem_Actions();
        _moveAction = _inputActions.Player.Move;
    }
    
    void OnEnable()
    {
        _inputActions.Enable();
    }
    
    void OnDisable()
    {
        _inputActions.Disable();
    }
    
    void OnDestroy()
    {
        _inputActions?.Dispose();
    }

    void Update()
    {
        if (GameManager.Instance.CurrentState != GameState.Playing) return;
        
        Vector2 moveInput = _moveAction.ReadValue<Vector2>();
        m_Horizontal = moveInput.x;
        Flip();
        
        //check horizontal velocity to avoid walk animation against a wall
        bool bWalk = Mathf.Abs(m_Rb.linearVelocity.x) > 0.1f;
        
        m_Animator.SetBool(Animator_Walk, bWalk);
    }

    void FixedUpdate()
    {
        m_Rb.linearVelocity = new Vector2(m_Horizontal * m_Speed, m_Rb.linearVelocity.y);
    }
    
    void Flip()
    {
        if ((m_Horizontal > 0f && !m_InRightDirection) || (m_Horizontal < 0f && m_InRightDirection))
        {
            m_InRightDirection = !m_InRightDirection;

            Vector3 scale = transform.localScale;
            scale.x *= -1f;
            transform.localScale = scale;
        }
    }
}

