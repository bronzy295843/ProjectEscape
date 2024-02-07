using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.TerrainTools;
using UnityEngine;

public class Arrowcontroller : MonoBehaviour
{
    [SerializeField]
    List<GameObject> All_Arrows;

    public GameObject door;

    int count = 0;

    bool IsLoop = false;
    bool InArrowMovement = false;

    public bool PuzzleCompleted = false;

    public GameObject PuzzlePanel;

    [SerializeField]
    DraggableTextBox Loop;

    [SerializeField]
    CharacterMovement character;

    public float timer = 2f;

    void Update()
    {
        if(!InArrowMovement)
        {
            InArrowMovement = true;
            Invoke("ArrowMovement", timer);
        }
    }

    void ArrowMovement()
    {
        if(Loop.NewSnap)
        {
            Loop.NewSnap = false;
            All_Arrows[1].SetActive(false);
            All_Arrows.RemoveAt(1);
        }
        
        InArrowMovement = false;

        All_Arrows[count].SetActive(false);

        count++;

        //Loop reverting to start of function
        if (IsLoop)
        {
            //All_Arrows[count - 1].SetActive(false);
            count = 0;
            IsLoop = false;
        }

        All_Arrows[count].SetActive(true);

        Arrows a = All_Arrows[count].GetComponent<Enums>().codeLineType;
        if (a == Arrows.Loop)
        {
            IsLoop = true;
        }
        //Win condition
        /*
            * I am considering for this example that the Code Line with EndCodeLine
            * will always be at the end of the Puzzle Code. So once we find that 
            * we can say that puzzle has been solved.
            */
        else if (a == Arrows.EndCodeLine)
        {
            PuzzleCompleted = true;
            character.canMove = true;
            PuzzlePanel.SetActive(false);
            door.transform.localScale = new Vector3(1f,0.1f,1f);
        }

    }    
}

public enum Arrows
{
    Loop,
    SimpleCodeLine,
    EndCodeLine
};