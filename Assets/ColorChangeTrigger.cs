using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChangeTrigger : MonoBehaviour
{
    public static event System.Action OnObjectTriggered;
    [SerializeField] GameObject Indicator;

    private bool isTriggered = false; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isTriggered) 
        {
            //Debug.Log("Triggered");
            GetComponent<Renderer>().material.color = Color.green;
            Indicator.GetComponent<Renderer>().material.color = Color.green;
            isTriggered = true;
            OnObjectTriggered?.Invoke(); // Notify that this object has been triggered
        }
    }
}
