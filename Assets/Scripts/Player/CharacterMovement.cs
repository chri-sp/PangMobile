using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
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
    }

    void Update()
    {
        if (GameManager.Instance.CurrentState != GameState.Playing) return;
        
        m_Horizontal = Input.GetAxisRaw("Horizontal");
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

