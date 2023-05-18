using System;
using System.Collections;
using UnityEngine;

public class EntityAttackTest : MonoBehaviour
{
	[SerializeField] protected GameObject prefab;

	[SerializeField] protected float delayShooting;

	[SerializeField] protected int numberBarrel;

	[SerializeField] protected float degreeBetweenBarrel;

	private void Start()
	{
		CreateBarrel();
		UpdateSpawnRotation();
		StartCoroutine(Attack());
	}

	protected virtual IEnumerator Attack()
	{
		yield return null;
	}

	protected virtual void CreateBarrel()
	{
		ClearAllBarrel();

		for (int i = 0; i < numberBarrel; i++)
		{
			Quaternion rot = Quaternion.Euler(0f, 0f, i * degreeBetweenBarrel);
			GameObject barrel = new($"barrel_{i + 1}");
			barrel.transform.rotation = rot;
			barrel.transform.SetParent(transform);
		}
	}

	protected virtual void UpdateSpawnRotation()
	{
		float offset = (numberBarrel - 1) * degreeBetweenBarrel / 2;
		transform.rotation = Quaternion.Euler(0f, 0f, -offset);
	}

	protected virtual void ClearAllBarrel()
	{
		for (int i = 0; i < transform.childCount; i++)
		{
			Destroy(transform.GetChild(i).gameObject);
		}
	}
}