using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : EntityManager
{
	private static EnemyManager instance;
	public static EnemyManager Instance { get => instance; }

	private void Awake()
	{
		if (instance != null)
		{
			Debug.LogError("Only 1 Enemy Manager exist");
		}
		instance = this;
		ObjPool = new Stack<GameObject>();
	}

	protected override void LoadContainer()
	{
		Container = GameObject.Find("EnemyContainer").transform;
	}
}
