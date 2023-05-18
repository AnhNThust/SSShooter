using UnityEngine;

public class BulletDespawn : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("PlayerBullet"))
		{
			PlayerBulletManager.Instance.ReturnObjectToPool(collision.gameObject);
		}
	}
}
