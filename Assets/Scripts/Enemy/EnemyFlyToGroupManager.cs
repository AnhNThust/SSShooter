using System.Collections.Generic;
using UnityEngine;

public class EnemyFlyToGroupManager : EnemyManagerTest
{
	private static EnemyFlyToGroupManager instance;
	public static EnemyFlyToGroupManager Instance { get => instance; }

	protected override void Awake()
	{
		base.Awake();
		if (instance != null) Debug.LogError("Only 1 EnemyFlyToGroupManager allow exists");

		instance = this;
	}

	protected override void CreateObjectToPool(GameObject prefab, Vector3 position, Quaternion rotation)
	{
		GameObject obj = Instantiate(prefab, position, rotation);

		obj.GetComponent<EnemyDamageReceiver>().callbackDie +=
			EnemySpawnerForGroup.Instance.UpdateSlotInGroup;

		obj.transform.parent = container;
		obj.SetActive(true);
	}

	protected override void GetObjectFromPool(GameObject prefab, Vector3 position, Quaternion rotation)
	{
		Stack<GameObject> pool = GetPool(prefab);
		GameObject obj = pool.Pop();

		obj.GetComponent<EnemyDamageReceiver>().callbackDie +=
			EnemySpawnerForGroup.Instance.UpdateSlotInGroup;

		obj.transform.SetPositionAndRotation(position, rotation);
		obj.SetActive(true);
	}

	public override void ReturnObjectToPool(GameObject _object)
	{
		_object.GetComponent<EnemyDamageReceiver>().callbackDie -= EnemySpawnerForGroup.Instance.UpdateSlotInGroup;

		Stack<GameObject> pool = GetPool(_object);
		pool.Push(_object);
		_object.SetActive(false);
	}
}
