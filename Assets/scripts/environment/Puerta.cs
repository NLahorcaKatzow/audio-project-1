using UnityEngine;
using DG.Tweening;

public class Puerta : MonoBehaviour, ISeleccionable
{
    private bool isInteracting = false;
    private bool isDoorOpen = false;
    private float rotationDuration = 0.5f;
    public Transform targetRotation;
    [SerializeField] private bool canBeSelected = false;
    [SerializeField] private AudioClip openSound;
    [SerializeField] private AudioClip closeSound;
    [SerializeField] private string mixerOutput = "SFX";

    public bool CanBeSelected => canBeSelected;

    private void Start()
    {
        // The local AudioSource component is removed, so this block is no longer needed.
    }

    public void Seleccionar()
    {
        Debug.Log("Puerta seleccionada");
    }

    public void Deseleccionar()
    {
        Debug.Log("Puerta deseleccionada");
    }

    public void Interactuar()
    {
        if (isInteracting)
            return;

        isInteracting = true;
        PlayInteractionSound();

        // Determine rotation based on door state
        float rotationAmount = isDoorOpen ? -90f : 90f;

        // Rotate 90 degrees around global Y axis
        targetRotation.DOLocalRotate(targetRotation.localEulerAngles + new Vector3(0, rotationAmount, 0), rotationDuration, RotateMode.FastBeyond360)
            .OnComplete(() => {
            Debug.Log("Interaccion completada");
            isDoorOpen = !isDoorOpen;
            isInteracting = false;});
    }

    public void OnDragStart()
    {
        Debug.Log("Puerta drag started");
    }

    public void OnDrag(Vector3 worldPosition)
    {
        // Doors don't drag
    }

    public void OnDragEnd()
    {
        Debug.Log("Puerta drag ended");
    }

    private void PlayInteractionSound()
    {
        AudioClip soundToPlay = isDoorOpen ? closeSound : openSound;
        if (soundToPlay != null)
        {
            AudioManager.GetInstance().PlaySoundAt(soundToPlay, transform.position, mixerOutput);
        }
    }

}
