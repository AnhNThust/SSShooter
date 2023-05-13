using UnityEngine;

public class PlayerAttack : EntityAttack
{
	private void Start()
	{
		Application.targetFrameRate = 60;
		QualitySettings.vSyncCount = 1;

		StartCoroutine(Shoot());
	}

	protected override void Attack()
	{
		BulletManager.Instance.SpawnObj(transform.parent.position, Quaternion.identity);
	}

	/// <summary>
	/// Dùng để tăng tốc độ ra đạn
	/// </summary>
	protected virtual void UpdateShootDelay(float newShootDelay)
	{
		ShootDelay = newShootDelay;
	}

	protected override void ResetValue()
	{
		ShootDelay = 0.3f;
	}
}
