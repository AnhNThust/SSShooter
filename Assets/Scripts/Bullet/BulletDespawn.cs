using UnityEngine;

public class BulletDespawn : MonoBehaviour
{
  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.gameObject.CompareTag("Bullet"))
    {
      BulletManager.Instance.ReturnObjToPool(collision.gameObject);
    }
  }
}
