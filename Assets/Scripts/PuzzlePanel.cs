using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PuzzlePanel : MonoBehaviour
{
    [SerializeField] private TypeOfLine[] codeLine;
    [SerializeField] private TypeOfLine[] toolBox;

    private float yDeviation_codeBox = 80f;
    private float xDeviation_codeBox = -200f;

    private float yDeviation_toolBox = 80f;
    private float xDeviation_toolBox = 200f;

    private float delay = 1f;
    private float lastTime = 0f;

    private int codeLineIndex = 1;


    void Start()
    {
        InstantiateCodeLines(codeLine,xDeviation_codeBox, yDeviation_codeBox);
        SetText(codeLine);

        InstantiateCodeLines(toolBox, xDeviation_toolBox, yDeviation_toolBox);
        SetText(toolBox);
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
        }
    }

    private void InstantiateCodeLines(TypeOfLine[] codeLine, float xDeviation, float yDeviation)
    {
        for (int i = 0; i < codeLine.Length; i++) 
        {
            GameObject instantiatedLine = Instantiate(codeLine[i].linePrefab, transform);
            instantiatedLine.transform.position += Vector3.up*yDeviation;
            instantiatedLine.transform.position += Vector3.right * xDeviation;
            yDeviation -= 70;

            codeLine[i].linePrefab = instantiatedLine;
        }
    }

    private void RunCodeBlock()
    {

        codeLine[codeLineIndex].linePrefab.GetComponentInChildren<SelectedCodeLine>().EnableHighlight();

        
        if (Time.time - lastTime > delay)
        {
            codeLine[codeLineIndex].linePrefab.GetComponentInChildren<SelectedCodeLine>().DisableHighlight();

            if (codeLineIndex + 1 >= codeLine.Length || codeLine[codeLineIndex].lineType == Line.Loop)
                codeLineIndex = 1;
            else
                codeLineIndex++;
            codeLine[codeLineIndex].linePrefab.GetComponentInChildren<SelectedCodeLine>().EnableHighlight();
            lastTime = Time.time;
        }
        
    }
}
