using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class PatrolPointManager : MonoBehaviour
{
	private List<PatrolPointGroup> patrolPointGroups = new List<PatrolPointGroup>();

	void Start()
	{
		GameObject[] patrolPointGroupGOs = GameObject.FindGameObjectsWithTag(GameConstants.PATROL_POINT_GROUP_TAG);
		patrolPointGroups.AddRange(patrolPointGroupGOs.Select(
								   patrolPointGroupGO => patrolPointGroupGO.GetComponent<PatrolPointGroup>()));
	}
		
	public PatrolPointGroup GetPatrolPointGroup(Vector3 initialPosition)
	{
		foreach (PatrolPointGroup patrolPointGroup in patrolPointGroups)
		{
			Bounds groupBounds = patrolPointGroup.CalculateOrGetGroupBounds();
			if (groupBounds != default(Bounds) && groupBounds.Contains(initialPosition))
			{
				return patrolPointGroup;
			}
		}
		return null;
	}
}