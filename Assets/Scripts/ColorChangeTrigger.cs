using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChangeTrigger : MonoBehaviour
{
    public static event System.Action OnObjectTriggered; 

    private bool isTriggered = false; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isTriggered) 
        {
            
            GetComponent<Renderer>().material.color = Color.green; // Change color to green
            isTriggered = true;
            OnObjectTriggered?.Invoke(); // Notify that this object has been triggered
        }
    }
}
