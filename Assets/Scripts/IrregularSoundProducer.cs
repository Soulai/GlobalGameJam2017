using UnityEngine;
using System.Collections;

public class IrregularSoundProducer : ASoundProducer 
{
	[SerializeField]
	private float minInterval = 3f;
	[SerializeField]
	private float maxInterval = 7f;
	[SerializeField]
	private float soundDuration = 2.5f;
	[SerializeField]
	private float soundVolume = 25f;

	public override Vector3 SourcePosition
	{
		get
		{
			return transform.position;
		}
	}

	protected override void Start()
	{
		base.Start();
		StartCoroutine(ProduceSound());
	}

	public override float GetVolume(Vector3 fromPosition)
	{
		return SourceVolume / (fromPosition - cachedTransform.position).sqrMagnitude;
	}

	private IEnumerator ProduceSound()
	{
		Renderer renderer = GetComponent<Renderer>();
		while (true)
		{
			yield return new WaitForSeconds(Random.Range(minInterval, maxInterval));
			SourceVolume = soundVolume;
			Color initialColor = renderer.material.color;
			renderer.material.color = Color.cyan;
			yield return new WaitForSeconds(soundDuration);
			SourceVolume = 0f;
			renderer.material.color = Color.white;
		}
	}
}
