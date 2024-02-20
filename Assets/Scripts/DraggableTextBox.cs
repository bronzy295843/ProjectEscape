using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableTextBox : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private bool isDragging = false;
    private Vector2 offset;
    private Vector3 position;

    public bool NewSnap = false;

    private void Start()
    {
        position = transform.localPosition;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        isDragging = true;
        offset = eventData.position - (Vector2) GetComponentInChildren<TextMeshProUGUI>().transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDragging)
        {
            GetComponentInChildren<TextMeshProUGUI>().transform.position = eventData.position - offset;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;

        // Check if the textbox is in a snappable position
        // You need to implement your logic for snapping here
        // For simplicity, let's assume there's a SnapArea script on snappable areas

        SnapArea[] snapAreas = FindObjectsOfType<SnapArea>();
        foreach (SnapArea snapArea in snapAreas)
        {
            if (snapArea.IsInSnapRange(GetComponentInChildren<TextMeshProUGUI>().transform.position))
            {
                // Snap to the snappable area
                GetComponentInChildren<TextMeshProUGUI>().transform.position = snapArea.GetSnapPosition();
                NewSnap = true;
                return;
            }
        }

        // If not in a snappable area, return to the original position
        // You can modify this part based on your requirements
        GetComponentInChildren<TextMeshProUGUI>().transform.localPosition = position;
    }
}

