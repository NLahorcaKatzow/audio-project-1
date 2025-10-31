using UnityEngine;
using DG.Tweening;

public class DragController : MonoBehaviour
{
    private Camera mainCamera;
    private ISeleccionable currentDraggedObject;
    private Rigidbody draggedRigidbody;
    
    [Header("Drag Distance Settings")]
    private float dragDistance = 5f;
    [SerializeField] private float minDragDistance = 1f;
    [SerializeField] private float maxDragDistance = 20f;
    
    [Header("Speed Settings")]
    [SerializeField] private float dragSpeed = 2f;
    [SerializeField] private float zoomSpeed = 3f;
    
    private Tweener distanceTweener;

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
        if (Input.GetMouseButtonDown(1))
        {
            TryStartDrag();
        }

        if (Input.GetMouseButton(1) && currentDraggedObject != null)
        {
            DragObject();
        }

        if (Input.GetMouseButtonUp(1))
        {
            TryStopDrag();
        }

        if (currentDraggedObject != null)
        {
            HandleMouseScroll();
        }
    }

    private void TryStartDrag()
    {
        Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000f))
        {
            ISeleccionable seleccionable = hit.collider.GetComponent<ISeleccionable>();

            if (seleccionable != null && seleccionable.CanBeSelected)
            {
                currentDraggedObject = seleccionable;
                draggedRigidbody = hit.collider.GetComponent<Rigidbody>();

                if (draggedRigidbody != null)
                {
                    draggedRigidbody.useGravity = false;
                    draggedRigidbody.linearVelocity = Vector3.zero;
                    draggedRigidbody.angularVelocity = Vector3.zero;
                }

                currentDraggedObject.OnDragStart();
                Debug.Log("Started dragging: " + hit.collider.gameObject.name);
            }
        }
    }

    private void DragObject()
    {
        if (currentDraggedObject == null || draggedRigidbody == null)
            return;

        Vector3 targetPosition = mainCamera.transform.position + mainCamera.transform.forward * dragDistance;
        currentDraggedObject.OnDrag(targetPosition);

        if (draggedRigidbody != null)
        {
            Vector3 direction = (targetPosition - draggedRigidbody.position).normalized;
            float distance = Vector3.Distance(draggedRigidbody.position, targetPosition);
            
            draggedRigidbody.linearVelocity = direction * distance * dragSpeed;
        }
    }

    private void TryStopDrag()
    {
        if (currentDraggedObject != null)
        {
            if (draggedRigidbody != null)
            {
                draggedRigidbody.useGravity = true;
                draggedRigidbody.linearVelocity = Vector3.zero;
            }

            currentDraggedObject.OnDragEnd();
            Debug.Log("Stopped dragging");
            currentDraggedObject = null;
            draggedRigidbody = null;
        }
    }

    private void HandleMouseScroll()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (Mathf.Abs(scroll) > 0.01f)
        {
            float targetDistance = dragDistance - (scroll * zoomSpeed);
            targetDistance = Mathf.Clamp(targetDistance, minDragDistance, maxDragDistance);

            if (distanceTweener != null)
            {
                distanceTweener.Kill();
            }

            distanceTweener = DOTween.To(() => dragDistance, x => dragDistance = x, targetDistance, 0.3f)
                .SetEase(Ease.OutCubic);

            Debug.Log("Drag distance: " + dragDistance);
        }
    }
}
