using UnityEngine;

public class PlayerBulletManager : BulletManagerTest
{
	private static PlayerBulletManager instance;
	public static PlayerBulletManager Instance { get => instance; }

	protected override void Awake()
	{
		base.Awake();
		if (instance != null) Debug.LogError("Only 1 PlayerBulletManager allow exist");
		instance = this;
	}
}
