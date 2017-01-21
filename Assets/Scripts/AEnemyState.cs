using System;
using UnityEngine;

public abstract class AEnemyState
{
	protected EnemyBehaviour enemyBehaviour;
	protected Transform cachedTransform;

	public abstract EnemyStates stateName
	{
		get;
	}

	public AEnemyState(EnemyBehaviour enemyBehaviour)
	{
		this.enemyBehaviour = enemyBehaviour;
		cachedTransform = enemyBehaviour.transform;
	}

	public virtual void OnStateEnter(EnemyStates previousState)
	{
		SoundProducerManager soundProducerManager = Utils.GetSoundProducerManager(GameConstants.GAME_MANAGER_TAG);
		soundProducerManager.SoundProducerAddedEvent += OnSoundProducerAdded;
		soundProducerManager.SoundProducerRemovedEvent += OnSoundProducerRemoved;
	}

	public virtual void OnStateExit()
	{
		SoundProducerManager soundProducerManager = Utils.GetSoundProducerManager(GameConstants.GAME_MANAGER_TAG);
		if (soundProducerManager != null)
		{
			soundProducerManager.SoundProducerAddedEvent -= OnSoundProducerAdded;
			soundProducerManager.SoundProducerRemovedEvent -= OnSoundProducerRemoved;
			foreach (ASoundProducer soundProducer in soundProducerManager.GetSoundProducers())
			{
				soundProducer.VolumeModifiedEvent -= OnVolumeModified;
			}
		}
	}

	public abstract void UpdateState();

	protected virtual void OnSoundProducerAdded(ASoundProducer soundProducer)
	{
		soundProducer.VolumeModifiedEvent += OnVolumeModified;
	}

	protected virtual void OnSoundProducerRemoved(ASoundProducer soundProducer)
	{
		soundProducer.VolumeModifiedEvent -= OnVolumeModified;
	}

	protected virtual void OnVolumeModified(float volume)
	{
	}
}