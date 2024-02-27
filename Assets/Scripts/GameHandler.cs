using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.TerrainTools;
using UnityEngine;

public class GameHandler : MonoBehaviour
{

    public static GameHandler Instance { get; private set; }

    public GameObject door;

    public bool PuzzleCompleted = false;

    public GameObject PuzzlePanel;

    [SerializeField]
    CharacterMovement character;

    Vector3 startpos;
    Vector3 endpos;

    private void Start()
    {
        startpos = new Vector3(1f, 1f, 1f);
        endpos = new Vector3(1f, 0f, 1f);
    }

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
            startpos = Vector3.Lerp(startpos, endpos, .005f);
            door.transform.localScale = startpos;
            character.canMove = true;
        }
    }
}