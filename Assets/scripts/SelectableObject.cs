using UnityEngine;

public class SelectableObject : MonoBehaviour, ISeleccionable
{
    [SerializeField] private bool canBeSelected = true;

    public bool CanBeSelected => canBeSelected;

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
}
