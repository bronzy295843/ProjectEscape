using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using TMPro;

public class ObjectDetection : MonoBehaviour
{
    [SerializeField] private Transform ray_pos;

    private GameObject interactedObject;
    private GameObject interactedPlacementObject;

    private bool objectDetected;
    private bool placementObjectDetected;
    void Start()
    {
        
    }

    void Update()
    {
        raycast();
    }


    void raycast()
    {
        int layer_mask = 1 << 2;
        float distance = 4f;           
        layer_mask = ~layer_mask;
        RaycastHit hit;
        if (Physics.Raycast(ray_pos.position, ray_pos.TransformDirection(Vector3.forward), out hit, distance))
        {
            if (hit.transform.gameObject.GetComponent<ObjectHoldInteractable>() && !GetComponent<CharacterMovement>().IsHoldingObject())
            {
                objectDetected = true;
                SetInteractedObject(hit.transform.gameObject);
                hit.transform.gameObject.GetComponent<ObjectHoldInteractable>().ShowInteractPopUp();
                Debug.DrawRay(ray_pos.position, ray_pos.TransformDirection(Vector3.forward) * hit.distance, Color.black);
                //Debug.Log("Hit " + hit.collider.name);
                
            }
            else if (hit.transform.gameObject.GetComponent<ObjectPlacementInteractable>() && GetComponent<CharacterMovement>().IsHoldingObject())
            {
                placementObjectDetected = true;
                SetInteractedPlacementObject(hit.transform.gameObject);
                hit.transform.gameObject.GetComponent<ObjectPlacementInteractable>().ShowInteractPopUp();
            }
            else
            {
                SetInteractedObject(null);
                SetInteractedPlacementObject(null);
                objectDetected = false;
                placementObjectDetected = false;
                GetComponent<CharacterMovement>().HidePickUpInteractPopUp();
                GetComponent<CharacterMovement>().HideDropInteractPopUp();
            }
        }
        else
        {
            Debug.DrawRay(ray_pos.position, ray_pos.TransformDirection(Vector3.forward) * 1000, Color.white);
            //Debug.Log("Did not Hit");
        }
    }

    private void SetInteractedObject(GameObject obj)
    {
        interactedObject = obj;
    }
    public bool AnyObjectDetected()
    {
        return objectDetected;
    }

    public GameObject GetInteractedObject()
    {
        return interactedObject;
    }

    private void SetInteractedPlacementObject(GameObject obj)
    {
        interactedPlacementObject = obj;
    }
    public bool AnyPlacementObjectDetected()
    {
        return placementObjectDetected;
    }

    public GameObject GetInteractedPlacementObject()
    {
        return interactedPlacementObject;
    }
}