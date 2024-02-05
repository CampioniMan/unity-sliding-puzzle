using System;
using DG.Tweening;
using UnityEngine;

namespace Grid
{
    public class PlaceController : MonoBehaviour
    {
        private int _placeIndex;
        private PieceController _content;

        public PieceController Content => _content;

        public int PlaceIndex => _placeIndex;

        public void Init(PieceController content, int index)
        {
            _placeIndex = index;
            SetContent(content);
        }

        public bool HasContent()
        {
            return _content != null;
        }

        public bool SetContent(PieceController content)
        {
            if (HasContent() || content == null) return false;

            _content = content;
            var contentTransform = _content.transform;
            contentTransform.parent = transform;
            contentTransform.DOKill();
            contentTransform.DOLocalMove(Vector3.zero, 0.5f);
            return true;
        }

        public PieceController RemoveContent()
        {
            var result = _content;
            _content = null;
            return result;
        }
    }
}
