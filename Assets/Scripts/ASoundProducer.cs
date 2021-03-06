﻿using System;
using UnityEngine;

public abstract class ASoundProducer : MonoBehaviour
{
	public event Action<ASoundProducer> VolumeModifiedEvent;

	protected Transform cachedTransform;

	private SoundProducerManager soundProducerManager;
	private float sourceVolume = 0f;

	protected float SourceVolume
	{
		get
		{
			return sourceVolume;
		}
		set
		{
			bool isAdded = false;
			if (value > 0f)
			{
				if (sourceVolume == 0f)
				{
					isAdded = true;
				}
			}
			else if (sourceVolume > 0f)
			{
				soundProducerManager.RemoveSoundProducer(this);
			}

			sourceVolume = value;

			if (isAdded)
			{
				soundProducerManager.AddSoundProducer(this);
			}
			else if (sourceVolume > 0f)
			{
				VolumeModifiedEvent.Fire(this);
			}
		}
	}

	public virtual Vector3 SourcePosition
	{
		get
		{
			return cachedTransform.position;
		}
	}

	protected virtual void Start()
	{
		cachedTransform = transform;
		soundProducerManager = Utils.GetSoundProducerManager(GameConstants.GAME_MANAGER_TAG);
	}

	public virtual float GetVolume(Vector3 fromPosition)
	{
		return SourceVolume / (fromPosition - cachedTransform.position).sqrMagnitude;
	}
}