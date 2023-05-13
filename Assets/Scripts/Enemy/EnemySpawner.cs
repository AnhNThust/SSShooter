using Assets.Scripts.Common;
using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	[SerializeField] private GameObject[] spawnPositions;
	[SerializeField] private GameObject[] spawnObjects;

	[SerializeField] private GameObject spawnObject;
	[SerializeField] private float timerWave = 2f;
	[SerializeField] private int delay = 6;
	[SerializeField] private int count = 0;

	private void Reset()
	{
		spawnPositions = GameObject.FindGameObjectsWithTag("SpawnPosition");
	}

	private void Awake()
	{
		spawnObjects = GameObject.FindGameObjectsWithTag("Enemy");
		HideGameObject();
	}

	private void Start()
	{
		StartCoroutine(SpawnByWave());
	}

	IEnumerator SpawnByWave()
	{
		while (true)
		{
			if (count <= 0)
			{
				spawnObject = GetRandomObject();
				count = delay;
			}

			Spawn(spawnObject);
			yield return new WaitForSeconds(timerWave);
			--count;
		}
	}

	protected virtual GameObject GetRandomSpawnPosition()
	{
		int randIndex = Random.Range(0, spawnPositions.Length);
		return spawnPositions[randIndex];
	}

	public GameObject GetRandomObject()
	{
		int randIndex = Random.Range(0, spawnObjects.Length);
		return spawnObjects[randIndex];
	}

	protected virtual void Spawn(GameObject prefab)
	{
		Transform spawnTrans = GetRandomSpawnPosition().transform;
		//GameObject prefab = GetRandomObject();

		//EnemyManager.Instance.SpawnObj(spawnPos.position, spawnPos.rotation);
		EnemyManagerTest.Instance.SpawnObject(prefab, spawnTrans.position, spawnTrans.rotation);
	}

	public void HideGameObject()
	{
		foreach (var _spawnObject in spawnObjects)
		{
			_spawnObject.SetActive(false);
		}
	}
}
