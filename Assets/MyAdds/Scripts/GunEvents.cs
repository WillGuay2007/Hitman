using UnityEngine;

public class GunEvents : MonoBehaviour
{
    [SerializeField] PlayerControls playerControls;
    [SerializeField ] private AudioPlayer SoundPlayer;
    [SerializeField] private Shake GunShake;
    [SerializeField] private ParticleSystem MuzzleFlash;

    public void Shoot()
    {
        GunShake.start = true;
        SoundPlayer.PlayShootSound();
        MuzzleFlash.Play();
        playerControls.Shoot();
    }
    public void CasingRelease() => playerControls.CasingRelease();
}
