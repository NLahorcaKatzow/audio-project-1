using UnityEngine;

public class Pelota : SelectableObject
{
    private Rigidbody rb;
    private Vector3 initialPosition;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        initialPosition = transform.position;
    }

    public override void Interactuar()
    {
        base.Interactuar();
        Debug.Log("Pelota interactuada");
    }

    public override void OnDragStart()
    {
        base.OnDragStart();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
        }
    }

    public override void OnDragEnd()
    {
        base.OnDragEnd();
        Debug.Log("Pelota soltada");
    }
}
