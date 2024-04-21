using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.GetComponent<CharacterMovement>())
        {
            SoundManager.Instance.PlaySFX("Laser Hit");
            collision.gameObject.GetComponent<CharacterMovement>().Respawn(spawnPoint.position);
        }
    }
}
