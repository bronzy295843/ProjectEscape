using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PuzzlePanel : MonoBehaviour
{
    public static PuzzlePanel Instance;
    [SerializeField] private TypeOfLine[] codeLine;
    [SerializeField] private TypeOfLine[] toolBox;

    private Vector3 lastInstantiatedPosition = Vector3.zero;

    private float yDeviation_codeBox = 80f;
    private float xDeviation_codeBox = -300f;

    private float yDeviation_toolBox = 80f;
    private float xDeviation_toolBox = 400f;

    private float delay = 1f;
    private float lastTime = 0f;

    private int codeLineIndex = 1;

    private Line selectedCodeLineType = Line.Empty;
    private string selectedCodeLineText;
    private GameObject selectedCodeLinePrefab;


    void Start()
    {
        InstantiateCodeLines(codeLine,xDeviation_codeBox, yDeviation_codeBox);
        SetText(codeLine);

        InstantiateCodeLines(toolBox, xDeviation_toolBox, yDeviation_toolBox);
        SetText(toolBox);
    }

    void Awake()
    {
        if (Instance == null)
            Instance = this;

    }

    // Update is called once per frame
    void Update()
    {
        RunCodeBlock();
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
       // lastInstantiatedPosition = Vector3.zero;
        //lastInstantiatedPosition += Vector3.right * xDeviation;
        for (int i = 0; i < codeLine.Length; i++) 
        {
            GameObject instantiatedLine = Instantiate(codeLine[i].linePrefab, transform);
            //instantiatedLine.transform.position = lastInstantiatedPosition;
            instantiatedLine.GetComponent<RectTransform>().anchoredPosition += Vector2.up * yDeviation;
            instantiatedLine.GetComponent<RectTransform>().anchoredPosition += Vector2.right * xDeviation;
            yDeviation -= 100;
            //lastInstantiatedPosition = instantiatedLine.transform.position;
            codeLine[i].linePrefab = instantiatedLine;
        }
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

    private void RunCodeBlock()
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
            else if(codeLineIndex >= codeLine.Length || codeLine[codeLineIndex].lineType == Line.EndCodeLine)
            {
                GameHandler.Instance.PuzzleCompleted = true;
            }
            else 
                codeLineIndex++;
            codeLine[codeLineIndex].linePrefab.GetComponentInChildren<SelectedCodeLine>().EnableHighlight();
            lastTime = Time.time;
        }
        
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
}
