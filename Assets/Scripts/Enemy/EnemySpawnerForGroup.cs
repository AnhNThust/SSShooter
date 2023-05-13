using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerForGroup : MonoBehaviour
{
	private static EnemySpawnerForGroup instance;
	public static EnemySpawnerForGroup Instance { get => instance; }

	[SerializeField] private Stack<Transform> slots;
	public Stack<Transform> Slots { get => slots; }

	[SerializeField] private bool canSpawn = true;
	public bool CanSpawn { get => canSpawn; set => canSpawn = value; }

	[SerializeField] private Transform spawnPositionsParent;
	[SerializeField] private Transform[] spawnPositions;
	[SerializeField] private Transform[] spawnObjects;

	[SerializeField] private Transform groupsParent;
	[SerializeField] private Transform[] groups;
	[SerializeField] private Transform group;

	private readonly float timeSpawn = 0.1f;

	private void Awake()
	{
		if (instance != null) Debug.LogError("Only 1 EnemySpawnerForGroup allow exist");
		instance = this;
		slots = new Stack<Transform>();
	}

	[ContextMenu("Reload")]
	private void Reload()
	{
		spawnPositions = new Transform[spawnPositionsParent.childCount];
		for (int i = 0; i < spawnPositions.Length; i++)
		{
			spawnPositions[i] = spawnPositionsParent.GetChild(i).transform;
		}

		groups = new Transform[groupsParent.childCount];
		for (int i = 0; i < groups.Length; i++)
		{
			groups[i] = groupsParent.GetChild(i).transform;
		}

		spawnObjects = new Transform[transform.childCount];
		for(int i = 0; i < spawnObjects.Length; i++)
		{
			spawnObjects[i] = transform.GetChild(i).transform;
		}
	}

	private void Start()
	{
		StartCoroutine(SpawnForGroup());
	}

	IEnumerator SpawnForGroup()
	{
		while (true)
		{
			if (canSpawn)
			{
				group = GetRandomGroup();
				for (int i = 0; i < group.childCount; i++)
				{
					slots.Push(group.GetChild(i));
				}
				canSpawn = false;
			}

			if (slots.Count > 0)
			{
				Transform spawnTransform = GetRandomSpawnTransform();
				GameObject obj = GetRandomSpawnObject();
				EnemyFlyToGroupManager.Instance.SpawnObject(obj, spawnTransform.position, spawnTransform.rotation);
			}
			yield return new WaitForSeconds(timeSpawn);
		}
	}

	/// <summary>
	/// Lay ra 1 group ngau nhien
	/// </summary>
	/// <returns>Tra ve Transform cua group</returns>
	public Transform GetRandomGroup()
	{
		int randIndex = Random.Range(0, groups.Length);
		return groups[randIndex].transform;
	}

	/// <summary>
	/// Lay ra vi tri spawn ngau nhien
	/// </summary>
	/// <returns>Tra ve transform cua spawnPosition</returns>
	public Transform GetRandomSpawnTransform()
	{
		int randIndex = Random.Range(0, spawnPositions.Length);
		return spawnPositions[randIndex].transform;
	}

	/// <summary>
	/// Lay ra prefab ngau nhien
	/// </summary>
	/// <returns>Tra ve 1 GameObject</returns>
	public GameObject GetRandomSpawnObject()
	{
		int randIndex = Random.Range(0, spawnObjects.Length);
		return spawnObjects[randIndex].gameObject;
	}
}
