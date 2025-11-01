using UnityEngine;

public class UIButtonSound : MonoBehaviour
{
    [SerializeField] private AudioClip buttonSound;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void PlayButtonSound()
    {
        if (audioSource != null && buttonSound != null)
        {
            audioSource.PlayOneShot(buttonSound);
        }
        else if (buttonSound == null)
        {
            Debug.LogWarning("Button sound not assigned to " + gameObject.name);
        }
    }

    public void SetButtonSound(AudioClip clip)
    {
        buttonSound = clip;
    }
}
