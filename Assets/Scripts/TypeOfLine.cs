using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TypeOfLine
{
    public Line lineType;
    public string text;
    public GameObject linePrefab; 
}

public enum Line
{
    Loop,
    SimpleCodeLine,
    EndCodeLine,
    GoAhead,
    Increment,
    If,
    Empty,
    Bool,
    LocalVariable
};
