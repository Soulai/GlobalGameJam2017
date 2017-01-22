using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PatrolEnemyState : AEnemyState
{
	private const int AVERAGE_VELOCITY_MIN_COUNT = 15;

	public override EnemyStates stateName
	{
		get
		{
			return EnemyStates.Patrol;
		}
	}
		
	private PatrolEnemyStateData balanceData;
	private PatrolPoint currentPatrolPointTarget;
	private PatrolPointManager patrolPointManager;
	private Coroutine waitAndChangeTargetCoroutine;

	public PatrolEnemyState(EnemyBehaviour enemyBehaviour, PatrolEnemyStateData balanceData) : base(enemyBehaviour)
	{
		this.balanceData = balanceData;
		patrolPointManager = Utils.GetPatrolPointManager(GameConstants.GAME_MANAGER_TAG);
		if (balanceData.patrolPointGroup == null)
		{
			balanceData.patrolPointGroup = patrolPointManager.GetPatrolPointGroup(cachedTransform.position);
		}
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
		int layerMask = 1 << LayerMask.NameToLayer(GameConstants.DESTROYABLE_BOXES_LAYER);
		Vector3 point1 = cachedTransform.position;
		Vector3 point2 = new Vector3(cachedTransform.position.x, 
			                 cachedTransform.position.y + navMeshAgent.height,
			                 cachedTransform.position.z);
		if (Physics.CapsuleCast(point1, point2, navMeshAgent.radius * 2f, cachedTransform.forward, 2f, layerMask))
		{
			MoveToRandomPatrolPoint();
		}

		Vector3 currentTarget = currentPatrolPointTarget.transform.position;
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
		currentPatrolPointTarget = balanceData.patrolPointGroup.GetRandomPatrolPoint(currentPatrolPointTarget);
		navMeshAgent.SetDestination(currentPatrolPointTarget.transform.position);
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