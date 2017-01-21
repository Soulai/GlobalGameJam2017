using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

public class SoundProducerManager : MonoBehaviour
{
	public event Action<ASoundProducer> SoundProducerAddedEvent;
	public event Action<ASoundProducer> SoundProducerRemovedEvent;

	private List<ASoundProducer> soundProducers = new List<ASoundProducer>();

	public void AddSoundProducer(ASoundProducer soundProducer)
	{
		soundProducers.Add(soundProducer);
		SoundProducerAddedEvent.Fire(soundProducer);
	}

	public ASoundProducer FindSoundProducerMaxVolume(Vector3 fromPosition)
	{
		ASoundProducer maxVolumeSoundProducer = null;
		float maxVolume = float.MinValue;
		foreach (ASoundProducer soundProducer in soundProducers)
		{
			float volume = soundProducer.GetVolume(fromPosition);
			if (volume > maxVolume)
			{
				maxVolume = volume;
				maxVolumeSoundProducer = soundProducer;
			}
		}
		return maxVolumeSoundProducer;
	}

	public IEnumerable<ASoundProducer> GetSoundProducers()
	{
		return soundProducers;
	}

	public void RemoveSoundProducer(ASoundProducer soundProducer)
	{
		soundProducers.Remove(soundProducer);
		SoundProducerRemovedEvent.Fire(soundProducer);
	}
}