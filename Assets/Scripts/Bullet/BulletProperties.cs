using Assets.Scripts.Bullet;
using UnityEngine;

public class BulletProperties : EntityProperties
{
	[SerializeField] private BulletType bulletType;
	public BulletType BulletType { get => bulletType; }

	protected override void ResetValue()
	{
	}
}
