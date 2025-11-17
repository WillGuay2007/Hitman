using UnityEngine;

public class GunEvents : MonoBehaviour
{
    [SerializeField] PlayerControls playerControls;
    [SerializeField ]private AudioPlayer SoundPlayer;

    public void Shoot()
    {
        SoundPlayer.PlayShootSound();
        playerControls.Shoot();
    }
    public void CasingRelease() => playerControls.CasingRelease();
}
