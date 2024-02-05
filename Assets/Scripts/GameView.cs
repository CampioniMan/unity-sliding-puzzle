using System;
using System.Text;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameView : MonoBehaviour
{
	[SerializeField] private TMP_Text playingTime;
	[SerializeField] private TMP_Text movementCount;
	[SerializeField] private Button playButton;
	[SerializeField] private Button replayButton;

	public void Setup(Action startGame)
	{
		playButton.onClick.AddListener(OnPlayClick);
		playButton.transform.DOScale(1.2f, 0.6f).SetLoops(-1, LoopType.Yoyo);
		replayButton.onClick.AddListener(OnPlayClick);
		replayButton.transform.DOScale(1.2f, 0.6f).SetLoops(-1, LoopType.Yoyo);
		
		void OnPlayClick()
		{
			startGame.Invoke();
			playButton.gameObject.SetActive(false);
			replayButton.gameObject.SetActive(false);
		}
	}

	public void SetReplayButtonVisibility(bool visible)
	{
		replayButton.gameObject.SetActive(visible);
	}
	
	public void SetMovementCount(int count)
	{
		movementCount.text = $"{count}";
	}

	public void SetPlayingTime(int seconds)
	{
		// shouldn't take more than a day, ignoring numbers above it
		var builder = new StringBuilder();

		var remainingSeconds = seconds;
		
		var hours = remainingSeconds / 3600;
		if (hours > 0)
		{
			builder.Append($"{hours}h ");
			remainingSeconds -= hours * 3600;
		}
		
		var minutes = remainingSeconds / 60;
		if (minutes > 0)
		{
			builder.Append($"{minutes}m ");
			remainingSeconds -= minutes * 60;
		}
		
		builder.Append($"{remainingSeconds}s");
		playingTime.text = builder.ToString();
	}
}