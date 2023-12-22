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

    Vector3 startpos;
    Vector3 endpos;

    public float timer = 2f;
    private void Start()
    {
       startpos = new Vector3(1f, 1f, 1f);
       endpos = new Vector3(1f, 0f, 1f);
    }


    void Update()
    {
        if(!InArrowMovement)
        {
            InArrowMovement = true;
            Invoke("ArrowMovement", timer);

        }
        if (PuzzleCompleted==true)
        {
            door.transform.localScale = Vector3.Lerp(startpos, endpos, .05f);
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
            InArrowMovement = true;
            
        }

    }    
}

public enum Arrows
{
    Loop,
    SimpleCodeLine,
    EndCodeLine
};