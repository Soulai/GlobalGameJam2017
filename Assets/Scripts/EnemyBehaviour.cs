using UnityEngine;

public class EnemyBehaviour : MonoBehaviour 
{


	void Start()
	{
		SoundProducerManager soundProducerManager = Utils.GetSoundProducerManager(GameConstants.GAME_MANAGER_TAG);
		soundProducerManager.SoundProducerAddedEvent += OnSoundProducerAdded;
		soundProducerManager.SoundProducerRemovedEvent += OnSoundProducerRemoved;
	}

	void OnDestroy()
	{
		SoundProducerManager soundProducerManager = Utils.GetSoundProducerManager(GameConstants.GAME_MANAGER_TAG);
		if (soundProducerManager != null)
		{
			soundProducerManager.SoundProducerAddedEvent += OnSoundProducerAdded;
			soundProducerManager.SoundProducerRemovedEvent += OnSoundProducerRemoved;
		}
	}

	private void OnSoundProducerAdded(ASoundProducer soundProducer)
	{
		soundProducer.VolumeModifiedEvent += OnVolumeModified;
	}

	private void OnSoundProducerRemoved(ASoundProducer soundProducer)
	{
		soundProducer.VolumeModifiedEvent -= OnVolumeModified;
	}

	private void OnVolumeModified(float volume)
	{
	}
}