using Assets.Scripts.Common;
using UnityEngine;

public class EnemyProperties : EntityProperties
{
	[SerializeField] private EnemyType eType;
	public EnemyType EType { get => eType; set => eType = value; }

	protected override void ResetValue()
	{
		Hp = 10;
		Damage = 1;
	}
}
