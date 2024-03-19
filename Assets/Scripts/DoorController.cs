using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public GameObject door; 
    public int requiredTriggers = 1; 

    private int currentTriggers = 0; // Current count

    private Vector3 startPos;
    private Vector3 endPos;

    float speed = 0.25f;

    void Start()
    {
        startPos = transform.localPosition;
        endPos = new Vector3(0f, 1.2f, 0f);
    }



    private void OnEnable()
    {
        ColorChangeTrigger.OnObjectTriggered += HandleObjectTriggered;
    }

    private void OnDisable()
    {
        ColorChangeTrigger.OnObjectTriggered -= HandleObjectTriggered;
    }

    private void HandleObjectTriggered()
    {
        currentTriggers++;
        if (currentTriggers >= requiredTriggers)
        {
            OpenDoor();
        }
    }

    private void OpenDoor()
    {
        // Here you can disable the door, play an animation, or whatever signifies the door opening
        door.SetActive(false); // Simple example: just hide the door
        StartCoroutine(SlidingOpen());
    }

    private IEnumerator SlidingOpen()
    {
        float time = 0f;

        while (time < 1)
        {
            transform.localPosition = Vector3.Lerp(startPos, endPos, time);
            yield return null;
            time += Time.deltaTime * speed;
        }
    }
}
