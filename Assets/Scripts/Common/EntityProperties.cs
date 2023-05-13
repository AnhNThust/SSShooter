using UnityEngine;

public abstract class EntityProperties : MonoBehaviour
{
  [SerializeField] private float hp = 1;
  public float Hp { get => hp; set => hp = value; }

  [SerializeField] private float damage = 1;
  public float Damage { get => damage; set => damage = value; }

  protected virtual void Reset()
  {
    ResetValue();
  }

  protected abstract void ResetValue();
}
