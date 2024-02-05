using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;

public class SoundManager : MonoBehaviour
{
	[SerializeField] private AudioClip[] bloops;
	[SerializeField] private AudioClip[] swoops;
	[SerializeField] private AudioClip[] claps;

	[SerializeField] private AudioClip bgm;

	[SerializeField] private AudioSource audioSource2D;
	[SerializeField] private AudioSource audioSourceBackgroundMusic;
	
	private Random _random;
	
	public void Init()
	{
		_random = new Random();
		audioSource2D = GetComponent<AudioSource>();
		audioSourceBackgroundMusic.loop = true;
		audioSourceBackgroundMusic.clip = bgm;
		audioSourceBackgroundMusic.Play();
	}
	
	public void PlayMoveSound()
	{
		audioSource2D.PlayOneShot(GetRandomFromArray(swoops));
	}
	
	public void PlayButtonSound()
	{
		audioSource2D.PlayOneShot(GetRandomFromArray(bloops));
	}

	public void PlayWinSound()
	{
		audioSource2D.PlayOneShot(GetRandomFromArray(claps));
	}

	private AudioClip GetRandomFromArray(AudioClip[] audios)
	{
		return audios[_random.Next(audios.Length)];
	}
}