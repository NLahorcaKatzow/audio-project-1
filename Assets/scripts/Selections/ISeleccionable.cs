using UnityEngine;

public interface ISeleccionable
{
    bool CanBeSelected { get; }
    
    void Seleccionar();
    void Deseleccionar();
    void Interactuar();
    void OnDragStart();
    void OnDrag(Vector3 worldPosition);
    void OnDragEnd();
}
