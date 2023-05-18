using Assets.Scripts.Common;
using UnityEngine;

public class EnemyFlyToGroupManager : EnemyManagerTest
{
	private static EnemyFlyToGroupManager instance;
	public static EnemyFlyToGroupManager Instance { get => instance; }

	protected override void Awake()
	{
		base.Awake();
		if (instance != null) Debug.LogError("Only 1 EnemyFlyToGroupManager allow exists");

		instance = this;
	}
}
