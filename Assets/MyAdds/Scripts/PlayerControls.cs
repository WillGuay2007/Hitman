using UnityEngine;
using UnityEngine.InputSystem;

//Script fait par moi
public class PlayerControls : MonoBehaviour
{
    [SerializeField] private InputActionAsset m_InputActionAsset;
    [SerializeField] private Transform Gun;
    [SerializeField] private Transform PlayerCamera;
    private Shake CameraShake;
    private InputAction ShootAction;
    private Animator GunAnimator;


    void Start()
    {
        GunAnimator = Gun.GetComponent<Animator>();
        ShootAction = m_InputActionAsset.FindAction("Shoot");
        CameraShake = PlayerCamera.GetComponent<Shake>();
    }

    void Update()
    {
        if (ShootAction.WasPressedThisFrame())
        {
            CameraShake.start = true;
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
