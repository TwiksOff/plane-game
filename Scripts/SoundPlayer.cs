using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;

    private float volume {
        get { return audioSource.volume; }
        set { audioSource.volume = value; }
    }

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = audioClip;
        audioSource.loop = true;
        audioSource.Play();
    }


}
