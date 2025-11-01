using UnityEngine;

public class SelectableObject : MonoBehaviour, ISeleccionable
{
    [SerializeField] private bool canBeSelected = true;
    [SerializeField] private AudioClip interactionSound;
    private AudioSource audioSource;

    public bool CanBeSelected => canBeSelected;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public virtual void Seleccionar()
    {
        Debug.Log(gameObject.name + " selected");
    }

    public virtual void Deseleccionar()
    {
        Debug.Log(gameObject.name + " deselected");
    }

    public virtual void Interactuar()
    {
        Debug.Log(gameObject.name + " interacted");
        PlayInteractionSound();
    }

    public virtual void OnDragStart()
    {
        Debug.Log(gameObject.name + " drag started");
    }

    public virtual void OnDrag(Vector3 worldPosition)
    {
        // Override this method in child classes if needed
    }

    public virtual void OnDragEnd()
    {
        Debug.Log(gameObject.name + " drag ended");
    }

    private void PlayInteractionSound()
    {
        if (audioSource != null && interactionSound != null)
        {
            audioSource.PlayOneShot(interactionSound);
        }
        else if (interactionSound == null)
        {
            Debug.LogWarning(gameObject.name + " has no interaction sound assigned");
        }
    }

    public void SetInteractionSound(AudioClip clip)
    {
        interactionSound = clip;
    }
}
