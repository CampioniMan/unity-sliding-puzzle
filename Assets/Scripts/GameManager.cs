using Grid;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GridController gridController;
    [SerializeField] private int squareSideSize;

    public void Start()
    {
        gridController.Setup(squareSideSize);
    }
}
