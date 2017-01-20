using System;

public abstract class ASoundProducer
{
	public event Action<float> VolumeModifiedEvent;

	public float Volume
	{
		get
		{
			return volume;
		}
	}

	private float volume;
}