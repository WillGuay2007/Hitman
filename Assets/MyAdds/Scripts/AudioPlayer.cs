using UnityEngine;

public class AudioPlayer : MonoBehaviour
{

    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _shoot;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void PlaySound(AudioClip sound)
    {
        _audioSource.PlayOneShot(sound);
    }

    public void PlayShootSound() => PlaySound(_shoot);
}
