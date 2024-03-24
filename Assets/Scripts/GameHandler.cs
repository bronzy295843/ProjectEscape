using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameHandler : MonoBehaviour
{

    public static GameHandler Instance { get; private set; }

    // Starting Room
    public GameObject door;

    // Stairs
    [SerializeField] private GameObject stairsToFirstFloor;

    //1st Floor
    [SerializeField] private GameObject Platform;
    [SerializeField] private GameObject InvisibleWall;

    public bool PuzzleCompleted = false;
    public bool SidePuzzleStorageRoomCompleted = false;
    public bool StairsPuzzleCompleted = false;
    public bool PlatformPuzzleCompleted = false;
    public bool EnemyDisablePuzzleCompleted = false;

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

        if (PlatformPuzzleCompleted)
        {
            PuzzlePanel.Instance.DestroyPuzzle();
            Platform.SetActive(true);
            InvisibleWall.SetActive(false);
            character.canMove = true;
            PlatformPuzzleCompleted = false;

            character.canInteract = false;
            HideMouseCursor();
        }
        if(EnemyDisablePuzzleCompleted)
        {
            PuzzlePanel.Instance.HidePuzzle();
            character.EnemyInteractingWith.GetComponent<EnemyController>().FreezeEnemy();
            character.HideEnemyInteractPopUp();
            EnemyDisablePuzzleCompleted = false;
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