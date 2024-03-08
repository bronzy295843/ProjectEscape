using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameHandler : MonoBehaviour
{

    public static GameHandler Instance { get; private set; }

    public GameObject door;
    [SerializeField] private GameObject stairsToFirstFloor;

    public bool PuzzleCompleted = false;
    public bool SidePuzzleStorageRoomCompleted = false;
    public bool StairsPuzzleCompleted = false;

    //public GameObject PuzzlePanel;

    [SerializeField]
    CharacterMovement character;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    void Update()
    {
        if (PuzzleCompleted)
        {
            PuzzlePanel.Instance.DestroyPuzzle();
            door.GetComponent<Door>().OpenDoor();
            character.canMove = true;
            PuzzleCompleted = false;

            character.canInteract = false;
            HideMouseCursor();
        }

        if(StairsPuzzleCompleted)
        {
            PuzzlePanel.Instance.DestroyPuzzle();
            stairsToFirstFloor.SetActive(true);
            character.canMove = true;
            StairsPuzzleCompleted = false;

            character.canInteract = false;
            HideMouseCursor();
        }
    }

    public void PuzzleExit()
    {
        PuzzlePanel.Instance.gameObject.SetActive(false);
        character.canMove = true;

        HideMouseCursor();
    }

    private void HideMouseCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}