using TMPro;
using UnityEngine;

public class CellView : MonoBehaviour
{
    [SerializeField] private TMP_Text displayedNumber;

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