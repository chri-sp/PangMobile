using UnityEngine;

public class BalloonMovement : MonoBehaviour
{
    [SerializeField] private float m_HorizontalSpeed = 3f;
    [SerializeField] private bool m_StartRightDirection = true;
    private Vector3 m_MoveDir;

    private Rigidbody m_Rb;
    private float m_InitialHeight;

    void Awake()
    {
        m_Rb = GetComponent<Rigidbody>();
        SetRightDirection(m_StartRightDirection);
        m_InitialHeight = transform.position.y;
    }

    void FixedUpdate()
    {
        // Add constant horizontal movement
        m_Rb.linearVelocity = new Vector3(m_MoveDir.x * m_HorizontalSpeed, m_Rb.linearVelocity.y);
    }

    void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.GetContact(0);
        Vector3 normal = contact.normal;

        //Debug.DrawRay(contact.point, normal, Color.green, 4f);

        // Add upward or downward force if collision normal is almost vertical
        if (Mathf.Abs(Vector3.Dot(normal, Vector3.up)) > 0.9f)
        {
            float bounceVelocity = GetBounceVelocity(contact.point.y);

            if (normal.y < 0)
                bounceVelocity = -bounceVelocity;

            m_Rb.linearVelocity = new Vector3(m_Rb.linearVelocity.x, bounceVelocity);
        }

        // Invert balloon direction if collision normal is almost horizontal
        if (Mathf.Abs(Vector3.Dot(normal, Vector3.right)) > 0.8f)
            m_MoveDir = -m_MoveDir;
    }

    //Calculates the required velocity to reach the starting height after the bounce
    private float GetBounceVelocity(float contactHeight)
    {
        float g = -Physics.gravity.y;
        float heightDiff = m_InitialHeight - contactHeight;
        
        // Avoid Nan due to square root of negative values
        if (heightDiff <= 0f)
            heightDiff = 0.1f;
        
        return Mathf.Sqrt(2f * g * heightDiff);
    }
    
    public void SetRightDirection(bool right)
    {
        m_StartRightDirection = right;
        m_MoveDir = m_StartRightDirection ? Vector3.right : Vector3.left;
    }
}
