using UnityEngine;

public class ChaseSoundEnemyState : AEnemyState
{
	public override EnemyStates stateName
	{
		get
		{
			return EnemyStates.ChaseSound;
		}
	}

	private ChaseSoundEnemyStateData balanceData;
	private ASoundProducer currentSoundProducerTarget;
	private float previousTime;
	private float previousVolume;

	public ChaseSoundEnemyState(EnemyBehaviour enemyBehaviour, ChaseSoundEnemyStateData balanceData) : base(enemyBehaviour)
	{
		this.balanceData = balanceData;
	}

	public override void UpdateState()
	{
		if (Time.time - previousTime > balanceData.updateCycleDuration)
		{
			RunUpdateCycle();
		}
	}

	public override void OnStateEnter(EnemyStates previousState)
	{
		base.OnStateEnter(previousState);

		currentSoundProducerTarget = soundProducerManager.FindSoundProducerMaxVolume(cachedTransform.position);
		previousTime = Time.time;
	}

	public void MoveToSoundProducerTarget()
	{
		navMeshAgent.SetDestination(currentSoundProducerTarget.SourcePosition);
	}

	protected override void OnSoundProducerAdded(ASoundProducer soundProducer)
	{
		base.OnSoundProducerAdded(soundProducer);

		if (soundProducer.GetVolume(cachedTransform.position) > currentSoundProducerTarget.GetVolume(cachedTransform.position))
		{
			currentSoundProducerTarget = soundProducer;
			MoveToSoundProducerTarget();
		}
	}

	protected override void OnSoundProducerRemoved(ASoundProducer soundProducer)
	{
		base.OnSoundProducerRemoved(soundProducer);

		if (soundProducer == currentSoundProducerTarget)
		{
			currentSoundProducerTarget = soundProducerManager.FindSoundProducerMaxVolume(cachedTransform.position);
			if (currentSoundProducerTarget == null)
			{
				enemyBehaviour.ChangeState(EnemyStates.Patrol);
			} 
			else
			{
				MoveToSoundProducerTarget();
			}
		}
	}

	protected override void OnVolumeModified(ASoundProducer soundProducer)
	{
		base.OnVolumeModified(soundProducer);

		if (soundProducer.GetVolume(cachedTransform.position) > previousVolume)
		{
			RunUpdateCycle();
		}
	}

	private void RunUpdateCycle()
	{
		UpdateSoundProducerTarget();
		MoveToSoundProducerTarget();
		previousTime = Time.time;
	}

	private void UpdateSoundProducerTarget()
	{
		currentSoundProducerTarget = soundProducerManager.FindSoundProducerMaxVolume(cachedTransform.position);
		previousVolume = currentSoundProducerTarget.GetVolume(cachedTransform.position);
	}
}