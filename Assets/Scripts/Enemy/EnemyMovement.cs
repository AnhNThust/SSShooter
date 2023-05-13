using UnityEngine;

public class EnemyMovement : MovementByPath
{
	//protected override void ResetValue()
	//{
	//	base.ResetValue();
	//	MoveSpeed = 1f;
	//	RotSpeed = 100f;
	//}

	protected override void DespawnAndResetNode()
	{
		//EnemyManager.Instance.ReturnObjToPool(transform.parent.gameObject);
		EnemyManagerTest.Instance.ReturnObjectToPool(transform.parent.gameObject);
		CurrentNode = 0;
	}
}
