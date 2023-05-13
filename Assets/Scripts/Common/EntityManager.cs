using System.Collections.Generic;
using UnityEngine;

public abstract class EntityManager : MonoBehaviour
{
	[SerializeField] private Stack<GameObject> objPool;
	public Stack<GameObject> ObjPool { get => objPool; set => objPool = value; }

	[SerializeField] private GameObject obj;
	public GameObject Obj { get => obj; set => obj = value; }

	[SerializeField] private Transform container;
	public Transform Container { get => container; set => container = value; }

	private void Reset()
	{
		LoadContainer();
	}

	public void SpawnObj(Vector3 pos, Quaternion rot)
	{
		if (objPool.Count <= 0)
		{
			CreateObj(pos, rot);
		}
		else
		{
			GetObjFromPool(pos, rot);
		}
	}

	public void CreateObj(Vector3 pos, Quaternion rot)
	{
		GameObject newObj = Instantiate(obj, pos, rot);
		newObj.name = obj.name;
		newObj.transform.SetParent(container);
		newObj.SetActive(true);
	}

	public void ReturnObjToPool(GameObject _obj)
	{
		_obj.SetActive(false);
		objPool.Push(_obj);
	}

	public void GetObjFromPool(Vector3 pos, Quaternion rot)
	{
		GameObject _obj = objPool.Pop();
		_obj.transform.SetPositionAndRotation(pos, rot);
		_obj.SetActive(true);
	}

	protected abstract void LoadContainer();

}
