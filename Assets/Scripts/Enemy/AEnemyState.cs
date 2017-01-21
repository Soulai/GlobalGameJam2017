using System;
using UnityEngine;
using UnityEngine.AI;

public abstract class AEnemyState
{
	protected EnemyBehaviour enemyBehaviour;
	protected Transform cachedTransform;
	protected NavMeshAgent navMeshAgent;
	protected SoundProducerManager soundProducerManager;

	public abstract EnemyStates stateName
	{
		get;
	}

	public AEnemyState(EnemyBehaviour enemyBehaviour)
	{
		this.enemyBehaviour = enemyBehaviour;
		cachedTransform = enemyBehaviour.transform;
		navMeshAgent = enemyBehaviour.GetComponent<NavMeshAgent>();
		soundProducerManager = Utils.GetSoundProducerManager(GameConstants.GAME_MANAGER_TAG);
	}

	public virtual void OnStateEnter(EnemyStates previousState)
	{
		soundProducerManager.SoundProducerAddedEvent += OnSoundProducerAdded;
		soundProducerManager.SoundProducerRemovedEvent += OnSoundProducerRemoved;
		foreach (ASoundProducer soundProducer in soundProducerManager.GetSoundProducers())
		{
			soundProducer.VolumeModifiedEvent += OnVolumeModified;
		}
	}

	public virtual void OnStateExit()
	{
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

	protected virtual void OnVolumeModified(ASoundProducer soundProducer)
	{
	}
}