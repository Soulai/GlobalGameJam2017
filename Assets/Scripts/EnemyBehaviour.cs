using UnityEngine;
using System.Collections.Generic;

public class EnemyBehaviour : MonoBehaviour 
{
	[SerializeField]
	private PatrolEnemyStateData patrolStateData;

	private AEnemyState currentState;
	private List<AEnemyState> states = new List<AEnemyState>();

	void Start()
	{
		states.Add(new PatrolEnemyState(this, patrolStateData));

		ChangeState(EnemyStates.Patrol);
	}

	void OnDestroy()
	{
		currentState.OnStateExit();
	}

	void Update()
	{
		currentState.UpdateState();
	}

	public void ChangeState(EnemyStates newState)
	{
		EnemyStates previousState;
		if (currentState != null)
		{
			currentState.OnStateExit();
			previousState = currentState.stateName;
		} 
		else
		{
			previousState = EnemyStates.Undefined;
		}
		currentState = states.Find(state => state.stateName == newState);
		currentState.OnStateEnter(previousState);
	}
}