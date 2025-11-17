using UnityEngine;
using UnityEngine.InputSystem;

//Script fait par moi
public class PlayerControls : MonoBehaviour
{
    [SerializeField] private InputActionAsset m_InputActionAsset;
    [SerializeField] private Transform Gun;
    [SerializeField] private Transform PlayerCamera;
    private CameraShaker Shaker;
    private InputAction ShootAction;
    private Animator GunAnimator;


    void Start()
    {
        GunAnimator = Gun.GetComponent<Animator>();
        ShootAction = m_InputActionAsset.FindAction("Shoot");
        Shaker = gameObject.AddComponent<CameraShaker>();
        Shaker.Camera = PlayerCamera;
    }

    void Update()
    {
        if (ShootAction.WasPressedThisFrame())
        {
            GunAnimator.SetTrigger("Fire");
            Shaker.Shake(1);
        }
    }

 

    public void Shoot()
    {

    }

    public void CasingRelease()
    {
        
    }
}
