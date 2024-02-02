using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Grid
{
    public class PieceView : MonoBehaviour
    {
        [SerializeField] private Image background;
        [SerializeField] private TMP_Text displayedNumber;
        [ColorPalette] [OnValueChanged(nameof(UpdateColors))]
        [SerializeField] private Color backgroundColor;
        [ColorPalette] [OnValueChanged(nameof(UpdateColors))]
        [SerializeField] private Color textColor;

        private void UpdateColors()
        {
            displayedNumber.color = textColor;
            background.color = backgroundColor;
        }

        public void SetDisplayedNumber(int number)
        {
            displayedNumber.text = number.ToString();
        }
    }
}