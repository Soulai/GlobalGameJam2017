using UnityEngine;

public class PatrolPoint : MonoBehaviour 
{
	[SerializeField]
	private PatrolPointGroup group;

	public PatrolPointGroup Group
	{
		get
		{
			return group;
		}
	}

	void OnDrawGizmos()
	{
		Gizmos.color = new Color(1f, 0f, 0f, 0.5f);
		Gizmos.DrawCube(transform.position + Vector3.up * 0.125f, Vector3.one * 0.25f);
	}
}