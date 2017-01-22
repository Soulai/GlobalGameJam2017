using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPointGroup : MonoBehaviour 
{
	private Bounds bounds;
	private List<PatrolPoint> patrolPoints;

	void Start()
	{
		FindOwnedPatrolPoints();
	}

	void OnDrawGizmos()
	{
		FindOwnedPatrolPoints();
		CalculateOrGetGroupBounds();
		if (bounds != default(Bounds))
		{
			Gizmos.color = new Color(1f, 0f, 0f, 0.3f);
			Gizmos.DrawCube(bounds.center + Vector3.up * 1f, bounds.size);
		}
	}

	public Bounds CalculateOrGetGroupBounds()
	{
		if (patrolPoints.Count > 0)
		{
			bounds = new Bounds();
			bounds.center = patrolPoints [0].transform.position;
			bounds.size = new Vector3(0f, 4f, 0f);
			for (int i = 1; i < patrolPoints.Count; i++)
			{
				bounds.Encapsulate(patrolPoints [i].transform.position);
			}
		}
		return bounds;
	}

	public PatrolPoint GetRandomPatrolPoint(PatrolPoint previousPatrolPoint)
	{
		PatrolPoint randomPatrolPoint = previousPatrolPoint;
		while (randomPatrolPoint == previousPatrolPoint)
		{
			randomPatrolPoint = patrolPoints[Random.Range(0, patrolPoints.Count)];
		}
		return randomPatrolPoint;
	}
		
	private void FindOwnedPatrolPoints()
	{
		patrolPoints = new List<PatrolPoint>();
		GameObject[] patrolPointGOs = GameObject.FindGameObjectsWithTag(GameConstants.PATROL_POINT_TAG);
		foreach (GameObject patrolPointGO in patrolPointGOs)
		{
			PatrolPoint patrolPoint = patrolPointGO.GetComponent<PatrolPoint>();
			if (patrolPoint.Group == this)
			{
				patrolPoints.Add(patrolPoint);
			}
		}
	}
}
