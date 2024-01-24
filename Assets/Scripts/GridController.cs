using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    [SerializeField] private GridView view;
    
    private int _size;
    private CellController[] _cells;

    public void Setup(int size)
    {
        _size = size;
        _cells = view.Setup(size, OnCellClick);
        RandomizeStart();
    }

    private void RandomizeStart()
    {
        var emptyIndex = _cells.Length - 1;

        for (var i = 0; i < 100; i++)
        {
            var options = new List<int>();
            if (emptyIndex >= _size)
            {
                options.Add(emptyIndex - _size);
            }
            if (emptyIndex % _size != 0)
            {
                options.Add(emptyIndex - 1);
            }
            if ((emptyIndex + 1) % _size != 0)
            {
                options.Add(emptyIndex + 1);
            }
            if (_cells.Length - _size > emptyIndex)
            {
                options.Add(emptyIndex + _size);
            }
            var rnd = new System.Random();
            var clickedIndex = options[rnd.Next(0, options.Count)];
            _cells[clickedIndex].ForceClick();
            emptyIndex = clickedIndex;
        }
    }

    private void OnCellClick(int clickedIndex, int displayedIndex)
    {
        if (clickedIndex >= _size)
        {
            var cellAbove = _cells[clickedIndex - _size];
            if (cellAbove.CurrentState == CellState.Hide)
            {
                cellAbove.SetCellIndex(displayedIndex);
                cellAbove.Show();
                _cells[clickedIndex].Hide();
                return;
            }
        }

        if (clickedIndex % _size != 0)
        {
            var cellToTheLeft = _cells[clickedIndex - 1];
            if (cellToTheLeft.CurrentState == CellState.Hide)
            {
                cellToTheLeft.SetCellIndex(displayedIndex);
                cellToTheLeft.Show();
                _cells[clickedIndex].Hide();
                return;
            }
        }

        if ((clickedIndex + 1) % _size != 0)
        {
            var cellToTheRight = _cells[clickedIndex + 1];
            if (cellToTheRight.CurrentState == CellState.Hide)
            {
                cellToTheRight.SetCellIndex(displayedIndex);
                cellToTheRight.Show();
                _cells[clickedIndex].Hide();
                return;
            }
        }

        if (_cells.Length - _size > clickedIndex)
        {
            var cellBelow = _cells[clickedIndex + _size];
            if (cellBelow.CurrentState == CellState.Hide)
            {
                cellBelow.SetCellIndex(displayedIndex);
                cellBelow.Show();
                _cells[clickedIndex].Hide();
                return;
            }
        }

        Debug.Log("Piece can't be moved.");
    }
}
