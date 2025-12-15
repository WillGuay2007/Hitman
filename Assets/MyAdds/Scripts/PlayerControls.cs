using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

//Script fait par moi et non unity.
public class PlayerControls : MonoBehaviour
{
    //Ma nomenclature est pas idéale mais y'est trop tard pour changer ca dans tous mes scripts.
    private float Health = 100;
    [SerializeField] private float m_ShootDelay = 1f;
    [SerializeField] private InputActionAsset m_InputActionAsset;
    [SerializeField] private Transform Gun;
    [SerializeField] private Transform PlayerCamera;
    [SerializeField] private GunHandler MyGunHandler;
    [SerializeField] private int gunDamage;
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] GameObject hurtPanel;
    private float soundRadius = 15f;
    private bool canShoot = true;
    private Shake CameraShake;
    private InputAction ShootAction;
    private InputAction ToggleGunEquipAction;
    private Animator GunAnimator;
    public bool HasGunEquipped = true;
    private NPC_Infos npc_infos;


    void Start()
    {
        GunAnimator = Gun.GetComponent<Animator>();
        ShootAction = m_InputActionAsset.FindAction("Shoot");
        ToggleGunEquipAction = m_InputActionAsset.FindAction("ToggleGunEquip");
        CameraShake = PlayerCamera.GetComponent<Shake>();
        npc_infos = FindAnyObjectByType<NPC_Infos>();
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

        npc_infos.ApplyFunctionToEachNPC(NPC =>
        {
            if (
            NPC._stateMachine._currentState is AlertState ||
            NPC._stateMachine._currentState is DiedState ||
            NPC._stateMachine._currentState is GoingForAlarmState ||
            NPC._stateMachine._currentState is FleeState ||
            NPC._stateMachine._currentState is AttackState
            ) return;


            if (NPC.GetDistanceWithPlayer() <= soundRadius) //Si le npc a entendu applique la logique ci dessous.
            {
                if (NPC is Citizen)
                {
                    NPC._stateMachine.ChangeState(NPC._fleeState); //Je veut pas qu'ils ayent alarmer ca serait trop chaotique, meme si cetais dans les criteres.
                    //Pour le critère: il regarde autour et cherche la source d'alerte, j'ai juste mis flee encore une fois pour garder le réalisme
                }
                else
                {
                    NPC._stateMachine.ChangeState(NPC._lookAroundState); //Tu avais mentionné de mettre un radius mais j'ai utilisé cette facon
                    //J'espere que ca derange pas trop, c'est similaire de toute facon.
                    
                }
            }
            
        }); //J'ai fait mes recherches sur les lambdas.

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
