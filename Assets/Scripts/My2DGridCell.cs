using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System;
using UnityEngine.Serialization;

public class My2DGridCell : MonoBehaviour, IPointerDownHandler
{
    public enum MyCellState
    {
        Common,
        Empty
    }

    [FormerlySerializedAs("_displayedNumber")] [SerializeField] private TMP_Text displayedNumber;

    private int _originalCellIndex;
    private int _cellIndex;
    private MyCellState _cellState;
    private Action<int, int> _onClick;

    public MyCellState CellState
    {
        get => _cellState;
    }

    public void Init(int index, Action<int, int> onClick)
    {
        _originalCellIndex = index;
        SetCellIndex(index);
        _onClick = onClick;
        SetAsCommon();
    }

    public void SetAsEmpty()
    {
        _cellState = MyCellState.Empty;
        SetCellIndex(-1);
        displayedNumber.gameObject.SetActive(false);
    }

    public void SetAsCommon()
    {
        _cellState = MyCellState.Common;
        displayedNumber.gameObject.SetActive(true);
    }

    public void SetCellIndex(int newIndex)
    {
        _cellIndex = newIndex;
        displayedNumber.text = (_cellIndex + 1).ToString();
    }

    public void OnPointerDown(PointerEventData _)
    {
        ForceClick();
    }

    public void ForceClick()
    {
        _onClick.Invoke(_originalCellIndex, _cellIndex);
    }
}
