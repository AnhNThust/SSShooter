using UnityEngine;
using Assets.Scripts.Common;
using System.Collections;
using Unity.IO.LowLevel.Unsafe;
using System.Threading;

public class EnemyDamageReceiver : MonoBehaviour
{
	[SerializeField] protected Transform hpTransform;

	[SerializeField] protected float totalHp;
	[SerializeField] protected float currentHp;

	[SerializeField] protected Transform explodeAnimParent;
	[SerializeField] protected Transform[] explodeAnims;

	EnemyProperties eProperty;
	//int count = 0;

	[ContextMenu("Reload")]
	private void Reload()
	{
		explodeAnims = new Transform[explodeAnimParent.childCount];
		for (int i = 0; i < explodeAnims.Length; i++)
		{
			explodeAnims[i] = explodeAnimParent.GetChild(i);
		}
	}

	private void Awake()
	{
		totalHp = transform.GetComponent<EnemyProperties>().Hp;
		currentHp = totalHp;
		eProperty = GetComponent<EnemyProperties>();
	}

	private void Update()
	{
		if (currentHp > 0) return;

		if (eProperty.EType == EnemyType.E1 || eProperty.EType == EnemyType.E2
				|| eProperty.EType == EnemyType.E3)
		{
			EnemyFollowPathManager.Instance.ReturnObjectToPool(gameObject);
		}
		else
		{
			EnemyFlyToGroupManager.Instance.ReturnObjectToPool(gameObject);
			//count++;
		}
		Spawn();

		ResetHp();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("PlayerBullet"))
		{
			PlayerBulletManager.Instance.ReturnObjectToPool(collision.gameObject);
			float damage = collision.gameObject.GetComponent<BulletProperties>().Damage;
			TakeDamage(damage);
		}
	}

	private void TakeDamage(float damage)
	{
		currentHp -= damage;
		float offsetHp = currentHp / totalHp;
		hpTransform.localScale = new(offsetHp, 1f, 0f);
	}

	private void ResetHp()
	{
		currentHp = totalHp;
		hpTransform.localScale = new(1f, 1f, 0f);
	}

	private GameObject GetRandomExplode()
	{
		int randIndex = Random.Range(0, explodeAnims.Length);
		return explodeAnims[randIndex].gameObject;
	}

	protected virtual void Spawn()
	{
		GameObject obj = GetRandomExplode();

		ExplodeManager.Instance.SpawnObject(obj, transform.position, Quaternion.identity);
	}
}
