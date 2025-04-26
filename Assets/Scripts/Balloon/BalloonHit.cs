using UnityEngine;

public class BalloonHit : MonoBehaviour
{
    [SerializeField] private GameObject m_SmallerBalloonPrefab;
    
    private Rigidbody m_Rb; 
    private Collider m_Collider;
    private Animator m_Animator;
    private static int Animator_Pop = Animator.StringToHash("POP");
    [SerializeField] private float popSizeAnimation = 5f;

    void Awake()
    {
        m_Rb = GetComponent<Rigidbody>();
        m_Collider = GetComponent<Collider>();
        m_Animator = GetComponent<Animator>();
    }

    void OnCollisionEnter(Collision collision)
    {
        int bulletLayer = LayerMask.NameToLayer("Bullet");
        if (collision.gameObject.layer == bulletLayer)
        {
            m_Rb.constraints = RigidbodyConstraints.FreezeAll;
            m_Collider.enabled = false;
            
            // Adapt pop animation to balloon size
            transform.localScale = new Vector3(popSizeAnimation, popSizeAnimation, 1f);
            m_Animator.SetBool(Animator_Pop, true);
            
            if (m_SmallerBalloonPrefab != null)
            {
                SpawnSmallerBalloons();
            }

            Destroy(gameObject, 0.5f);
        }
    }

    void SpawnSmallerBalloons()
    {
        Vector3 spawnPos = transform.position;
        
        GameObject balloon1 = Instantiate(m_SmallerBalloonPrefab, spawnPos + Vector3.left * 0.5f, Quaternion.identity);
        GameObject balloon2 = Instantiate(m_SmallerBalloonPrefab, spawnPos + Vector3.right * 0.5f, Quaternion.identity);
        
        balloon1.GetComponent<BalloonMovement>().SetRightDirection(false);
        balloon2.GetComponent<BalloonMovement>().SetRightDirection(true);
    }
}
