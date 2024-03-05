using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacementInteractable : MonoBehaviour
{
    [SerializeField] private int numberOfObjects;
    [SerializeField] private GameObject interactionText;

    [SerializeField] private GameObject door;

    public void ShowInteractPopUp()
    {
        interactionText.SetActive(true);
    }

    void Update()
    {
        if(transform.childCount >= numberOfObjects && !GameHandler.Instance.SidePuzzleStorageRoomCompleted) 
        {
            GameHandler.Instance.SidePuzzleStorageRoomCompleted = true;
            door.GetComponent<Door>().OpenDoor();
        }
    }
}
