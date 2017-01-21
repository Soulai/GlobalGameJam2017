using UnityEngine;
using System;
using System.Collections.Generic;

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