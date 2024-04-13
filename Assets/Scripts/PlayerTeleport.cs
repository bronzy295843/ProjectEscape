using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleport : MonoBehaviour
{
    [SerializeField] private Transform invisFloorPuzzleResetPosition;


    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.GetComponent<CharacterMovement>())
        {

            collision.gameObject.GetComponent<CharacterMovement>().Respawn(invisFloorPuzzleResetPosition.position);
        }
    }


}
