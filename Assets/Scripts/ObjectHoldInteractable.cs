using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHoldInteractable : MonoBehaviour
{
    [SerializeField] private GameObject interactionText;
    public void ShowInteractPopUp()
    {
        interactionText.SetActive(true);
    }

    public void HideInteractPopUp() 
    {
        interactionText.SetActive(false);
    }

    public void ChangeParent(Transform parent)
    {
        transform.parent = parent;
    }
}
