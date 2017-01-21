using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class DecoyBehaviour : MonoBehaviour 
{
	[SerializeField]
	private float maxSpeed = 10f;
	[SerializeField]
	private float smoothTime = 0.2f;

	private CharacterController characterController;
	private float currentSpeed;
	private float currentVelocity;
	private Coroutine moveTowardsDirectionCoroutine;
	private Coroutine waitForPlayerInputCoroutine;

	void Start()
	{
		characterController = GetComponent<CharacterController>();
	}

	void OnTriggerEnter(Collider collider)
	{
		if (collider.tag == GameConstants.PLAYER_TAG)
		{
			PlayerActions playerActions = collider.GetComponent<PlayerActions>();
			waitForPlayerInputCoroutine = StartCoroutine(WaitForPlayerInput(playerActions));
		}
	}

	void OntriggerExit(Collider collider)
	{
		if (collider.tag == GameConstants.PLAYER_TAG)
		{
			if (waitForPlayerInputCoroutine != null)
			{
				StopCoroutine(waitForPlayerInputCoroutine);
			}
		}
	}

	private IEnumerator MoveTowardsDirection(Vector3 moveDirection)
	{
		while (true)
		{
			currentSpeed = Mathf.SmoothDamp(currentSpeed, maxSpeed, ref currentVelocity, smoothTime);
			Vector3 motion = moveDirection * currentSpeed;
			motion += Physics.gravity;
			characterController.Move(motion);
			yield return null;
		}
	}

	private void OnControllerColliderHit(ControllerColliderHit hit)
	{
		if (hit.gameObject.CompareTag(GameConstants.WALL_TAG))
		{
			StopCoroutine(moveTowardsDirectionCoroutine);
		}
	}

	private IEnumerator WaitForPlayerInput(PlayerActions playerActions)
	{
		while (true)
		{
			if (Input.GetButtonDown(playerActions.AxisPrefix + "-Fire2"))
			{
				Vector3 moveDirection = transform.position - playerActions.transform.position;
				moveDirection.y = 0f;
				moveDirection.Normalize();
				moveTowardsDirectionCoroutine = StartCoroutine(MoveTowardsDirection(moveDirection));
				break;
			}
			yield return null;
		}
	}
}
