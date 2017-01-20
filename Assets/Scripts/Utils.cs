using UnityEngine;

public class Utils
{
	public static SoundProducerManager GetSoundProducerManager(string tag)
	{
		GameManager gameManager = GetGameManager(tag);
		return gameManager.SoundProducerManager;
	}

	public static GameManager GetGameManager(string tag)
	{
		GameObject gameManagerGO = GameObject.FindGameObjectWithTag(tag);
		return gameManagerGO.GetComponent<GameManager>();
	}
}