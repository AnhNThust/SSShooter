using Assets.Scripts.Bullet;
using System.Collections.Generic;
using UnityEngine;

public class BulletManagerTest : MonoBehaviour
{
	[SerializeField] private Dictionary<BulletType, Stack<GameObject>> dicOfPoolObjects;
	[SerializeField] private Transform container;

	protected virtual void Awake()
	{
		dicOfPoolObjects = new Dictionary<BulletType, Stack<GameObject>>();
	}

	public void SpawnObject(GameObject prefab, Vector3 position, Quaternion rotation)
	{
		BulletProperties bulletProperties = prefab.GetComponent<BulletProperties>();

		if (!dicOfPoolObjects.ContainsKey(bulletProperties.BulletType))
		{
			_ = CreatePoolFromKey(prefab);
		}

		foreach (BulletType bulletType in dicOfPoolObjects.Keys) // duyet danh sach key trong dictionary
		{
			if (bulletProperties.BulletType != bulletType) continue;

			if (dicOfPoolObjects[bulletType].Count <= 0)
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
	public void CreateObjectToPool(GameObject prefab, Vector3 position, Quaternion rotation)
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
		BulletProperties bulletProperties = prefab.GetComponent<BulletProperties>();
		dicOfPoolObjects.Add(bulletProperties.BulletType, new Stack<GameObject>());
		return dicOfPoolObjects[bulletProperties.BulletType];
	}

	/// <summary>
	/// Lay ra pool co cung BulletType voi prefab
	/// </summary>
	/// <param name="prefab"></param>
	/// <returns></returns>
	public Stack<GameObject> GetPool(GameObject prefab)
	{
		BulletProperties bulletProperties = prefab.GetComponent<BulletProperties>();
		if (!dicOfPoolObjects.ContainsKey(bulletProperties.BulletType))
			return CreatePoolFromKey(prefab);

		return dicOfPoolObjects[bulletProperties.BulletType];
	}

	/// <summary>
	/// Lay ra phan tu co cung BulletType voi prefab
	/// </summary>
	/// <param name="prefab"></param>
	public void GetObjectFromPool(GameObject prefab, Vector3 position, Quaternion rotation)
	{
		Stack<GameObject> pool = GetPool(prefab);
		GameObject obj = pool.Pop();
		obj.transform.SetPositionAndRotation(position, rotation);
		obj.SetActive(true);
	}

	/// <summary>
	/// Tra object ve pool
	/// </summary>
	public void ReturnObjectToPool(GameObject _object)
	{
		Stack<GameObject> pool = GetPool(_object);
		pool.Push(_object);
		_object.SetActive(false);
	}
}
