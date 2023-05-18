using Assets.Scripts.Common;
using System.Collections;
using UnityEngine;

public class EnemySpawnerFollowPath : MonoBehaviour
{
	[SerializeField] private Transform spawnPositionsParent;
	[SerializeField] private Transform[] spawnPositions;
	[SerializeField] private Transform[] spawnObjects;
	[SerializeField] private GameObject spawnObject;

	[SerializeField] private float timerWave = 2f;
	[SerializeField] private int delay = 6;
	[SerializeField] private int count = 0;

	[ContextMenu("Reload")]
	private void Reload()
	{
		spawnPositions = new Transform[spawnPositionsParent.childCount];
		for (int i = 0; i < spawnPositions.Length; i++)
		{
			spawnPositions[i] = spawnPositionsParent.GetChild(i).transform;
		}

		spawnObjects = new Transform[transform.childCount];
		for(int i = 0; i < spawnObjects.Length; i++)
		{
			spawnObjects[i] = transform.GetChild(i).transform;
		}
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

	protected virtual Transform GetRandomSpawnPosition()
	{
		int randIndex = Random.Range(0, spawnPositions.Length);
		return spawnPositions[randIndex];
	}

	public GameObject GetRandomObject()
	{
		int randIndex = Random.Range(0, spawnObjects.Length);
		return spawnObjects[randIndex].gameObject;
	}

	protected virtual void Spawn(GameObject prefab)
	{
		Transform spawnTrans = GetRandomSpawnPosition();
		//GameObject prefab = GetRandomObject();

		//EnemyManager.Instance.SpawnObj(spawnPos.position, spawnPos.rotation);
		EnemyFollowPathManager.Instance.SpawnObject(prefab, spawnTrans.position, spawnTrans.rotation);
	}
}
