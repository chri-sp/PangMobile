using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterShooting : MonoBehaviour
{

    private InputSystem_Actions inputActions;
    private InputAction _shootAction;
    
    [SerializeField] private GameObject m_MuzzleFlash;
    
    private Rigidbody m_Rb;
    private Animator m_Animator;
    private static int Animator_Shoot = Animator.StringToHash("SHOOT");

    private bool m_CanShoot = true;
    
    [SerializeField] private float shootLockMovement = 0.2f;
    [SerializeField] private float shootCooldown = 0.3f;
    private float hideMuzzleFlash = 0.15f;
    
    [SerializeField] private GameObject m_BulletPrefab;
    [SerializeField] private Transform m_FirePoint;

    
    void Awake()
    {
        m_Rb = GetComponent<Rigidbody>();
        m_Animator = GetComponent<Animator>();
        
        inputActions = new InputSystem_Actions();
        _shootAction = inputActions.Player.Attack;
    }
    
    void OnEnable()
    {
        inputActions.Enable();
    }
    
    void OnDisable()
    {
        inputActions.Disable();
    }
    
    
    void OnDestroy()
    {
        inputActions?.Dispose();
    }

    void Update()
    {
        if (GameManager.Instance.CurrentState != GameState.Playing) return;
        
        if (_shootAction.WasPressedThisFrame() && m_CanShoot)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        m_CanShoot = false;
        MuzzleFlash();
        AudioManager.Instance.PlaySFX("shoot");
        
        Instantiate(m_BulletPrefab, m_FirePoint.position, m_FirePoint.rotation);
            
        m_Rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
        m_Animator.SetBool(Animator_Shoot,true);
        
        Invoke("UnlockMovement", shootLockMovement);
        Invoke("UnlockShoot", shootCooldown);
    }
    
    void UnlockMovement()
    {
        m_Rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
        m_Animator.SetBool(Animator_Shoot,false);
    }
    
    void UnlockShoot()
    {
        m_CanShoot = true;
    }

    void MuzzleFlash()
    {
        m_MuzzleFlash.SetActive(true);
        Invoke("HideMuzzleFlash", hideMuzzleFlash);
    }

    void HideMuzzleFlash()
    {
        m_MuzzleFlash.SetActive(false);
    }
}