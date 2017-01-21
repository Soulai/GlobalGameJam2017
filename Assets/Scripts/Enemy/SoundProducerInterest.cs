using UnityEngine;

public class SoundProducerInterest
{
	private float interest;
	private ASoundProducer soundProducer;

	public float Interest
	{
		get
		{
			return interest;
		}
	}

	public ASoundProducer SoundProducer
	{
		get
		{
			return soundProducer;
		}
	}

	public SoundProducerInterest(ASoundProducer soundProducer, float interest)
	{
		this.soundProducer = soundProducer;
		this.interest = interest;
	}

	public float AddInterest(Vector3 fromPosition, float duration, float loseInterestRate, float maxInterest)
	{
		float volume = soundProducer.GetVolume(fromPosition);
		if (volume > 0f)
		{
			interest += Mathf.Min(volume * duration, maxInterest);
		} 
		else
		{
			interest -= loseInterestRate * duration;
		}
		return interest;
	}
}