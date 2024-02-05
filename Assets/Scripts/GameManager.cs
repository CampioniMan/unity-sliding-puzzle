using System.Collections;
using Grid;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GridController gridController;
    [SerializeField] private GameView view;
    [SerializeField] private SoundManager soundManager;
    [SerializeField] private int squareSideSize;

    private int _moveCount;
    private int _currentSecondsCount;
    private Coroutine _timerCoroutine;

    public void Start()
    {
        view.Setup(StartGame);
        soundManager.Init();
    }

    private void StartGame()
    {
        soundManager.PlayButtonSound();
        _currentSecondsCount = 0;
        _moveCount = 0;
        view.SetMovementCount(_moveCount);
        gridController.Setup(squareSideSize);
        gridController.OnMove += OnMove;
        gridController.OnFisish += OnFinish;
        view.SetPlayingTime(_currentSecondsCount);
        _timerCoroutine = StartCoroutine(UpdateTimer());
    }

    private void OnMove()
    {
        view.SetMovementCount(++_moveCount);
        soundManager.PlayMoveSound();
    }

    private void OnFinish()
    {
        soundManager.PlayWinSound();
        StopCoroutine(_timerCoroutine);
        view.SetReplayButtonVisibility(true);
        gridController.OnMove -= OnMove;
        gridController.OnFisish -= OnFinish;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private IEnumerator UpdateTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            view.SetPlayingTime(++_currentSecondsCount);
        }
    }

    private void OnDestroy()
    {
        gridController.OnMove -= OnMove;
        gridController.OnFisish -= OnFinish;
    }
}
