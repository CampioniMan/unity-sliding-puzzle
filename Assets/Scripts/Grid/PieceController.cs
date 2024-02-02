using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Grid
{
    public class PieceController : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private PieceView view;

        private int _displayedIndex;
        private Action<int, int> _onClick;
        
        public void Init(int index, Action<int, int> onClick)
        {
            _displayedIndex = index + 1;
            view.SetDisplayedNumber(_displayedIndex);
            _onClick = onClick;
        }
        
        public void OnPointerDown(PointerEventData _)
        {
            ForceClick();
        }

        public void ForceClick()
        {
            var placeIndex = transform.parent.GetComponent<PlaceController>().PlaceIndex;
            _onClick.Invoke(placeIndex, _displayedIndex);
        }
    }
}