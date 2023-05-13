using UnityEngine;

public class PlayerProperties : EntityProperties
{
	protected override void ResetValue()
	{
		Hp = 10;
		Damage = 1;
	}
}
