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
	private SoundProducerInterest currentSoundProducerTarget;
	private float previousTime;

	public ChaseSoundEnemyState(EnemyBehaviour enemyBehaviour, ChaseSoundEnemyStateData balanceData) : base(enemyBehaviour)
	{
		this.balanceData = balanceData;
	}

	public override void UpdateState()
	{
		if (Time.time - previousTime > balanceData.updateNavigationTickRate)
		{
			RunUpdateCycle();
		}
	}

	public override void OnStateEnter(EnemyStates previousState)
	{
		currentSoundProducerTarget = enemyBehaviour.FindSoundProducerMaxInterest();
		previousTime = Time.time;
	}

	public void MoveToSoundProducerTarget()
	{
		navMeshAgent.SetDestination(currentSoundProducerTarget.SoundProducer.SourcePosition);
	}

	private void RunUpdateCycle()
	{
		UpdateSoundProducerTarget();
		MoveToSoundProducerTarget();
		previousTime = Time.time;
	}

	public override void OnSoundProducerInterestsUpdated(SoundProducerInterest maxSoundProducerInterest)
	{
		UpdateSoundProducerTarget(maxSoundProducerInterest);
	}

	private void UpdateSoundProducerTarget(SoundProducerInterest maxSoundProducerInterest = null)
	{
		if (maxSoundProducerInterest == null)
		{
			maxSoundProducerInterest = enemyBehaviour.FindSoundProducerMaxInterest(); 
		}

		if (maxSoundProducerInterest == null || maxSoundProducerInterest.Interest < balanceData.patrolMaxInterestRequired)
		{
			enemyBehaviour.ChangeState(EnemyStates.Patrol);
		} 
		else
		{
			currentSoundProducerTarget = maxSoundProducerInterest;
		}
	}
}