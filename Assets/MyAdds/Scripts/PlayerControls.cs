using UnityEngine;
using UnityEngine.InputSystem;

//Script fait par moi
public class PlayerControls : MonoBehaviour
{
    [SerializeField] private InputActionAsset m_InputActionAsset;
    [SerializeField] private Transform Gun;
    [SerializeField] private Transform PlayerCamera;
    [SerializeField] private GunHandler MyGunHandler;
    private Shake CameraShake;
    private InputAction ShootAction;
    private InputAction ToggleGunEquipAction;
    private Animator GunAnimator;
    public bool HasGunEquipped = true;


    void Start()
    {
        GunAnimator = Gun.GetComponent<Animator>();
        ShootAction = m_InputActionAsset.FindAction("Shoot");
        ToggleGunEquipAction = m_InputActionAsset.FindAction("ToggleGunEquip");
        CameraShake = PlayerCamera.GetComponent<Shake>();
    }

    void Update()
    {
        if (ToggleGunEquipAction.WasPressedThisFrame())
        {
            CameraShake.start = true;
            MyGunHandler.ToggleGunEquip();
            HasGunEquipped = MyGunHandler.HasGunEquipped();
        }
        if (ShootAction.WasPressedThisFrame())
        {

            CameraShake.start = true;
            GunAnimator.SetTrigger("Fire");

            if (!MyGunHandler.HasGunEquipped()) return; //Pas faire la logique de tir si il est pas equipped.
            Ray myRay = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            RaycastHit hit;

            if (Physics.Raycast(myRay, out hit))
            {
                if (hit.collider != null)
                {
                    BasePersonnage NPC = hit.collider.gameObject.GetComponent<BasePersonnage>();
                    if (NPC == null) return;

                    NPC.TakeDamage(10);

                }
            }
        }
    }

 

    public void Shoot()
    {

    }

    public void CasingRelease()
    {
        
    }
}
