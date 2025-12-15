using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

//Script fait par moi
public class PlayerControls : MonoBehaviour
{
    private float Health = 100;
    [SerializeField] private float m_ShootDelay = 1f;
    [SerializeField] private InputActionAsset m_InputActionAsset;
    [SerializeField] private Transform Gun;
    [SerializeField] private Transform PlayerCamera;
    [SerializeField] private GunHandler MyGunHandler;
    [SerializeField] private int gunDamage;
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] GameObject hurtPanel;
    private bool canShoot = true;
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
            ToggleGun();
        }
        if (ShootAction.WasPressedThisFrame())
        {
            Shoot();
        }
    }

 

    public void Shoot()
    {
        if (!MyGunHandler.HasGunEquipped() || !canShoot) return; //Pas faire la logique de tir si il est pas equipped.
        canShoot = false;
        StartCoroutine(ShootDelayCoroutine());
        CameraShake.start = true;
        GunAnimator.SetTrigger("Fire");

        Ray myRay = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;

        if (Physics.Raycast(myRay, out hit))
        {
            if (hit.collider != null)
            {
                BasePersonnage NPC = hit.collider.gameObject.GetComponent<BasePersonnage>();
                if (NPC == null) return;

                NPC.TakeDamage(gunDamage);

            }
        }
    }

    public void ToggleGun()
    {
        CameraShake.start = true;
        MyGunHandler.ToggleGunEquip();
        HasGunEquipped = MyGunHandler.HasGunEquipped();
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
        StartCoroutine(damageEffect());
        healthText.text = "Health: " + Health.ToString();
        if (Health <= 0) SceneManager.LoadScene("YouLost");
    }

    IEnumerator ShootDelayCoroutine()
    {
        yield return new WaitForSeconds(m_ShootDelay);
        canShoot = true;
    }

    IEnumerator damageEffect()
    {
        hurtPanel.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        hurtPanel.SetActive(false);
    }
}
