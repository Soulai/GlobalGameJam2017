using System;
using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.ObjectModel;

public abstract class AEnemyState
{
	protected EnemyBehaviour enemyBehaviour;
	protected Transform cachedTransform;
	protected NavMeshAgent navMeshAgent;

	private float previousUpdateInterestsTime;
	private Coroutine updateInterestsCoroutine;

	public abstract EnemyStates stateName
	{
		get;
	}

	public AEnemyState(EnemyBehaviour enemyBehaviour)
	{
		this.enemyBehaviour = enemyBehaviour;
		cachedTransform = enemyBehaviour.transform;
		navMeshAgent = enemyBehaviour.GetComponent<NavMeshAgent>();
	}

	public abstract void OnSoundProducerInterestsUpdated(SoundProducerInterest maxSoundProducerInterest);

	public abstract void OnStateEnter(EnemyStates previousState);

	public virtual void OnStateExit()
	{
	}

	public abstract void UpdateState();
}