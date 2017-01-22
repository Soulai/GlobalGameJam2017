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
		Gizmos.color = new Color(0f, 0f, 1f, 0.75f);
		Gizmos.DrawCube(transform.position + Vector3.up * 0.5f, new Vector3(0.75f, 1f, 0.75f));
	}
}