using UnityEngine;

public class EnemyFlyToGroupManager : EnemyManagerTest
{
	private static EnemyFlyToGroupManager instance;
	public static EnemyFlyToGroupManager Instance { get => instance; }

	private void Awake()
	{
		if (instance != null) Debug.LogError("Only 1 EnemyManagerTest allow exists");

		instance = this;
	}
}
