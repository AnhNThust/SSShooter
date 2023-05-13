using System.Collections.Generic;
using UnityEngine;

public class BulletManager : EntityManager
{
	private static BulletManager instance;
	public static BulletManager Instance { get => instance; }

	private void Awake()
	{
		if (instance != null)
		{
			Debug.LogError("Only 1 Bullet Manager exist");
		}
		instance = this;
		ObjPool = new Stack<GameObject>();
	}

	protected override void LoadContainer()
	{
		Container = GameObject.Find("BulletContainer").transform;
	}
}
