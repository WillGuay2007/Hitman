using UnityEngine;

public class GunHandler : MonoBehaviour
{
    [SerializeField] PlayerControls playerControls;
    [SerializeField ] private AudioPlayer SoundPlayer;
    [SerializeField] private Shake GunShake;
    [SerializeField] private ParticleSystem MuzzleFlash;
    private bool hasGunEquipped = true;

    public void Shoot() //Event d'animation
    {
        GunShake.start = true;
        SoundPlayer.PlayShootSound();
        MuzzleFlash.Play();
    }
    public void CasingRelease() {} // au cas ou que je mette quelquechose dedan.

    public void ToggleGunEquip()
    {
        SoundPlayer.PlayGunToggleSound();
        bool EquippedStatus = !hasGunEquipped;
        hasGunEquipped = EquippedStatus;
        transform.GetComponent<MeshRenderer>().enabled = EquippedStatus;
        foreach (Transform child in  transform)
        {
            if (!child.GetComponent<MeshRenderer>()) continue;
            child.GetComponent<MeshRenderer>().enabled = EquippedStatus;
        }
    }

    public bool HasGunEquipped()
    {
        return hasGunEquipped;
    }
}
