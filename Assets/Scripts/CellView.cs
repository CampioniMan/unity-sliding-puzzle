using System;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class CellView : MonoBehaviour
{
    [SerializeField] private TMP_Text displayedNumber;
    [ColorPalette] [OnValueChanged("UpdateDisplayedNumberColor")]
    [SerializeField] private Color textColor;

    [UsedImplicitly]
    private void UpdateDisplayedNumberColor()
    {
        displayedNumber.color = textColor;
    }

    public void SetState(CellState state)
    {
        switch (state)
        {
            case CellState.Show:
                displayedNumber.gameObject.SetActive(true);
                break;
            case CellState.Hide:
                displayedNumber.gameObject.SetActive(false);
                break;
        }
    }

    public void SetDisplayedNumber(int number)
    {
        displayedNumber.text = number.ToString();
    }
}