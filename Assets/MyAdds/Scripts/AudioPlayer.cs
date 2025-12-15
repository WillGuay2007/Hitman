using UnityEngine;

public class AudioPlayer : MonoBehaviour
{

    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _shoot;
    [SerializeField] AudioClip _eat;
    [SerializeField] AudioClip _spotted;
    [SerializeField] AudioClip _alarm;
    [SerializeField] AudioClip _spottedAttack;
    [SerializeField] AudioClip _death;
    [SerializeField] AudioClip _gunToggle;
    [SerializeField] AudioClip _guardShoot;
    [SerializeField] AudioClip _bulletHit;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void PlaySound(AudioClip sound)
    {
        _audioSource.PlayOneShot(sound);
    }

    public void PlayShootSound() => PlaySound(_shoot);
    public void PlayEatSound() => PlaySound(_eat);
    public void PlaySpottedSound() => PlaySound(_spotted);
    public void PlayAlarmSound() => PlaySound(_alarm);
    public void PlaySpottedAttackSound() => PlaySound(_spottedAttack);
    public void PlayDeathSound() => PlaySound(_death);
    public void PlayGunToggleSound() => PlaySound(_gunToggle);
    public void PlayGuardShootSound() => PlaySound(_guardShoot);
    public void PlayBulletHitSound() => PlaySound(_bulletHit);
}
