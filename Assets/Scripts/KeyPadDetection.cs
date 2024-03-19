using TMPro;
using UnityEngine;

public class KeyPadDetection : MonoBehaviour
{
    public TMP_Text text;

    //[SerializeField]
    //GameHandler GameOver;

    [SerializeField] private GameObject PuzzlePanel;

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<CharacterMovement>().SetPuzzleToInteract(PuzzlePanel);
            text.gameObject.SetActive(true);
            other.GetComponent<CharacterMovement>().canInteract = true;
        }
    }
    private void OnTriggerExit(Collider other) {
        if(other.CompareTag("Player")){
            text.gameObject.SetActive(false);
            other.GetComponent<CharacterMovement>().canInteract = false;
        }
    }
    
}
