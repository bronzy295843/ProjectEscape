using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameHandler : MonoBehaviour
{

    public static GameHandler Instance { get; private set; }

    public GameObject door;

    public bool PuzzleCompleted = false;
    public bool SidePuzzleStorageRoomCompleted = false;

    public GameObject PuzzlePanel;

    [SerializeField]
    CharacterMovement character;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    void Update()
    {
        if (PuzzleCompleted == true)
        {
            PuzzlePanel.SetActive(false);
            door.GetComponent<Door>().OpenDoor();
            character.canMove = true;
        }
    }
}