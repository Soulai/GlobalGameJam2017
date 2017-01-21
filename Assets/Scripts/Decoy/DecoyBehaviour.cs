using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class DecoyBehaviour : ASoundProducer 
{
	[SerializeField]
	private float lifetime = 15f;
	[SerializeField]
	private float maxSpeed = 10f;
	[SerializeField]
	private float smoothTime = 0.2f;
	[SerializeField]
	private float soundMaxVolume = 3000f;
	[SerializeField]
	private AnimationCurve soundCurve;

	private CharacterController characterController;
	private float currentSpeed;
	private float currentVelocity;
	private Coroutine moveTowardsDirectionCoroutine;
	private float triggerTime;
	private Coroutine waitForPlayerInputCoroutine;

	protected override void Start()
	{
		base.Start();
		characterController = GetComponent<CharacterController>();
	}

	void OnTriggerEnter(Collider collider)
	{
		if (collider.tag == GameConstants.PLAYER_TAG && triggerTime == 0f)
		{
			PlayerActions playerActions = collider.GetComponent<PlayerActions>();
			waitForPlayerInputCoroutine = StartCoroutine(WaitForPlayerInput(playerActions));
			StartCoroutine(ProduceSound());
			StartCoroutine(WaitAndDestroy());
			triggerTime = Time.time;
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
			characterController.Move(motion * Time.fixedDeltaTime);
			yield return new WaitForFixedUpdate();
		}
	}

	private void OnControllerColliderHit(ControllerColliderHit hit)
	{
		if (hit.gameObject.CompareTag(GameConstants.WALL_TAG))
		{
			StopCoroutine(moveTowardsDirectionCoroutine);
			float rotationAngle = Random.Range(-80f, 80f);
			Vector3 target = Vector3.Cross(Vector3.up, hit.normal);
			Vector3 newDirection = Vector3.RotateTowards(hit.normal, target, Mathf.Deg2Rad * rotationAngle, 0f);
			moveTowardsDirectionCoroutine = StartCoroutine(MoveTowardsDirection(newDirection));
		}
	}

	private IEnumerator ProduceSound()
	{
		while (true)
		{
			SourceVolume = soundCurve.Evaluate((Time.time - triggerTime) / lifetime) * soundMaxVolume;
			yield return null;
		}
	}

	private IEnumerator WaitAndDestroy()
	{
		yield return new WaitForSeconds(lifetime);
		Destroy(gameObject);
	}

	private IEnumerator WaitForPlayerInput(PlayerActions playerActions)
	{
		while (true)
		{
			if (Input.GetButtonDown(playerActions.AxisPrefix + "-Fire2"))
			{
				Vector3 moveDirection = cachedTransform.position - playerActions.transform.position;
				moveDirection.y = 0f;
				moveDirection.Normalize();
				moveTowardsDirectionCoroutine = StartCoroutine(MoveTowardsDirection(moveDirection));
				break;
			}
			yield return null;
		}
	}
}
