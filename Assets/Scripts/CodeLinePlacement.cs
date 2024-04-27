using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class CodeLinePlacement : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData) 
    {
        if (GetComponent<CodeLineInformation>().GetLineType() == Line.Empty)
        {
            GetComponent<CodeLineInformation>().text = PuzzlePanel.Instance.GetSelcetedCodeLineText();
            GetComponent<CodeLineInformation>().lineType = PuzzlePanel.Instance.GetSelcetedCodeLineType();
            PuzzlePanel.Instance.ChangeSelectedCodeLineInformation();
            SoundManager.Instance.PlaySFX("PuzzleBlock_Place");
        }
    }
}
