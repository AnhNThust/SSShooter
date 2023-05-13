using UnityEngine;

public class EnemyFollowPathManager : EnemyManagerTest
{
	private static EnemyFollowPathManager instance;
	public static EnemyFollowPathManager Instance { get => instance; }

	private void Awake()
	{
		if (instance != null) Debug.LogError("Only 1 EnemyManagerTest allow exists");

		instance = this;
	}
}
