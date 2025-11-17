using UnityEngine;
using UnityEngine.InputSystem;

//Script fait par moi
public class PlayerControls : MonoBehaviour
{
    [SerializeField] private InputActionAsset m_InputActionAsset;
    private InputAction m_Shoot;
    [SerializeField] private Transform Gun;
    private Animator GunAnimator;
    void Start()
    {
        GunAnimator = Gun.GetComponent<Animator>();
        m_Shoot = m_InputActionAsset.FindAction("Shoot");
    }

    void Update()
    {
        if (m_Shoot.WasPressedThisFrame())
        {
            GunAnimator.SetTrigger("Fire");
        }
    }

 

    public void Shoot()
    {
        
    }

    public void CasingRelease()
    {
        
    }
}
