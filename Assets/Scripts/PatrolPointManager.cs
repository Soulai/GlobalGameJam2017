using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class PatrolPointManager : MonoBehaviour
{
	private List<Transform> patrolPoints = new List<Transform>();

	void Start()
	{
		GameObject[] patrolPointsGO = GameObject.FindGameObjectsWithTag(GameConstants.PATROL_POINT_TAG);
		patrolPoints.AddRange(patrolPointsGO.Select(patrolPointGO => patrolPointGO.transform));
	}

	public Transform GetRandomPatrolPoint(Transform previousPatrolPoint)
	{
		Transform randomPatrolPoint = previousPatrolPoint;
		while (randomPatrolPoint == previousPatrolPoint)
		{
			randomPatrolPoint = patrolPoints[Random.Range(0, patrolPoints.Count)];
		}
		return randomPatrolPoint;
	}
}