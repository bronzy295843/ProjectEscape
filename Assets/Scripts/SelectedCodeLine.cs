using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCodeLine : MonoBehaviour
{
    [SerializeField] private GameObject HighlightedBackground;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnableHighlight()
    {
        HighlightedBackground.SetActive(true);
    }

    public void DisableHighlight()
    {
        HighlightedBackground.SetActive(false);
    }
}
