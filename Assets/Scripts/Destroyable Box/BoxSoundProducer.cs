using UnityEngine;
using DestroyableBox;
using System.Collections;

[RequireComponent(typeof(BoxHealth))]
public class BoxSoundProducer : ASoundProducer
{
	[SerializeField]
	private float soundDuration = 5f;
	[SerializeField]
	private float soundVolume = 2500f;

	protected override void Start()
	{
		base.Start();
		GetComponent<BoxHealth>().BoxDestroyedEvent += OnBoxDestroyed;
	}

	void OnDestroy()
	{
		BoxHealth boxHealth = GetComponent<BoxHealth>();
		if (boxHealth != null)
		{
			boxHealth.BoxDestroyedEvent -= OnBoxDestroyed;
		}
	}

	private void OnBoxDestroyed(BoxHealth boxHealth)
	{
		StartCoroutine(ProduceSoundWaitAndRemoveSound());
	}

	private IEnumerator ProduceSoundWaitAndRemoveSound()
	{
		SourceVolume = soundVolume;
		yield return new WaitForSeconds(soundDuration);
		SourceVolume = 0f;
	}
}