using UnityEngine;

public class GameManager : MonoBehaviour
{
	public PatrolPointManager PatrolPointManager
	{
		get
		{
			return patrolPointManager;
		}
	}

	public SoundProducerManager SoundProducerManager
	{
		get
		{
			return soundProducerManager;
		}
	}
		
	[SerializeField]
	private PatrolPointManager patrolPointManager;
	[SerializeField]
	private SoundProducerManager soundProducerManager;
}