using System;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;

namespace Grid
{
    public class GridView : MonoBehaviour
    {
        [SerializeField] private Transform vertical;
        [SerializeField] private GameObject horizontalPrefab;
        [SerializeField] private GameObject placePrefab;
        [SerializeField] private GameObject piecePrefab;
    
        public PlaceController[] Setup(int size, Action<int, int> onPieceClick)
        {
            var places = new PlaceController[size * size];
            var missingPieceIndex = 8;//new Random().Next(0, places.Length);
            for (var i = 0; i < size; i++)
            {
                var horizontalList = Instantiate(horizontalPrefab, vertical);
                for (var j = 0; j < size; j++)
                {
                    var index = i * size + j;
                    var placeGameObject = Instantiate(placePrefab, horizontalList.transform);
                    
                    PieceController piece = null;
                    if (index != missingPieceIndex)
                    {
                        var pieceGameObject = Instantiate(piecePrefab, placeGameObject.transform);
                        piece = pieceGameObject.GetComponent<PieceController>();
                        piece.Init(index, onPieceClick);
                    }
                    places[index] = placeGameObject.GetComponent<PlaceController>();
                    places[index].Init(piece, index);
                }
            }

            return places;
        }
    }
}