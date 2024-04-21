using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeamDisable : MonoBehaviour
{
    [SerializeField] private GameObject LaserBeams;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.GetComponent<CharacterMovement>())
        {
            SoundManager.Instance.PlaySFX("Laser Deactivate");
            LaserBeams.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}
