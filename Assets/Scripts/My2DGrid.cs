using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class My2DGrid : MonoBehaviour
{
    [FormerlySerializedAs("_size")] [SerializeField] private int size;
    [FormerlySerializedAs("_vertical")] [SerializeField] private Transform vertical;
    [FormerlySerializedAs("_horizontalPrefab")] [SerializeField] private GameObject horizontalPrefab;
    [FormerlySerializedAs("_cellPrefab")] [SerializeField] private GameObject cellPrefab;

    private My2DGridCell[] _cells;
    private GameObject[] _horizontalList;

    public void Setup()
    {
        _horizontalList = new GameObject[size];
        _cells = new My2DGridCell[size * size];
        for (var i = 0; i < size; i++)
        {
            _horizontalList[i] = Instantiate(horizontalPrefab, vertical);
            for (var j = 0; j < size; j++)
            {
                var index = i * size + j;
                var cellGameObject = Instantiate(cellPrefab, _horizontalList[i].transform);
                _cells[index] = cellGameObject.GetComponent<My2DGridCell>();
                _cells[index].Init(index, OnCellClick);
            }
        }

        _cells[^1].SetAsEmpty();
        RandomizeStart();
    }

    private void RandomizeStart()
    {
        var emptyIndex = _cells.Length - 1;

        for (var i = 0; i < 100; i++)
        {
            var options = new List<int>();
            if (emptyIndex >= size)
            {
                options.Add(emptyIndex - size);
            }
            if (emptyIndex % size != 0)
            {
                options.Add(emptyIndex - 1);
            }
            if ((emptyIndex + 1) % size != 0)
            {
                options.Add(emptyIndex + 1);
            }
            if (_cells.Length - size > emptyIndex)
            {
                options.Add(emptyIndex + size);
            }
            var rnd = new System.Random();
            var clickedIndex = options[rnd.Next(0, options.Count)];
            _cells[clickedIndex].ForceClick();
            emptyIndex = clickedIndex;
        }
    }

    private void OnCellClick(int clickedIndex, int displayedIndex)
    {
        // Checando tenho o lado de cima
        if (clickedIndex >= size)
        {
            if (_cells[clickedIndex - size].CellState == My2DGridCell.MyCellState.Empty)
            {
                // Conseguirei mexer pra cima
                _cells[clickedIndex - size].SetCellIndex(displayedIndex);
                _cells[clickedIndex - size].SetAsCommon();
                _cells[clickedIndex].SetAsEmpty();
                return;
            }
        }

        // Checando se tenho uma esquerda
        if (clickedIndex % size != 0)
        {
            if (_cells[clickedIndex - 1].CellState == My2DGridCell.MyCellState.Empty)
            {
                // Conseguirei mexer pra esquerda
                _cells[clickedIndex - 1].SetCellIndex(displayedIndex);
                _cells[clickedIndex - 1].SetAsCommon();
                _cells[clickedIndex].SetAsEmpty();
                return;
            }
        }

        // Checando se tenho uma direita
        if ((clickedIndex + 1) % size != 0)
        {
            if (_cells[clickedIndex + 1].CellState == My2DGridCell.MyCellState.Empty)
            {
                // Conseguirei mexer pra direita
                _cells[clickedIndex + 1].SetCellIndex(displayedIndex);
                _cells[clickedIndex + 1].SetAsCommon();
                _cells[clickedIndex].SetAsEmpty();
                return;
            }
        }

        // Checando tenho o lado de baixo
        if (_cells.Length - size > clickedIndex)
        {
            if (_cells[clickedIndex + size].CellState == My2DGridCell.MyCellState.Empty)
            {
                // Conseguirei mexer pra baixo
                _cells[clickedIndex + size].SetCellIndex(displayedIndex);
                _cells[clickedIndex + size].SetAsCommon();
                _cells[clickedIndex].SetAsEmpty();
                return;
            }
        }

        Debug.Log("Can't do anything");
    }
}
