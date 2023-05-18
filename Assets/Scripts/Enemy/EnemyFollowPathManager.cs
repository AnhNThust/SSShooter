using Assets.Scripts.Common;
using UnityEngine;

public class EnemyFollowPathManager : EnemyManagerTest
{
	private static EnemyFollowPathManager instance;
	public static EnemyFollowPathManager Instance { get => instance; }

	protected override void Awake()
	{
		base.Awake();
		if (instance != null) Debug.LogError("Only 1 EnemyFollowPathManager allow exists");

		instance = this;
	}
}
