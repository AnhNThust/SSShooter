using System.Collections;
using UnityEngine;

public class ExplodeDespawnByTime : MonoBehaviour
{
	//private void Start()
	//{
	//	//StartCoroutine(Despawn());
	//	Invoke(nameof(Despawn), 0.5f);
	//}

	private void OnEnable()
	{
		Invoke(nameof(Despawn), 0.5f);
	}

	public void Despawn()
	{
		ExplodeManager.Instance.ReturnObjectToPool(transform.gameObject);
	}
}
