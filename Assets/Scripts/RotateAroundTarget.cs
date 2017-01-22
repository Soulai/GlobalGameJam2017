using UnityEngine;

public class RotateAroundTarget : MonoBehaviour 
{
	public float rotationSpeed = 5f;
	public Transform target;

	void Update()
	{
		transform.RotateAround(target.position, Vector3.up, rotationSpeed * Time.deltaTime);
	}
}
