using UnityEngine;
using UnityEngine.Events;

public class RedTrigger : MonoBehaviour
{
    [SerializeField] private AudioClip triggerSound;
    [SerializeField] private string mixerOutput = "SFX";
    [SerializeField] private UnityEvent onRedTriggerEnter = new UnityEvent();
    [SerializeField] private bool playOnce = true;
    private bool hasTriggered = false;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Rojo"))
        {
            if (playOnce && hasTriggered)
                return;

            hasTriggered = true;
            ExecuteTriggerEvent();
        }
    }

    private void ExecuteTriggerEvent()
    {
        onRedTriggerEnter.Invoke();
        PlayTriggerSound();
    }

    private void PlayTriggerSound()
    {
        if (triggerSound != null)
        {
            AudioManager.GetInstance().PlaySoundAt(triggerSound, transform.position, mixerOutput, false);
        }
        else
        {
            Debug.LogWarning("Trigger sound not assigned to " + gameObject.name);
        }
    }

    public void ResetTrigger()
    {
        hasTriggered = false;
    }
}
