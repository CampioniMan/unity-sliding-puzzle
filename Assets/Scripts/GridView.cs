using System;
using UnityEngine;

public class GridView : MonoBehaviour
{
    [SerializeField] private Transform vertical;
    [SerializeField] private GameObject horizontalPrefab;
    [SerializeField] private GameObject cellPrefab;
    
    public CellController[] Setup(int size, Action<int, int> onCellClick)
    {
        var cells = new CellController[size * size];
        for (var i = 0; i < size; i++)
        {
            var horizontalList = Instantiate(horizontalPrefab, vertical);
            for (var j = 0; j < size; j++)
            {
                var index = i * size + j;
                var cellGameObject = Instantiate(cellPrefab, horizontalList.transform);
                cells[index] = cellGameObject.GetComponent<CellController>();
                cells[index].Init(index, onCellClick);
            }
        }

        cells[^1].Hide();
        return cells;
    }
}