using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour 
{
	[SerializeField]
	private ChaseSoundEnemyStateData chaseSoundStateData;
	[SerializeField]
	private PatrolEnemyStateData patrolStateData;
	[SerializeField]
	private float loseInterestRate = 1000f;
	[SerializeField]
	private float maxInterest = 10000f;
	[SerializeField]
	private float updateInterestsTickRate = 0.15f;

	private Transform cachedTransform;
	private AEnemyState currentState;
	private List<SoundProducerInterest> soundProducerInterests = new List<SoundProducerInterest>();
	private SoundProducerManager soundProducerManager;
	private List<AEnemyState> states = new List<AEnemyState>();

    private Animator _animator;
    private NavMeshAgent _navMeshAgent;

	public ReadOnlyCollection<SoundProducerInterest> SoundProducerInterests
	{
		get
		{
			return soundProducerInterests.AsReadOnly();
		}
	}

	void Start()
	{
		cachedTransform = transform;
		soundProducerManager = Utils.GetSoundProducerManager(GameConstants.GAME_MANAGER_TAG);
		soundProducerManager.SoundProducerAddedEvent += OnSoundProducerAdded;

        _animator = GetComponentInChildren<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();

        states.Add(new PatrolEnemyState(this, patrolStateData));
		states.Add(new ChaseSoundEnemyState(this, chaseSoundStateData));

		ChangeState(EnemyStates.Patrol);

		StartCoroutine(UpdateInterests());
	}

	void OnDestroy()
	{
		currentState.OnStateExit();
		if (soundProducerManager != null)
		{
			soundProducerManager.SoundProducerAddedEvent -= OnSoundProducerAdded;
		}
	}

	void Update()
	{
		currentState.UpdateState();
        _animator.SetFloat("Velocity", _navMeshAgent.velocity.magnitude / _navMeshAgent.speed);
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

	public SoundProducerInterest FindSoundProducerMaxInterest()
	{
		if (soundProducerInterests.Count == 0)
		{
			return null;
		}

		SoundProducerInterest maxSoundProducerInterest = soundProducerInterests[0];
		for (int i = 1; i < soundProducerInterests.Count; i++)
		{
			if (soundProducerInterests[i].Interest > maxSoundProducerInterest.Interest)
			{
				maxSoundProducerInterest = soundProducerInterests[i];
			}
		}
		return maxSoundProducerInterest;
	}

	private void OnSoundProducerAdded(ASoundProducer soundProducer)
	{
		TryAddSoundProducerInterest(soundProducer);
	}

	private void TryAddSoundProducerInterest(ASoundProducer soundProducer)
	{
		if (soundProducerInterests.Find(soundProducerInterest => soundProducerInterest.SoundProducer == soundProducer) == null)
		{
			soundProducerInterests.Add(new SoundProducerInterest(soundProducer, 0f));
		}
	}

	private void RemoveSoundProducerInterest(SoundProducerInterest soundProducerInterest)
	{
		soundProducerInterests.Remove(soundProducerInterest);
	}

	private IEnumerator UpdateInterests()
	{
		while (true)
		{
			yield return new WaitForSeconds(updateInterestsTickRate);
			SoundProducerInterest maxSoundProducerInterest = null;
			for (int i = soundProducerInterests.Count - 1; i >= 0; i--)
			{
				SoundProducerInterest soundProducerInterest = soundProducerInterests[i];
				float newInterest = soundProducerInterest.AddInterest(cachedTransform.position, updateInterestsTickRate,
																	  loseInterestRate, maxInterest);
				if (newInterest <= 0f)
				{
					RemoveSoundProducerInterest(soundProducerInterest);
				} 
				else if (maxSoundProducerInterest == null || newInterest > maxSoundProducerInterest.Interest)
				{
					maxSoundProducerInterest = soundProducerInterest;
				}
			}

			if (maxSoundProducerInterest != null)
			{
				currentState.OnSoundProducerInterestsUpdated(maxSoundProducerInterest);
			}
		}
	}
}