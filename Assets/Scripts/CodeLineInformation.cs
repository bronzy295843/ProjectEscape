using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeLineInformation : MonoBehaviour
{
    public Line lineType;
    public string text;
    private GameObject linePrefab;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCodeLineText(string text)
    {
        this.text = text;
    }

    public void SetCodeLineType(Line lineType)
    {
        this.lineType = lineType;
    }

    public Line GetLineType()
    {
        return lineType;
    }

    public string GetCodeLineText()
    {
        return this.text;
    }

    public void ChangeToEmptyLine()
    {
        lineType = Line.Empty;
        text = string.Empty;

        PuzzlePanel.Instance.UpdateCodeLines();
    }

    public void SelectedLine()
    {
        PuzzlePanel.Instance.SetSelectedCodeLine(text, lineType);
        PuzzlePanel.Instance.SetSelectedCodeLinePrefab(this.gameObject);
    }
}
