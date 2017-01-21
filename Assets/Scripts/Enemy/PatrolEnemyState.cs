using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class PatrolEnemyState : AEnemyState
{
	public override EnemyStates stateName
	{
		get
		{
			return EnemyStates.Patrol;
		}
	}

	private PatrolEnemyStateData balanceData;
	private Transform currentPatrolPointTarget;
	private PatrolPointManager patrolPointManager;
	private Coroutine waitAndChangeTargetCoroutine;

	public PatrolEnemyState(EnemyBehaviour enemyBehaviour, PatrolEnemyStateData balanceData) : base(enemyBehaviour)
	{
		this.balanceData = balanceData;
		patrolPointManager = Utils.GetPatrolPointManager(GameConstants.GAME_MANAGER_TAG);
	}

	public override void OnStateEnter(EnemyStates previousState)
	{
		MoveToRandomPatrolPoint();
	}

	public override void OnStateExit()
	{
		StopWaitAndChangeTargetCoroutine();
	}

	public override void OnSoundProducerInterestsUpdated(SoundProducerInterest maxSoundProducerInterest)
	{
		TryChaseSound(maxSoundProducerInterest);
	}

	public override void UpdateState()
	{
		Vector3 currentTarget = currentPatrolPointTarget.position;
		currentTarget.y = cachedTransform.position.y;
		float distanceToTarget = (currentTarget - cachedTransform.position).magnitude;
		if (distanceToTarget < 0.1f)
		{
			currentPatrolPointTarget = null;
			MoveToRandomPatrolPoint();
		} 
		else if (distanceToTarget < balanceData.distanceTryChangeTarget && waitAndChangeTargetCoroutine == null)
		{
			waitAndChangeTargetCoroutine = enemyBehaviour.StartCoroutine(WaitAndChangeTarget());
		}
	}

	private void MoveToRandomPatrolPoint()
	{
		StopWaitAndChangeTargetCoroutine();
		currentPatrolPointTarget = patrolPointManager.GetRandomPatrolPoint(currentPatrolPointTarget);
		navMeshAgent.SetDestination(currentPatrolPointTarget.position);
	}

	private void StopWaitAndChangeTargetCoroutine()
	{
		if (waitAndChangeTargetCoroutine != null)
		{
			enemyBehaviour.StopCoroutine(waitAndChangeTargetCoroutine);
			waitAndChangeTargetCoroutine = null;
		}
	}

	private void TryChaseSound(SoundProducerInterest soundProducerInterest)
	{
		if (soundProducerInterest.Interest > balanceData.chaseSoundMinInterestRequired)
		{
			enemyBehaviour.ChangeState(EnemyStates.ChaseSound);
		}
	}

	private IEnumerator WaitAndChangeTarget()
	{
		yield return new WaitForSeconds(balanceData.waitDurationChangeTarget);
		MoveToRandomPatrolPoint();
	}
}