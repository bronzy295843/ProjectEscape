using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeOfLine : MonoBehaviour
{
    [SerializeField] private Line lineType;
}

public enum Line
{
    Loop,
    SimpleCodeLine,
    EndCodeLine,
    GoAhead
};
