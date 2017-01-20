using UnityEngine;

public class GameManager : MonoBehaviour
{
	public SoundProducerManager SoundProducerManager
	{
		get
		{
			return soundProducerManager;
		}
	}

	[SerializeField]
	private SoundProducerManager soundProducerManager;
}