using TMPro;
using UnityEngine;

public class KeyPadDetection : MonoBehaviour
{
    public TMP_Text text;

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")){
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
