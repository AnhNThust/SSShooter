using Assets.Scripts.Explode;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeManager : MonoBehaviour
{
	private static ExplodeManager instance;
	public static ExplodeManager Instance { get => instance; }

	[SerializeField] private Dictionary<ExplodeType, Stack<GameObject>> dicOfPoolObjects;
	[SerializeField] private Transform container;

	protected virtual void Awake()
	{
		if (instance != null) Debug.LogError("Only 1 ExplodeManager allow exist");
		instance = this;
		dicOfPoolObjects = new Dictionary<ExplodeType, Stack<GameObject>>();
	}

	public void SpawnObject(GameObject prefab, Vector3 position, Quaternion rotation)
	{
		ExplodeProperties explodeProperties = prefab.GetComponent<ExplodeProperties>();

		if (!dicOfPoolObjects.ContainsKey(explodeProperties.ExplodeType))
		{
			_ = CreatePoolFromKey(prefab);
		}

		foreach (ExplodeType bulletType in dicOfPoolObjects.Keys) // duyet danh sach key trong dictionary
		{
			if (explodeProperties.ExplodeType != bulletType) continue;

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
		ExplodeProperties explodeProperties = prefab.GetComponent<ExplodeProperties>();
		dicOfPoolObjects.Add(explodeProperties.ExplodeType, new Stack<GameObject>());
		return dicOfPoolObjects[explodeProperties.ExplodeType];
	}

	/// <summary>
	/// Lay ra pool co cung ExplodeType voi prefab
	/// </summary>
	/// <param name="prefab"></param>
	/// <returns></returns>
	public Stack<GameObject> GetPool(GameObject prefab)
	{
		ExplodeProperties explodeProperties = prefab.GetComponent<ExplodeProperties>();
		if (!dicOfPoolObjects.ContainsKey(explodeProperties.ExplodeType))
			return CreatePoolFromKey(prefab);

		return dicOfPoolObjects[explodeProperties.ExplodeType];
	}

	/// <summary>
	/// Lay ra phan tu co cung ExplodeType voi prefab
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
