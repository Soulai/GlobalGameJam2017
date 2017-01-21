using UnityEngine;

public class Utils
{
	public static GameManager GetGameManager(string tag)
	{
		GameObject gameManagerGO = GameObject.FindGameObjectWithTag(tag);
		if (gameManagerGO != null)
		{
			return gameManagerGO.GetComponent<GameManager>();
		}
		return null;
	}

	public static PatrolPointManager GetPatrolPointManager(string tag)
	{
		GameManager gameManager = GetGameManager(tag);
		if (gameManager != null)
		{
			return gameManager.PatrolPointManager;
		}
		return null;
	}

	public static SoundProducerManager GetSoundProducerManager(string tag)
	{
		GameManager gameManager = GetGameManager(tag);
		if (gameManager != null)
		{
			return gameManager.SoundProducerManager;
		}
		return null;
	}
}