using UnityEngine;

public class UIButtonSound : MonoBehaviour
{
    [SerializeField] private AudioClip buttonSound;
    [SerializeField] private string mixerOutput = "UI";

    public void PlayButtonSound()
    {
        if (buttonSound != null)
        {
            AudioManager.GetInstance().PlaySoundAt(buttonSound, transform.position, mixerOutput, true);
        }
        else
        {
            Debug.LogWarning("Button sound not assigned to " + gameObject.name);
        }
    }

    public void SetButtonSound(AudioClip clip)
    {
        buttonSound = clip;
    }
}
