using System.Collections;
using Grid;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GridController gridController;
    [SerializeField] private GameView view;
    [SerializeField] private int squareSideSize;

    private int _moveCount;
    private int _currentSecondsCount;
    private Coroutine _timerCoroutine;

    public void Start()
    {
        view.Setup(StartGame);
    }

    private void StartGame()
    {
        _currentSecondsCount = -1;
        _moveCount = 0;
        gridController.Setup(squareSideSize);
        gridController.OnMove += OnMove;
        gridController.OnFisish += OnFinish;
        _timerCoroutine = StartCoroutine(UpdateTimer());
    }

    private void OnMove()
    {
        view.SetMovementCount(++_moveCount);
    }

    private void OnFinish()
    {
        StopCoroutine(_timerCoroutine);
        view.SetReplayButtonVisibility(true);
        gridController.OnMove -= OnMove;
        gridController.OnFisish -= OnFinish;
    }

    private IEnumerator UpdateTimer()
    {
        while (true)
        {
            view.SetPlayingTime(++_currentSecondsCount);
            yield return new WaitForSeconds(1f);
        }
    }

    private void OnDestroy()
    {
        gridController.OnMove -= OnMove;
        gridController.OnFisish -= OnFinish;
    }
}
