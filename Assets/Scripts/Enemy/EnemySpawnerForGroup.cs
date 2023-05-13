using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerForGroup : MonoBehaviour
{
	private static EnemySpawnerForGroup instance;
	public static EnemySpawnerForGroup Instance { get => instance; }

	[SerializeField] private GameObject[] spawnPositions;
    [SerializeField] private GameObject[] spawnObjects;
	[SerializeField] private GameObject[] groups;
	[SerializeField] private Transform group;
	[SerializeField] private Stack<Transform> slots;

	private void Awake()
	{
		if (instance != null) Debug.LogError("Only 1 EnemySpawnerForGroup allow exist");
		instance = this;

		spawnPositions = GameObject.FindGameObjectsWithTag("SpawnPosition");
		groups = GameObject.FindGameObjectsWithTag("Group");
		slots = new Stack<Transform>();
	}

	private void Start()
	{
		StartCoroutine(SpawnForGroup());
	}

	IEnumerator SpawnForGroup()
	{
		while (true)
		{
			//GameObject spawnObject = GetRandomSpawnObject();
			//Transform spawnTransform = GetRandomSpawnTransform();
			//EnemyManagerTest.Instance.SpawnObject(spawnObject, spawnTransform.position, spawnTransform.rotation);
			//yield return new WaitForSeconds(1);
			if (group == null || group.childCount <= 0)
			{
				group = GetRandomGroup();
				for (int i = 0; i < group.childCount; i++)
				{
					slots.Push(group.GetChild(i));
				}
			}

			Transform spawnTransform = GetRandomSpawnTransform();
			GameObject obj = GetRandomSpawnObject();
			MovementToGroup objToGroup = obj.GetComponent<MovementToGroup>();
			objToGroup.Target = slots.Pop();
			EnemyManagerTest.Instance.SpawnObject(obj, spawnTransform.position, spawnTransform.rotation);

			yield return new WaitForSeconds(5);
		}
	}

	public Transform GetRandomGroup()
	{
		int randIndex = Random.Range(0, groups.Length);
		return groups[randIndex].transform;
	}

	public Transform GetRandomSpawnTransform()
	{
		int randIndex = Random.Range(0, spawnPositions.Length);
		return spawnPositions[randIndex].transform;
	}

	public GameObject GetRandomSpawnObject()
	{
		int randIndex = Random.Range(0, spawnObjects.Length);
		return spawnObjects[randIndex];
	}
}
