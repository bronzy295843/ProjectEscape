using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableTextBox : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    Vector3 originalPosition;


    private CanvasGroup canvasGroup;

    private void Start()
    {

    }

    private void Awake()
    {
        originalPosition = GetComponentInChildren<TextMeshProUGUI>().rectTransform.anchoredPosition;
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Update()
    {

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        GetComponent<CodeLineInformation>().SelectedLine();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        GetComponentInChildren<TextMeshProUGUI>().transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        GetComponentInChildren<TextMeshProUGUI>().rectTransform.anchoredPosition = originalPosition;
    }
}

