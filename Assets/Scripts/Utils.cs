using UnityEngine;

public class Utils
{
	public static GameManager GetGameManager(string tag)
	{
		GameObject gameManagerGO = GameObject.FindGameObjectWithTag(tag);
		return gameManagerGO.GetComponent<GameManager>();
	}

	public static PatrolPointManager GetPatrolPointManager(string tag)
	{
		GameManager gameManager = GetGameManager(tag);
		return gameManager.PatrolPointManager;
	}

	public static SoundProducerManager GetSoundProducerManager(string tag)
	{
		GameManager gameManager = GetGameManager(tag);
		return gameManager.SoundProducerManager;
	}
}