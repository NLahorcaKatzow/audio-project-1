using UnityEngine;

public class RaycastClickController : MonoBehaviour
{
    private Camera mainCamera;
    public float rayDistance = 1000f;

    private void Start()
    {
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("Main camera not found!");
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse button down");
            PerformRaycast();
        }
    }

    private void PerformRaycast()
    {
        Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            Debug.Log("Raycast hit: " + hit.collider.gameObject.name);
            ISeleccionable seleccionable = hit.collider.GetComponent<ISeleccionable>();

            if (seleccionable != null)
            {
                Debug.Log("Object clicked: " + hit.collider.gameObject.name);
                seleccionable.Interactuar();
            }
        }
    }
}
