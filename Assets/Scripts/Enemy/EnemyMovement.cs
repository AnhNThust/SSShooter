using UnityEngine;

public class EnemyMovement : MovementByPath
{
	protected override void DespawnAndResetNode()
	{
		//EnemyManager.Instance.ReturnObjToPool(transform.parent.gameObject);
		EnemyFollowPathManager.Instance.ReturnObjectToPool(transform.parent.gameObject);
		CurrentNode = 0;
	}
}
