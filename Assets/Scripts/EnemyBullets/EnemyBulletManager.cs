using UnityEngine;

public class EnemyBulletManager : BulletManagerTest
{
    private static EnemyBulletManager instance;
    public static EnemyBulletManager Instance { get => instance; }

	protected override void Awake()
	{
		base.Awake();
		if (instance != null) Debug.LogError("Only 1 EnemyBulletManager allow exist");
		instance = this;
	}
}
