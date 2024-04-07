using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class PuzzlePanel : MonoBehaviour
{
    public static PuzzlePanel Instance;

    [SerializeField] private int puzzleNumber;

    [SerializeField] private TypeOfLine[] codeLine;
    [SerializeField] private TypeOfLine[] toolBox;

    //private TypeOfLine[] tempCodeLine;
    //private TypeOfLine[] tempToolBox;

    private Line[] tempCodeLineTypes;
    private Line[] tempToolBoxTypes;

    [SerializeField] private float yDeviation_codeBox = 80f;
    [SerializeField] private float xDeviation_codeBox = -300f;

    [SerializeField] private float yDeviation_toolBox = 80f;
    [SerializeField] private float xDeviation_toolBox = 400f;
    [SerializeField] private float spaceBetweenLines = 100f;

    [SerializeField] private float delay = 1f;
    private float lastTime = 0f;

    private int codeLineIndex = 1;

    private Line selectedCodeLineType = Line.Empty;
    private string selectedCodeLineText;
    private GameObject selectedCodeLinePrefab;

    [SerializeField] GameObject PuzzleTrigger;

    private int step_count = 1;
    [SerializeField] private int maxStepCount;

    [SerializeField] private TextMeshProUGUI WatcherOfVariables;
    [SerializeField] private TextMeshProUGUI WatcherOfVariablesEnemy_boolText;
    [SerializeField] private TextMeshProUGUI WatcherOfVariablesEnemy_countText;

    private string promptString = "";
    [SerializeField] private TMP_InputField promptField;

    //private bool isEnemyHacked = false;
    private int freeze_count;
    [SerializeField] private int maxFreezeCount;

    private int DNACount = 0;

    void Start()
    {
        InstantiateCodeLines(codeLine,xDeviation_codeBox, yDeviation_codeBox);
        SetText(codeLine);

        InstantiateCodeLines(toolBox, xDeviation_toolBox, yDeviation_toolBox);
        SetText(toolBox);

        if (puzzleNumber == 5)
            TrapPuzzleFake();
    }

    void Awake()
    {
        if (Instance == null)
            Instance = this;

    }

    void Update()
    {
        if (puzzleNumber == 1 || puzzleNumber == 3 || puzzleNumber == 7)
            RunCodeBlock();

        else if (puzzleNumber == 2)
            RunStairsPuzzleCodeBlock();
        else if (puzzleNumber == 0)
            RunDisableEnemyPuzzleCodeBlock();
        else if (puzzleNumber == 6)
            BrainChipPuzzle();
        else if (puzzleNumber == 8)
            HiddenCodePuzzle();
        else if (puzzleNumber == 10)
            FinalExitPuzzle();
    }

    

    private void SetText(TypeOfLine[] codeLine)
    {
        for (int i = 0; i < codeLine.Length; i++)
        {
            codeLine[i].linePrefab.GetComponentInChildren<TextMeshProUGUI>().text = codeLine[i].text;
            codeLine[i].linePrefab.GetComponent<CodeLineInformation>().SetCodeLineText(codeLine[i].text);
            codeLine[i].linePrefab.GetComponent<CodeLineInformation>().SetCodeLineType(codeLine[i].lineType);
        }
    }

    private void InstantiateCodeLines(TypeOfLine[] codeLine, float xDeviation, float yDeviation)
    {
        for (int i = 0; i < codeLine.Length; i++) 
        {
            GameObject instantiatedLine = Instantiate(codeLine[i].linePrefab, transform);

            instantiatedLine.GetComponent<RectTransform>().anchoredPosition += Vector2.up * yDeviation;
            instantiatedLine.GetComponent<RectTransform>().anchoredPosition += Vector2.right * xDeviation;
            yDeviation -= spaceBetweenLines;
            codeLine[i].linePrefab = instantiatedLine;
        }
        SaveInitialState();
    }

    public void UpdateCodeLines()
    {
        for (int i = 0; i < codeLine.Length; i++)
        {
            codeLine[i].linePrefab.GetComponentInChildren<TextMeshProUGUI>().text = codeLine[i].linePrefab.GetComponent<CodeLineInformation>().GetCodeLineText();
            codeLine[i].lineType = codeLine[i].linePrefab.GetComponent<CodeLineInformation>().GetLineType();
        }

        for(int i = 0; i< toolBox.Length; i++)
        {
           toolBox[i].linePrefab.GetComponentInChildren<TextMeshProUGUI>().text = toolBox[i].linePrefab.GetComponent<CodeLineInformation>().GetCodeLineText();
           toolBox[i].lineType = toolBox[i].linePrefab.GetComponent<CodeLineInformation>().GetLineType();
        }
    }

    private void RunStairsPuzzleCodeBlock()
    {

        codeLine[codeLineIndex].linePrefab.GetComponentInChildren<SelectedCodeLine>().EnableHighlight();

        
        if (Time.time - lastTime > delay)
        {
            codeLine[codeLineIndex].linePrefab.GetComponentInChildren<SelectedCodeLine>().DisableHighlight();

            if (codeLineIndex >= codeLine.Length || codeLine[codeLineIndex].lineType == Line.Loop)
                codeLineIndex = 1;
            else if(codeLineIndex >= codeLine.Length || codeLine[codeLineIndex].lineType == Line.Empty)
            {

            }
            else if(codeLineIndex >= codeLine.Length || codeLine[codeLineIndex].lineType == Line.Increment)
            {
                step_count++;
                codeLineIndex++;

                WatcherOfVariables.text = step_count.ToString() + " of " + maxStepCount.ToString();
            }
            else if (codeLineIndex >= codeLine.Length || codeLine[codeLineIndex].lineType == Line.If)
            {
                codeLineIndex++;
                if (step_count >= maxStepCount)
                    GameHandler.Instance.StairsPuzzleCompleted = true;
            }
            //else if(codeLineIndex >= codeLine.Length || codeLine[codeLineIndex].lineType == Line.EndCodeLine)
            //{
            //    GameHandler.Instance.PuzzleCompleted = true;
            //}
            else 
                codeLineIndex++;
            codeLine[codeLineIndex].linePrefab.GetComponentInChildren<SelectedCodeLine>().EnableHighlight();
            lastTime = Time.time;
        }
        
    }

    private void RunDisableEnemyPuzzleCodeBlock()
    {

        GameHandler.Instance.EnemyDisablePuzzleOngoing = true;
        codeLine[codeLineIndex].linePrefab.GetComponentInChildren<SelectedCodeLine>().EnableHighlight();


        if (Time.time - lastTime > delay)
        {
            codeLine[codeLineIndex].linePrefab.GetComponentInChildren<SelectedCodeLine>().DisableHighlight();

            if (codeLineIndex >= codeLine.Length || codeLine[codeLineIndex].lineType == Line.Loop)
                codeLineIndex = 1;
            else if (codeLineIndex >= codeLine.Length || codeLine[codeLineIndex].lineType == Line.Empty)
            {

            }
            else if(codeLineIndex >= codeLine.Length || codeLine[codeLineIndex].lineType == Line.Bool)
            {
                codeLineIndex++;
                WatcherOfVariablesEnemy_boolText.text = "EnemyHacked = true";
            }    
            else if (codeLineIndex >= codeLine.Length || codeLine[codeLineIndex].lineType == Line.Increment)
            {
                if(freeze_count < maxFreezeCount)
                    freeze_count++;
                codeLineIndex++;

                WatcherOfVariablesEnemy_countText.text = "FreezeCount = " + freeze_count;
            }
            else if (codeLineIndex >= codeLine.Length || codeLine[codeLineIndex].lineType == Line.If)
            {
                codeLineIndex++;
                if (freeze_count == maxFreezeCount)
                    codeLineIndex++;
            }
            else if (codeLineIndex >= codeLine.Length || codeLine[codeLineIndex].lineType == Line.EndCodeLine)
            {
                if (freeze_count >= maxFreezeCount)
                {
                    GameHandler.Instance.EnemyDisablePuzzleCompleted = true;
                    Reset();
                }

            }
            else
                codeLineIndex++;
            codeLine[codeLineIndex].linePrefab.GetComponentInChildren<SelectedCodeLine>().EnableHighlight();
            lastTime = Time.time;
        }

    }

    private void RunCodeBlock()
    {

        codeLine[codeLineIndex].linePrefab.GetComponentInChildren<SelectedCodeLine>().EnableHighlight();


        if (Time.time - lastTime > delay)
        {
            codeLine[codeLineIndex].linePrefab.GetComponentInChildren<SelectedCodeLine>().DisableHighlight();

            if (codeLineIndex >= codeLine.Length || codeLine[codeLineIndex].lineType == Line.Loop)
                codeLineIndex = 1;
            else if (codeLineIndex >= codeLine.Length || codeLine[codeLineIndex].lineType == Line.Empty)
            {

            }
            else if (codeLineIndex >= codeLine.Length || codeLine[codeLineIndex].lineType == Line.EndCodeLine)
            {
                if (puzzleNumber == 1)
                    GameHandler.Instance.PuzzleCompleted = true;
                else if (puzzleNumber == 3)
                    GameHandler.Instance.PlatformPuzzleCompleted = true;
                else if (puzzleNumber == 7)
                    GameHandler.Instance.TrapCellTruePuzzleCompleted = true;
            }
            else
                codeLineIndex++;
            codeLine[codeLineIndex].linePrefab.GetComponentInChildren<SelectedCodeLine>().EnableHighlight();
            lastTime = Time.time;
        }
    }

    private void TrapPuzzleFake()
    {
        SoundManager.Instance.PlayMusic("YouAreTrapped!");
        Destroy(PuzzleTrigger);
        Destroy(this.gameObject, 1f);
        GameHandler.Instance.TrapCellFakePuzzleCompleted = true;
    }

    private void BrainChipPuzzle()
    {

        codeLine[codeLineIndex].linePrefab.GetComponentInChildren<SelectedCodeLine>().EnableHighlight();


        if (Time.time - lastTime > delay)
        {
            codeLine[codeLineIndex].linePrefab.GetComponentInChildren<SelectedCodeLine>().DisableHighlight();

            if (codeLineIndex >= codeLine.Length || codeLine[codeLineIndex].lineType == Line.Loop)
                codeLineIndex = 1;
            else if (codeLineIndex >= codeLine.Length || codeLine[codeLineIndex].lineType == Line.Empty)
            {

            }
            else if(codeLineIndex >= codeLine.Length || codeLine[codeLineIndex].lineType == Line.LocalVariable)
            {
                codeLine[codeLineIndex].linePrefab.GetComponentInChildren<TextMeshProUGUI>().text = promptString;
                codeLine[codeLineIndex].linePrefab.GetComponent<CodeLineInformation>().SetCodeLineText(promptString);
                codeLineIndex++;
                
            }
            else if (codeLineIndex >= codeLine.Length || codeLine[codeLineIndex].lineType == Line.If)
            {
                if (promptString == "hack is 1")
                    codeLineIndex++;
                else
                    codeLineIndex += 2;
            }
            else if (codeLineIndex >= codeLine.Length || codeLine[codeLineIndex].lineType == Line.EndCodeLine)
            {
                GameHandler.Instance.BrainChipPuzzleCompleted = true;
            }
            else
                codeLineIndex++;
            codeLine[codeLineIndex].linePrefab.GetComponentInChildren<SelectedCodeLine>().EnableHighlight();
            lastTime = Time.time;
        }
    }

    private void HiddenCodePuzzle()
    {
        if (promptString == "40395")
        {
            GameHandler.Instance.HiddenDoorPuzzleCompleted = true;
        }
    }
    private void FinalExitPuzzle()
    {

        codeLine[codeLineIndex].linePrefab.GetComponentInChildren<SelectedCodeLine>().EnableHighlight();


        if (Time.time - lastTime > delay)
        {
            codeLine[codeLineIndex].linePrefab.GetComponentInChildren<SelectedCodeLine>().DisableHighlight();

            
            if (codeLineIndex >= codeLine.Length || codeLine[codeLineIndex].lineType == Line.Loop)
                codeLineIndex = 1;
            else if (codeLineIndex >= codeLine.Length || codeLine[codeLineIndex].lineType == Line.Empty)
            {

            }
            else if (codeLineIndex >= codeLine.Length || codeLine[codeLineIndex].lineType == Line.Cyto)
            {
                DNACount++;
                codeLineIndex++;
            }
            else if (codeLineIndex >= codeLine.Length || codeLine[codeLineIndex].lineType == Line.Thy)
            {
                if (DNACount == 1)
                    DNACount++;
                else
                    DNACount = 0;
                codeLineIndex++;
            }
            else if (codeLineIndex >= codeLine.Length || codeLine[codeLineIndex].lineType == Line.Ade)
            {
                if (DNACount == 2)
                    DNACount++;
                else
                    DNACount = 0;
                codeLineIndex++;
            }
            else if (codeLineIndex >= codeLine.Length || codeLine[codeLineIndex].lineType == Line.Gua)
            {
                if (DNACount == 3)
                    DNACount++;
                else
                    DNACount = 0;
                codeLineIndex++;
            }
            else if (codeLineIndex >= codeLine.Length || codeLine[codeLineIndex].lineType == Line.LocalVariable)
            {
                codeLine[codeLineIndex].linePrefab.GetComponentInChildren<TextMeshProUGUI>().text = promptString;
                codeLine[codeLineIndex].linePrefab.GetComponent<CodeLineInformation>().SetCodeLineText(promptString);
                codeLineIndex++;

            }
            else if (codeLineIndex >= codeLine.Length || codeLine[codeLineIndex].lineType == Line.If)
            {
                if (DNACount >= 4)
                    codeLineIndex++;
                else
                    codeLineIndex += 2;
            }
            else if (codeLineIndex >= codeLine.Length || codeLine[codeLineIndex].lineType == Line.EndCodeLine)
            {
                GameHandler.Instance.FinalExitDoorPuzzleCompleted = true;
            }
            else
                codeLineIndex++;
            codeLine[codeLineIndex].linePrefab.GetComponentInChildren<SelectedCodeLine>().EnableHighlight();
            lastTime = Time.time;
        }
    }

    private void SaveInitialState()
    {
        //tempCodeLine = new TypeOfLine[codeLine.Length];
        //tempCodeLine = codeLine;
        tempCodeLineTypes = new Line[codeLine.Length];
        //for(int i = 0; i< codeLine.Length;i++)
        //{
        //    tempCodeLine[i].text = codeLine[i].text;
        //    tempCodeLine[i].lineType = codeLine[i].lineType;

        //    //codeLine[i].linePrefab.GetComponent<CodeLineInformation>().SetCodeLineText(codeLine[i].text);
        //    //codeLine[i].linePrefab.GetComponent<CodeLineInformation>().SetCodeLineType(codeLine[i].lineType);
        //}

        for(int i = 0; i<codeLine.Length; i++) 
        {
            tempCodeLineTypes[i] = codeLine[i].lineType;
        }

        //tempToolBox = new TypeOfLine[toolBox.Length];
        //tempToolBox = toolBox;

        tempToolBoxTypes = new Line[toolBox.Length];

        for (int i = 0; i < toolBox.Length; i++)
        {
            tempToolBoxTypes[i] = toolBox[i].lineType;    
        }

        //for (int i = 0; i < codeLine.Length; i++)
        //{
        //    tempToolBox[i].text = toolBox[i].text;
        //    tempToolBox[i].lineType = toolBox[i].lineType;
        //}

    }
    public void Reset()
    {
        for (int i = 0; i < codeLine.Length; i++)
        {
            codeLine[i].linePrefab.GetComponentInChildren<TextMeshProUGUI>().text = codeLine[i].text;
            codeLine[i].lineType = tempCodeLineTypes[i];
            codeLine[i].linePrefab.GetComponent<CodeLineInformation>().SetCodeLineText(codeLine[i].text);
            codeLine[i].linePrefab.GetComponent<CodeLineInformation>().SetCodeLineType(tempCodeLineTypes[i]);
        }


        for (int i = 0; i < toolBox.Length; i++)
        {
            toolBox[i].linePrefab.GetComponentInChildren<TextMeshProUGUI>().text = toolBox[i].text;
            toolBox[i].lineType = tempToolBoxTypes[i];
            toolBox[i].linePrefab.GetComponent<CodeLineInformation>().SetCodeLineText(toolBox[i].text);
            toolBox[i].linePrefab.GetComponent<CodeLineInformation>().SetCodeLineType(tempToolBoxTypes[i]);
        }

        freeze_count = 0;
        codeLineIndex = 1;
    }

    public void PromptChange()
    {
        promptString = promptField.text;
        Debug.Log(promptString);
    }
    public void SetSelectedCodeLine(string text, Line lineType)
    {
        selectedCodeLineText = text;
        selectedCodeLineType = lineType;
    }

    public string GetSelcetedCodeLineText()
    {
        return selectedCodeLineText;
    }

    public Line GetSelcetedCodeLineType()
    {
        return selectedCodeLineType;
    }

    public void SetSelectedCodeLinePrefab(GameObject prefab)
    {
        selectedCodeLinePrefab = prefab;
    }

    public GameObject GetSelectedCodeLinePrefab()
    {
        return selectedCodeLinePrefab;
    }

    public void ChangeSelectedCodeLineInformation()
    {
        selectedCodeLineType = Line.Empty;
        selectedCodeLineText = string.Empty;
        selectedCodeLinePrefab.GetComponent<CodeLineInformation>().ChangeToEmptyLine();
    }

    public void DestroyPuzzle()
    {
        Destroy(PuzzleTrigger);
        Destroy(this.gameObject);
    }

    public void HidePuzzle()
    {
        gameObject.SetActive(false);
    }
}
