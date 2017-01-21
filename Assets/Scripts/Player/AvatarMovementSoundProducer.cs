using UnityEngine;
using Player;

public class AvatarMovementSoundProducer : ASoundProducer
{
	[SerializeField]
	private float maxSoundVolume = 2500f;
	[SerializeField]
	private AnimationCurve soundVolumeCurve;

	private Rigidbody cachedRigidbody;
	private PlayerActions playerActions;

	protected override void Start()
	{
		base.Start();
		cachedRigidbody = GetComponent<Rigidbody>();
		playerActions = GetComponent<PlayerActions>();
	}

	void Update()
	{
		float newVolume = soundVolumeCurve.Evaluate(cachedRigidbody.velocity.magnitude / playerActions.MaximumRunningSpeed);
		newVolume *= maxSoundVolume;
		SourceVolume = newVolume;
	}
}