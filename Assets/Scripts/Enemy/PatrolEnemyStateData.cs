using System;

[Serializable]
public class PatrolEnemyStateData
{
	public float distanceTryChangeTarget = 2f;
	public float chaseSoundMinInterestRequired = 50f;
	public PatrolPointGroup patrolPointGroup;
	public float waitDurationChangeTarget = 2f;
}