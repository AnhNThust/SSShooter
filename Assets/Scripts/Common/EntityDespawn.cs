using UnityEngine;

public abstract class EntityDespawn : MonoBehaviour
{
  [SerializeField] private Transform topDeathZone;
  public Transform TopDeathZone { get => topDeathZone; }

  [SerializeField] private float deathZone = 5f;

  protected virtual void Despawn()
  {
    if (transform.parent.position.y >= deathZone)
    {
      Destroy(transform.parent.gameObject);
    }
  }
}
