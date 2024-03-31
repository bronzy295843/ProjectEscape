using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Vector3 startPos;
    [SerializeField] private Vector3 endPos;

    float speed = 0.25f;

    void Start()
    {
        startPos = transform.localPosition;
    }

    public void OpenDoor()
    {
        StartCoroutine(SlidingOpen());
    }

    private IEnumerator SlidingOpen()
    {
        float time = 0f;
        
        while(time < 1)
        {
            transform.localPosition = Vector3.Lerp(startPos, endPos, time);
            yield return null;
            time += Time.deltaTime * speed;
        }
    }
}
