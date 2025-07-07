using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float m_Speed = 8f;
    
    
    private Rigidbody m_Rb;
    private Animator m_Animator;
    private static int Animator_Impact = Animator.StringToHash("IMPACT");
    
    void Awake()
    {
        m_Rb = GetComponent<Rigidbody>();
        m_Animator = GetComponent<Animator>();
    }
    
    void FixedUpdate()
    {
        m_Rb.linearVelocity = transform.up * m_Speed;
    }
    
    void OnCollisionEnter(Collision collision)
    {
        AudioManager.Instance.PlaySFX("bulletImpact", .05f);
        
        m_Rb.constraints = RigidbodyConstraints.FreezeAll;
        
        int obstacleLayer = LayerMask.NameToLayer("Obstacle");
        int balloonLayer = LayerMask.NameToLayer("Balloon");
        if (collision.gameObject.layer == obstacleLayer)
        {
            m_Animator.SetBool(Animator_Impact, true);
            Destroy(gameObject, 1f);
        }
        
        if (collision.gameObject.layer == balloonLayer)
        {
            Destroy(gameObject);
        }
    }
}