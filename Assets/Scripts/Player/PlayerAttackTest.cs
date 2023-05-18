using System.Collections;
using UnityEngine;

public class PlayerAttackTest : EntityAttackTest
{
	protected override IEnumerator Attack()
	{
		while (true)
		{
			yield return new WaitForSeconds(delayShooting);
			for (int i = 0; i < numberBarrel; i++)
			{
				PlayerBulletManager.Instance.SpawnObject(prefab, transform.position, transform.GetChild(i).rotation);
			}
		}
	}
}
