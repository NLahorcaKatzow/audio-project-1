using UnityEngine;

public class EnableDisable : MonoBehaviour
{
    [SerializeField] private AudioClip openUISound;
    [SerializeField] private AudioClip closeUISound;
    [SerializeField] private string mixerOutput = "UI";



    private void OnDisable() {
        AudioManager.GetInstance().ExitUI();
        PlayCloseSound();
    }

    private void OnEnable() {
        AudioManager.GetInstance().EnterUI();
        PlayOpenSound();
    }

    private void PlayOpenSound()
    {
        if (openUISound != null)
        {
            AudioManager.GetInstance().PlaySoundAt(openUISound, transform.position, mixerOutput, true);
        }
    }

    private void PlayCloseSound()
    {
        if (closeUISound != null)
        {
            AudioManager.GetInstance().PlaySoundAt(closeUISound, transform.position, mixerOutput, true);
        }
    }
    
    public void ExitApplication()
    {
        Application.Quit();
    }

}
