using UnityEngine;

//Script fait par moi
public class PlayerControls : MonoBehaviour
{

    [SerializeField] private Transform Gun;
    private Animator GunAnimator;
    void Start()
    {
        GunAnimator = Gun.GetComponent<Animator>();
    }

    void Update()
    {
        GunAnimator.SetTrigger("Fire");
    }

    public void Shoot()
    {
        
    }

    public void CasingRelease()
    {
        
    }
}
