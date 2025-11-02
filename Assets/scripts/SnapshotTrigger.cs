using UnityEngine;
using UnityEngine.Audio;

public class SnapshotTrigger : MonoBehaviour
{
    [SerializeField] private AudioMixerSnapshot interiorSnapshot;
    [SerializeField] private AudioMixerSnapshot pasilloSnapshot;
    [SerializeField] private float transitionDuration = 0.5f;
    private bool isPlayerInside = false;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player") && !isPlayerInside)
        {
            isPlayerInside = true;
            ActivateInteriorSnapshot();
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("Player") && isPlayerInside)
        {
            isPlayerInside = false;
            ActivatePasilloSnapshot();
        }
    }

    private void ActivateInteriorSnapshot()
    {
        if (interiorSnapshot != null)
        {
            interiorSnapshot.TransitionTo(transitionDuration);
            Debug.Log("Interior snapshot activated");
        }
        else
        {
            Debug.LogError("Interior snapshot not assigned to SnapshotTrigger!");
        }
    }

    private void ActivatePasilloSnapshot()
    {
        if (pasilloSnapshot != null)
        {
            pasilloSnapshot.TransitionTo(transitionDuration);
            Debug.Log("Pasillo snapshot activated");
        }
        else
        {
            Debug.LogError("Pasillo snapshot not assigned to SnapshotTrigger!");
        }
    }

    public void SetTransitionDuration(float duration)
    {
        transitionDuration = duration;
    }
}
