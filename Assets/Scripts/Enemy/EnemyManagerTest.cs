using Assets.Scripts.Common;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManagerTest : MonoBehaviour
{
	[SerializeField] protected Dictionary<EnemyType, Stack<GameObject>> poolObjects;
	[SerializeField] protected Transform container;

	protected virtual void Awake()
	{
		poolObjects = new Dictionary<EnemyType, Stack<GameObject>>();
	}

	public virtual void SpawnObject(GameObject prefab, Vector3 position, Quaternion rotation)
	{
		EnemyProperties enemyProperty = prefab.GetComponent<EnemyProperties>();

		if (!poolObjects.ContainsKey(enemyProperty.EType))
		{
			_ = CreatePoolFromKey(prefab);
		}

		foreach (EnemyType enemyType in poolObjects.Keys) // duyet danh sach key trong dictionary
		{
			if (enemyProperty.EType != enemyType) continue;

			if (poolObjects[enemyType].Count <= 0)
			{
				CreateObjectToPool(prefab, position, rotation);
				break;
			}

			GetObjectFromPool(prefab, position, rotation);
			break;
		}
	}

	/// <summary>
	/// Tao object neu kiem tra trong pool khong con object
	/// </summary>
	protected virtual void CreateObjectToPool(GameObject prefab, Vector3 position, Quaternion rotation)
	{
		GameObject _gameObject = Instantiate(prefab, position, rotation);
		_gameObject.transform.parent = container;
		_gameObject.SetActive(true);
	}

	/// <summary>
	/// Tao ra pool tu prefab co san
	/// </summary>
	/// <param name="prefab"></param>
	public Stack<GameObject> CreatePoolFromKey(GameObject prefab)
	{
		EnemyProperties enemyProperty = prefab.GetComponent<EnemyProperties>();
		poolObjects.Add(enemyProperty.EType, new Stack<GameObject>());
		return poolObjects[enemyProperty.EType];
	}

	/// <summary>
	/// Lay ra pool co cung EnemyType voi prefab
	/// </summary>
	/// <param name="prefab"></param>
	/// <returns></returns>
	public Stack<GameObject> GetPool(GameObject prefab)
	{
		EnemyProperties enemyProperty = prefab.GetComponent<EnemyProperties>();
		if (!poolObjects.ContainsKey(enemyProperty.EType)) 
			return CreatePoolFromKey(prefab);

		return poolObjects[enemyProperty.EType];
	}

	/// <summary>
	/// Lay ra phan tu co cung EnemyType voi prefab
	/// </summary>
	/// <param name="prefab"></param>
	protected virtual void GetObjectFromPool(GameObject prefab, Vector3 position, Quaternion rotation)
	{
		Stack<GameObject> pool = GetPool(prefab);
		GameObject obj = pool.Pop();
		obj.transform.SetPositionAndRotation(position, rotation);
		obj.SetActive(true);
	}

	/// <summary>
	/// Tra object ve pool
	/// </summary>
	public virtual void ReturnObjectToPool(GameObject _object)
	{
		Stack<GameObject> pool = GetPool(_object);
		pool.Push(_object);
		_object.SetActive(false);
	}
}
