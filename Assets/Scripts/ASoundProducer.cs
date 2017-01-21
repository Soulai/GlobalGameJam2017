using System;
using UnityEngine;

public abstract class ASoundProducer
{
	public event Action<float> VolumeModifiedEvent;

	private float volume;

	public abstract float GetVolume(Vector3 fromPosition);
}