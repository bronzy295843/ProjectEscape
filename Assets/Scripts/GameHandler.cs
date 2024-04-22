using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameHandler : MonoBehaviour
{

    public static GameHandler Instance { get; private set; }

    // Starting Room
    public GameObject door;

    public GameObject playerCrossHair;

    // Stairs
    [SerializeField] private GameObject stairsToFirstFloor;

    //1st Floor
    [SerializeField] private GameObject Platform;
    [SerializeField] private GameObject InvisibleWall;
    [SerializeField] private GameObject TrapRoomInvisibleWall;
    [SerializeField] private GameObject TrapRoomEntranceDoor;
    [SerializeField] private GameObject TrapRoomExitDoor;
    [SerializeField] private GameObject FinalExitDoor;
    [SerializeField] private GameObject HiddenCodeDoor;
    [SerializeField] private GameObject BrainChipRemovedDisplay;

    [SerializeField] private GameObject TrapRoomExitDoorTrigger;

    public bool PuzzleCompleted = false;
    public bool SidePuzzleStorageRoomCompleted = false;
    public bool StairsPuzzleCompleted = false;
    public bool PlatformPuzzleCompleted = false;
    public bool EnemyDisablePuzzleCompleted = false;
    public bool EnemyDisablePuzzleOngoing = false;
    public bool TrapCellFakePuzzleCompleted = false;
    public bool BrainChipPuzzleCompleted = false;
    public bool TrapCellTruePuzzleCompleted = false;
    public bool HiddenDoorPuzzleCompleted = false;
    public bool FinalExitDoorPuzzleCompleted = false;


    [SerializeField]
    CharacterMovement character;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }

    private void Start()
    {
    }

    public void LoadPlayerPosition()
    {
        character.LoadPosition();
        character.canMove = true;
    }

    public void LoadPlayerStartingPosition()
    {
        character.LoadStartingPosition();
        character.canMove = true;
        SoundManager.Instance.PlayMusic("Room1_Awake");
    }

    void Update()
    {
        if (PuzzleCompleted)
        {
            playerCrossHair.SetActive(true);

            PuzzlePanel.Instance.DestroyPuzzle();
            door.GetComponent<Door>().OpenDoor();
            character.canMove = true;
            PuzzleCompleted = false;

            character.canInteract = false;
            HideMouseCursor();
            SoundManager.Instance.PlayMusic("AfterFirstPuzzle");
        }

        if (StairsPuzzleCompleted)
        {
            playerCrossHair.SetActive(true);

            PuzzlePanel.Instance.DestroyPuzzle();
            stairsToFirstFloor.SetActive(true);
            character.canMove = true;
            StairsPuzzleCompleted = false;

            character.canInteract = false;
            HideMouseCursor();
        }

        if (PlatformPuzzleCompleted)
        {
            playerCrossHair.SetActive(true);

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
            playerCrossHair.SetActive(true);

            PuzzlePanel.Instance.HidePuzzle();
            character.EnemyInteractingWith.GetComponent<EnemyController>().FreezeEnemy();
            character.HideEnemyInteractPopUp();
            EnemyDisablePuzzleCompleted = false;
            EnemyDisablePuzzleOngoing = false;

            character.canInteract = false;
            HideMouseCursor();

        }
        if(TrapCellFakePuzzleCompleted)
        {
            playerCrossHair.SetActive(true);

            character.canMove = true;
            character.canInteract = false;

            HideMouseCursor();

            TrapRoomEntranceDoor.SetActive(true);
            TrapRoomInvisibleWall.SetActive(false);

            TrapCellFakePuzzleCompleted = false;
        }
        if (BrainChipPuzzleCompleted)
        {
            playerCrossHair.SetActive(true);

            PuzzlePanel.Instance.DestroyPuzzle();

            character.canMove = true;
            BrainChipPuzzleCompleted = false;

            BrainChipRemovedDisplay.SetActive(true);
            TrapRoomEntranceDoor.SetActive(false);
            TrapRoomExitDoorTrigger.SetActive(true);

            character.canInteract = false;
            HideMouseCursor();
        }
        if (TrapCellTruePuzzleCompleted)
        {
            playerCrossHair.SetActive(true);

            PuzzlePanel.Instance.DestroyPuzzle();

            character.canMove = true;
            TrapCellTruePuzzleCompleted = false;

            TrapRoomExitDoor.GetComponent<Door>().OpenDoor();

            character.canInteract = false;
            HideMouseCursor();
        }
        if(HiddenDoorPuzzleCompleted)
        {
            playerCrossHair.SetActive(true);

            PuzzlePanel.Instance.DestroyPuzzle();

            character.canMove = true;
            HiddenDoorPuzzleCompleted = false;

            HiddenCodeDoor.SetActive(false);

            character.canInteract = false;
            HideMouseCursor();

            SoundManager.Instance.PlayMusic("AfterCodeDoor");
        }
        if (FinalExitDoorPuzzleCompleted)
        {
            playerCrossHair.SetActive(true);

            PuzzlePanel.Instance.DestroyPuzzle();

            character.canMove = false;
            FinalExitDoorPuzzleCompleted = false;

            FinalExitDoor.GetComponent<Door>().OpenDoorAnimation();

            character.canInteract = false;

            PlayerPrefs.SetInt("HasSaveState", 0);
          //  SoundManager.Instance.ToggleMusic();
            UIManager.Instance.ShowGameOverMenu();
        }
    }

    public void PuzzleExit()
    {
        playerCrossHair.SetActive(true);

        PuzzlePanel.Instance.gameObject.SetActive(false);
        character.canMove = true;

        HideMouseCursor();
    }

    public void HideMouseCursor()
    {
       // Debug.Log("Hide Cursor");
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void ShowMouseCursor()
    {
       // Debug.Log("Show Cursor");
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}