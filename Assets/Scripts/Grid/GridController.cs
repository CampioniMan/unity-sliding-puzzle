using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Grid
{
    public class GridController : MonoBehaviour
    {
        [SerializeField] private GridView view;

        public event Action OnMove;
        public event Action OnFisish;
        
        private int _size;
        private PlaceController[] _places;

        public void Setup(int size)
        {
            if (_places != null)
            {
                view.Clear();
            }
            _size = size;
            _places = view.Setup(size, OnPieceClick);
            RandomizeStart();
            MoveEmptyToLowerRightCorner();
        }

        private void RandomizeStart()
        {
            var previousEmpty = -1;
            var emptyIndex = _places.TakeWhile(place => place.HasContent()).Count();
            var rnd = new System.Random();
            var initialMovementCount = _size * 70 / 9;

            for (var i = 0; i < initialMovementCount; i++)
            {
                var options = new List<int>();
                if (emptyIndex >= _size)
                {
                    AddIfNotPrevious(emptyIndex - _size);
                }
                if (emptyIndex % _size != 0)
                {
                    AddIfNotPrevious(emptyIndex - 1);
                }
                if ((emptyIndex + 1) % _size != 0)
                {
                    AddIfNotPrevious(emptyIndex + 1);
                }
                if (_places.Length - _size > emptyIndex)
                {
                    AddIfNotPrevious(emptyIndex + _size);
                }
                var clickedIndex = options[rnd.Next(0, options.Count)];
                _places[clickedIndex].Content.ForceClick();
                previousEmpty = emptyIndex;
                emptyIndex = clickedIndex;
                continue;

                void AddIfNotPrevious(int index)
                {
                    if (previousEmpty != index)
                    {
                        options.Add(index);
                    }
                }
            }
        }

        private void MoveEmptyToLowerRightCorner()
        {
            var emptyIndex = _places.TakeWhile(place => place.HasContent()).Count();
            for (var i = 0; i < (_size - 1) * 2; i++)
            {
                if ((emptyIndex + 1) % _size != 0)
                {
                    _places[emptyIndex + 1].Content.ForceClick();
                    emptyIndex += 1;
                    continue;
                }

                if (_places.Length - _size > emptyIndex)
                {
                    _places[emptyIndex + _size].Content.ForceClick();
                    emptyIndex += _size;
                    continue;
                }

                break;
            }
        }

        private void OnPieceClick(int clickedIndex, int displayedIndex)
        {
            var content = _places[clickedIndex].RemoveContent();
            if (clickedIndex >= _size)
            {
                var cellAbove = _places[clickedIndex - _size];
                if (cellAbove.SetContent(content))
                {
                    OnMove?.Invoke();
                    if (HasWon()) OnFisish?.Invoke();
                    return;
                }
            }

            if (clickedIndex % _size != 0)
            {
                var cellToTheLeft = _places[clickedIndex - 1];
                if (cellToTheLeft.SetContent(content))
                {
                    OnMove?.Invoke();
                    if (HasWon()) OnFisish?.Invoke();
                    return;
                }
            }

            if ((clickedIndex + 1) % _size != 0)
            {
                var cellToTheRight = _places[clickedIndex + 1];
                if (cellToTheRight.SetContent(content))
                {
                    OnMove?.Invoke();
                    if (HasWon()) OnFisish?.Invoke();
                    return;
                }
            }

            if (_places.Length - _size > clickedIndex)
            {
                var cellBelow = _places[clickedIndex + _size];
                if (cellBelow.SetContent(content))
                {
                    OnMove?.Invoke();
                    if (HasWon()) OnFisish?.Invoke();
                    return;
                }
            }

            _places[clickedIndex].SetContent(content);
            Debug.Log("Piece can't be moved.");
        }

        private bool HasWon()
        {
            for (var index = 0; index < _places.Length; index++)
            {
                var place = _places[index];
                if (place.Content != null && place.Content.DisplayedIndex != index + 1) return false;
            }

            return true;
        }
    }
}
