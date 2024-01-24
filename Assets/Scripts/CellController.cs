using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class CellController : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private CellView view;

    private int _originalCellIndex;
    private int _cellIndex;
    private CellState _currentState;
    private Action<int, int> _onClick;

    public CellState CurrentState
    {
        get => _currentState;
        private set => _currentState = value;
    }

    public void Init(int index, Action<int, int> onClick)
    {
        _originalCellIndex = index;
        SetCellIndex(index);
        _onClick = onClick;
        Show();
    }

    public void Hide()
    {
        CurrentState = CellState.Hide;
        UpdateViewState();
    }

    public void Show()
    {
        CurrentState = CellState.Show;
        UpdateViewState();
    }

    private void UpdateViewState()
    {
        view.SetState(CurrentState);
    }

    public void SetCellIndex(int newIndex)
    {
        _cellIndex = newIndex;
        view.SetDisplayedNumber(_cellIndex + 1);
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
